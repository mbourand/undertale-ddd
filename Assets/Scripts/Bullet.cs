using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    PlayerManager player = null;

    protected void FixedUpdate()
    {
        if (player)
            player.DamageByBullet(this);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            player = collision.gameObject.GetComponent<PlayerManager>() as PlayerManager;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            player = null;
    }
}
