using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSinusoidalBulletHell : MonoBehaviour
{
    private int frame;
    public GameObject sinusoidalBullet;
    public GameObject linearBullet;
    private AudioSource bulletShotSound;

    float angle;
    float circleAngle;

    void Awake()
    {
        angle = 0.03f;
        frame = 0;
        bulletShotSound = GameObject.Find("Bullet Spawn Wave").GetComponent<AudioSource>();
        circleAngle = Random.Range(-0.2f, 0.2f);
    }

    void FixedUpdate()
    {
        /*
        ** Vague 1 : frame 50 à 200.
        ** vague 2 : frame x à y
        */
        if ((frame > 30 && frame < 500) || (frame > 550 && frame < 1050))
        {
            for (; angle < Mathf.PI * 2 / 5f * ((frame % 5) + 1); angle += Mathf.PI * 2 / 15f)
            {
                SinusoidalBullet bullet = Instantiate(sinusoidalBullet, new Vector3(0, 2.8f, -2), Quaternion.identity, transform).GetComponent<SinusoidalBullet>();
                bullet.offsetAngle = angle;
                bullet.waveSpeed = 0.14f;
                bullet.waveAmplitude = 1f;
                bullet.lifeSpan = 1.5f;
                if (frame > 550 && frame < 1050)
                {
                    SinusoidalBullet bullet2 = Instantiate(sinusoidalBullet, new Vector3(0, 2.8f, -2), Quaternion.identity, transform).GetComponent<SinusoidalBullet>();
                    bullet2.offsetAngle = angle;
                    bullet2.waveSpeed = 0.14f;
                    bullet2.waveAmplitude = 1f;
                    bullet2.lifeSpan = 1.5f;
                    bullet2.inverted = true;
                }
            }
            for (; circleAngle < Mathf.PI * 2 / 40f * ((frame % 40) + 1); circleAngle += 0.4f)
            {
                if (Random.value < 0.435f)
                {
                    LinearBullet lb = Instantiate(linearBullet, new Vector3(0, 2.8f, -2.01f), Quaternion.identity, transform).GetComponent<LinearBullet>();
                    lb.angle = circleAngle + Random.Range(-Mathf.PI / 6f, Mathf.PI / 6f);
                    lb.moveSpeed = 0.05f + Random.Range(0, 0.02f);
                    lb.lifeSpan = 4f;
                }
                if (Random.value < 1 / 5f)
                   continue;
                LinearBullet bullet = Instantiate(linearBullet, new Vector3(0, 2.4f, -2), Quaternion.identity, transform).GetComponent<LinearBullet>();
                bullet.angle = circleAngle;
                bullet.moveSpeed = 0.05f;
                bullet.lifeSpan = 4f;
            }
            if (frame % 40 == 0)
            {
                circleAngle = Random.Range(-0.2f, 0.2f);
                bulletShotSound.Play();
            }
            if (frame % 5 == 0)
                angle = 0.03f;
        }
        if (frame == 1175)
            Destroy(gameObject);
        frame++;
    }

    private void OnDestroy()
    {
        Turn.NextTurn();
        GameObject arena = GameObject.Find("Arena");
        arena.GetComponent<ArenaTransition>().ResetSize();
    }
}
