using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateEnum
{
    MENU,
    ATTACK
}

public class GameState
{
    public static GameStateEnum state = GameStateEnum.MENU;
}
