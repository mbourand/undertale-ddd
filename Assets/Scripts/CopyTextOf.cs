using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CopyTextOf : MonoBehaviour
{
    public TextMeshPro source;
    private TextMeshPro dest;

    void Start()
    {
        dest = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        dest.text = source.text;
    }
}
