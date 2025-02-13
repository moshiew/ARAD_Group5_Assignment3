using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image healthBar;
    public float baseHealth;
    public float maxHealth;
    public float playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = 100f;
        maxHealth = baseHealth;
        playerHealth = maxHealth;
        healthBar = healthBar.GetComponent<Image>();
        playerHealth -= 50f;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerHealth / maxHealth;
    }
}
