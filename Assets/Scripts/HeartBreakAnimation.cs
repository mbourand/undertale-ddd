using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBreakAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _heartPiecePrefab = null;

    void Start()
    {
        StartCoroutine("SpawnParticles");
        StartCoroutine("LateStart");
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        GameObject.Find("Sounds/Heart Break").GetComponent<AudioSource>().Play();
    }

    IEnumerator SpawnParticles()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Sounds/Piece Spawn").GetComponent<AudioSource>().Play();
        for (int i = 0; i < 6; i++)
        {
            GameObject inst = Instantiate(_heartPiecePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            Rigidbody2D rb = inst.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(0f, 0.3f)));
        }
        Destroy(gameObject);
    }

}
