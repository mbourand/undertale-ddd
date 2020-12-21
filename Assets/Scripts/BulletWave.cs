using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWave : Bullet
{
    float waveValue;
    public float waveAmplitude;
    public float waveSpeed;
    Vector3 startPosition;
    public float lifeSpan;
    public long spawnTime;
    public float moveSpeed;
    int frame;

    void Awake()
    {
        frame = 0;
        waveSpeed = 0.035f;
        waveAmplitude = 1.0f;
        waveValue = 0.0f;
        startPosition = transform.position;
        spawnTime = System.DateTime.Now.Millisecond;
        moveSpeed = -0.05f;
    }

    void FixedUpdate()
    {
        if (frame++ > lifeSpan * 60)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = new Vector3(startPosition.x + waveAmplitude * Mathf.Sin(waveValue), transform.position.y + moveSpeed, transform.position.z);
        waveValue += waveSpeed;
    }
}
