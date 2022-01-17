using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class RestartButton : MonoBehaviour
    {
        private Button _restartButton;

        private void Awake()
        {
            _restartButton = transform.GetComponent<Button>();
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            GameEvent.GameEventsStorage.OnRestartSceneHandler();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

