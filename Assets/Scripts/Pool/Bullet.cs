using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    public float speed;
    private bool _alive;
    public IBulletBehaviour bulletBehaviour;

    public Bullet(float speed)
    {
        this.speed = speed;
    }
    void Awake()
    {
        bulletBehaviour = new BaseBullet();
    }
    public virtual void ReturnToPool()
    {
        BulletsSpawner.Instance.ReturnBulletToPool(this);
    }
    void Update ()
    {

        bulletBehaviour.Move(transform, speed);
        
     
        
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
            bulletBehaviour.OnHit(other.transform, transform);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        bulletBehaviour.OnHitExit(transform);
    }
}
