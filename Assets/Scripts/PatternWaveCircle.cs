using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternWaveCircle : MonoBehaviour
{
    int frame;
    public GameObject bulletWavePrefab, linearBullet;
    float angle, angle2, offset;
    AudioSource bulletSpawnSound;

    void Awake()
    {
        offset = 0;
        angle = offset;
        angle2 = offset;
        bulletSpawnSound = GameObject.Find("Bullet Spawn Wave").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (frame < 1000 && frame % 2 == 0)
        {
            BulletWave wave = Instantiate(bulletWavePrefab, new Vector3(0, 6.7f, -5), Quaternion.identity, gameObject.transform).GetComponent<BulletWave>();
            wave.waveAmplitude = 7 * (frame % 4 == 0 ? 1 : -1);
            wave.waveSpeed = 0.08f;
            wave.moveSpeed = -0.08f;
            wave.lifeSpan = 2.8f;
            wave.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        if (frame > 100 && frame < 1000)
        {
            float time = 15;
            for (; angle < (Mathf.PI * 2 + offset) / time * ((frame % time) + 1); angle += Mathf.PI * 2 / 8f, angle2 -= Mathf.PI * 2 / 8f)
            {
                for (int i = 0; i < 2; i++)
                {
                    LinearBullet bullet = Instantiate(linearBullet, new Vector3(3f * (i == 0 ? 1 : -1), 2.5f, -5), Quaternion.identity, transform).GetComponent<LinearBullet>();
                    bullet.angle = (i == 0 ? angle : angle2);
                    bullet.moveSpeed = 0.046f;
                    bullet.lifeSpan = 5f;
                    bullet.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
            }
            if (frame % (int)time == 0)
            {
                offset += 0.3f;
                angle = offset;
                angle2 = -offset;
                bulletSpawnSound.Play();
            }
        }
        if (frame == 1200)
            Destroy(gameObject);
        frame++;
    }

    private void OnDestroy()
    {
        Turn.NextTurn();
        GameObject arena = GameObject.Find("Arena");
        if (arena && arena.GetComponent<ArenaTransition>())
            arena.GetComponent<ArenaTransition>().ResetSize();
    }
}
