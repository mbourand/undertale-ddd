using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCursor : MonoBehaviour
{
    private int dir;
    public float moveSpeed;
    private bool stopMove;

    private Animator animator;
    public AudioSource attackSound;

    public AttackSlash attackSlash;

    void Start()
    {
        this.stopMove = false;
        this.dir = (Random.value < 0.5 ? 1 : -1);
        transform.position = new Vector3(transform.position.x * this.dir, transform.position.y, transform.position.z);
        this.animator = GetComponent<Animator>() as Animator;
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Confirm") || ((dir == -1 && gameObject.transform.parent.position.x - transform.position.x < -7.8f) || (dir == 1 && gameObject.transform.parent.position.x - transform.position.x > 7.8f))) && !this.stopMove)
        {
            this.stopMove = true;
            this.animator.SetBool("Enter Pressed", true);
            int minDamage = 600, maxDamage = 1150;
            float distance = (gameObject.transform.parent.position.x - transform.position.x) / 2f;
            attackSlash.damage = Mathf.RoundToInt((maxDamage - minDamage) * Mathf.Exp(-distance * distance) + minDamage);
            attackSlash.StartAnimation();
            attackSound.Play();
        }
    }

    void FixedUpdate()
    {
        if (!this.stopMove)
            transform.position += Vector3.right * -this.dir * this.moveSpeed;
    }
}
