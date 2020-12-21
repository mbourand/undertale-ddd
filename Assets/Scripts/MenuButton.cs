using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [HideInInspector]
    public bool selected;
    public bool pressed;

    public bool pressed_once;
    public bool selected_once;

    public Sprite spriteNotSelected, spriteSelected;

    protected SpriteRenderer spriteRenderer;

    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
        this.spriteRenderer.sprite = spriteNotSelected;
        this.selected = false;
        this.pressed = false;
        this.selected_once = false;
        this.pressed_once = false;
    }

    void Update()
    {
        if (pressed)
            this.OnPressedUpdate();
        else if (selected)
            this.OnSelectedUpdate();
        else if (!selected)
            this.OnDeselectedUpdate();
        
        if (selected && !selected_once)
        {
            this.OnSelected();
            selected_once = true;
        }
        if (pressed && !pressed_once)
        {
            this.OnPressed();
            pressed_once = true;
        }
        if (!pressed && pressed_once)
        {
            this.OnDepressed();
            pressed_once = false;
        }
        if (!selected && selected_once)
        {
            this.OnDeselected();
            selected_once = false;
        }
    }

    protected virtual void OnSelected()
    {
        spriteRenderer.sprite = spriteSelected;
    }
    protected virtual void OnPressed() {}
    protected virtual void OnDeselected()
    {
        spriteRenderer.sprite = spriteNotSelected;
    }
    protected virtual void OnDepressed() {}
    protected virtual void OnSelectedUpdate() {}
    protected virtual void OnDeselectedUpdate() { }
    protected virtual void OnPressedUpdate() { }
}
