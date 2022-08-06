using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _speedText;
    [SerializeField]
    private TMP_Text _coordinatesText;
    [SerializeField]
    private TMP_Text _angleText;
    [SerializeField]
    private TMP_Text _lasersText;
    [SerializeField]
    private TMP_Text _laserCooldownText;
    [SerializeField]
    private GameOverWindow _gameOverWindow;
    [SerializeField]
    private StartGameWindow _startGameWindow;
    [SerializeField]
    private GameObject _gameUI;

    private EventManager _eventManager;

    public void Setup(EventManager eventManager)
    {
        _eventManager = eventManager;
        _eventManager.OnRotationChanged += UpdatePlayerAngle;
        _eventManager.OnPlayerScoreChange += UpdatePlayerScore;
        _eventManager.OnLaserCountChange += UpdatePlayerLaserCount;
        _eventManager.OnPlayerLaserCooldownChange += UpdatePlayerLaserCooldown;
        _eventManager.OnPlayerSpeedChange += UpdatePlayerSpeed;
        _eventManager.OnPlayerPositionChange += UpdatePlayerPosition;
        _eventManager.OnGameStart += OnGameStart;
        _eventManager.OnPlayerDeath += GameOver;
        _gameOverWindow.gameObject.SetActive(false);
        _gameUI.SetActive(false);
        _startGameWindow.Setup(_eventManager);
    }

    private void OnGameStart(object sender, EventArgs e)
    {
        _gameUI.SetActive(true);
        _startGameWindow.gameObject.SetActive(false);
    }

    private void GameOver(object sender, EventArgs e)
    {
        _gameOverWindow.Setup(_scoreText.text);
        _gameUI.SetActive(false);
        _gameOverWindow.gameObject.SetActive(true);
        _eventManager.OnRotationChanged -= UpdatePlayerAngle;
        _eventManager.OnPlayerScoreChange -= UpdatePlayerScore;
        _eventManager.OnLaserCountChange -= UpdatePlayerLaserCount;
        _eventManager.OnPlayerLaserCooldownChange -= UpdatePlayerLaserCooldown;
        _eventManager.OnPlayerSpeedChange -= UpdatePlayerSpeed;
        _eventManager.OnPlayerPositionChange -= UpdatePlayerPosition;
        _eventManager.OnPlayerDeath -= GameOver;
        _eventManager.OnGameStart -= OnGameStart;
    }

    private void UpdatePlayerAngle(object sender, Vector2 forward)
    {
        var angle = Vector2.SignedAngle(Vector2.up, forward);
        _angleText.text = string.Format("{0:f1}°", angle);
    }

    private void UpdatePlayerScore(object sender, float score)
    {
        _scoreText.text = score.ToString();
    }

    private void UpdatePlayerLaserCount(object sender, int count)
    {
        var laserMax = count >= PlayerModel.LaserCapacity;
        _laserCooldownText.gameObject.SetActive(!laserMax);
        _lasersText.text = count.ToString();
    }

    private void UpdatePlayerLaserCooldown(object sender, float cooldown)
    {
        _laserCooldownText.text = string.Format("{0:f1} s", cooldown);
    }

    private void UpdatePlayerSpeed(object sender, float speed)
    {
        _speedText.text = string.Format("{0:f1} m/s", speed);
    }

    private void UpdatePlayerPosition(object sender, Vector2 position)
    {
        _coordinatesText.text = string.Format("X:{0:f1} Y:{1:f1}", position.x, position.y);
    }
}
