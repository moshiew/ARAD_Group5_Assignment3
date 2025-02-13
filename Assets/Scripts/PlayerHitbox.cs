using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public Transform camera;
    Vector3 hitboxPosition;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        hitboxPosition = camera.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = hitboxPosition;
    }
}
