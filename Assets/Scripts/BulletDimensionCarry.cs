using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDimensionCarry : MonoBehaviour
{
    private float moveSpeed = 0.07f;
    public float rotationSpeed = 17f;
    int frame = 0;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        transform.position = transform.position + (transform.right * moveSpeed);
        transform.GetChild(0).Rotate(Vector3.forward, rotationSpeed);
        if (frame > 43)
            rotationSpeed /= 1.04f;
        frame++;
    }
}
