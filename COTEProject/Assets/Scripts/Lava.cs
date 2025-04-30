using System;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private GameObject _lava;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float interpolationSpeed = 5f;
    float t = 0f;

    void Start()
    {
        _lava = GameObject.FindWithTag("Lava");

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 y = new Vector3(_lava.transform.position.x, transform.position.y - 10, _lava.transform.position.z);
            _lava.transform.position = y;

        }
    }
    
}
