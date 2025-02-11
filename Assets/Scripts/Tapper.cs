using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapper : MonoBehaviour
{
    public Touch tap;
    public Enemy enemy;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            enemy.Damaged();
        }
    }
}
