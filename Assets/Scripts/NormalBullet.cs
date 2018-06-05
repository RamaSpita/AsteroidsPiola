using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : IBulletBehaviour
{
    public void Initialize(Sprite spr,Transform transform)
    {
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = spr;
    }

    public void Move(Transform transform , float speed)
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public void OnHit(Transform hitTarget, Transform bullet)
    {
        throw new System.NotImplementedException();
    }

    public void OnHitExit(Transform bullet)
    {
    }
}
