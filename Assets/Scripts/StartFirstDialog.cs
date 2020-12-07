using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFirstDialog : MonoBehaviour
{
    public DialogManager dialogManager;

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        dialogManager.RunDialog(0);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine("LateStart");
    }
}
