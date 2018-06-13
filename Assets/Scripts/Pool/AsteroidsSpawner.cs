using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour 
{
    private Pool<Asteroids> _asteroidsPool;
    private static AsteroidsSpawner _instance;

    public static AsteroidsSpawner Instance { get { return _instance; } }
    public int cantAsteroids;
    
    void Awake()
    {
        _instance = this;
        _asteroidsPool = new Pool<Asteroids>(cantAsteroids, AsteroidsFactory, Asteroids.InitializeAsteroid, Asteroids.DisposeAsteroids, true);
    }

    private Asteroids AsteroidsFactory()
    {
        return Instantiate((Asteroids)Resources.Load("Asteroid", typeof(Asteroids)));
    }

    public void ReturnAsteroidToPool(Asteroids asteroid)
    {
        _asteroidsPool.DisablePoolObject(asteroid);
    }
    public Asteroids SpawnAsteroid(Vector3 spawner,Vector3 dir,IAsteroidBehaviour asteroidBehaviour)
    {
        var asteroid = _asteroidsPool.GetObjectFromPool();
        asteroid.asteroidBehaviour = asteroidBehaviour;
        asteroid.transform.position = spawner+ dir * 0.25f;
        asteroid.dir = dir;
        return asteroid;
    }
}
