using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAsteroidBehaviour
{

    void Initialize(Transform transform);
    void BreakAsteroid(Asteroids asteroid);
    void ReturnToPool(Asteroids asteroid);
}
