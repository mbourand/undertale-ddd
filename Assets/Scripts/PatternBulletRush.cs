using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternBulletRush : MonoBehaviour
{
    private int frame;
    public GameObject spinningThenRushBullet;
    private int frequency;

    void Awake()
    {
        frequency = 100;
        frame = 0;
    }

    void FixedUpdate()
    {
        if (frame % frequency == 0 && frame < 800)
        {
            float baseAngle = Random.Range(0, 2 * Mathf.PI);
            int timeForOneTurn = Random.Range(30, 60);
            for (int i = 0; i < 4; i++)
            {
                BulletSpinningThenRush bullet = Instantiate(spinningThenRushBullet, new Vector3(0, -1.664f, -2), Quaternion.identity).GetComponent<BulletSpinningThenRush>();
                bullet.baseAngle = Mathf.PI * i / 2.0f + baseAngle;
                bullet.timeForOneTurn = timeForOneTurn;
                bullet.timeBeforeRushing = 148;
            }
            if (frequency > 50)
                frequency -= 3;
        }
        if (frame == 1300)
            Destroy(this);
        frame++;
    }
    private void OnDestroy()
    {
        Turn.NextTurn();
        GameObject arena = GameObject.Find("Arena");
        arena.GetComponent<ArenaTransition>().ResetSize();
    }
}
