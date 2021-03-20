using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButton : MenuButton
{
    public PlayerManager player;
    public GameObject choices;
    public DialogManager dialogManager;
    bool firstFrame = true;
    int selectedChoice = 0;
    bool blockButton = false;
    bool onlyOnce = true;
    AudioSource healSound = null;
    public MenuManager menuManager;

    protected override void OnPressed()
    {
        if (player.items.Count == 0)
        {
            menuManager.SetButtonPressed(2, false);
            return;
        }
        if (GameState.instance.state != GameStateEnum.MENU || blockButton)
            return;
        base.OnPressed();
        player.transform.position = new Vector3(choices.transform.GetChild(0).GetChild(0).position.x, choices.transform.GetChild(0).GetChild(0).position.y, player.transform.position.z);
        InitChoices();
    }

    private void FixedUpdate()
    {
        if (healSound == null)
            healSound = GameObject.Find("Heal").GetComponent<AudioSource>();
        if (onlyOnce && player.items.Count > 0 && dialogManager.GetDialog(player.items[selectedChoice].useDialogId).started && dialogManager.GetDialog(player.items[selectedChoice].useDialogId).obj.GetComponent<Dialog>().finish)
        {
            StartCoroutine("FinishButton");
            onlyOnce = false;
        }
        if (GameState.instance.state == GameStateEnum.MENU)
            onlyOnce = true;
    }

    protected override void OnPressedUpdate()
    {
        if (GameState.instance.state != GameStateEnum.MENU || blockButton)
            return;
        if (firstFrame)
        {
            firstFrame = false;
            return;
        }
        base.OnPressedUpdate();
        if (Input.GetButtonDown("Confirm"))
        {
            player.Heal(player.items[selectedChoice].heal);
            DestroyChoices();
            dialogManager.RunDialog(player.items[selectedChoice].useDialogId);
            blockButton = true;
            player.GetComponent<SpriteRenderer>().enabled = false;
            GameState.instance.state = GameStateEnum.ATTACK;
            healSound.Play();
        }
    }

    protected override void OnDepressed()
    {
        firstFrame = true;
        if (GameState.instance.state != GameStateEnum.MENU || blockButton)
            return;
        base.OnPressed();
        DestroyChoices();
        dialogManager.RunDialog(Turn.GetTurn().GetDialog());
    }

    IEnumerator FinishButton()
    {
        yield return new WaitForSeconds(1f);
        dialogManager.DeleteDialog(player.items[selectedChoice].useDialogId);
        player.items.RemoveAt(selectedChoice);
        selectedChoice = 0;
        GameObject.Find("Pattern Manager").GetComponent<PatternManager>().StartPattern(Turn.GetTurn().GetPattern());
        blockButton = false;
    }

    private void InitChoices()
    {
        for (int i = 0; i < Mathf.Min(player.items.Count, 4); i++)
        {
            TextMeshPro textBox = choices.transform.GetChild(0).GetChild(1).GetComponent<TextMeshPro>();
            string dialogId = dialogManager.GetDialogOnTextBox(ref textBox);
            dialogManager.DeleteDialog(dialogId);
            dialogManager.GetDialog(dialogId).textMeshPro = textBox;
            dialogManager.RunDialog(player.items[i].dialogId);
        }
    }

    private void DestroyChoices()
    {
        for (int i = 0; i < Mathf.Max(player.items.Count, 4); i++)
        {
            TextMeshPro textBox = choices.transform.GetChild(0).GetChild(1).GetComponent<TextMeshPro>();
            string dialogId = dialogManager.GetDialogOnTextBox(ref textBox);
            dialogManager.DeleteDialog(dialogId);
        }
    }

}
