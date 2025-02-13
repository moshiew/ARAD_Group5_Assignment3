using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform cameraTransform;
    private Animator animator;
    [SerializeField] private float speed;
    [HideInInspector] public int health;

    public Image[] healthIcons;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        health = healthIcons.Length; // Assign the healthIcons array length to health value;
    }

    void Update()
    {
        Vector3 targetPosition = cameraTransform.position; // Making Camera Position to Vector3
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // Moving Towards Camera
    }

    // Damaging Enemy
    public void Damaged()
    {
        health--;
        animator.SetTrigger("Damaged");

        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].gameObject.SetActive(i < health); // Do a check of the health and the length of the array
        }

        // Death check
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
