using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCinematic : MonoBehaviour
{
    [SerializeField] private DialogManager _dialogManager = null;
    [SerializeField] private TextMeshPro _dialogTextBox = null;

    private string[] _dialogs =
    {
        "game_over_1",
        "game_over_2"
    };

    private int _currentDialog = 0;

    void Start()
    {
        StartCoroutine("StartDialog");
    }

    private void Update()
    {
        DialogLink dialog = _dialogManager.GetDialog(_dialogManager.GetDialogOnTextBox(ref _dialogTextBox));
        if (dialog.started && dialog.obj.GetComponent<Dialog>().finish)
        {
            if (_currentDialog == _dialogs.Length - 1)
                return;
            _dialogManager.DeleteDialogOnTextBox(ref _dialogTextBox);
            _currentDialog++;
            _dialogManager.RunDialog(_dialogs[_currentDialog]);
        }
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(4.2f);
        _dialogManager.RunDialog(_dialogs[0]);
    }
}
