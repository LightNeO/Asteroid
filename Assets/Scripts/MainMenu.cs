 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text topScoreText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _yesExit;
    [SerializeField] private Button _noExit;
    [SerializeField] private Button _onOffSound;
    [SerializeField] private GameObject _exitConfirmation;
    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private Sprite _onMusic;
    [SerializeField] private Sprite _offMusic;

    void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(ShowExitConfirmation);
        topScoreText.text = PlayerPrefs.GetInt("TopScore", 0).ToString();
        _yesExit.onClick.AddListener(ExitGame);
        _noExit.onClick.AddListener(CloseExitConfirmation);
        _onOffSound.onClick.AddListener(OnOrOffSound);

        if (PlayerPrefs.GetInt("saveSound") == 1)
        {
            AudioListener.volume = 1;
            _menuMusic.Play();
        }
        else if (PlayerPrefs.GetInt("saveSound") == 0)
        {
            AudioListener.volume = 0;
            _menuMusic.Stop();
        }

        ChangeSoundSprite();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    private void ShowExitConfirmation()
    {
        _exitConfirmation.SetActive(true);
        Time.timeScale = 0;
    }
    private void CloseExitConfirmation()
    {
        _exitConfirmation.SetActive(false);
        Time.timeScale = 1;
    }
    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }

    private void OnOrOffSound()
    {
        if (PlayerPrefs.GetInt("saveSound") == 1) 
        {
            AudioListener.volume = 0;
            _menuMusic.Stop();
            PlayerPrefs.SetInt("saveSound", 0);
            ChangeSoundSprite();
        }
        else if (PlayerPrefs.GetInt("saveSound") == 0)
        {
            AudioListener.volume = 1;
            AudioListener.pause = false;
            _menuMusic.Play();
            PlayerPrefs.SetInt("saveSound", 1);
            ChangeSoundSprite();
        }

    }

    private void ChangeSoundSprite()
    {
        if (PlayerPrefs.GetInt("saveSound") == 1)
        {
            _onOffSound.GetComponent<Image>().sprite = _onMusic;
        }
        else if (PlayerPrefs.GetInt("saveSound") == 0)
        {
            _onOffSound.GetComponent<Image>().sprite = _offMusic;
        }
    }
}
