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
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(SpawnDragon());
        }
    }

    public IEnumerator SpawnDragon()
    {
        yield return new WaitForSeconds(3f);

        //GameObject dragon = Instantiate(dragonPrefab, gameObject.transform.position, Quaternion.identity);
        GameObject dragon = Instantiate(dragonPrefab, new Vector3((gameObject.transform.position.x), gameObject.transform.position.y, (gameObject.transform.position.z+Random.Range(-0.5f, 0.5f))), Quaternion.identity);
        dragon.transform.LookAt(cameraTransform);
    }
}
