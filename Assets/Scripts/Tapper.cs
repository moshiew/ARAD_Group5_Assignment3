using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tapper : MonoBehaviour
{
    public Touch tap;
    public Enemy enemy;
    public GameObject sensor;
    RaycastHit hit;
    bool isTapped = false;
    void Start()
    {
        sensor = gameObject.GetComponent<GameObject>();
        sensor = GameObject.Find("TouchSensor");
        isTapped = false;
    }
    void Update()
    {
        CheckForEnemies();
        if (Input.touchCount > 0 && isTapped == false)
        {
            isTapped = true;
            Debug.Log(Input.touchCount);
            Touch tap = Input.GetTouch(0);
            Debug.Log(tap.position);
            sensor.transform.position = Camera.main.ScreenToViewportPoint(tap.position);
            Ray lineOfSight = Camera.main.ScreenPointToRay(tap.position);
            RaycastHit hit;
            if (enemy != null)
            {
                Debug.Log(Physics.Raycast(sensor.transform.position, enemy.transform.position));
                Debug.DrawRay(sensor.transform.position, enemy.transform.position);
                if (Physics.Raycast(lineOfSight, out hit))
                {
                    Debug.Log(hit);
                    enemy.Damaged();
                }
            }
        }
        if (Input.touchCount <= 0)
        {
            isTapped = false;
        }
    }
    void CheckForEnemies()
    {
        if (FindAnyObjectByType<Enemy>() != null && enemy == null)
        {
            Debug.Log("Enemy found.");
            enemy = FindAnyObjectByType<Enemy>();

        }
    }
}
