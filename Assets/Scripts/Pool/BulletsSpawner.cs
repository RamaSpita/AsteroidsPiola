using UnityEngine;
using System.Collections;

public class BulletsSpawner : MonoBehaviour 
{
    public Bullet bulletPrefab;
    private Pool<Bullet> _bulletPool;
    public int cantBalas;
    static BulletsSpawner _instance;
    public static BulletsSpawner Instance { get { return _instance; } }
    
    void Awake()
    {
        _instance = this;
        _bulletPool = new Pool<Bullet>(cantBalas, BulletFactory, Bullet.InitializeBullet, Bullet.DisposeBullet, true);
    }

    private Bullet BulletFactory()
    {
        return Instantiate<Bullet>(bulletPrefab);
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
