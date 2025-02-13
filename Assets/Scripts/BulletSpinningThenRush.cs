﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpinningThenRush : Bullet
{
    public int timeForOneTurn = 97;
    public int timeBeforeRushing = 200;
    public float radius = 2.07f;
    private Vector2 center;
    int frame;
    float toCenterCos = float.NaN, toCenterSin = float.NaN;
    public float baseAngle;
    private float toCenterAngle;
    bool shot;

    public GameObject linearBullet;

    private AudioSource bulletSpawnWaveSound;

    void Awake()
    {
        center = transform.position;
        transform.position = new Vector3(center.x + Mathf.Cos(frame / (float)timeForOneTurn * Mathf.PI + baseAngle) * radius, center.y + Mathf.Sin(frame / (float)timeForOneTurn * Mathf.PI + baseAngle) * radius, transform.position.z);
        frame = 0;
        shot = false;
        bulletSpawnWaveSound = GameObject.Find("Bullet Spawn Wave").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (frame < timeBeforeRushing)
        {
            float distX = center.x - transform.position.x, distY = center.y - transform.position.y;
            transform.position = new Vector3(center.x + Mathf.Cos(frame / (float)timeForOneTurn * Mathf.PI + baseAngle) * radius, center.y + Mathf.Sin(frame / (float)timeForOneTurn * Mathf.PI + baseAngle) * radius, transform.position.z);
            transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(distY, distX) + Mathf.PI / 2.0f) * Mathf.Rad2Deg, Vector3.forward);
        }
        if (frame > timeBeforeRushing + 30)
        {
            if (float.IsNaN(toCenterCos))
            {
                float distX = center.x - transform.position.x, distY = center.y - transform.position.y;
                toCenterAngle = Mathf.Atan2(distY, distX);
                toCenterCos = Mathf.Cos(toCenterAngle);
                toCenterSin = Mathf.Sin(toCenterAngle);
            }
            if (!shot && Vector2.Distance(center, new Vector3(transform.position.x, transform.position.y)) < 0.175f)
            {
                for (int i = -2; i < 2; i++)
                {
                    LinearBullet bullet = Instantiate(linearBullet, transform.position, Quaternion.identity, transform.parent).GetComponent<LinearBullet>();
                    bullet.lifeSpan = 3f;
                    bullet.moveSpeed = 0.13f;
                    bullet.angle = toCenterAngle + i * 0.17f;
                    shot = true;
                }
                bulletSpawnWaveSound.Play();
                Destroy(gameObject);
            }
            transform.position += new Vector3(toCenterCos * 0.15f, toCenterSin * 0.15f, 0);
        }
        if (frame == timeBeforeRushing + 30 + 200)
            Destroy(gameObject);
        frame++;
    }
}
