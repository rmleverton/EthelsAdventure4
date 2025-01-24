using UnityEngine;
using System;
using UnityEngine.UI;

public class Chest_Behavior : MonoBehaviour
{
    int collisionCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionCount = 0;
        Debug.Log(collisionCount);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collisionCount++;
            Debug.Log(collisionCount);
        }
    }
}


// Check to make sure we are only ever inside one collider. 