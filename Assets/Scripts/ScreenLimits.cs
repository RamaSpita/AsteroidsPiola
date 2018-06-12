using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLimits : MonoBehaviour
{
    private static ScreenLimits _instance;
    public static ScreenLimits Instance { get { return _instance; } }

    private float _rightLimit;
    private float _leftLimit;
    private float _upLimit;
    private float _downLimit;
    public float RightLimit
    {
        get
        {
            return _rightLimit;
        }
    }
    public float LeftLimit
    {
        get
        {
            return _leftLimit;
        }
    }
    public float UpLimit
    {
        get
        {
            return _upLimit;
        }
    }
    public float DownLimit
    {
        get
        {
            return _downLimit;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        var alto = Camera.main.orthographicSize * 2;
        var ancho = 16 * alto / 9;
        _rightLimit = Camera.main.transform.position.x + ancho / 2;
        _leftLimit = Camera.main.transform.position.x - ancho / 2;
        _upLimit = Camera.main.transform.position.x + alto / 2;
        _downLimit = Camera.main.transform.position.x - alto / 2;
    }

}
