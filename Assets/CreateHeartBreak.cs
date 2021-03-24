using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHeartBreak : MonoBehaviour
{
    [SerializeField] private GameObject _heartBreakPrefab = null;

    void Start()
    {
        GameObject player = GameObject.Find("Player");

        Vector3 pos = new Vector3(0, 0, -0.6f);
        if (player != null)
            pos = player.transform.position;

        Instantiate(_heartBreakPrefab, pos, Quaternion.identity);
        Destroy(gameObject);
        Destroy(player);
    }
}
