using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Vector3 prefabOffset;

    private ARTrackedImageManager arTrackedImageManager;
    private Transform cameraTransform;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void OnEnable()
    {
        arTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;

        cameraTransform = Camera.main.transform;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage image in obj.added)
        {
            int index = GetImageIndex(image.referenceImage.name);

            Debug.Log("Image Tracked: " + image.referenceImage.name + " - Prefab Index: " + index);

            if (index >= 0 && index < enemyPrefabs.Count)
            { 
                GameObject enemy = Instantiate(enemyPrefabs[index], image.transform.position, image.transform.rotation);
                enemy.transform.position += prefabOffset;
                enemy.transform.SetParent(image.transform);

                spawnedEnemies.Add(enemy);
            }
        }

        foreach (ARTrackedImage image in obj.removed)
        {
            Transform enemyTransform = image.transform.GetChild(0);
            if(enemyTransform != null)
            {
                spawnedEnemies.Remove(enemyTransform.gameObject);
                Destroy(enemyTransform.gameObject);
            }
        }
    }

    private void Update()
    {
        foreach(GameObject enemy in spawnedEnemies)
        {
            if(enemy != null)
            {
                LookAtCamera(enemy);
            }
        }
    }

    private void LookAtCamera(GameObject enemy)
    {
        Vector3 directionToCamera = cameraTransform.position - enemy.transform.position;

        directionToCamera.y = 0;

        if(directionToCamera != Vector3.zero)
        {
            enemy.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }

    private int GetImageIndex(string imageName)
    {
        for(int i = 0; i < arTrackedImageManager.referenceLibrary.count; i++)
        {
            if (arTrackedImageManager.referenceLibrary[i].name == imageName)
            {
                return i;
            }
        }
        return -1;
    }
}
