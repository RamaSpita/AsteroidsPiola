using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletBehaviour
{
    void Move(Bullet bullet, float speed);
    void Initialize(Sprite spr, Bullet bullet);
    void OnHit(Transform hitTarget, Bullet bullet);
    void OnHitExit(Transform bullet);
    void OnAsteroidHit(Asteroids asteroid,Bullet bullet);
}
