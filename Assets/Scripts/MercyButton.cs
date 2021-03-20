using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MercyButton : MenuButton
{
    public DialogManager dialogManager;

    public TextMeshPro textBox;

    public AudioSource pressedSound;

    public GameObject player;
    public GameObject choice1;

    private string dialogId;
    private bool firstFrame = true;
    private bool blockButton = false;
    private bool onlyOnce = true;

    private void FixedUpdate()
    {
        if (onlyOnce && dialogManager.GetDialog("spare_text_1").started && dialogManager.GetDialog("spare_text_1").obj.GetComponent<Dialog>().finish)
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
        dialogManager.RunDialog("mercy_choice_1");
    }

    protected override void OnDepressed()
    {
        firstFrame = true;
        if (GameState.instance.state != GameStateEnum.MENU)
            return;
        dialogManager.DeleteDialog("mercy_choice_1");
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
        if (firstFrame && !Input.GetButtonDown("Confirm"))
            firstFrame = false;
        if (!firstFrame && Input.GetButtonDown("Confirm"))
        {
            pressedSound.Play();
            dialogManager.DeleteDialog("mercy_choice_1");
            dialogManager.RunDialog("spare_text_1");
            blockButton = true;
            player.GetComponent<SpriteRenderer>().enabled = false;
            GameState.instance.state = GameStateEnum.ATTACK;
        }
    }

    IEnumerator FinishButton()
    {
        yield return new WaitForSeconds(1f);
        dialogManager.DeleteDialog("spare_text_1");
        GameObject.Find("Pattern Manager").GetComponent<PatternManager>().StartPattern(Turn.GetTurn().GetPattern());
        blockButton = false;
    }
}
