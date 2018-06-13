using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private bool _alive;
    private  float _spawningTime = 1;
    private bool _initialized= false;

    public float speed, damage;
    public Vector3 dir;
    public IAsteroidBehaviour asteroidBehaviour;
    public float life;
    private Rigidbody2D rb;
    public int brokeDebug = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (asteroidBehaviour == null)
        {
           asteroidBehaviour = new BaseAsteroid();
        }
    }


    public Asteroids(float speed)
    {
        this.speed = speed;
    }

    public virtual void ReturnToPool()
    {
        _spawningTime = 1;
        _initialized = false;
        life = 1;
        asteroidBehaviour = new BaseAsteroid();
        AsteroidsSpawner.Instance.ReturnAsteroidToPool(this);
    }
    void Update()
    {
        if (!_initialized)
        {
            asteroidBehaviour.Initialize(transform);
            brokeDebug = 0;
            _initialized = true;
            rb.AddForce((Vector2)dir * speed, ForceMode2D.Impulse);

        }



        if (SpawnFinished())
        {

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


    public void OutOfScreenReturn()
    {

        if (transform.position.x > ScreenLimits.Instance.RightLimit || transform.position.x < ScreenLimits.Instance.LeftLimit || 
            transform.position.y < ScreenLimits.Instance.DownLimit || transform.position.y > ScreenLimits.Instance.UpLimit)
        {
            asteroidBehaviour.ReturnToPool(this);
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
    public void Hit(float bulletDamage)
    {
        if (life> bulletDamage)
        {
            life -= bulletDamage;
        }
        else if(brokeDebug<1)
        {
            brokeDebug++;
            EventsManager.TriggerEvent("ScoreUpdate", new object[] { 10f });
            asteroidBehaviour.BreakAsteroid(this);
            EventsManager.TriggerEvent("Particles",this.transform.position);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventsManager.TriggerEvent("ShipDamaged", new object[] { damage });
        }

    }

}
