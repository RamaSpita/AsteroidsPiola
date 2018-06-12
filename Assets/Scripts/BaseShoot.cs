using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : IShootBehaviour
{

    private BulletsSpawner _bulletSpawner;
    private Transform _spawn;
    private Sprite _spr;
    private IBulletBehaviour _bB;

    public float nextTimeToFire = 0 , fireRate = 5;

    public BaseShoot(BulletsSpawner bulletSpawner, Transform spawn,IBulletBehaviour bulletBehaviour,Sprite spr)
    {
        _spawn = spawn;
        _bulletSpawner = bulletSpawner;
        _bB = bulletBehaviour;
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
            bullet.bulletBehaviour = _bB;

            bullet.bulletBehaviour.Initialize(_spr, bullet.transform);

        }
    }
    public void End()
    {
    }
}
