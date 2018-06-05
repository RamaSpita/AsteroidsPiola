using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveController : IUpdate
{
    Nave _nave;
    bool _automatic;
    
    public NaveController(Nave nave,bool automatic)
    {
        _nave = nave;
        _automatic = automatic;
    }
    public void Update()
    {
        _nave.Move(Input.GetAxis("Vertical"));
        _nave.Rotation(Input.GetAxis("Horizontal"));



        if (!_automatic)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _nave.ShootEnter();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _nave.Shoot();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _nave.ShootEnd();
            }
        }
        else
            _nave.Shoot();

        if (Input.GetKeyDown(KeyCode.Z))
            _nave.AutomaticFire();
        if (Input.GetKeyDown(KeyCode.X))
            _nave.BaseFire();
        if (Input.GetKeyDown(KeyCode.C))
            _nave.LaserFire();
    }

}
