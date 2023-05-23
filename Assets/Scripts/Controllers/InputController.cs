using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool shoot;

    public Action<bool> actionShoot;
    private void Awake()
    {
        horizontal = 0f;
        vertical = 0f;
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetMouseButtonDown(0) && !shoot)
        {
            shoot = true;
            actionShoot?.Invoke(shoot);
        }

        if (Input.GetMouseButtonUp(0) && shoot)
        {
            shoot = false;
            actionShoot?.Invoke(shoot);
        }
    }
}
