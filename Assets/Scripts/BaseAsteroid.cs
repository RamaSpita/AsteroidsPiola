using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAsteroid : IAsteroidBehaviour
{

    public bool spawned = false;
    public void Initialize(Transform transform)
    {
    }
    public void BreakAsteroid(Asteroids asteroid)
    {
        AsteroidsSpawner.Instance.SpawnAsteroid(asteroid.transform.position, asteroid.transform.up, new SmallAsteroid());
        AsteroidsSpawner.Instance.SpawnAsteroid(asteroid.transform.position, -asteroid.transform.up, new SmallAsteroid());
        AsteroidsSpawner.Instance.SpawnAsteroid(asteroid.transform.position, asteroid.transform.right, new SmallAsteroid());
        AsteroidsSpawner.Instance.SpawnAsteroid(asteroid.transform.position, -asteroid.transform.right, new SmallAsteroid());

        ReturnToPool(asteroid);
    }
    public void ReturnToPool(Asteroids asteroid)
    {
        asteroid.ReturnToPool();
    }

}

