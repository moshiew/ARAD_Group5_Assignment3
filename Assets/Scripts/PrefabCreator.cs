using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject portalPrefab; // Portal prefab to instantiate
    [SerializeField] private Vector3 prefabOffset; // Offset to adjust the portal's position

    [SerializeField] private GameObject winUI; // Reference to the Win UI
    [SerializeField] private TextMeshProUGUI timerUI; // Reference to the Timer UI
    [SerializeField] private float gameDuration = 10f; // Total gameplay time

    private float time;
    public bool timeCountDown = false;
    private ARTrackedImageManager arTrackedImageManager;
    private Transform cameraTransform;

    public GameObject spawnedPortal; // Only one portal will be tracked and spawned

    private void OnEnable()
    {
        // Instantiate AR image tracking manager and camera
        arTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;

        cameraTransform = Camera.main.transform; // Get the camera's transform
        winUI.SetActive(false);
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // Handle the addition of new tracked images
        foreach (ARTrackedImage image in obj.added)
        {
            Debug.Log("Image Tracked: " + image.referenceImage.name);

            // Instantiate portal at the image's position
            if (spawnedPortal == null)
            {
                // Instantiate the portal at the image's position
                GameObject portal = Instantiate(portalPrefab, image.transform.position, image.transform.rotation);
                portal.transform.position += prefabOffset; // Apply the offset to the portal's position
                portal.transform.SetParent(image.transform); // Set the portal as a child of the tracked image

                LookAtCamera(portal);

                spawnedPortal = portal;
                StartCoroutine(endGameplay());
                timeCountDown = true;
            }
        }

        // Handle removal of tracked images
        foreach (ARTrackedImage image in obj.removed)
        {
            if (spawnedPortal != null)
            {
                Destroy(spawnedPortal);
                spawnedPortal = null;
            }
        }
    }

    private void Update()
    {
        if (spawnedPortal != null)
        {
            LookAtCamera(spawnedPortal);
        }
        if (timeCountDown)
        {
            time = Mathf.CeilToInt(gameDuration -= Time.deltaTime);
            timerUI.text = time.ToString();
        }
        else 
        {
            time = 0f;
        }
    }

    private void LookAtCamera(GameObject portal)
    {
        // Calculate the direction from the portal to the camera
        Vector3 directionToCamera = cameraTransform.position - portal.transform.position;
        // Set y axis to 0
        directionToCamera.y = 0;

        if (directionToCamera != Vector3.zero)
        {
            // Rotate the portal to face the camera
            portal.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }

    private IEnumerator endGameplay()
    {
        yield return new WaitForSeconds(gameDuration);
        GameObject[] dragonPrefabGroup = GameObject.FindGameObjectsWithTag("Dragon");
        if (spawnedPortal != null)
        {
            Destroy(spawnedPortal);
            foreach (var dragon in dragonPrefabGroup)
            {
                Destroy(dragon);
            }
            timeCountDown = false;

            winUI.SetActive(true);
            yield return new WaitForSeconds(3f);
            winUI.SetActive(false);

        }
    }
}
