using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    [TextArea(1, 3)]
    public string text;
    public float timeBetweenCharacters;

    private float currentTimeBetweenCharacters;
    private string currentText;

    public bool start = false;
    public bool finish;

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
        else if ((currentTimeBetweenCharacters += 1.0f) >= timeBetweenCharacters + 1)
        {
            char newChar = text[currentText.Length];
            do
            {
                currentText += (newChar == '-' ? ' ' : newChar);
            } while (currentText.Length != text.Length && (newChar = text[currentText.Length]) == '-');

            textMeshPro.text = currentText;
            if (audioSource != null && char.IsLetterOrDigit(newChar))
                audioSource.Play();
            if (currentText.Length == text.Length)
                finish = true;
            currentTimeBetweenCharacters -= timeBetweenCharacters + 1;
        }
    }
    
    void OnDestroy()
    {
        textMeshPro.text = "";
    }
}
