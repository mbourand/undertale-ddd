using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleVfxSpawner : MonoBehaviour
{
    int frame;
    public int frequency = 60;
    public GameObject circleVfxPrefab;
    void Awake()
    {
        frame = frequency;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (frequency >= 60 && Turn.current_turn >= 2)
            frequency = 20;
        if (frame >= frequency)
        {
            Instantiate(circleVfxPrefab, new Vector3(0, 2.35f, 1), Quaternion.identity, gameObject.transform);
            frame = 0;
        }
        frame++;
    }
}
