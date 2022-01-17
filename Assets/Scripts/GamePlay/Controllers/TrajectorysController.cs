using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public interface ITrajectorysController
    {
        void ShowTrajectory(Vector3 originPoint, Vector3 direction);
        void ActiveTrajectory(bool isOn);
    }

    public class TrajectorysController : MonoBehaviour, ITrajectorysController
    {
        [SerializeField] private GameObject _mainTrajectory;
        [SerializeField] private GameObject _mainResultTrajectory;
        [SerializeField] private GameObject _secondaryTrajectory;

        private GameObject _circle;

        private ITrajectory _main;
        private ITrajectory _mainResult;
        private ITrajectory _secondary;
        private List<ITrajectory> _allTrajectories = new List<ITrajectory>();

        private Vector3 _mainTrueDirection;
        private Vector3 _mainTrueOrigin;

        private IRaycaster _raycaster;

        private void Awake()
        {
            Helper.ServiceLocator.SharedInstanse.Register<ITrajectorysController>(this);
            _raycaster = new Raycaster(SetOptionsToDrawTrajectory, OnNoRayCastTarget);
        }

        private void Start()
        {
            PrepareComponents();
        }

        private void OnDestroy()
        {
            Helper.ServiceLocator.SharedInstanse.Unregister<ITrajectorysController>();
        }

        public void ShowTrajectory(Vector3 originPoint, Vector3 direction)
        {
            ActiveTrajectory(true);
            _mainTrueDirection = direction;
            _mainTrueOrigin = originPoint;
            _main.ShowTrajectory(originPoint, direction);
            SecondaryTrajectory(originPoint, direction, _secondary);
        }

        private void ShowMainResultTrajectory(Vector3 origin, Vector3 direction)
        {
            var newResultDirection = direction +_mainTrueDirection;
            newResultDirection = newResultDirection.normalized;

            _mainResult.ShowTrajectory(origin, newResultDirection);
        }

        private void SecondaryTrajectory(Vector3 originPoint, Vector3 direction, ITrajectory trajectory)
        {
            _raycaster.TryRaycast(originPoint, direction, trajectory);
        }

        private void PrepareComponents()
        {
            _main = _mainTrajectory.GetComponent<ITrajectory>();
            _mainResult = _mainResultTrajectory.GetComponent<ITrajectory>();
            _secondary = _secondaryTrajectory.GetComponent<ITrajectory>();

            _allTrajectories.Add(_main);
            _allTrajectories.Add(_mainResult);
            _allTrajectories.Add(_secondary);

            var circlePrefab = Resources.Load<GameObject>(Helper.GameСonstants.CirclePrefab);
            _circle = Instantiate(circlePrefab);

            ActiveTrajectory(false);

        }
        private void SetOptionsToDrawTrajectory(Vector3 origin, Vector3 secondary, ITrajectory trajectory)
        {
            var secondaryDirection = origin - secondary;
            _secondary.ShowTrajectory(origin, secondaryDirection);

            var mainResultDirection = CalculateMainResultVector(secondaryDirection, origin, secondary);
            ShowMainResultTrajectory(origin, mainResultDirection);

            Vector3 newMainDirection = origin - _mainTrueOrigin;
            float newDirectionLenght = newMainDirection.magnitude;
            float Speedlenght = _mainTrueDirection.magnitude;

            if (Speedlenght > newDirectionLenght)
            {
                _main.ShowTrajectory(_mainTrueOrigin, -newMainDirection);
            }

            _circle.transform.position = origin;
        }

        private Vector2 CalculateMainResultVector(Vector3 secondaryDirection, Vector3 origin, Vector3 secondary)
        {
            Vector2 newDirection = Vector2.zero;
            newDirection.y = 1;
            newDirection.x = -secondaryDirection.y / secondaryDirection.x;
            newDirection = newDirection.normalized;

            if (origin.y < secondary.y)
            {
                newDirection = -newDirection;
            }
            return newDirection;
        }

        private void OnNoRayCastTarget(bool isOn)
        {
            _circle.SetActive(isOn);
            _mainResult.Activate(isOn);
            _secondary.Activate(isOn);
        }

        public void ActiveTrajectory(bool isOn)
        {
            foreach (var trajectory in _allTrajectories)
            {
                trajectory.Activate(isOn);
            }
            _circle.SetActive(isOn);
        }
    }
}

