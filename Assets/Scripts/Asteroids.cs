using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float speed, damage;
    private bool _alive;
    public Vector3 dir;
    private  float _spawningTime = 1;

    public Asteroids(float speed)
    {
        this.speed = speed;
    }

    public virtual void ReturnToPool()
    {
        AsteroidsSpawner.Instance.ReturnAsteroidToPool(this);
        _spawningTime = 1;
    }
    void Update()
    {
        transform.position += dir.normalized * speed * Time.deltaTime;
        if (SpawnFinished())
        {
            //LoopInScreen();
            OutOfScreenReturn();
        }
    }

    public void Initialize()
    {

        transform.position = Vector3.zero;
    }

    public static void InitializeAsteroid(Asteroids bulletObj)
    {
        bulletObj.gameObject.SetActive(true);
        bulletObj.Initialize();
    }

    public static void DisposeAsteroids(Asteroids bulletObj)
    {
        bulletObj.gameObject.SetActive(false);
    }

    public void LoopInScreen()
    {
        var auxPosVec = transform.position;
        if (transform.position.x > ScreenLimits.Instance.RightLimit)
            auxPosVec.x = ScreenLimits.Instance.LeftLimit;
        if (transform.position.x < ScreenLimits.Instance.LeftLimit)
            auxPosVec.x = ScreenLimits.Instance.RightLimit;
        if (transform.position.y < ScreenLimits.Instance.DownLimit)
            auxPosVec.y = ScreenLimits.Instance.UpLimit;
        if (transform.position.y > ScreenLimits.Instance.UpLimit)
            auxPosVec.y = ScreenLimits.Instance.DownLimit;

        transform.position = auxPosVec;
    }

    public void OutOfScreenReturn()
    {

        if (transform.position.x > ScreenLimits.Instance.RightLimit || transform.position.x < ScreenLimits.Instance.LeftLimit || 
            transform.position.y < ScreenLimits.Instance.DownLimit || transform.position.y > ScreenLimits.Instance.UpLimit)
        {
            transform.GetComponent<Asteroids>().ReturnToPool();
        }
    }

    public bool SpawnFinished()
    {
        if (_spawningTime > 0)
        {
            _spawningTime -= Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.TriggerEvent(EventType.NaveDamaged, new object[] { damage });
        }
    }
 
}
