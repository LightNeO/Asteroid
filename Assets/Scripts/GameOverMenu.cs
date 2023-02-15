using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _continueWithAdButton;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _continueWithAdButtonObject;
    public static bool continueIsTapped = false;
    
    //[SerializeField] private Player _player;
    public AdOnContinue adOnContinue;

    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _mainMenuButton.onClick.AddListener(OpenMainMenu);
        _continueWithAdButton.onClick.AddListener(ContinueWithAd);

    }



    private void RestartGame()
    {
        Score.scoreValue = 0;
        Time.timeScale = 1;
        _gameOverMenu.SetActive(false);
        SceneManager.LoadScene(1);
        _pauseButton.enabled = true;

    }

    private void OpenMainMenu()
    {
        _pauseMenu.SetActive(false);
        Score.scoreValue = 0;
        Time.timeScale = 1;
        _gameOverMenu.SetActive(false);
        SceneManager.LoadScene(0);
        _pauseButton.enabled = true;
    }

    private void ContinueWithAd()
    {
        _pauseButton.enabled = true;
        _continueWithAdButtonObject.SetActive(false);
        continueIsTapped = true;
        //method for Ad displaying
        adOnContinue.ShowAd();
    }
}
