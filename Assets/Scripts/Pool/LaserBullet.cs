using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : IBulletBehaviour
{

    public void Initialize(Sprite spr, Bullet bullet)
    {
        bullet.damage = 0.1f;
        SpriteRenderer spriteRenderer = bullet.transform.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spr;
        spriteRenderer.color = Color.cyan;
        bullet.transform.localScale = new Vector3(25, 0.25f, bullet.transform.localScale.z);
    }

    public void Move(Bullet bullet, float speed)
    {
        //var bulleOffset = trans.parent.transform.position + trans.parent.transform.right * trans.localScale.x / 2;
        //trans.position = bulleOffset;
        //trans.right = trans.parent.transform.right;
    }

    public void OnAsteroidHit(Asteroids asteroid, Bullet bullet)
    {
        asteroid.Hit(bullet.damage);
    }

    public void OnHit(Transform hitTarget, Bullet bullet)
    {
        bullet.transform.localScale = new Vector3(Vector3.Distance(hitTarget.position,(bullet.transform.position - bullet.transform.right * bullet.transform.localScale.x / 2)), 
            bullet.transform.localScale.y, bullet.transform.localScale.z);
    }

    public void OnHitExit(Transform bullet)
    {
        bullet.localScale = new Vector3(25, 0.25f, bullet.localScale.z);
    }
}
