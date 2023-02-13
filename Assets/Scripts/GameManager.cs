using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _respawnDelay = 2.0f;
    [SerializeField] private float _immortalityTime = 5.0f;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _thirdLivesImage;
    [SerializeField] private GameObject _secondLivesImage;
    [SerializeField] private GameObject _continueWithAdButtonObject;
    [SerializeField] private AudioSource _explosionSound;
    [SerializeField] private AudioSource _explosionAsteroidSound;
    public static int _countDestroyedAsteroids = 0;
    public static int _lives = 3;
    private Vector2 _startPosition = new Vector2(0, 24);
    public static float _addWaitTime = 30;

    private void Start()
    {
        _lives = 3;
        GameOverMenu.continueIsTapped = false;
        _addWaitTime = 30;
    }

    private void Update()
    {
        _addWaitTime -= Time.deltaTime;
        if (_addWaitTime <= 0)
        {
            GameOverMenu.continueIsTapped = false;
        }
    }
    public void KillPlayer()
    {
        if (_lives == 3)
        {
            _explosionSound.Play();
            _lives--;
            _explosion.transform.position = _player.transform.position;
            _explosion.Play();
            Player.isAlive = false;
            _thirdLivesImage.SetActive(false);
            Invoke(nameof(Respawn), _respawnDelay);
        }
        else if (_lives == 2)
        {
            _explosionSound.Play();
            _lives--;
            _explosion.transform.position = _player.transform.position;
            _explosion.Play();
            Player.isAlive = false;
            _secondLivesImage.SetActive(false);
            Invoke(nameof(Respawn), _respawnDelay);
        }
        else if (_lives <= 1)
        {
            _explosionSound.Play();
            _lives--;
            GameOver();
        }

    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        _explosionAsteroidSound.Play();
        _explosion.transform.position = asteroid.transform.position;
        _explosion.Play();
        _countDestroyedAsteroids++;
    }

    public void Respawn()
    {
        _player.transform.position = _startPosition;
        _player.gameObject.layer = LayerMask.NameToLayer("IngoreLayer");
        _player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), _immortalityTime);
        Player.isAlive = true ;
    }

    private void TurnOnCollision()
    {
        _player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        Player.isAlive = false;
        Time.timeScale = 0;
        _gameOverMenu.SetActive(true);
        if (!GameOverMenu.continueIsTapped)
        {
            _continueWithAdButtonObject.SetActive(true);
        }
        if(GameOverMenu.continueIsTapped)
        {
            _continueWithAdButtonObject.SetActive(false);
        }
    }
}
