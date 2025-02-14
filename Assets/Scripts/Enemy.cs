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
    public PlayerUI playerUI;
    public Collider2D hitter;
    public bool invincible;
    public bool touchPlayer;
    public float attackPower = 20f;

    public Image[] healthIcons;

    private void Start()
    {
        touchPlayer = false;
        invincible = false;
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        health = healthIcons.Length; // Assign the healthIcons array length to health value;
        playerUI = GetComponent<PlayerUI>();
        StartCoroutine(DamagePlayer());
    }

    void Update()
    {
        Vector3 targetPosition = cameraTransform.position; // Making Camera Position to Vector3
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // Moving Towards Camera
        CheckPlayer();
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
    public void CheckPlayer()
    {
        if (FindAnyObjectByType<PlayerUI>() != null && playerUI == null)
        {
            //Debug.Log("Enemy found.");
            playerUI = FindAnyObjectByType<PlayerUI>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHitbox>() != null)
        {
            touchPlayer = true;
        }
        else
        {
            touchPlayer = false;
        }
        //Debug.Log(other.gameObject);
    }
    IEnumerator DamagePlayer()
    {
        while (true)
        {
            if (invincible == false && touchPlayer == true)
            {
                invincible = true;
                if (playerUI != null)
                {
                    playerUI.Damaged(attackPower);
                }
                Debug.Log("Hit");
                playerUI.UpdateText();
                yield return new WaitForSeconds(2f);
                invincible = false;
            }
            yield return new WaitForSeconds(0f);
        }
    }
}
