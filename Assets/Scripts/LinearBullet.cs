using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearBullet : Bullet
{
    public float lifeSpan;
    long spawnTime;
    public float moveSpeed;
    public float angle;

    // Start is called before the first frame update
    void Awake()
    {
        angle = 0.0f;
        spawnTime = System.DateTime.Now.Millisecond;
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();
        if (System.DateTime.Now.Millisecond > spawnTime + lifeSpan * 1000)
        {
            Destroy(gameObject);
            return;
        }
        transform.position += new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized * moveSpeed;
    }
}
