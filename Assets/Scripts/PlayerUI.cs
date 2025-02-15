using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public float baseHealth;
    public float maxHealth;
    public float playerHealth;
    public Text healthtext;
    public GameObject loseUI;
    public PrefabCreator prefabCreator;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = 100f;
        maxHealth = baseHealth;
        playerHealth = maxHealth;
        //playerHealth -= 50f;
        //healthtext = GetComponent<Text>();
        healthtext = GameObject.Find("HealthTxt").GetComponent<Text>();
        loseUI.SetActive(false);
        UpdateText();
    }

    // Update is called once per frame
    
    public void Damaged(float amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            StartCoroutine(gameOver());
        }
    }
    public void UpdateText()
    {
        if (healthtext != null)
        {
            healthtext.text = ("Health: " + playerHealth); //Adjusts the text in the input.
        }
    }
    private IEnumerator gameOver()
    {
        GameObject[] dragonPrefabGroup = GameObject.FindGameObjectsWithTag("Dragon");
        if (prefabCreator.spawnedPortal != null)
        {
            Destroy(prefabCreator.spawnedPortal);
            foreach (var dragon in dragonPrefabGroup)
            {
                Destroy(dragon);
            }
            prefabCreator.timeCountDown = false;

            loseUI.SetActive(true);
            yield return new WaitForSeconds(3f);
            loseUI.SetActive(false);

        }
    }
}
