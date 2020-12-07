using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    [TextArea(1, 3)]
    public string text;
    public int timeBetweenCharacters;

    private int currentTimeBetweenCharacters;
    private string currentText;

    public bool start = false, finish;

    public TextMeshPro textMeshPro;
    public AudioSource audioSource;

    void Start()
    {
        currentText = "";
        currentTimeBetweenCharacters = 0;
        finish = false;
    }

    void FixedUpdate()
    {
        if (!start || finish)
            return;
        
        if (timeBetweenCharacters < 0)
        {
            currentText = text;
            textMeshPro.text = currentText;
            finish = true;
        }
        else if (currentTimeBetweenCharacters++ >= timeBetweenCharacters)
        {
            char newChar = text[currentText.Length];
            currentText += newChar;
            textMeshPro.text = currentText;
            if (audioSource.clip && char.IsLetterOrDigit(newChar))
                audioSource.Play();
            if (currentText == text)
                finish = true;
            currentTimeBetweenCharacters = 0;
        }
    }
    
    void OnDestroy()
    {
        textMeshPro.text = "";
    }
}
