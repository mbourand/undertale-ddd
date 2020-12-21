using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    public string dialogId;
    public string useDialogId;
    public uint heal;
}

public class PlayerManager : MonoBehaviour
{
    public PlayerSettings settings;
    private SoulController soulController;
    public List<Item> items;

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

    public void Heal(uint amount)
    {
        soulController.hp = (int)Mathf.Min(GetHP() + amount, GetMaxHP());
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
