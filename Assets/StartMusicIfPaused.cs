using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusicIfPaused : MonoBehaviour
{
    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        AudioSource src = GameObject.Find("Fight Music Player").GetComponent<AudioSource>();
        if (!src.isPlaying)
            src.Play();
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine("LateStart");
    }
}
