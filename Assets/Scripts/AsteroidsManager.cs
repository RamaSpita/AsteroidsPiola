using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {

    public float timeBetweenSpawns;
    private Dictionary<int, Func<Vector2>> _spawnPositions = new Dictionary<int, Func<Vector2>>();

	void Awake ()
    {
        
        _spawnPositions[0] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.x = ScreenLimits.Instance.RightLimit;
            auxVec.y = UnityEngine.Random.Range(ScreenLimits.Instance.UpLimit, ScreenLimits.Instance.DownLimit);
            return auxVec;
        };
        _spawnPositions[1] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.x = ScreenLimits.Instance.LeftLimit;
            auxVec.y = UnityEngine.Random.Range(ScreenLimits.Instance.UpLimit, ScreenLimits.Instance.DownLimit);
            return auxVec;
        };
        _spawnPositions[2] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.y = ScreenLimits.Instance.UpLimit; // 1 es up
            auxVec.x = UnityEngine.Random.Range(ScreenLimits.Instance.RightLimit, ScreenLimits.Instance.LeftLimit);
            return auxVec;
        };
        _spawnPositions[3] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.y = ScreenLimits.Instance.DownLimit; // 2 es down
            auxVec.x = UnityEngine.Random.Range(ScreenLimits.Instance.RightLimit, ScreenLimits.Instance.LeftLimit);
            return auxVec;
        };

    }
    private void Start()
    {
        StartCoroutine(AsteroidsAutomaticSpawner());

    }

 
    void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnPosition();
        }
	}
    public void SpawnPosition()
    {
        Vector3 auxPos = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Count)]();
        Vector3 auxDir = auxPos;
        while (auxDir.x == auxPos.x || auxDir.y == auxPos.y)
        {
            auxDir = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Count)]();

        }

        auxDir = (auxDir - auxPos).normalized;
        AsteroidsSpawner.Instance.SpawnAsteroid(auxPos, auxDir, new BaseAsteroid());

    }

    public IEnumerator AsteroidsAutomaticSpawner()
    {
        var wait = new WaitForSeconds(timeBetweenSpawns);
        while (true)
        {
            SpawnPosition();
            yield return wait;
        }
    }
}
