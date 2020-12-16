using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearBulletDieAtPoint : LinearBullet
{
    public Vector2 dieAt;

    void Update()
    {
        if (Vector2.Distance(transform.position, dieAt) < base.moveSpeed)
            Destroy(gameObject);
    }
}
