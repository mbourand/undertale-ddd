using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerManager playerManager;

    void Update()
    {
        transform.localScale = new Vector3(playerManager.GetHP() / (float)playerManager.GetMaxHP(), 1, 1);        
    }
}
