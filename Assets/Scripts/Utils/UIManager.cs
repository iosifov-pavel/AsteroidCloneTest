using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public void Setup()
    {
        EventManager.OnRotationChanged += UpdatePlayerAngle;
        EventManager.OnPlayerScoreChange += UpdatePlayerScore;
        EventManager.OnLaserCountChange += UpdatePlayerLaserCount;
        EventManager.OnPlayerLaserCooldownChange += UpdatePlayerLaserCooldown;
        EventManager.OnPlayerSpeedChange += UpdatePlayerSpeed;
        EventManager.OnPlayerPositionChange += UpdatePlayerPosition;
    }

    private void UpdatePlayerAngle( object sender, Vector2 forward)
    {
        var angle = Vector2.SignedAngle(Vector2.up, forward);
        _angleText.text = $"{angle}";
    }

    private void UpdatePlayerScore( object sender, float score)
    {
        _scoreText.text = score.ToString();
    }

    private void UpdatePlayerLaserCount(object sender, int count)
    {
        var laserMax = count >= Utils.Constants.PlayerMaxLazers;
        _laserCooldownText.gameObject.SetActive(!laserMax);
        _lasersText.text = count.ToString();
    }

    private void UpdatePlayerLaserCooldown(object sender, float cooldown)
    {
        _laserCooldownText.text = cooldown.ToString();
    }

    private void UpdatePlayerSpeed( object sender, float speed)
    {
        _speedText.text = speed.ToString();
    }

    private void UpdatePlayerPosition( object sender, Vector2 position)
    {
        _coordinatesText.text = $"{position.x} : {position.y}";
    }
}
