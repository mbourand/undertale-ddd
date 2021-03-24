using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField] private GameObject _fadeInPrefab = null;
    [SerializeField] private GameObject _musicSource = null;

    private bool once = true;

    private IEnumerator GoToFightScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Fight Scene");
    }

    void Update()
    {
        if (Input.GetButtonDown("Confirm") && once && GameObject.Find("FadeOut").GetComponent<SpriteRenderer>().color.a == 0)
        {
            once = false;
            _musicSource.GetComponent<Animation>().Play();
            Instantiate(_fadeInPrefab, Vector3.zero, Quaternion.identity);
            StartCoroutine("GoToFightScene");
        }
    }
}
