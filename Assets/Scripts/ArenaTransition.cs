using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTransition : MonoBehaviour
{
    private Vector3 leftScale, topScale, rightScale, bottomScale;
    public GameObject left, top, right, bottom, background;
    private PatternManager patternManager;
    private DialogManager dialogManager;
    public string dialogId;
    private float speedLeft, speedRight, speedTop, speedBottom;
    private bool start = false;
    private int frame = 0, time = 45;
    private bool startPattern = false;

    // Start is called before the first frame update
    void Start()
    {
        this.patternManager = GameObject.Find("Pattern Manager").GetComponent<PatternManager>();
        leftScale = left.transform.localScale;
        topScale = top.transform.localScale;
        rightScale = right.transform.localScale;
        bottomScale = bottom.transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (frame == time)
        {
            start = false;
            if (startPattern)
                patternManager.canStartPattern = true;
            else
            {
                GameObject.Find("Dialog Manager").GetComponent<DialogManager>().RunDialog(dialogId);
                GameState.state = GameStateEnum.MENU;
            }
            frame = 0;
        }

        if (start && frame < time)
        {
            left.transform.localScale = new Vector3(left.transform.localScale.x, left.transform.localScale.y + speedLeft, 1);
            right.transform.localScale = new Vector3(right.transform.localScale.x, right.transform.localScale.y + speedRight, 1);
            top.transform.localScale = new Vector3(top.transform.localScale.x + speedTop, top.transform.localScale.y, 1);
            bottom.transform.localScale = new Vector3(bottom.transform.localScale.x + speedBottom, bottom.transform.localScale.y, 1);

            left.GetComponent<Rigidbody2D>().MovePosition(new Vector2(top.transform.GetChild(0).position.x + 0.07f, left.transform.position.y));
            right.GetComponent<Rigidbody2D>().MovePosition(new Vector2(top.transform.GetChild(1).position.x - 0.07f, right.transform.position.y));
            top.GetComponent<Rigidbody2D>().MovePosition(new Vector2(top.transform.position.x, left.transform.GetChild(1).position.y - 0.07f));
            bottom.GetComponent<Rigidbody2D>().MovePosition(new Vector2(bottom.transform.position.x, left.transform.GetChild(2).position.y + 0.07f));

            background.transform.localScale = new Vector3(top.transform.GetChild(1).transform.position.x - top.transform.GetChild(0).transform.position.x, left.transform.GetChild(0).transform.position.y - left.transform.GetChild(2).transform.position.y, 1);
            frame++;
        }
    }

    public void GoTo(float sizeX, float sizeY)
    {
        speedTop = (topScale.x * sizeX - top.transform.localScale.x) / (float)(time);
        speedBottom = (bottomScale.x * sizeX - bottom.transform.localScale.x) / (float)(time);
        speedLeft = (leftScale.y * sizeY - left.transform.localScale.y) / (float)(time);
        speedRight = (rightScale.y * sizeY - right.transform.localScale.y) / (float)(time);
        start = true;
        startPattern = true;
        frame = 0;
    }

    public void ResetSize(string dialogId)
    {
        GoTo(1, 1);
        startPattern = false;
        this.dialogId = dialogId;
    }
}
