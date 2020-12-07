using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerSettings settings;
    private SoulController soulController;

    void Start()
    {
        soulController = new SoulController(ref settings, GetComponent<Rigidbody2D>(), gameObject);
    }

    void FixedUpdate()
    {
        soulController.Update();
    }

    public void DamageByBullet(Bullet b)
    {
        soulController.DamageByBullet(b);
    }

    public int GetHP()
    {
        return soulController.hp;
    }

    public int GetMaxHP()
    {
        return settings.maxHP;
    }
}
