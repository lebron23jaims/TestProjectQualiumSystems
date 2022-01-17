using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    public interface IUICreator
    {
        GameObject GetUIElement(UIElementType elementType);
    }

    public class UICreator : MonoBehaviour, IUICreator
    {
        [SerializeField] private Transform _uiElementsParrent;
        private Dictionary<UIElementType, GameObject> _uiElements = new Dictionary<UIElementType, GameObject>();

        private void Awake()
        {
            Helper.ServiceLocator.SharedInstanse.Register<IUICreator>(this);
        }
        private void OnDestroy()
        {
            Helper.ServiceLocator.SharedInstanse.Unregister<IUICreator>();
        }

        public GameObject GetUIElement(UIElementType elementType)
        {
            if (_uiElements.ContainsKey(elementType))
            {
                return _uiElements[elementType];
            }

            var prefab = Resources.Load<GameObject>(UIStorage.GetElement(elementType));
            var instance = Instantiate(prefab, _uiElementsParrent);
            _uiElements[elementType] = instance;

            return instance;
        }
    }
}



