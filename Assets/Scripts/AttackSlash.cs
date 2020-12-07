using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlash : MonoBehaviour
{
    private Animator animator;

    public AudioSource hitSound;
    public Enemy enemy;
    public int damage;

    private bool started = false;

    void Start()
    {
        animator = GetComponent<Animator>() as Animator;    
    }

    private void Update()
    {
        if (animator.GetBool("Start"))
            started = true;
        if (started && !animator.GetBool("Start"))
        {
            enemy.TakeDamage(damage);
            hitSound.Play();
            started = false;
        }
    }

    public void StartAnimation()
    {
        animator.SetBool("Start", true);
    }
}
