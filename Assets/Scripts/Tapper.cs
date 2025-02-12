using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tapper : MonoBehaviour
{
    public Touch tap;
    public Enemy enemy;
    public GameObject sensor;
    RaycastHit hit;
    void Start()
    {
        sensor = gameObject.GetComponent<GameObject>();
        sensor = GameObject.Find("TouchSensor");
    }
    void Update()
    {
        CheckForEnemies();
        if (Input.touchCount > 0)
        {
            Debug.Log(Input.touchCount);
            Touch tap = Input.GetTouch(0);
            Debug.Log(tap.position);
            sensor.transform.position = Camera.main.ScreenToViewportPoint(tap.position);
            if (enemy != null)
            {
                Debug.Log(Physics.Raycast(sensor.transform.position, enemy.transform.position));
                if (Physics.Raycast(sensor.transform.position, enemy.transform.position))
                {
                    enemy.Damaged();
                }
            }
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
