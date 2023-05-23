using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private LevelController _levelController;
    public int health;
    public int countDamage;
    public int score;

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            _levelController.KillOneAlien(this);
        }
    }

    public void Init(LevelController lvlController)
    {
        _levelController = lvlController;
        _levelController.AddToList(this);
    }
}
