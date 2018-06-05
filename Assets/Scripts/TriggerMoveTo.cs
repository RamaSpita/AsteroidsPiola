using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoveTo : MonoBehaviour {

    public Transform moveTo;
    public bool keepX;
    public bool keepY;

    void Start()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 sa = collision.transform.position;
            if (!keepX)
            {
                sa.x = moveTo.position.x;

            }
            if (!keepY)
            {
                sa.y = moveTo.position.y;

            }
            collision.transform.position = sa;
        }
    }
}

