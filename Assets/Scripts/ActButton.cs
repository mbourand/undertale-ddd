using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActButton : MenuButton
{
    public DialogManager dialogManager;

    public TextMeshPro textBox;

    public AudioSource pressedSound;
    public AudioSource selectedSound;

    public GameObject player;
    public GameObject choice1;
    public GameObject choice2;

    private string dialogId;
    private bool firstFrame = true;
    private bool blockButton = false;
    private bool onlyOnce = true;
    bool selectedChoice = false;
    bool axisPressed = false;

    private void FixedUpdate()
    {
        if (onlyOnce && dialogManager.GetDialog((!selectedChoice ? "check_text_1" : "talk_text_1")).started && dialogManager.GetDialog((!selectedChoice ? "check_text_1" : "talk_text_1")).obj.GetComponent<Dialog>().finish)
        {
            StartCoroutine("FinishButton");
            onlyOnce = false;
        }
        if (GameState.instance.state == GameStateEnum.MENU)
            onlyOnce = true;
    }

    protected override void OnPressed()
    {
        if (GameState.instance.state != GameStateEnum.MENU || blockButton)
            return;
        base.OnPressed();
        player.transform.position = new Vector3(choice1.transform.GetChild(0).position.x, choice1.transform.GetChild(0).position.y, player.transform.position.z);
        this.dialogId = dialogManager.GetDialogOnTextBox(ref textBox);
        dialogManager.DeleteDialog(this.dialogId);
        dialogManager.RunDialog("act_choice_1");
        dialogManager.RunDialog("act_choice_2");
    }

    protected override void OnDepressed()
    {
        firstFrame = true;
        if (GameState.instance.state != GameStateEnum.MENU)
            return;
        dialogManager.DeleteDialog("act_choice_1");
        dialogManager.DeleteDialog("act_choice_2");
        dialogManager.RunDialog(this.dialogId);
    }

    protected override void OnSelected()
    {
        if (GameState.instance.state != GameStateEnum.MENU)
            return;
        base.OnSelected();
    }

    protected override void OnPressedUpdate()
    {
        if (GameState.instance.state != GameStateEnum.MENU)
            return;
       
        if (Input.GetAxis("Menu Horizontal") == 1 || Input.GetAxis("Menu Horizontal") == -1)
        {
            if (!axisPressed)
            {
                selectedChoice = !selectedChoice;
                player.transform.position = (!selectedChoice ? choice1.transform.GetChild(0).transform.position : choice2.transform.GetChild(0).transform.position);
                selectedSound.Play();
            }
            axisPressed = true;
        }
        else
            axisPressed = false;

        if (firstFrame && !Input.GetButtonDown("Confirm"))
            firstFrame = false;
        if (!firstFrame && Input.GetButtonDown("Confirm"))
        {
            pressedSound.Play();
            dialogManager.DeleteDialog("act_choice_1");
            dialogManager.DeleteDialog("act_choice_2");
            dialogManager.RunDialog((!selectedChoice ? "check_text_1" : "talk_text_1"));
            blockButton = true;
            player.GetComponent<SpriteRenderer>().enabled = false;
            GameState.instance.state = GameStateEnum.ATTACK;
        }
    }

    IEnumerator FinishButton()
    {
        yield return new WaitForSeconds(1f);
        dialogManager.DeleteDialog((!selectedChoice ? "check_text_1" : "talk_text_1"));
        selectedChoice = false;
        GameObject.Find("Pattern Manager").GetComponent<PatternManager>().StartPattern(Turn.GetTurn().GetPattern());
        blockButton = false;
    }
}
