using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {

    public Transform[] spawnLimits;
    private AsteroidsSpawner _asteroidSpawner;
    public float timeBetweenSpawns;
    private Dictionary<int, Func<Vector2>> _spawnPositions = new Dictionary<int, Func<Vector2>>();

	void Awake ()
    {
        _asteroidSpawner = GetComponent<AsteroidsSpawner>();
        _spawnPositions[0] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.x = spawnLimits[0].position.x;
            auxVec.y = UnityEngine.Random.Range(spawnLimits[1].position.y, spawnLimits[2].position.y);
            return auxVec;
        };
        _spawnPositions[1] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.x = spawnLimits[3].position.x;
            auxVec.y = UnityEngine.Random.Range(spawnLimits[1].position.y, spawnLimits[2].position.y);
            return auxVec;
        };
        _spawnPositions[2] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.y = spawnLimits[1].position.y; // 1 es up
            auxVec.x = UnityEngine.Random.Range(spawnLimits[0].position.x, spawnLimits[3].position.x);
            return auxVec;
        };
        _spawnPositions[3] = () => {
            Vector2 auxVec = Vector2.zero;
            auxVec.y = spawnLimits[2].position.y; // 2 es down
            auxVec.x = UnityEngine.Random.Range(spawnLimits[0].position.x, spawnLimits[3].position.x);
            return auxVec;
        };

    }
    private void Start()
    {
        StartCoroutine(AsteroidsSpawner());
        
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
        _asteroidSpawner.SpawnAsteroid(auxPos,auxDir);

    }

    public IEnumerator AsteroidsSpawner()
    {
        var wait = new WaitForSeconds(timeBetweenSpawns);
        while (true)
        {
            SpawnPosition();
            yield return wait;
        }
    }
}
