using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoulState
{
    RED
}

public class SoulController
{
    public PlayerSettings settings;
    public SoulState state;
    public int hp;
    public GameObject obj;

    public Rigidbody2D rigidbody;

    private long invincibility_time;

    public SoulController(ref PlayerSettings settings, Rigidbody2D rb, GameObject obj)
    {
        rigidbody = rb;
        this.settings = settings;
        this.obj = obj;
        this.state = SoulState.RED;
        this.hp = settings.maxHP;
        this.invincibility_time = 0;
    }

    public void RedSoulMovement()
    {
        if (GameState.state != GameStateEnum.ATTACK)
            return;

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        obj.transform.position += dir.normalized * this.settings.moveSpeed * Time.deltaTime;
    }

    public void Update()
    {
        if (invincibility_time > 0)
            invincibility_time--;
        switch (this.state)
        {
            case SoulState.RED:
                RedSoulMovement();
                break;
            default:
                break;
        }
        if (invincibility_time == 0)
            CheckBulletCollide();
    }

    public void takeDamage(int amount)
    {
        if (amount > hp)
            hp = 0;
        else
            hp -= amount;
    }

    public void recoverHP(int amount)
    {
        if (amount + hp > settings.maxHP)
            hp = settings.maxHP;
        else
            hp += amount;
    }

    public void DamageByBullet(Bullet b)
    {
        if (invincibility_time == 0)
        {
            takeDamage(b.damage);
            invincibility_time = settings.invincibilityTime;
            GameObject.Find("/Sounds/Hurt").GetComponent<AudioSource>().Play();
        }
    }

    void CheckBulletCollide()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(obj.transform.position, 0.13f);
        foreach (Collider2D c in colliders)
        {
            if (c.CompareTag("Bullet"))
            {
                DamageByBullet(c.gameObject.GetComponent<Bullet>());
                break;
            }
        }
    }
}
