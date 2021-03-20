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
    public GameObject player;
    bool axisPressed = false;

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

    public void SetButtonPressed(int idx, bool b)
    {
        this.pressed = b;
        buttons[idx].pressed = b;
        if (b)
            soundPressed.Play();
    }

    void Update()
    {
        if (GameState.instance.state != GameStateEnum.MENU)
        {
            SetButtonPressed(selected, false);
            return;
        }

        if (Input.GetAxis("Menu Horizontal") == 1)
        {
            if (!axisPressed)
            {
                SelectButton((selected + 1) % buttons.Length);
                axisPressed = true;
            }
        }
        else if (Input.GetAxis("Menu Horizontal") == -1)
        {
            if (!axisPressed)
            {
                SelectButton((selected == 0 ? buttons.Length : selected) - 1);
                axisPressed = true;
            }
        }
        else
            axisPressed = false;

        if (Input.GetButtonDown("Confirm") && !this.pressed)
            SetButtonPressed(selected, true);
        if (Input.GetButtonDown("Cancel") && this.pressed)
        {
            player.transform.position = new Vector3(buttons[this.selected].transform.position.x - 0.95f, buttons[this.selected].transform.position.y, player.transform.position.z);
            SetButtonPressed(selected, false);
        }
        if (!this.pressed)
            player.transform.position = new Vector3(buttons[this.selected].transform.position.x - 0.95f, buttons[this.selected].transform.position.y, player.transform.position.z);
    }
}
