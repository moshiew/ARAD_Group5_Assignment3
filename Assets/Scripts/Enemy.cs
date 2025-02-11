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
        health = healthIcons.Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Damaged();
        Vector3 targetPosition = cameraTransform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void Damaged()
    {
        health--;
        animator.SetTrigger("Damaged");

        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].gameObject.SetActive(i < health); // Do a check of the health and the length of the array
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
