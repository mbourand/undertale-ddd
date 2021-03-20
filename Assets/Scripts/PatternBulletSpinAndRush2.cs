using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternBulletSpinAndRush2 : MonoBehaviour
{
    private int frame;
    public GameObject spinningThenRushBullet, sinusoidalBullet, linearBullet;
    public Sprite borderSpinningThenRush;
    private int frequency;
    int toFrequency;

    void Awake()
    {
        toFrequency = 0;
        frequency = 38;
        frame = 0;
    }

    void FixedUpdate()
    {
        if (frame % 5 == 0 && frame < 1000)
        {
            SinusoidalBullet bullet = Instantiate(sinusoidalBullet, new Vector3(6, -1.664f, -2), Quaternion.AngleAxis(180, Vector3.forward), transform).GetComponent<SinusoidalBullet>();
            bullet.lifeSpan = 1.04f;
            bullet.waveSpeed = 0.2f;
            bullet.offsetAngle = Mathf.PI;
            bullet.waveAmplitude = 1.9f;
            bullet.waveLength = 0.5f;

            bullet = Instantiate(sinusoidalBullet, new Vector3(-6, -1.664f, -2), Quaternion.identity, transform).GetComponent<SinusoidalBullet>();
            bullet.lifeSpan = 1.04f;
            bullet.waveSpeed = 0.2f;
            bullet.offsetAngle = 0;
            bullet.waveAmplitude = 1.9f;
            bullet.waveLength = 0.5f;
        }

        if (toFrequency == frequency && frame < 800)
        {
            float baseAngle = Random.Range(0, 2 * Mathf.PI);
            int timeForOneTurn = Random.Range(30, 60);
            for (int i = 0; i < 4; i++)
            {
                BulletSpinningThenRush bullet = Instantiate(spinningThenRushBullet, new Vector3(0, -1.664f, -2), Quaternion.identity, transform).GetComponent<BulletSpinningThenRush>();
                bullet.baseAngle = Mathf.PI * i / 2.0f + baseAngle;
                bullet.timeForOneTurn = timeForOneTurn;
                bullet.timeBeforeRushing = 148;
                bullet.radius = 3f;
                bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = borderSpinningThenRush;
            }
            toFrequency = 0;
        }

        if (frame > 55 && frame % 5 == 0 && frame < 1055)
        {
            for (float angle = -Mathf.PI / 2.5f; angle <= Mathf.PI / 2.5f; angle += 2 * (Mathf.PI / 2.5f) / 5f)
            {
                LinearBullet bullet = Instantiate(linearBullet, new Vector3(-6, -1.664f, -2), Quaternion.identity, transform).GetComponent<LinearBullet>();
                bullet.angle = angle + Mathf.PI;
                bullet.lifeSpan = 2f;
                bullet.moveSpeed = 0.15f;
                bullet.transform.localScale = new Vector3(2f, 2f, 1);
            }
            for (float angle = -Mathf.PI / 2.5f; angle <= Mathf.PI / 2.5f; angle += 2 * (Mathf.PI / 2.5f) / 5f)
            {
                LinearBullet bullet = Instantiate(linearBullet, new Vector3(6, -1.664f, -2), Quaternion.identity, transform).GetComponent<LinearBullet>();
                bullet.angle = angle;
                bullet.lifeSpan = 2f;
                bullet.moveSpeed = 0.15f;
                bullet.transform.localScale = new Vector3(2f, 2f, 1);
            }
        }

        if (frame == 1100)
            Destroy(gameObject);
        frame++;
        toFrequency++;
    }
    private void OnDestroy()
    {
        Turn.NextTurn();
        GameObject arena = GameObject.Find("Arena");
        if (arena && arena.GetComponent<ArenaTransition>())
            arena.GetComponent<ArenaTransition>().ResetSize();
    }
}