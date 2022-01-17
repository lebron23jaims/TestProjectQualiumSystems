using UnityEngine;

namespace GamePlay
{
    public interface ICue
    {
        void SetTarget(Transform target);
        void SetPosition();
        void SetDirection(Vector3 direction);
    }

    public class Cue : MonoBehaviour, ICue
    {
        [SerializeField] private float _deflectionDelta;
        private Transform _targetTransform;

        private void Awake()
        {
            Helper.ServiceLocator.SharedInstanse.Register<ICue>(this);
        }

        private void OnDestroy()
        {
            Helper.ServiceLocator.SharedInstanse.Unregister<ICue>();
        }

        public void SetTarget(Transform target)
        {
            _targetTransform = target;
        }

        public void SetPosition()
        {
            transform.position = _targetTransform.position;
        }

        public void SetDirection(Vector3 direction)
        {
            transform.position = _targetTransform.position + direction * _deflectionDelta;

            var rotateDiraction = _targetTransform.position - transform.position;
            rotateDiraction = rotateDiraction.normalized;
            var angle = Mathf.Atan2(rotateDiraction.y, rotateDiraction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
    }
}

