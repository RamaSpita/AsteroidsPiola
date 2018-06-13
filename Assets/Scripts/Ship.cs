using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ship : MonoBehaviour {
    private BulletsSpawner _bulletSpawner;
    private IShootBehaviour _shootBehaviour;
    private IBulletBehaviour _bulletBehaviour;
    private IUpdate _controller;
    private Rigidbody2D _rb;
    private Sprite _spr;
    private Sprite _sprAuto;
    private Sprite _sprLaser;
    private SpriteRenderer _shipSpriteRenderer;
    public Sprite[] _sprsShip = new Sprite[4];
    private int state = 3;

    public float life,totalLife, speed, speedRot, fireRate = 5, nextTimeToFire = 0;
    public Transform spawnerFront;
    public Transform spawnerFrontLeft;
    public Transform spawnerFrontRight;
    public Camera mainCamera;
   

    void Awake ()
    {

        _spr = Resources.Load<Sprite>("NormalBullet");
        _sprLaser = Resources.Load<Sprite>("Laser");
        _sprAuto = Resources.Load<Sprite>("Automatic");
        _shipSpriteRenderer = GetComponent<SpriteRenderer>();

        var sprites = Resources.LoadAll("Ships");

        _sprsShip[0] = (Sprite)sprites[6];
        _sprsShip[1] = (Sprite)sprites[8];
        _sprsShip[2] = (Sprite)sprites[5];
        _sprsShip[3] = (Sprite)sprites[1];

        _shipSpriteRenderer.sprite = _sprsShip[3];
        _bulletSpawner = GetComponent<BulletsSpawner>();
        _rb = GetComponent<Rigidbody2D>();
        _controller = new ShipController(this,false);
        _shootBehaviour = new BaseShoot(_bulletSpawner, spawnerFront, new BaseBullet(), _spr);
        mainCamera = Camera.main;



        EventsManager.SubscribeToEvent("ShipDefeated", OnShipDefeated);
        EventsManager.SubscribeToEvent("ShipDamaged", ShipDamaged);
        EventsManager.SubscribeToEvent("Win", Unsuscribe);
        EventsManager.SubscribeToEvent("Lose", Unsuscribe);


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
        _shootBehaviour = new LaserShoot(_bulletSpawner, spawnerFront, new LaserBullet(), _sprLaser);
    }
    public void BombFire()
    {
        ShootEnd();
        _controller = new ShipController(this, false);
        _shootBehaviour = new BaseShoot(_bulletSpawner, spawnerFront, new BombBullet(), _sprLaser);
    }
    public void AutomaticFire()
    {
        ShootEnd();
        _controller = new ShipController(this, true);
        _shootBehaviour = new AutomaticShoot(_bulletSpawner, spawnerFrontRight, spawnerFrontLeft,new AutomaticBullet(),_sprAuto);
      
    }
    public void BaseFire()
    {
        ShootEnd();
        _controller = new ShipController(this, false);
        _shootBehaviour = new BaseShoot(_bulletSpawner, spawnerFront, new BaseBullet(), _spr);
    }

    public void Move(float acelY)
    {
        if (acelY > 0 && state != 2 )
        {
            state = 2;
            _shipSpriteRenderer.sprite = _sprsShip[2];
        }
        else if(acelY <= 0 && state != 3)
        {
            state = 3;
            _shipSpriteRenderer.sprite = _sprsShip[3];
        }
        var dir = (acelY* transform.up);

        if (dir.magnitude > 1)
            dir = dir.normalized;

       
        if(acelY > 0)
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, speed);
            _rb.velocity += (Vector2)dir * speed / 20;
        }
        else
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, speed/2);
            _rb.velocity += (Vector2)dir * speed / 40;
        }

    }
    public void Rotation(float rot)
    {
        transform.Rotate(0, 0, -speedRot * rot * Time.deltaTime);
        if (rot > 0 && state != 0)
        {
            state = 0;
            _shipSpriteRenderer.sprite = _sprsShip[0];
        }
        else if (rot < 0 && state != 1)
        {
            state = 1;
            _shipSpriteRenderer.sprite = _sprsShip[1];
        }

        
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
        ShootEnd();
        EventsManager.TriggerEvent("Lose");

    }
    private void ShipDamaged(params object[] parameters)
    {
        life -= (float)parameters[0];
        EventsManager.TriggerEvent("ShipLifeModified", new object[] { life, totalLife });
        if (life <= 0)
        {
            EventsManager.TriggerEvent("ShipDefeated");
        }

    }
    public void Unsuscribe(params object[] parameters)
    {
        EventsManager.UnsubscribeToEvent("ShipDefeated", OnShipDefeated);
        EventsManager.UnsubscribeToEvent("ShipDamaged", ShipDamaged);
        EventsManager.UnsubscribeToEvent("Win", Unsuscribe);
        EventsManager.UnsubscribeToEvent("Lose", Unsuscribe);
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
