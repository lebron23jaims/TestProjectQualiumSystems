using UnityEngine;

public class AplicationStart : MonoBehaviour
{
    private void Start()
    {
        Prepare();
    }

    private void OnApplicationQuit()
    {
        OnUnsubscribeAll();
    }

    private void OnUnsubscribeAll()
    {
        Helper.SudscriberTool.UnsubscribeAll();
        Helper.SudscriberTool.Reset();
        Helper.TransformStorage.Reset();
        GameEvent.GameEventsStorage.OnRestartScene -= OnUnsubscribeAll;
    }

    private void Prepare()
    {
        CreateUIElements();
        CreateGamePlayElements();
        GameEvent.GameEventsStorage.OnRestartScene += OnUnsubscribeAll;
    }

    private void CreateGamePlayElements()
    {
        CreateHitController();
    }

    private void CreateHitController()
    {
        var hitController = Factory.GameControllersFactory.CreateHitController();
        Helper.SudscriberTool.AddSubscriber(hitController);
    }

    private void CreateUIElements()
    {
        CreateJoystick();
    }

    private void CreateJoystick()
    {
        Factory.UIElementsFactory.CreateJoystick();
    }
}

