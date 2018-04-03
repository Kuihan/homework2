using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    private int result_code = 1;
    void Start()
    {
        action = GameDirector.getInstance().currentGameController as IUserAction;
    }
    void OnGUI()
    {
        result_code = action.Check();
        if (result_code == 0)
        {
            GUI.TextField(new Rect(355, 20, 80, 30), "Game Over!");
        }
        else if (result_code == 1)
        {
            GUI.TextField(new Rect(355, 20, 80, 30), "Playing...");
        }
        else
        {
            GUI.TextField(new Rect(355, 20, 80, 30), "You win!");
        }
        if (GUI.Button(new Rect(50, 30, 70, 30), "Devil On"))
        {
            action.Devil_Left_On();
        }
        if (GUI.Button(new Rect(180, 30, 70, 30), "Priest On"))
        {
            action.Priest_Left_On();
        }
        if (GUI.Button(new Rect(550, 30, 70, 30), "Priest On"))
        {
            action.Priest_Right_On();
        }
        if (GUI.Button(new Rect(680, 30, 70, 30), "Devil On"))
        {
            action.Devil_Right_On();
        }
        if (GUI.Button(new Rect(300, 180, 40, 30), "Off"))
        {
            action.Boat_Left_Off();
        }
        if (GUI.Button(new Rect(450, 180, 40, 30), "Off"))
        {
            action.Boat_Right_Off();
        }
        if (GUI.Button(new Rect(375, 120, 40, 30), "Go"))
        {
            action.Boat_Go();
        }
    }
}