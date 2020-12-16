using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternBulletWave : MonoBehaviour
{

    public GameObject bulletWavePrefab, linearBulletPrefab;
    private AudioSource bulletSpawnWaveSound;
    int frame;

    private void Start()
    {
        frame = 0;
        bulletSpawnWaveSound = GameObject.Find("/Sounds/Bullet Spawn Wave").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (frame >= 800)
        {
            Destroy(gameObject);
            return;
        }

        if (frame < 600 && frame % 5 == 0)
        {
            BulletWave wave = Instantiate(bulletWavePrefab, new Vector3(0, 7 - frame / 130.0f, -5), Quaternion.identity, gameObject.transform).GetComponent<BulletWave>();
            wave.waveAmplitude = 5 * (frame % 10 == 0 ? 1 : -1);
            wave.lifeSpan = 4f;
        }

        if (frame > 200 && frame % 40 == 0 && frame < 600)
        {
            float startAngle = Random.Range(0.0f, Mathf.PI * 2 / 10.0f);
            for (float baseAngle = startAngle; baseAngle < Mathf.PI * 2 + startAngle; baseAngle += Mathf.PI * 2 / 14.0f)
            {
                for (int i = -2; i < 3; i++)
                {
                    LinearBullet wave = Instantiate(linearBulletPrefab, new Vector3(0, 2.5f, -5), Quaternion.identity, gameObject.transform).GetComponent<LinearBullet>();
                    wave.angle = baseAngle + (i * 0.1f);
                    wave.moveSpeed = 0.07f + Mathf.Cos(Mathf.Min(i, -i) * -Mathf.PI / 4.0f) * 0.03f;
                    wave.lifeSpan = 4f;
                }
            }
            bulletSpawnWaveSound.Play();
        }
        frame++;
    }

    private void OnDestroy()
    {
        Turn.NextTurn();
        GameObject arena = GameObject.Find("Arena");
        arena.GetComponent<ArenaTransition>().ResetSize();
    }
}
