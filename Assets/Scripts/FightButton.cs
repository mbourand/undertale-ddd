﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightButton : MenuButton
{
    public DialogManager dialogManager;

    public TextMeshPro textBox;

    public GameObject attackPrefab;

    public AudioSource pressedSound;
    public AudioSource attackSound;

    public AttackSlash attackSlash;

    private string dialogId;
    private bool firstFrame = true;

    protected override void OnPressed()
    {
        if (GameState.state != GameStateEnum.MENU)
            return;
        base.OnPressed();
        this.dialogId = dialogManager.GetDialogOnTextBox(ref textBox);
        dialogManager.DeleteDialog(this.dialogId);
        dialogManager.RunDialog("fight_choice_1");
    }

    protected override void OnDepressed()
    {
        firstFrame = true;
        if (GameState.state != GameStateEnum.MENU)
            return;
        dialogManager.DeleteDialog("fight_choice_1");
        dialogManager.RunDialog(this.dialogId);
    }

    protected override void OnSelected()
    {
        if (GameState.state != GameStateEnum.MENU)
            return;
        base.OnSelected();
    }

    protected override void OnPressedUpdate()
    {
        if (GameState.state != GameStateEnum.MENU)
            return;
        if (firstFrame && !Input.GetKeyDown(KeyCode.Return))
            firstFrame = false;
        if (!firstFrame && Input.GetKeyDown(KeyCode.Return))
        {
            pressedSound.Play();
            dialogManager.DeleteDialog("fight_choice_1");
            GameObject attack = Instantiate(attackPrefab);
            AttackCursor cursor = attack.GetComponentInChildren<AttackCursor>() as AttackCursor;
            cursor.attackSound = attackSound;
            cursor.attackSlash = this.attackSlash;
            GameState.state = GameStateEnum.ATTACK;
        }
    }
}
