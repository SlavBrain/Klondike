using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] List<Transform> gameObjects;

    private void OnValidate()
    {
        foreach (Transform go in gameObjects)
        {
            go.rotation=Quaternion.Euler(new Vector3(Random.Range(-40,40),Random.Range(-40,40),Random.Range(-40,40)));
        }
    }
}
