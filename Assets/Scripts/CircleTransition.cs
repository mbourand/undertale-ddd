using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTransition : MonoBehaviour
{
    [HideInInspector]
    public bool start = false;
    float finalSize = 6f;
    float time = 100f;
    public GameObject opaque;
    SpriteRenderer opaqueRenderer;

    void Start()
    {
       transform.localScale = new Vector3(0, 0, 1);
       opaqueRenderer = opaque.GetComponent<SpriteRenderer>();
       opaqueRenderer.color = new Color(opaqueRenderer.color.r, opaqueRenderer.color.g, opaqueRenderer.color.b, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!start)
            return;
        transform.localScale = new Vector3(transform.localScale.x + (finalSize / time), transform.localScale.y + (finalSize / time), transform.localScale.z);
        if (transform.localScale.x >= finalSize)
            opaqueRenderer.color = new Color(opaqueRenderer.color.r, opaqueRenderer.color.g, opaqueRenderer.color.b, opaqueRenderer.color.a + 0.02f);
    }
}
