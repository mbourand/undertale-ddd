using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Bullet
{
    public SimpleBullet()
    {
        base.damage = 5;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
