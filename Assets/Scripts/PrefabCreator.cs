using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> portalPrefabs;
    [SerializeField] private Vector3 prefabOffset;
    private float delayBeforeEnemySpawn = 3.0f;

    private ARTrackedImageManager arTrackedImageManager;
    private Transform cameraTransform;

    private List<GameObject> spawnedPortals = new List<GameObject>();
    private EnemySpawner enemySpawner;

    private void OnEnable()
    {
        arTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;

        cameraTransform = Camera.main.transform;

        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage image in obj.added)
        {
            int index = GetImageIndex(image.referenceImage.name);

            Debug.Log("Image Tracked: " + image.referenceImage.name + " - Prefab Index: " + index);

            if (index >= 0 && index < portalPrefabs.Count)
            {
                // Instantiate the portal
                GameObject portal = Instantiate(portalPrefabs[index], image.transform.position, image.transform.rotation);
                portal.transform.position += prefabOffset;
                portal.transform.SetParent(image.transform);

                // Add the portal to the list
                spawnedPortals.Add(portal);

                StartCoroutine(SpawnEnemyAfter(image));
            }
        }

        foreach (ARTrackedImage image in obj.removed)
        {
            Transform portalTransform = image.transform.GetChild(0);
            if (portalTransform != null)
            {
                spawnedPortals.Remove(portalTransform.gameObject);
                Destroy(portalTransform.gameObject);
            }
        }
    }

    private void Update()
    {
        foreach (GameObject portal in spawnedPortals)
        {
            if (portal != null)
            {
                LookAtCamera(portal);
            }
        }
    }

    private void LookAtCamera(GameObject portal)
    {
        Vector3 directionToCamera = cameraTransform.position - portal.transform.position;

        directionToCamera.y = 0;

        if (directionToCamera != Vector3.zero)
        {
            portal.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }

    private int GetImageIndex(string imageName)
    {
        for (int i = 0; i < arTrackedImageManager.referenceLibrary.count; i++)
        {
            if (arTrackedImageManager.referenceLibrary[i].name == imageName)
            {
                return i;
            }
        }
        return -1;
    }

    private IEnumerator SpawnEnemyAfter(ARTrackedImage image)
    {
        // Wait for a specified delay before spawning the enemy
        yield return new WaitForSeconds(delayBeforeEnemySpawn);  // Adjust the delay time here for testing

        // Debug log to confirm that the delay is done
        Debug.Log("Spawn delay passed. Now spawning the dragon...");

        // Now, call the EnemySpawner to spawn the appropriate enemy (e.g., dragon)
        if (enemySpawner != null)
        {
            enemySpawner.SpawnDragon(image.transform.position);
        }
    }
}

