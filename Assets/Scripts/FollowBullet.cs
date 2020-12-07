using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : Bullet
{
    private GameObject target;

    public float moveSpeed;

    public FollowBullet()
    {
        this.moveSpeed = 0.02f;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 targetPos = target.transform.position;
        float angle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        transform.position += direction * moveSpeed;
    }
}
