using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {
    private BulletsSpawner _bulletSpawner;
    private IShootBehaviour _shootBehaviour;
    private IBulletBehaviour _bulletBehaviour;
    private IUpdate _controller;

    public float life,totalLife, speed, speedRot, fireRate = 5, nextTimeToFire = 0;
    public Transform spawnerFront;
    public Transform spawnerFrontLeft;
    public Transform spawnerFrontRight;
    public Sprite spr;
    public Sprite sprAuto;
    public Sprite sprLaser;
    public Camera mainCamera;
    public Image lifebar;

    void Awake ()
    {
        _bulletSpawner = GetComponent<BulletsSpawner>();
        _controller = new ShipController(this,false);
        _shootBehaviour = new BaseShoot(_bulletSpawner, spawnerFront, new BaseBullet(), spr);
        mainCamera = Camera.main;

        EventsManager.SubscribeToEvent(EventType.ShipLifeModified, OnShipLifeUpdated);
        EventsManager.SubscribeToEvent(EventType.ShipDefeated, OnShipDefeated);
        EventsManager.SubscribeToEvent(EventType.NaveDamaged, ShipDamaged);


    }

    void Update ()
    {
        _controller.Update();
        LoopInScreen();
    }
    public void LaserFire()
    {
        ShootEnd();
        _controller = new ShipController(this, false);
        _shootBehaviour = new LaserShoot(_bulletSpawner, spawnerFront, new LaserBullet(), sprLaser);
    }
    public void AutomaticFire()
    {
        ShootEnd();
        _controller = new ShipController(this, true);
        _shootBehaviour = new AutomaticShoot(_bulletSpawner, spawnerFrontRight, spawnerFrontLeft,new AutomaticBullet(),sprAuto);
      
    }
    public void BaseFire()
    {
        ShootEnd();
        _controller = new ShipController(this, false);
        _shootBehaviour = new BaseShoot(_bulletSpawner, spawnerFront, new BaseBullet(), spr);
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
        _shootBehaviour.Shoot();
    }
    public void ShootEnd()
    {
        _shootBehaviour.End();
    }
    public void ShootEnter()
    {
        _shootBehaviour.Enter();

    }
    private void OnShipDefeated(params object[] parameters)
    {
        EventsManager.UnsubscribeToEvent(EventType.ShipDefeated, OnShipDefeated);
        EventsManager.UnsubscribeToEvent(EventType.ShipLifeModified, OnShipLifeUpdated);
    }
    private void OnShipLifeUpdated(params object[] parameters)
    {
        var currentHp = (float)parameters[0];
        var totalHp = (float)parameters[1];
        lifebar.fillAmount = (currentHp / totalHp);
    }
    private void ShipDamaged(params object[] parameters)
    {
        life -= (float)parameters[0];
        lifebar.fillAmount = (life / totalLife);
        EventsManager.TriggerEvent(EventType.ShipLifeModified, new object[] { life, totalLife });
        if (life<=0)
            EventsManager.TriggerEvent(EventType.ShipDefeated);

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
}
