using UnityEngine;
using System.Collections;

public class AteroidsSpawner : MonoBehaviour 
{
    public Asteroids asteroidPrefab;
    private Pool<Asteroids> _asteroidsPool;
    public int cantAsteroids;
    private static AteroidsSpawner _instance;
    public static AteroidsSpawner Instance { get { return _instance; } }
    
    void Awake()
    {
        _instance = this;
        _asteroidsPool = new Pool<Asteroids>(cantAsteroids, BulletFactory, Asteroids.InitializeAsteroid, Asteroids.DisposeAsteroids, true);
    }

    private Asteroids BulletFactory()
    {
        return Instantiate<Asteroids>(asteroidPrefab);
    }

    public void ReturnBulletToPool(Asteroids asteroid)
    {
        _asteroidsPool.DisablePoolObject(asteroid);
    }
    public Asteroids SpawnAsteroid(Vector3 spawner,Vector3 dir)
    {
        var asteroid = _asteroidsPool.GetObjectFromPool();
        asteroid.transform.position = spawner;
        asteroid.dir = dir;
        return  asteroid;
    }
}
