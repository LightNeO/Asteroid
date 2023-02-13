using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private float _speed = 15.0f;
    
    private bool _isActive = false;
    public float size = 1.0f;
    public float minSize = 1f;
    public float maxSize = 2.5f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        if (!_isActive)
        {
            _spriteRenderer.sprite = _emptySprite;
        }
        else
        {
            _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
        }
        

        transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        transform.localScale = Vector3.one * size;

        _rigidbody.mass = size;
    }

    public void SetTrijectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this._speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border" && !_isActive)
        {
            _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
            _isActive = true;
        }
        else if (collision.gameObject.tag == "DestroyBorder" && _isActive)
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            if (this.size / 2 >= minSize)
            {
                SplitAsteroid();
                SplitAsteroid();
                
            }
            
        }
    }



    private void SplitAsteroid()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 1.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = this.size / 2;
        half.SetTrijectory(Random.insideUnitCircle.normalized * _speed * 3);
        half._boxCollider.enabled = true;
        half.enabled = true;
        half._isActive = true;
    }
}
