using System;
using UnityEngine;
using UnityEngine.UI;

public class StartGameWindow : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    private EventManager _eventManager;

    public void Setup(EventManager eventManager)
    {
        gameObject.SetActive(true);
        _eventManager = eventManager;
        _startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        gameObject.SetActive(false);
        _eventManager.OnGameStart?.Invoke(this, EventArgs.Empty);
    }
}
