using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : IBulletBehaviour
{
    public void Initialize(Sprite spr, Transform transform)
    {
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = spr;
        transform.localScale = new Vector3(25, 0.25f, transform.localScale.z);
    }

    public void Move(Transform trans, float speed)
    {
        //var bulleOffset = trans.parent.transform.position + trans.parent.transform.right * trans.localScale.x / 2;
        //trans.position = bulleOffset;
        //trans.right = trans.parent.transform.right;
    }

    public void OnHit(Transform hitTarget, Transform bullet)
    {
        bullet.localScale = new Vector3(Vector3.Distance(hitTarget.position,(bullet.position - bullet.right * bullet.transform.localScale.x / 2)), bullet.localScale.y, bullet.localScale.z);
    }

    public void OnHitExit(Transform bullet)
    {
        bullet.localScale = new Vector3(25, 0.25f, bullet.localScale.z);
    }
}
