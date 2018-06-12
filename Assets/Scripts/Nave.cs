using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Deberia llamarse ship
public class Nave : MonoBehaviour {
    private IUpdate _controller;
    public float life,totalLife, speed, speedRot, fireRate = 5, nextTimeToFire = 0;
    public Transform spawnerFront;
    public Transform spawnerFrontLeft;
    public Transform spawnerFrontRight;
    public BulletsSpawner _bulletSpawner;
    public AutomaticBulletsSpawner _automaticBulletsSpawner;
    private IShootBehaviour shootBehaviour;
    public Sprite spr;
    public Sprite sprAuto;
    public Sprite sprLaser;
    public Camera mainCamera;


    public IBulletBehaviour bulletBehaviour;


    private LineRenderer lineR;
    public Image lifebar;

    void Awake ()
    {
        lineR = GetComponent<LineRenderer>();
        _controller = new NaveController(this,false);
        shootBehaviour = new BaseShoot(_bulletSpawner, spawnerFront, new BaseBullet(), spr);
        mainCamera = Camera.main;

        EventsManager.SubscribeToEvent(EventType.NaveLifeModified, OnNaveLifeUpdated);
        EventsManager.SubscribeToEvent(EventType.NaveDefeated, OnNaveDefeated);
        EventsManager.SubscribeToEvent(EventType.NaveDamaged, NaveDamaged);


    }

    void Update ()
    {
        _controller.Update();
        LoopInScreen();
    }
    public void LaserFire()
    {
        ShootEnd();
        _controller = new NaveController(this, false);
        shootBehaviour = new LaserSho(_bulletSpawner, spawnerFront, new LaserBullet(), sprLaser);
    }
    public void AutomaticFire()
    {
        ShootEnd();
        _controller = new NaveController(this, true);
        shootBehaviour = new AutomaticShoot(_bulletSpawner, spawnerFrontRight, spawnerFrontLeft,new AutomaticBullet(),sprAuto);
      
    }
    public void BaseFire()
    {
        ShootEnd();
        _controller = new NaveController(this, false);
        shootBehaviour = new BaseShoot(_bulletSpawner, spawnerFront, new BaseBullet(), spr);
    }

    public void Move(float acelY)
    {
        var dir = (acelY* transform.right);

        if (dir.magnitude > 1)
            dir = dir.normalized;

        transform.position +=  dir * speed * Time.deltaTime;

    }
    public void Rotation(float rot)
    {
        transform.Rotate(0, 0, -speedRot * rot * Time.deltaTime);
    }

    public void Shoot()
    {
        shootBehaviour.Shoot();
    }
    public void ShootEnd()
    {
        shootBehaviour.End();
    }
    public void ShootEnter()
    {
        shootBehaviour.Enter();

    }
    private void OnNaveDefeated(params object[] parameters)
    {
        EventsManager.UnsubscribeToEvent(EventType.NaveDefeated, OnNaveDefeated);
        EventsManager.UnsubscribeToEvent(EventType.NaveLifeModified, OnNaveLifeUpdated);
    }
    private void OnNaveLifeUpdated(params object[] parameters)
    {
        var currentHp = (float)parameters[0];
        var totalHp = (float)parameters[1];
        lifebar.fillAmount = (currentHp / totalHp);
    }
    private void NaveDamaged(params object[] parameters)
    {
        life -= (float)parameters[0];
        lifebar.fillAmount = (life / totalLife);
        EventsManager.TriggerEvent(EventType.NaveLifeModified, new object[] { life, totalLife });
        if (life<=0)
            EventsManager.TriggerEvent(EventType.NaveDefeated);

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
}
