using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDimensionCarry : MonoBehaviour
{
    int frame;
    public GameObject bulletDimensionCarry;
    Vector3 spawner = new Vector3(-4f, 3.5f, -3f);
    Vector3 spawner2 = new Vector3(4f, 3.5f, -3f);

    void Awake()
    {
        frame = 0;
    }

    void FixedUpdate()
    {
        if (frame % 5 == 0)
        {
            for (float a = 90f; a < 268f; a += (270 - 90) / 9f)
                Instantiate(bulletDimensionCarry, spawner, Quaternion.AngleAxis(a, Vector3.forward));
            for (float a = -90f; a < 88f; a += (-90 - 90) / 9f)
            {
                GameObject go = Instantiate(bulletDimensionCarry, spawner2, Quaternion.AngleAxis(a, Vector3.forward));
                go.GetComponent<BulletDimensionCarry>().rotationSpeed *= -1;
            }
        }
        frame++;
    }
}
