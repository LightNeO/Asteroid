using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Button _shootButtonPortrait;
    [SerializeField] private Button _shootButtonLandscape;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _rotationSpeed = 700.0f;
    [SerializeField] private GameObject _shootPosition;
    [SerializeField] private AudioSource _shotSound;

    private float _topBound = 56.0f;
    private float _bottomBound = -6.5f;
    private float _sideBound = 36.0f;
    private float _horizontalInput = 0;
    private float _verticalInput = 0;
    public static bool isAlive = true;
    

    void Awake()
    {
        isAlive = true;
        _playerRb = GetComponent<Rigidbody2D>();
        _shootButtonPortrait.onClick.AddListener(Shoot);
        _shootButtonLandscape.onClick.AddListener(Shoot);
    }

    void Update()
    {      
        switchSides();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Move
        
        _verticalInput = _joystick.Vertical;
        _horizontalInput = _joystick.Horizontal;
        Vector2 moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        _playerRb.AddForce(moveInput * _speed, ForceMode2D.Force);

        //Rorate
        if (moveInput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, moveInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }


    private void switchSides()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (x > _sideBound)
        {
            x = -_sideBound;
        }
        if (x < -_sideBound)
        {
            x = _sideBound;
        }
        if (y > _topBound)
        {
            y = _bottomBound;
        }
        if (y < _bottomBound)
        {
            y = _topBound;
        }
        transform.position = new Vector2(x, y);
    }

    private void Shoot()
    {
        if (isAlive)
        {
            _shotSound.Play();
            Bullet _bullet = Instantiate(_bulletPrefab, _shootPosition.transform.position, transform.rotation);
            _bullet.LaunchBullet(transform.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            _playerRb.velocity = Vector2.zero;
            _playerRb.angularVelocity = 0.0f;

            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().KillPlayer();
        }
    }
}
