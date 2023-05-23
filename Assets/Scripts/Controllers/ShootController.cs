using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Shoot shootObject;
    
    [Inject] private InputController _input;
    [Inject] private PlayerMovementController _player;

    private List<Shoot> _poolPlayer;
    private List<Shoot> _enemyPool;

    private void Awake()
    {
        _input.actionShoot += PlayerShoot;
        
        GameObject playerShootContainer = new GameObject("PlayerShoots");
        GameObject enemiesShootContainer = new GameObject("EnemyShoot");
        
        int poolSize = 10;

        _poolPlayer = new List<Shoot>();
        
        for (int i = 0; i < poolSize; i++)
        {
            Shoot shoot = Instantiate(shootObject, Vector3.zero, Quaternion.identity, playerShootContainer.transform);
            shoot.Init(1, Color.green, 8, this);
            _poolPlayer.Add(shoot);
        }
        
        _enemyPool = new List<Shoot>();
        poolSize = 3;
        for (int i = 0; i < poolSize; ++i)
        {
            Shoot shoot = Instantiate(shootObject, Vector3.zero, Quaternion.identity, enemiesShootContainer.transform);
            shoot.Init(1, Color.red, 9, this);
            _enemyPool.Add(shoot);
        }
    }


    private void PlayerShoot(bool shoot)
    {
        if (shoot)
        {
            StartCoroutine(ShootWaiter());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator ShootWaiter()
    {
        Shoot shoot = _poolPlayer[0];
        _poolPlayer.RemoveAt(0);
        shoot.StartMovementForward(_player.transform.position, 1);
        yield return new WaitForSeconds(0.5f);
    }

    public void ReturnToPoolPlayer(Shoot obj, int layer)
    {
        if (layer.Equals(8))
        {
            _poolPlayer.Add(obj);
        }
        else
        {
            _enemyPool.Add(obj);
        }
    }

    public void EnemyShoot(Vector3 enemyPos)
    {
        Shoot shoot = _enemyPool[0];
        _enemyPool.RemoveAt(0);
        shoot.StartMovementForward(enemyPos, -1);
    }
}
