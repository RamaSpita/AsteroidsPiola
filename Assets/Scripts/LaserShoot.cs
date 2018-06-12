using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : IShootBehaviour
{

    private IBulletBehaviour _bB;
    private BulletsSpawner _bulletSpawner;
    private Transform _spawn;
    private Sprite _spr;
    private Bullet _bullet;

    public float nextTimeToFire = 0 , fireRate = 5;
    public bool canShoot;


    public LaserShoot(BulletsSpawner bulletSpawner, Transform spawn,IBulletBehaviour bulletBehaviour,Sprite spr)
    {
        _spawn = spawn;
        _bulletSpawner = bulletSpawner;
        _bB = bulletBehaviour;
        _spr = spr;
        canShoot = true;
    }


    public void Enter()
    {
        if (canShoot)
        {

            _bullet = _bulletSpawner.SpawnBullet(_spawn);
            _bullet.bulletBehaviour = _bB;

            _bullet.bulletBehaviour.Initialize(_spr, _bullet.transform);

            canShoot = false;
        }
    }

    public void Shoot()
    {
        if (!canShoot)
        {
            //Esto no va aca, va en bullet
            var bulleOffset = _bulletSpawner.transform.position + _bulletSpawner.transform.right * _bullet.transform.localScale.x / 2;
            _bullet.transform.position = bulleOffset;
            _bullet.transform.right = _bulletSpawner.transform.right;
        }
    }
    public void End()
    {
        if (!canShoot)
        {
            _bullet.ReturnToPool();
            canShoot = true;
           
        }
    }
}
