using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct DialogLink
{
    [TextArea(1, 3)]
    public string text;
    public string id;
    public int timeBetweenCharacters;
    public TextMeshPro textMeshPro;
    public AudioSource sound;
    [HideInInspector]
    public bool started;
    [HideInInspector]
    public GameObject obj;
}

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPrefab;
    public DialogLink[] dialogs;

    void Start()
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            dialogs[i].started = false;
            dialogs[i].obj = null;
        }
    }

    public void RunDialog(int i)
    {
        if (i < 0 || i >= dialogs.Length || dialogs[i].started)
            return;
        dialogs[i].obj = Instantiate(dialogPrefab);
        Dialog newDialog = dialogs[i].obj.GetComponent<Dialog>() as Dialog;
        newDialog.text = this.dialogs[i].text;
        newDialog.textMeshPro = this.dialogs[i].textMeshPro;
        newDialog.timeBetweenCharacters = this.dialogs[i].timeBetweenCharacters;
        newDialog.audioSource = this.dialogs[i].sound;
        newDialog.start = true;
        dialogs[i].started = true;
    }

    public void RunDialog(string id)
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            if (id == dialogs[i].id)
            {
                RunDialog(i);
                break;
            }
        }
    }

    public void DeleteDialog(int i)
    {
        if (dialogs[i].started)
        {
            Destroy(dialogs[i].obj);
            dialogs[i].started = false;
        }
    }

    public void DeleteDialog(string id)
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            if (id == dialogs[i].id)
            {
                DeleteDialog(i);
                break;
            }
        }
    }

    public void DeleteDialogOnTextBox(ref TextMeshPro textBox)
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            if (textBox == dialogs[i].textMeshPro && dialogs[i].started)
            {
                DeleteDialog(i);
                break;
            }
        }
    }

    public string GetDialogOnTextBox(ref TextMeshPro textBox)
    {
        for (int i = 0; i < dialogs.Length; i++)
            if (textBox == dialogs[i].textMeshPro && dialogs[i].started)
                return dialogs[i].id;
        return null;
    }
}
