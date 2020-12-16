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

    void Awake()
    {
        waveSpeed = 0.035f;
        waveAmplitude = 1.0f;
        waveValue = 0.0f;
        startPosition = transform.position;
        spawnTime = System.DateTime.Now.Millisecond;
    }

    void FixedUpdate()
    {
        if (System.DateTime.Now.Millisecond > spawnTime + lifeSpan * 1000)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = new Vector3(startPosition.x + waveAmplitude * Mathf.Sin(waveValue), transform.position.y - 0.05f, transform.position.z);
        waveValue += waveSpeed;
    }
}
