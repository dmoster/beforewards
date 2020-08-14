using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        objectPooler.SpawnFromPool("Snowflake", transform.position, Quaternion.identity);
    }
}
