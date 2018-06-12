using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticBullet : IBulletBehaviour
{
    public void Initialize(Sprite spr, Transform transform)
    {
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = spr;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }

    public void Move(Transform trans, float speed)
    {
        trans.position += trans.right * speed * Time.deltaTime;

        if (trans.position.x > ScreenLimits.Instance.RightLimit || trans.position.x < ScreenLimits.Instance.LeftLimit ||
           trans.position.y < ScreenLimits.Instance.DownLimit || trans.position.y > ScreenLimits.Instance.UpLimit)
        {
            trans.GetComponent<Bullet>().ReturnToPool();
        }
    }

    public void OnHit(Transform hitTarget, Transform bullet)
    {
    }

    public void OnHitExit(Transform bullet)
    {
    }
}
