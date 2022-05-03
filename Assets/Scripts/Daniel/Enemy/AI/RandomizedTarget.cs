using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedTarget : MonoBehaviour
{
    [Range(0, 15)]
    public int value;
    public float timeChangeTarget = 10.0f;

    void Start()
    {
        InvokeRepeating("CalculateNewPosition", 1.0f, timeChangeTarget);
    }

    private void CalculateNewPosition()
    {
        transform.position = new Vector3(Random.Range(-value, value), 1, Random.Range(-value, value));
    }
}
