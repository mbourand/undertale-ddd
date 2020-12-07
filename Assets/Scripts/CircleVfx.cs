using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleVfx : MonoBehaviour
{
    int frame;
    float rotateSpeed;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rotateSpeed = Random.Range(5, 10) * (Random.value < 0.5 ? -1 : 1);
        frame = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        frame++;
        transform.localScale = new Vector3(transform.localScale.x * 1.015f, transform.localScale.y * 1.015f, transform.localScale.z);
        rotateSpeed /= 1.01f;
        transform.Rotate(Vector3.forward * rotateSpeed);
        if (frame >= 200)
            spriteRenderer.color = new Color(1, Mathf.Max(1 - (frame - 200) / 125.0f, 0), Mathf.Max(1 - (frame - 200) / 75.5f, 0));
        if (frame == 450)
            Destroy(gameObject);
    }
}
