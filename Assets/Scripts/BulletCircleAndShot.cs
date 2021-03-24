using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircleAndShot : MonoBehaviour
{
    public GameObject linearBullet;
    public GameObject lazer;
    public AudioSource lazerAlertSound;
    int frame;
    public Vector2 circleCenter;
    public float radius;
    public float lifeSpan;
    public float angle;
    public float goToAngle;
    int frameSinceGoToReached;
    float offset;
    private float bufferAngle;
    private AudioSource bulletSpawnSound;

    void Awake()
    {
        angle = 0.0f;
        frame = 0;
        lifeSpan = 0f;
        circleCenter = transform.position;
        offset = Random.Range(-0.4f, 0.4f);
        bufferAngle = offset;
        lazerAlertSound = GameObject.Find("Sounds/Lazer Alert").GetComponent<AudioSource>();
    }

    private void Start()
    {
        bulletSpawnSound = GameObject.Find("Bullet Spawn Wave").GetComponent<AudioSource>();        
    }

    void FixedUpdate()
    {
        if (frame > lifeSpan * 60)
        {
            Destroy(gameObject);
            return;
        }
        transform.GetChild(0).position = new Vector3(transform.position.x + Mathf.Cos(angle) * radius, transform.position.y + Mathf.Sin(angle) * radius, transform.GetChild(0).position.z);
        transform.GetChild(1).position = new Vector3(transform.position.x + Mathf.Cos(angle + Mathf.PI) * radius, transform.position.y + Mathf.Sin(angle + Mathf.PI) * radius, transform.GetChild(0).position.z);
        if (Mathf.Abs(goToAngle - angle) > 0.05f)
        {
            offset = Random.Range(-0.4f, 0.4f);
            bufferAngle = offset;
            frameSinceGoToReached = 0;
            if (angle < Mathf.PI / 2f && goToAngle > Mathf.PI * 3 / 2f)
                angle -= 0.05f;
            else if (angle > Mathf.PI * 3 / 2f && goToAngle < Mathf.PI / 2f)
                angle += 0.05f;
            else
                angle += 0.05f * (angle < goToAngle ? 1 : -1);
            while (angle < 0)
                angle += Mathf.PI * 2;
            while (angle >= Mathf.PI * 2)
                angle -= Mathf.PI * 2;
        }
        else
        {
            if (frameSinceGoToReached > 25)
            {
                for (; bufferAngle < (Mathf.PI * 2 + offset) / 5f * ((frame % 5) + 1); bufferAngle += Mathf.PI * 2 / 16f)
                {
                    Vector3 pos = transform.GetChild(0).transform.position;
                    pos.z = pos.z + 0.01f;
                    LinearBullet bullet = Instantiate(linearBullet, pos, Quaternion.identity, transform.parent).GetComponent<LinearBullet>();
                    bullet.angle = bufferAngle;
                    bullet.moveSpeed = 0.2f + Random.Range(-0.006f, 0.006f);
                    bullet.lifeSpan = 0.8f;
                    bullet.transform.localScale = new Vector3(2f, 2f, 1);

                    pos = transform.GetChild(1).transform.position;
                    pos.z = pos.z + 0.01f;
                    bullet = Instantiate(linearBullet, pos, Quaternion.identity, transform.parent).GetComponent<LinearBullet>();
                    bullet.angle = bufferAngle;
                    bullet.moveSpeed = 0.2f + Random.Range(-0.006f, 0.006f);
                    bullet.lifeSpan = 0.8f;
                    bullet.transform.localScale = new Vector3(2f, 2f, 1);
                }
                if (frame % 5 == 0)
                {
                    offset += 0.004f;
                    bufferAngle = offset;
                    bulletSpawnSound.Play();
                }
            }
            else if (frameSinceGoToReached == 0)
            {
                for (float angle = offset; angle < Mathf.PI * 2; angle += Mathf.PI * 2 / 16f)
                {
                    GameObject lazerInstance = Instantiate(lazer, transform.GetChild(0).position, Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), transform.parent);
                    Destroy(lazerInstance, 25 / 50f);
                    lazerAlertSound.Play();
                    StartCoroutine("StopLazerSound");
                    lazerInstance = Instantiate(lazer, transform.GetChild(1).position, Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), transform.parent);
                    Destroy(lazerInstance, 25 / 50f);
                }
            }
            frameSinceGoToReached++;
        }
        frame++;
    }

    private IEnumerator StopLazerSound()
    {
        yield return new WaitForSeconds(0.5f);
        lazerAlertSound.Stop();
    }
}
