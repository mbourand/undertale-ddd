using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimation : MonoBehaviour
{
    public GameObject eyeParticle;
    float time = 60.0f;
    int frame = 0;
    public bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 0, 1);
        transform.GetComponentInParent<SpriteGlow.SpriteGlowEffect>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!start)
            return;
        if (frame < time)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + (1.08f / time), transform.localScale.z);
            frame++;
        }
        if (frame == time)
        {
            Instantiate(eyeParticle);
            frame = 0;
            transform.GetComponentInParent<SpriteGlow.SpriteGlowEffect>().enabled = true;
            Destroy(GameObject.Find("Oeil noir"));
            start = false;
        }
    }
}
