using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3 prefabOffset;

    private GameObject enemy;
    private ARTrackedImageManager arTrackedImageManager;
    private Transform cameraTransform;

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
            enemy = Instantiate(enemyPrefab, image.transform.position, image.transform.rotation);
            enemy.transform.position += prefabOffset;
            enemy.transform.SetParent(image.transform);
        }

        foreach (ARTrackedImage image in obj.removed)
        {
            if(enemy!= null)
            {
                Destroy(enemy);
            }
        }
    }

    private void Update()
    {
        if(enemy!= null)
        {
            LookAtCamera();
        }
    }

    private void LookAtCamera()
    {
        Vector3 directionToCamera = cameraTransform.position - enemy.transform.position;

        directionToCamera.y = 0;

        if(directionToCamera != Vector3.zero)
        {
            enemy.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}
