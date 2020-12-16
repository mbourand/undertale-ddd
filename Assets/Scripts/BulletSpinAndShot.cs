using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpinAndShot : Bullet
{
    public GameObject linearBulletDieAtPoint;
    public GameObject linearBullet;
    int frame;
    public float lifeSpan;
    public float radius;
    public float timeForOneTurn;
    public float baseAngle;
    Vector3 center;
    float angleAtPhase2;
    int phase2Frame;
    public int timeBeforePhase2;

    private AudioSource bulletShotSound;

    void Awake()
    {
        frame = 0;
        phase2Frame = 0;
        /*lifeSpan = 0f;
        radius = 0f;*/
        center = transform.position;
        /*timeForOneTurn = 180f;
        baseAngle = 0;*/
        bulletShotSound = GameObject.Find("Bullet Spawn Wave").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (frame > lifeSpan * 60)
            Destroy(gameObject);
        if (frame < timeBeforePhase2)
        {
            transform.position = new Vector3(center.x + Mathf.Cos(frame / (float)timeForOneTurn * Mathf.PI + baseAngle) * radius, center.y + Mathf.Sin(frame / (float)timeForOneTurn * Mathf.PI + baseAngle) * radius, transform.position.z);
            float distX = center.x - transform.position.x, distY = center.y - transform.position.y;
            transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(distY, distX) + Mathf.PI / 2.0f) * Mathf.Rad2Deg, Vector3.forward);
            if (frame > 75 && frame % 5 == 0)
                ShootBulletDie();
        }
        else if (frame >= timeBeforePhase2 + 60)
        {
            if (frame == timeBeforePhase2 + 60)
                angleAtPhase2 = Mathf.Atan2(center.y - transform.position.y, center.x - transform.position.x);
            transform.position = new Vector3(center.x + Mathf.Cos((frame - timeBeforePhase2 - 60 - phase2Frame) / (float)timeForOneTurn * Mathf.PI + angleAtPhase2) * radius,
                                             center.y + Mathf.Sin((frame - timeBeforePhase2 - 60 - phase2Frame) / (float)timeForOneTurn * Mathf.PI + angleAtPhase2) * radius,
                                             transform.position.z);
            float distX = center.x - transform.position.x, distY = center.y - transform.position.y;
            transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(distY, distX) + Mathf.PI / 2.0f) * Mathf.Rad2Deg, Vector3.forward);
            radius += 0.05f;
            if (frame % 5 == 0 && frame >= timeBeforePhase2 + 100)
            {
                ShootBullet();
                if (frame % 5 == 0)
                    bulletShotSound.Play();
            }
            phase2Frame += 2;
        }
        frame++;
    }

    public void ShootBulletDie()
    {
        float distX = center.x - transform.position.x, distY = center.y - transform.position.y;
        LinearBulletDieAtPoint bullet = Instantiate(linearBulletDieAtPoint, transform.position, Quaternion.identity).GetComponent<LinearBulletDieAtPoint>();
        bullet.angle = Mathf.Atan2(distY, distX);
        bullet.moveSpeed = 0.067f;
        bullet.lifeSpan = 4f;
        bullet.dieAt = center;
    }

    public void ShootBullet()
    {
        float distX = center.x - transform.position.x, distY = center.y - transform.position.y;
        LinearBulletDieAtPoint bullet = Instantiate(linearBulletDieAtPoint, transform.position, Quaternion.identity).GetComponent<LinearBulletDieAtPoint>();
        bullet.angle = Mathf.Atan2(distY, distX);
        bullet.moveSpeed = 0.1f;
        bullet.lifeSpan = 6f;
        bullet.dieAt = center;

        Vector3 posBetween = new Vector3(center.x + Mathf.Cos(Mathf.Atan2(distY, distX) + Mathf.PI / 4f) * radius, center.y + Mathf.Sin(Mathf.Atan2(distY, distX) + Mathf.PI / 4f) * radius, transform.position.z);
        distX = center.x - posBetween.x;
        distY = center.y - posBetween.y;
        LinearBulletDieAtPoint bullet2 = Instantiate(linearBulletDieAtPoint, posBetween, Quaternion.identity).GetComponent<LinearBulletDieAtPoint>();
        bullet2.angle = Mathf.Atan2(distY, distX);
        bullet2.moveSpeed = 0.1f;
        bullet2.lifeSpan = 6f;
        bullet2.dieAt = center;
    }
}
