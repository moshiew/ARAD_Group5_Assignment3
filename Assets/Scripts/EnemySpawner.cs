using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject dragonPrefab;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        SpawnDragon();
    }

    // Instantiate dragon from the portal
    private void SpawnDragon()
    {
        GameObject dragon = Instantiate(dragonPrefab, gameObject.transform.position, Quaternion.identity);
        dragon.transform.LookAt(cameraTransform);
    }
}
