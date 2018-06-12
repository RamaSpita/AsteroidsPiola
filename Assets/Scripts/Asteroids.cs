using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float speed, damage;
    private bool _alive;
    public Vector3 dir;
    private  float spawningTime = 1;
    public Camera mainCamera;

    public Asteroids(float speed)
    {
        this.speed = speed;
    }
    void Awake()
    {
        mainCamera = Camera.main;
    }
    public virtual void ReturnToPool()
    {
        AteroidsSpawner.Instance.ReturnBulletToPool(this);
        spawningTime = 1;
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
        var alto = mainCamera.orthographicSize * 2;
        var ancho = 16 * alto / 9;
        var limiteder = mainCamera.transform.position.x + ancho / 2;
        var limiteizq = mainCamera.transform.position.x - ancho / 2;
        var limitearr = mainCamera.transform.position.x + alto / 2;
        var limiteab = mainCamera.transform.position.x - alto / 2;
        var auxPosVec = transform.position;
        if (transform.position.x > limiteder)
            auxPosVec.x = limiteizq;
        if (transform.position.x < limiteizq)
            auxPosVec.x = limiteder;
        if (transform.position.y < limiteab)
            auxPosVec.y = limitearr;
        if (transform.position.y > limitearr)
            auxPosVec.y = limiteab;

        transform.position = auxPosVec;
    }

    public void OutOfScreenReturn()
    {

        var alto = mainCamera.orthographicSize * 2;
        var ancho = 16 * alto / 9;
        var limiteder = mainCamera.transform.position.x + ancho / 2;
        var limiteizq = mainCamera.transform.position.x - ancho / 2;
        var limitearr = mainCamera.transform.position.x + alto / 2;
        var limiteab = mainCamera.transform.position.x - alto / 2;
        if (transform.position.x > limiteder || transform.position.x < limiteizq || transform.position.y < limiteab || transform.position.y > limitearr)
        {
            transform.GetComponent<Asteroids>().ReturnToPool();
        }
    }

    public bool SpawnFinished()
    {
        if (spawningTime > 0)
        {
            spawningTime -= Time.deltaTime;
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
