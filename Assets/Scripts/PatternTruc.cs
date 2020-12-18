using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTruc : MonoBehaviour
{
    int frame;
    public GameObject bulletCircleAndShot;
    BulletCircleAndShot bullet;

    void Awake()
    {
        frame = 0;
    }

    void FixedUpdate()
    {
        if (frame == 0)
        {
            bullet = Instantiate(bulletCircleAndShot, new Vector3(0, 2.4f, -4), Quaternion.identity, transform).GetComponent<BulletCircleAndShot>();
            bullet.radius = 2f;
            bullet.goToAngle = Mathf.PI / 2 + Random.Range(0, Mathf.PI * 1.5f);
            bullet.lifeSpan = 300f;
        }
        if (frame % 200 == 0)
            bullet.goToAngle = Random.Range(0, Mathf.PI * 2);
        if (frame == 1200)
            Destroy(bullet);
        if (frame == 1300)
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
