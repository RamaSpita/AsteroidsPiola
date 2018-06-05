using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : IShootBehaviour
{

    Transform _spawn;
    LineRenderer _lineR;
    public RaycastHit hitInfo;

    public LaserShoot(LineRenderer lineR, Transform spawn)
    {
        _spawn = spawn;
        _lineR = lineR;
    }
    public void Enter()
    {
        _lineR.enabled = true;
    }

    public void Shoot()
    {
        _lineR.SetPosition(0, _spawn.position);
        _lineR.SetPosition(1, _spawn.right * 200);
        Ray ray = new Ray();
        ray.origin = _spawn.position;
        ray.direction = _spawn.right * 200;
        if (Physics.Raycast(ray, out hitInfo, 1000) && hitInfo.transform.gameObject != null)
        {
            _lineR.SetPosition(1, hitInfo.point);
        }
    }

    public void End()
    {
        _lineR.enabled = false;
    }

}
