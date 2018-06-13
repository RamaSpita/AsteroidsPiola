using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroid : IAsteroidBehaviour
{
    public Color color;
    public void Initialize(Transform transform)
    {
        transform.localScale /= 2;
        color = transform.gameObject.GetComponent<SpriteRenderer>().color;
        transform.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }
    public void BreakAsteroid(Asteroids asteroid)
    {
        ReturnToPool(asteroid);
    }

    public void ReturnToPool(Asteroids asteroid)
    {
        asteroid.transform.localScale *= 2;
        asteroid.transform.gameObject.GetComponent<SpriteRenderer>().color = color;
        asteroid.ReturnToPool();
    }
}
