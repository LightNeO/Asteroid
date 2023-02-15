using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButtonFirst;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _yesExit;
    [SerializeField] private Button _noExit;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _onOffSound;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _exitConfirmation;
    [SerializeField] private AudioSource _gameMusic;
    [SerializeField] private Sprite _onMusic;
    [SerializeField] private Sprite _offMusic;
    [SerializeField] private AudioClip pauseSound;
    private bool isOpened = false;
    void Start()
    {
        _continueButton.onClick.AddListener(ContinueGame);
        _mainMenuButton.onClick.AddListener(OpenMainMenu);
        _exitButtonFirst.onClick.AddListener(ShowExitConfirmation);
        _pauseButton.onClick.AddListener(PauseGame);
        _yesExit.onClick.AddListener(ExitGame);
        _noExit.onClick.AddListener(CloseExitConfirmation);
        _onOffSound.onClick.AddListener(OnOrOffSound);

        ChangeSoundSprite();
    }

    private void PauseGame()
    {
        if (!isOpened)
        {
            Time.timeScale = 0;
            isOpened = true;
            _pauseMenu.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(pauseSound);
            _shootButton.enabled= false;
        }
        else if (isOpened)
        {
            Time.timeScale = 1;
            isOpened = false;
            _pauseMenu.SetActive(false);
            _shootButton.enabled = true;
        }
        
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _shootButton.enabled = true;
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        _shootButton.enabled = true;
    }

    private void ShowExitConfirmation()
    {
        _exitConfirmation.SetActive(true);
        Time.timeScale = 0;
        _pauseMenu.SetActive(false);
        _shootButton.enabled = true;
    }
    private void CloseExitConfirmation()
    {
        _exitConfirmation.SetActive(false);
        Time.timeScale = 1;
        _shootButton.enabled = true;
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
            _gameMusic.Stop();
            PlayerPrefs.SetInt("saveSound", 0);
            ChangeSoundSprite();
        }
        else if (PlayerPrefs.GetInt("saveSound") == 0)
        {
            
            AudioListener.volume = 1;
            AudioListener.pause = false;
            _gameMusic.Play();
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
