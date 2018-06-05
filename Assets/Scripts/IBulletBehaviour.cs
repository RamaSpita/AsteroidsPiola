using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletBehaviour
{
    void Move(Transform trans,float speed);
    void Initialize(Sprite spr, Transform transform);
    void OnHit(Transform hitTarget, Transform bullet);
    void OnHitExit(Transform bullet);
}
