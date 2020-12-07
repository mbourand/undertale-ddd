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
        if (frame >= frequency)
        {
            Instantiate(circleVfxPrefab, new Vector3(0, 2.35f, 2), Quaternion.identity, gameObject.transform);
            frame = 0;
        }
        frame++;
    }
}
