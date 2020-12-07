using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private int selected;
    private bool pressed;

    public MenuButton[] buttons;

    public AudioSource soundSelect;
    public AudioSource soundPressed;

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        SelectButton(0);
    }

    void Start()
    {
        this.selected = 0;
        this.pressed = false;
        StartCoroutine("LateStart");
    }

    void SelectButton(int idx)
    {
        if (pressed)
            return;
        buttons[this.selected].selected = false;
        this.selected = idx;
        buttons[this.selected].selected = true;
        soundSelect.Play();
    }

    void SetButtonPressed(int idx, bool b)
    {
        this.pressed = b;
        buttons[idx].pressed = b;
        if (b)
            soundPressed.Play();
    }

    void Update()
    {
        if (GameState.state != GameStateEnum.MENU)
        {
            SetButtonPressed(selected, false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SelectButton((selected + 1) % buttons.Length);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            SelectButton((selected == 0 ? buttons.Length : selected) - 1);

        if (Input.GetKeyDown(KeyCode.Return) && !this.pressed)
            SetButtonPressed(selected, true);
        if (Input.GetKeyDown(KeyCode.X) && this.pressed)
            SetButtonPressed(selected, false);
    }
}
