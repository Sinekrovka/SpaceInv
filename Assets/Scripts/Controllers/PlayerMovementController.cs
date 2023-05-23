using UnityEngine;
using Zenject;

public class PlayerMovementController : MonoBehaviour, IEnemy
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _minCoordinates;
    [SerializeField] private Vector2 _maxCoordinates;

    [Inject] private InputController _input;
    [Inject] private UIController _uiController;

    private void FixedUpdate()
    {
        Vector3 newPos = Vector3.zero;
        float x = transform.position.x;
        float y = transform.position.y;
        
        if ((x > _minCoordinates.x && _input.horizontal < 0) || (x<_maxCoordinates.x && _input.horizontal>0))
        {
            newPos.x = _input.horizontal;
        }
        else
        {
            newPos.x = 0f;
        }
        
        if((y>_minCoordinates.y && _input.vertical < 0)||(y<_maxCoordinates.y && _input.vertical>0))
        {
            newPos.y = _input.vertical;
        }
        else
        {
            newPos.y = 0;
        }
        
        transform.position = transform.position + newPos * _speed * Time.deltaTime;
    }

    public void Damage(int damage)
    {
        _uiController.DamageLife();
    }
}
