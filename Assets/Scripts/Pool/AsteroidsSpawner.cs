using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour 
{
    private Pool<Asteroids> _asteroidsPool;
    private static AsteroidsSpawner _instance;

    public static AsteroidsSpawner Instance { get { return _instance; } }
    public Asteroids asteroidPrefab;
    public int cantAsteroids;
    
    void Awake()
    {
        _instance = this;
        _asteroidsPool = new Pool<Asteroids>(cantAsteroids, AsteroidsFactory, Asteroids.InitializeAsteroid, Asteroids.DisposeAsteroids, true);
    }

    private Asteroids AsteroidsFactory()
    {
        return Instantiate<Asteroids>(asteroidPrefab);
    }

    public void ReturnAsteroidToPool(Asteroids asteroid)
    {
        _asteroidsPool.DisablePoolObject(asteroid);
    }
    public Asteroids SpawnAsteroid(Vector3 spawner,Vector3 dir)
    {
        var asteroid = _asteroidsPool.GetObjectFromPool();
        asteroid.transform.position = spawner;
        asteroid.dir = dir;
        return asteroid;
    }
}
