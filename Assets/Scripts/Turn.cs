using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TurnData
{
    string ui_dialog_id;
    string pattern_id;

    public TurnData(string ui_dialog_id, string pattern_id)
    {
        this.ui_dialog_id = ui_dialog_id;
        this.pattern_id = pattern_id;
    }

    public string GetDialog() { return ui_dialog_id; }
    public string GetPattern() { return pattern_id; }
}

public class Turn : MonoBehaviour
{
    public static Turn instance;
    public int current_turn = 0;
    public TurnData[] turns =
    {
        new TurnData("ui_text_1", "pattern_wave"),
        new TurnData("ui_text_2", "pattern_bullet_rush"),
        new TurnData("ui_text_3", "pattern_sinusoidal_bullet_hell"),
        new TurnData("ui_text_4", "pattern_spin_and_shot"),
        new TurnData("ui_text_5", "pattern_truc"),
        new TurnData("ui_text_6", "pattern_wave_circle"),
        new TurnData("ui_text_6", "pattern_bullet_rush_2"),
        new TurnData("ui_text_1", "random")
    };

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static TurnData GetTurn() { return instance.turns[instance.current_turn]; }
    public static void NextTurn()
    {
        if (instance.current_turn == instance.turns.Length - 1)
            return;
        instance.current_turn++;
    }
}
