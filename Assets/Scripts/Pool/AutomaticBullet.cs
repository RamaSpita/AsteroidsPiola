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
        var alto = Camera.main.orthographicSize * 2;
        var ancho = 16 * alto / 9;
        var limiteder = Camera.main.transform.position.x + ancho / 2;
        var limiteizq = Camera.main.transform.position.x - ancho / 2;
        var limitearr = Camera.main.transform.position.x + alto / 2;
        var limiteab = Camera.main.transform.position.x - alto / 2;
        if (trans.position.x > limiteder || trans.position.x < limiteizq || trans.position.y < limiteab || trans.position.y > limitearr)
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
