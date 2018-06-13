using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    private GameObject _particlesPrefab;
	void Start ()
    {
        EventsManager.SubscribeToEvent("Particles", SpawnParticles);
        _particlesPrefab = Resources.Load<GameObject>("Explotion");
    }

    private void SpawnParticles(object[] parameter)
    {
        var particle = GameObject.Instantiate(_particlesPrefab);
        particle.transform.position = (Vector3)parameter[0];
        Destroy(particle, 2f);
    }
}
