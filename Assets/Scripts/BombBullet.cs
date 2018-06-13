using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : IBulletBehaviour
{
    public void Initialize(Sprite spr, Bullet bullet)
    {
        bullet.damage = 1;
        SpriteRenderer spriteRenderer = bullet.transform.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spr;
        spriteRenderer.color = Color.red;
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
        GameObject[] cols = GameObject.FindGameObjectsWithTag("Asteroid");
        List<Asteroids> asteroidsAtRange = new List<Asteroids>();

        for (int i = 0; i < cols.Length; i++)
        {
            if (Vector3.Distance(cols[i].transform.position,bullet.transform.position) < bullet.explotionForceRange)
            {
                asteroidsAtRange.Add(cols[i].GetComponent<Asteroids>());
            }
        }
        for (int i = 0; i < asteroidsAtRange.Count; i++)
        {
            var dir = (asteroidsAtRange[i].transform.position - bullet.transform.position);
            asteroidsAtRange[i].gameObject.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
            asteroidsAtRange[i].Hit(bullet.damage);
        }

        bullet.ReturnToPool();
    }

    public void OnHit(Transform hitTarget, Bullet bullet)
    {
        
        bullet.ReturnToPool();
    }

    public void OnHitExit(Transform bullet)
    {
    }
}
