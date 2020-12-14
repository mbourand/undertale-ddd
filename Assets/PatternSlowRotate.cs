using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSlowRotate : MonoBehaviour
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

        frame++;
    }

    private void OnDestroy()
    {
        Turn.NextTurn();
        GameObject arena = GameObject.Find("Arena");
        arena.GetComponent<ArenaTransition>().ResetSize();
    }
}
