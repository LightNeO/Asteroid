using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 500.0f;
    private Rigidbody2D _bulletRb;
    
    void Awake()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
    }

    public void LaunchBullet(Vector2 direction)
    {
        _bulletRb.AddForce(direction * _speed);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            Destroy(collision.gameObject);
            Score.scoreValue++;
            if (Score.scoreValue > 50 && Score.scoreValue % 50 == 1)
            {
                AsteroidSpawner.spawnAmount++;
            }
        }
        Destroy(this.gameObject);
    }

}
