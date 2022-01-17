using UnityEngine;

namespace GamePlay
{
    public interface IHitController
    {
        void SetHitOptions(Vector3 direction, float deflection);
        void Hit();
    }

    public class HitController : IHitController, Helper.ISubscriber
    {
        private ICue _cue;
        private IMainBall _ball;
        private ITrajectorysController _trajectoryController;
        private Transform _ballTransform;
        private HitsData _hitsData = new HitsData();

        public HitController()
        {
            PrepareHitObjects();
        }

        public void Hit()
        {
            _cue.SetPosition();
            _ball.HitInTheDirection(_hitsData.HitSpeed);
            _trajectoryController.ActiveTrajectory(false);
        }

        public void SetHitOptions(Vector3 direction, float deflection)
        {
            _hitsData.SetData(direction, deflection);
            _cue.SetDirection(_hitsData.HitDirection);

            _trajectoryController.ShowTrajectory(_ballTransform.position, _hitsData.HitSpeed);
        }

        public void Subscribe()
        {
            Helper.ServiceLocator.SharedInstanse.Register<IHitController>(this);
        }

        public void UnSubscribe()
        {
            Helper.ServiceLocator.SharedInstanse.Unregister<IHitController>();
        }
    
        private void PrepareHitObjects()
        {
            _cue = Helper.ServiceLocator.SharedInstanse.Resolve<ICue>();
            _ball = Helper.ServiceLocator.SharedInstanse.Resolve<IMainBall>();
            _trajectoryController = Helper.ServiceLocator.SharedInstanse.Resolve<ITrajectorysController>();
            _ballTransform = Helper.TransformStorage.GetTarnsform(Helper.GameСonstants.Ball);

            _cue.SetTarget(_ballTransform);
            _cue.SetPosition();
        }
    }

    public class HitsData
    {
        private const float HitPowerСoefficient = 0.75f;

        public float HitDeflection => _hitDeflection;
        public Vector3 HitDirection => _hitDirection;
        public Vector3 HitSpeed => _hitSpeed;

        private float _hitDeflection;
        private float _hitPower;
        private Vector3 _hitDirection;
        private Vector3 _hitSpeed;

        public void SetData(Vector3 direction, float deflection)
        {
            _hitDirection = direction;
            _hitDeflection = deflection / 10;
            _hitPower = _hitDeflection * HitPowerСoefficient;

            _hitSpeed = _hitDirection * _hitPower;
        }
    }
}

