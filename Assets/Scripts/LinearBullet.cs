using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearBullet : Bullet
{
    public float lifeSpan;
    public float moveSpeed;
    public float angle;
    int frame;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        frame = 0;
        angle = 0f;
        moveSpeed = 0f;
        lifeSpan = 0f;
    }

    void FixedUpdate()
    {
        if (frame > lifeSpan * 60)
        {
            Destroy(gameObject);
            return;
        }
        rb.MovePosition(new Vector2(transform.position.x + Mathf.Cos(angle) * moveSpeed, transform.position.y + Mathf.Sin(angle) * moveSpeed));
        transform.rotation = Quaternion.AngleAxis((angle + Mathf.PI / 2.0f) * Mathf.Rad2Deg, Vector3.forward);
        frame++;
    }
}
