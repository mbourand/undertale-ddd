using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSpinAndShot : MonoBehaviour
{
    int frame;

    public GameObject bulletSpinAndShot;
    public GameObject linearBullet;

    void Awake()
    {
        frame = 0;
    }
    
    void FixedUpdate()
    {
        if (frame == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                BulletSpinAndShot bullet = Instantiate(bulletSpinAndShot, new Vector3(0, -1.664f, -2), Quaternion.identity, transform).GetComponent<BulletSpinAndShot>();
                bullet.timeForOneTurn = 180;
                bullet.lifeSpan = 17f;
                bullet.radius = 3.5f;
                bullet.baseAngle = i * Mathf.PI / 2f;
                bullet.timeBeforePhase2 = 10 * 60;
            }
        }
        if (frame > 100 && frame % 40 == 0 && frame < 12 * 60)
        {
            float left = -2.3f;
            float right = 2.3f;
            float top = 0.5f;
            float bottom = -4f;

            Vector3 fixedCoord = new Vector3(0, 0, -2);
            if (Random.value < 0.5f)
            {
                fixedCoord.y = Random.Range(bottom, top);
                fixedCoord.x = Random.value < 0.5f ? left : right;
            }
            else
            {
                fixedCoord.x = Random.Range(left, right);
                fixedCoord.y = Random.value < 0.5f ? top : bottom;
            }
            LinearBullet bullet = Instantiate(linearBullet, fixedCoord, Quaternion.identity, transform).GetComponent<LinearBullet>();
            float distY = -1.664f - bullet.transform.position.y;
            float distX = 0 - bullet.transform.position.x;
            bullet.transform.localScale = new Vector3(0.75f, 0.75f, 1);
            bullet.angle = Mathf.Atan2(distY, distX);
            bullet.moveSpeed = 0.015f;
            bullet.lifeSpan = 7f;
        }
        if (frame == 1300)
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
