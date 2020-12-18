using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleVfx : MonoBehaviour
{
    int frame;
    float rotateSpeed;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rotateSpeed = Random.Range(5, 10) * (Random.value < 0.5 ? -1 : 1);
        frame = 0;
    }

    void FixedUpdate()
    {
        frame++;
        transform.localScale = new Vector3(transform.localScale.x * 1.015f, transform.localScale.y * 1.015f, transform.localScale.z);
        rotateSpeed /= 1.01f;
        transform.Rotate(Vector3.forward * rotateSpeed);
        if (frame >= 200)
        {
            if (frame < 250)
            {
                spriteRenderer.color = Color.HSVToRGB(60 / 360f, Mathf.Min((frame - 200) / 50f, 1f), 1);
                this.spriteRenderer.material.SetColor("Color", spriteRenderer.color);
            }
            else if (frame < 310)
            {
                spriteRenderer.color = Color.HSVToRGB(Mathf.Max(6 / 36f - (frame - 250) / 360f, 0f), 1, 1);
                this.spriteRenderer.material.SetColor("Color", spriteRenderer.color);
            }
        }
        if (frame == 360)
            Destroy(gameObject);
    }
}
