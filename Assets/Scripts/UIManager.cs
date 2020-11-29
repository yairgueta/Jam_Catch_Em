using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Image pokeballLogo;
    public static UIManager Instance;
    public GameObject pauseScreen;

    void Start()
    {
        Instance = this;
        scoreText.text = "" + 0;
    }

    public void SetScore(int i)
    {
        scoreText.text = "" + i;
        LeanTween.scale(scoreText.gameObject, 1.5f * Vector2.one, .7f).setEaseOutCirc().setLoopPingPong(1);
        LeanTween.scale(pokeballLogo.gameObject, 1.15f * Vector2.one, .7f).setEaseOutCirc().setLoopPingPong(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }
}
