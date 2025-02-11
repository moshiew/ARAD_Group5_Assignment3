using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private float speed;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 targetPosition = cameraTransform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
