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
    }

    public void SpawnDragon(Vector3 spawnPosition)
    {
        Debug.Log("Spawning Dragon...");

        spawnPosition.y -= 1f;
        GameObject dragon = Instantiate(dragonPrefab, spawnPosition, Quaternion.identity);
        dragon.transform.LookAt(cameraTransform);

        StartCoroutine(RotateDragonTowardsCamera(dragon));

    }

    private IEnumerator RotateDragonTowardsCamera(GameObject dragon)
    {
        while(dragon != null)
        {
            Vector3 directionToCamera = cameraTransform.position  - dragon.transform.position;
            directionToCamera.y = 0;

            dragon.transform.rotation = Quaternion.LookRotation(directionToCamera);

            yield return null;
        }
    }
}
