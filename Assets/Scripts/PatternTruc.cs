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
            bullet.goToAngle = ChooseAngle();
            bullet.lifeSpan = 300f;
        }
        if (frame % 200 == 0)
            bullet.goToAngle = ChooseAngle();
        if (frame == 1200)
            Destroy(bullet);
        if (frame == 1300)
            Destroy(gameObject);
        frame++;
    }

    float ChooseAngle()
    {
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                return Random.Range(0, Mathf.PI / 4f);
            case 1:
                return Random.Range(3 * Mathf.PI / 4f, Mathf.PI);
            case 2:
                return Random.Range(Mathf.PI, 5 * Mathf.PI / 4f);
            case 3:
                return Random.Range(7 * Mathf.PI / 4f, Mathf.PI * 2);
        }
        return Mathf.PI;
    }

    private void OnDestroy()
    {
        Turn.NextTurn();
        GameObject arena = GameObject.Find("Arena");
        if (arena && arena.GetComponent<ArenaTransition>())
            arena.GetComponent<ArenaTransition>().ResetSize();
    }
}
