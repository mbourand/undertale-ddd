using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateEnum
{
    MENU,
    ATTACK
}

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public GameStateEnum state = GameStateEnum.MENU;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


}
