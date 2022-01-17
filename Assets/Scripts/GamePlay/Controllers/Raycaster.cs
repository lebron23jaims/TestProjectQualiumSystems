using System;
using UnityEngine;

namespace GamePlay
{
    public interface IRaycaster
    {
        Action<Vector3, Vector3, ITrajectory> Action { get; }
        void TryRaycast(Vector3 origin, Vector3 direction, ITrajectory trajectory);
    }

    public class Raycaster: IRaycaster
    {
        public Action<Vector3, Vector3, ITrajectory> Action => _action;
        private Action<Vector3, Vector3, ITrajectory> _action;
        private Action<bool> _onNoTarget;

        public Raycaster(Action<Vector3, Vector3, ITrajectory> action, Action<bool> onNoTarget)
        {
            _action = action;
            _onNoTarget = onNoTarget;
        }

        public void TryRaycast(Vector3 origin, Vector3 direction, ITrajectory trajectory)
        {
            var distance = direction.magnitude;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 0.25f, -direction, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag(Helper.GameСonstants.BallTag))
                {
                    _onNoTarget?.Invoke(true);

                    var hit = hits[i];
                    var point = new Vector3(hit.point.x, hit.point.y, 0);
                    var newDirection = hit.collider.transform.position - point;

                    var circleCastPoint = ((-newDirection) * 2) + hit.collider.transform.position;
                    var secondaryPoint = hit.collider.transform.position;

                    _action?.Invoke(circleCastPoint, secondaryPoint, trajectory);
                    return;
                }
                else
                {
                    _onNoTarget?.Invoke(false);
                }
            }
        }
    }
}

