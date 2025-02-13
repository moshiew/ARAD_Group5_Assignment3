using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject dragonPrefab;
    [SerializeField] private float spawnInterval = 3f;
    private Transform cameraTransform;
    private bool isSpawning = true;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        StartCoroutine(SpawnDragon());
    }

    public IEnumerator SpawnDragon()
    {
        while (isSpawning) 
        {
            yield return new WaitForSeconds(spawnInterval);
            if (!isSpawning)
            {
                yield break;
            }
            GameObject dragon = Instantiate(dragonPrefab, gameObject.transform.position, Quaternion.identity);
            dragon.transform.LookAt(cameraTransform);
        }
       
    }
}
