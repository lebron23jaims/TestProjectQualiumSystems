using System.Collections.Generic;

namespace GameUI
{
    public enum UIElementType
    {
        Joystick,
    }

    public static class UIStorage
    {
        private static Dictionary<UIElementType, string> _uiElements = new Dictionary<UIElementType, string>()
        {
            { UIElementType.Joystick, "UI/Joystick" },
        };

        public static string GetElement(UIElementType type)
        {
            return _uiElements[type];
        }
    }
}
