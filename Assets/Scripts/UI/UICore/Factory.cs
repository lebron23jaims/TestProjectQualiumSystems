using GamePlay;
using GameUI;
using UnityEngine;

public static class Factory
{
    public static class GameControllersFactory
    {
        public static HitController CreateHitController()
        {
            return new HitController();
        }
    }


    public static class UIElementsFactory
    {
        public static IJoystick CreateJoystick()
        {
            var uiCreator = Helper.ServiceLocator.SharedInstanse.Resolve<IUICreator>();
            var uiElement = uiCreator.GetUIElement(UIElementType.Joystick);
            var newElement = GetObjectComponent<IJoystick>(uiElement);
            return newElement;
        }
    }

    private static T GetObjectComponent<T>(GameObject obj)

    {
        return obj.GetComponent<T>();
    }
}

