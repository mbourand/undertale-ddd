using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPieceRotation : MonoBehaviour
{
    private int _direction;

    void Start()
    {
        StartCoroutine("StartFadeOut");
        Destroy(gameObject, 5f);
        _direction = (Random.value < 0.5f ? -1 : 1);
    }

    IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("FadeOut").GetComponent<Animation>().Play();
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Play();
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, 20 * _direction));
    }
}
