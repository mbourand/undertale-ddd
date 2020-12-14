using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PatternLink
{
    public string id;
    public GameObject pattern;
    public Vector2 arenaTransitionSize;
}

public class PatternManager : MonoBehaviour
{
    public ArenaTransition arenaTransition;
    public PatternLink[] patterns;
    public bool canStartPattern;
    private int index;

    // Start is called before the first frame update
    void Awake()
    {
        canStartPattern = false;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStartPattern)
        {
            Instantiate(patterns[index].pattern);
            canStartPattern = false;
        }
    }

    public void StartPattern(int i)
    {
        arenaTransition.GoTo(patterns[i].arenaTransitionSize.x, patterns[i].arenaTransitionSize.y);
        this.index = i;
    }

    public void StartPattern(string id)
    {
        if (id == "random")
        {
            StartPattern(Random.Range(0, patterns.Length));
            return;
        }

        for (int i = 0; i < patterns.Length; i++)
            if (patterns[i].id == id)
            {
                arenaTransition.GoTo(patterns[i].arenaTransitionSize.x, patterns[i].arenaTransitionSize.y);
                this.index = i;
            }
    }
}
