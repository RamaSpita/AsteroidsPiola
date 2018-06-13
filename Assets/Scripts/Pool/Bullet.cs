using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    private bool _alive;
    private Transform objectHit;
    private float debugHit = 0;

    public float speed;
    public float damage;
    public float explotionForceRange;
    public IBulletBehaviour bulletBehaviour;

    public Bullet(float speed)
    {
        this.speed = speed;
    }
    void Awake()
    {
        bulletBehaviour = new BaseBullet();
        debugHit = 0;

    }
    public virtual void ReturnToPool()
    {
        objectHit = null;
        debugHit = 0;
        BulletsSpawner.Instance.ReturnBulletToPool(this);
    }
    void Update ()
    {
        bulletBehaviour.Move(this, speed);

        if (objectHit != null)
        {
            bulletBehaviour.OnHit(objectHit, this);

        }

    }
    

    public void Initialize()
    {
        
        transform.position = Vector3.zero;
    }

    public static void InitializeBullet(Bullet bulletObj)
    {
        bulletObj.gameObject.SetActive(true);
        bulletObj.Initialize();
    }

    public static void DisposeBullet(Bullet bulletObj)
    {
        bulletObj.gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        objectHit = other.transform;
        if (other.gameObject.layer == 10 && debugHit < 1)
        {
            debugHit++;
            var asteroid = other.GetComponent<Asteroids>();
            bulletBehaviour.OnAsteroidHit(asteroid, this);

        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            var asteroid = other.GetComponent<Asteroids>();
            bulletBehaviour.OnAsteroidHit(asteroid,this);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        bulletBehaviour.OnHitExit(transform);
        objectHit = null;
    }
    

}
