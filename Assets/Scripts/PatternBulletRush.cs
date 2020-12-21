using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternBulletRush : MonoBehaviour
{
    private int frame;
    public GameObject spinningThenRushBullet;
    private int frequency;
    int toFrequency;

    void Awake()
    {
        toFrequency = 0;
        frequency = 75;
        frame = 0;
    }

    void FixedUpdate()
    {
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
            }
            if (frequency > 40)
                frequency -= 3;
            toFrequency = 0;
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
