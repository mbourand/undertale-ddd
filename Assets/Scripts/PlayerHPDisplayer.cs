using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHPDisplayer : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    public PlayerManager playerManager;
    
    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>(); 
    }

    void Update()
    {
        textMeshPro.text = "HP " + (playerManager.GetHP() < 10 ? " " : "") + playerManager.GetHP().ToString() + "/" + playerManager.GetMaxHP().ToString();
    }
}
