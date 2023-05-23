using DG.Tweening;
using UnityEngine;
using Zenject;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _maxY;
    private ShootController _shootController;
    private int _damage;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Init(int damage, Color color, int layer, ShootController shoot)
    {
        _damage = damage;
        _sprite.color = color;
        _sprite.enabled = false;
        _rb2d.simulated = false;
        gameObject.layer = layer;
        _shootController = shoot;
    }

    public void StartMovementForward(Vector3 playerPosition, int index)
    {
        _rb2d.simulated = true;
        _rb2d.position = playerPosition;
        _rb2d.DOMoveY(_rb2d.position.y + _maxY*index, _speedMovement)
            .OnComplete(StopMovement);
        _sprite.enabled = true;
    }

    private void StopMovement()
    {
        _sprite.enabled = false;
        _rb2d.simulated = false;
        _shootController.ReturnToPoolPlayer(this, gameObject.layer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IEnemy enemy))
        {
            enemy.Damage(_damage);
            _rb2d.DOKill();
            StopMovement();
        }
    }
}
