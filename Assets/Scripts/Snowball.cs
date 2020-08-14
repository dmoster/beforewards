using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour, IPooledObject
{
    public float sideForce = 0.1f;
    public float upForce = 1f;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);

        Vector2 force = new Vector2(xForce, yForce);

        GetComponent<Rigidbody2D>().velocity = force;
    }
}
