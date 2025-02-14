using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
-------------------------
 Scales and adjusts GameObject [HealthScalable] to represent player health appropriately
-------------------------
*/
public class UpdateHealthScalable : MonoBehaviour
{
    public PlayerUI playerUI;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = playerUI.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust it accordingly
        slider.value = playerUI.playerHealth;
    }
}
