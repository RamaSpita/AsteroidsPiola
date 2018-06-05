using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSho : IShootBehaviour
{
    public float nextTimeToFire = 0 , fireRate = 5;

    BulletsSpawner _bulletSpawner;
    Transform _spawn;
    IBulletBehaviour bB;

    Sprite _spr;
    Bullet bullet;

    public bool canShoot;


    public LaserSho(BulletsSpawner bulletSpawner, Transform spawn,IBulletBehaviour bulletBehaviour,Sprite spr)
    {
        _spawn = spawn;
        _bulletSpawner = bulletSpawner;
        bB = bulletBehaviour;
        _spr = spr;
        canShoot = true;
    }


    public void Enter()
    {
        if (canShoot)
        {

            bullet = _bulletSpawner.SpawnBullet(_spawn);
            bullet.bulletBehaviour = bB;

            bullet.bulletBehaviour.Initialize(_spr, bullet.transform);
            canShoot = false;
        }
    }

    public void Shoot()
    {
        if (!canShoot)
        {
            var bulleOffset = _bulletSpawner.transform.position + _bulletSpawner.transform.right * bullet.transform.localScale.x / 2;
            bullet.transform.position = bulleOffset;
            bullet.transform.right = _bulletSpawner.transform.right;
        }
    }
    public void End()
    {
        if (!canShoot)
        {
            bullet.ReturnToPool();
            canShoot = true;
           
        }
    }
}
