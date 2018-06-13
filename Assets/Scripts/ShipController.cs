using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : IUpdate
{
    private Ship _ship;
    private bool _automatic;
    
    public ShipController(Ship ship,bool automatic)
    {
        _ship = ship;
        _automatic = automatic;
    }
    public void Update()
    {
        _ship.Move(Input.GetAxis("Vertical"));
        _ship.Rotation(Input.GetAxis("Horizontal"));



        if (!_automatic)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _ship.ShootEnter();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _ship.Shoot();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _ship.ShootEnd();
            }
        }
        else
            _ship.Shoot();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _ship.BaseFire();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _ship.AutomaticFire();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _ship.LaserFire();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _ship.BombFire();
    }

}
