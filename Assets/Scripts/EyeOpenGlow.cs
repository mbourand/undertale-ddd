using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOpenGlow : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        transform.localScale = new Vector3(obj.transform.localScale.x * obj.transform.parent.localScale.x, obj.transform.localScale.y * obj.transform.parent.localScale.x, obj.transform.localScale.z * obj.transform.parent.localScale.x);
    }
}
