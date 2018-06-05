using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : IShootBehaviour
{
    public float nextTimeToFire = 0 , fireRate = 5;

    BulletsSpawner _bulletSpawner;
    Transform _spawn;
    IBulletBehaviour bB;

    Sprite _spr;


    public BaseShoot(BulletsSpawner bulletSpawner, Transform spawn,IBulletBehaviour bulletBehaviour,Sprite spr)
    {
        _spawn = spawn;
        _bulletSpawner = bulletSpawner;
        bB = bulletBehaviour;
        _spr = spr;
    }


    public void Enter()
    {
    }

    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            var bullet = _bulletSpawner.SpawnBullet(_spawn);
            bullet.bulletBehaviour = bB;

            bullet.bulletBehaviour.Initialize(_spr, bullet.transform);

        }
    }
    public void End()
    {
    }
}
