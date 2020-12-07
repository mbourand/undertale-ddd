using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject damageAnimationPrefab;

    int hp;
    public int maxHp;

    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        
    }

    void SetHp(int i)
    {
        if (i < 0)
            this.hp = 0;
        else
            this.hp = i;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        GameObject instance = Instantiate(damageAnimationPrefab);
        instance.transform.position = new Vector3(-2, 3, -1);
        EnemyHealthbar healthBar = instance.GetComponentInChildren<EnemyHealthbar>() as EnemyHealthbar;
        healthBar.maxHp = maxHp;
        healthBar.from = hp;
        SetHp(hp - damage);
        healthBar.to = hp;
        healthBar.start = true;
    }
}
