using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticBullet : IBulletBehaviour
{

    public void Initialize(Sprite spr, Bullet bullet)
    {
        bullet.damage = 1;
        SpriteRenderer spriteRenderer = bullet.transform.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spr;
        spriteRenderer.color = Color.yellow;
        bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }

    public void Move(Bullet bullet, float speed)
    {
        bullet.transform.position += bullet.transform.right * speed * Time.deltaTime;

        if (bullet.transform.position.x > ScreenLimits.Instance.RightLimit || bullet.transform.position.x < ScreenLimits.Instance.LeftLimit ||
           bullet.transform.position.y < ScreenLimits.Instance.DownLimit || bullet.transform.position.y > ScreenLimits.Instance.UpLimit)
        {
            bullet.ReturnToPool();
        }
    }

    public void OnAsteroidHit(Asteroids asteroid, Bullet bullet)
    {
        asteroid.Hit(bullet.damage);
        bullet.ReturnToPool();

    }

    public void OnHit(Transform hitTarget, Bullet bullet)
    {
    }

    public void OnHitExit(Transform bullet)
    {
    }
}
