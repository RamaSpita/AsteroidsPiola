using UnityEngine;
using System.Collections;

public class BulletsSpawner : MonoBehaviour 
{
    private Pool<Bullet> _bulletPool;
    private static BulletsSpawner _instance;

    public int cantBalas;
    public static BulletsSpawner Instance { get { return _instance; } }
    
    void Awake()
    {
        _instance = this;
        _bulletPool = new Pool<Bullet>(cantBalas, BulletFactory, Bullet.InitializeBullet, Bullet.DisposeBullet, true);
    }

    private Bullet BulletFactory()
    {
        return Instantiate((Bullet)Resources.Load("Bullet", typeof(Bullet)));
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        _bulletPool.DisablePoolObject(bullet);

    }
    public Bullet SpawnBullet(Transform spawner)
    {
        var bullet = _bulletPool.GetObjectFromPool();
        bullet.transform.position = spawner.position;
        bullet.transform.right = spawner.right;
        return  bullet;
    }
}
