using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticShoot : IShootBehaviour
{
    public float nextTimeToFire = 0, fireRate = 5;

    BulletsSpawner _bulletSpawner;
    Transform _spawnRight;
    Transform _spawnLeft;
    IBulletBehaviour bB;
    Sprite _spr;

    public AutomaticShoot(BulletsSpawner bulletSpawner, Transform spawnRight, Transform spawnLeft, IBulletBehaviour bulletBehaviour, Sprite spr)
    {
        _spawnRight = spawnRight;
        _spawnLeft = spawnLeft;
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
            var bullet = _bulletSpawner.SpawnBullet(_spawnRight);
            bullet.bulletBehaviour = bB;
            bullet.bulletBehaviour.Initialize(_spr, bullet.transform);

            bullet = _bulletSpawner.SpawnBullet(_spawnLeft);
            bullet.bulletBehaviour = bB;
            bullet.bulletBehaviour.Initialize(_spr, bullet.transform);
        }
    }
    public void End()
    {
    }

}
