using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {

    public Transform[] spawnLimits;
    private AteroidsSpawner asteroidSpawner;
    public float timeBetweenSpawns;

    //private Dictionary<int, Action> _spawnPositions;

	void Awake ()
    {
        asteroidSpawner = GetComponent<AteroidsSpawner>();

        /*_spawnPositions = new Dictionary<int, Action>();
        _spawnPositions[0] = () => {
            auxVec.x = spawnLimits[0].position.x; // 0 es right
            auxVec.y = Random.Range(spawnLimits[1].position.y, spawnLimits[2].position.y);
        };*/
	}
    private void Start()
    {
        StartCoroutine(AsteroidsSpawner());
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnPosition();
        }
	}
    public void SpawnPosition()
    {
        Vector3 auxPos = GetVec3FromRandom(UnityEngine.Random.Range(1, 5));
        Vector3 auxDir = auxPos;
        while (auxDir.x == auxPos.x || auxDir.y == auxPos.y)
        {
            auxDir = GetVec3FromRandom(UnityEngine.Random.Range(1, 5));

        }

        auxDir = (auxDir - auxPos).normalized;
        asteroidSpawner.SpawnAsteroid(auxPos,auxDir);

    }

    public Vector3 GetVec3FromRandom(int randomInt)
    {
        Vector3 auxVec = Vector3.zero;

        switch (randomInt)
        {
            case 1:
                auxVec.x = spawnLimits[0].position.x; // 0 es right
                auxVec.y = UnityEngine.Random.Range(spawnLimits[1].position.y, spawnLimits[2].position.y);

                break;
            case 2:
                auxVec.x = spawnLimits[3].position.x; // 3 es left
                auxVec.y = UnityEngine.Random.Range(spawnLimits[1].position.y, spawnLimits[2].position.y);
                break;
            case 3:
                auxVec.y = spawnLimits[1].position.y; // 1 es up
                auxVec.x = UnityEngine.Random.Range(spawnLimits[0].position.x, spawnLimits[3].position.x);
                break;
            case 4:
                auxVec.y = spawnLimits[2].position.y; // 2 es down
                auxVec.x = UnityEngine.Random.Range(spawnLimits[0].position.x, spawnLimits[3].position.x);
                break;
        }
        return auxVec;
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
