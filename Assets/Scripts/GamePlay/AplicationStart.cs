using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameEvent.GameEventsStorage.OnRestartScene -= RestartScene;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Prepare()
    {
        CreateUIElements();
        CreateGamePlayElements();
        GameEvent.GameEventsStorage.OnRestartScene += OnUnsubscribeAll;
        GameEvent.GameEventsStorage.OnRestartScene += RestartScene;
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

