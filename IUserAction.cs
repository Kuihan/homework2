using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void Priest_Right_On();
    void Priest_Left_On();
    void Devil_Right_On();
    void Devil_Left_On();
    void Boat_Left_Off();
    void Boat_Right_Off();
    void Boat_Go();
    int Check();
}