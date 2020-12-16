﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalBullet : Bullet
{
    float waveValue;
    public float waveAmplitude;
    public float waveSpeed;
    Vector3 startPosition;
    public float lifeSpan;
    public float offsetAngle;
    public bool inverted;
    int frame;
    public Rigidbody2D rb;

    void Awake()
    {
        offsetAngle = 0;
        waveSpeed = 0.035f;
        waveAmplitude = 1.0f;
        waveValue = 0.0f;
        startPosition = transform.position;
        inverted = false;
        frame = 0;
    }

    void FixedUpdate()
    {
        if (frame >= lifeSpan * 60)
        {
            Destroy(gameObject);
            return;
        }

        float fixedSpeed = Time.deltaTime * 50;
        float curve = Mathf.Sin(waveValue * 0.5f) * (inverted ? -1 : 1);
        rb.MovePosition(new Vector2(startPosition.x + Mathf.Cos(Mathf.Atan2(curve, waveValue) + offsetAngle) * Mathf.Sqrt(waveValue * waveValue + curve * curve) * waveAmplitude, startPosition.y + Mathf.Sin(Mathf.Atan2(curve, waveValue) + offsetAngle) * Mathf.Sqrt(waveValue * waveValue + curve * curve) * waveAmplitude));
        waveValue += waveSpeed;
        frame++;
    }
}
