using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _score;
    [SerializeField]
    private Button _restartButton;

    public void Setup(string score)
    {
        _score.text = score;
        _restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
