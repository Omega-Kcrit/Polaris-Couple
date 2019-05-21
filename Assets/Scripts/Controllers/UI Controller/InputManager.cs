using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    // - - Axis - -
    public static float MainHorizontal() //COMENTARIO IMPORTANTE DENTRO
    {
        float r = 0f;
        r += Input.GetAxis("J_MainHorizontal");     //Como el movimiento lateral es unidimensional, quizás conviene usar esta variable
                                                    //para que se mueva el player, en lugar de tocar el MainVertical y el MainJoystick
        return Mathf.Clamp(r, -1f, 1f);
    }
    
    public static float MainVertical()
    {
        float r = 0f;
        r += Input.GetAxis("J_MainVertical");

        return Mathf.Clamp(r, -1f, 1f);
    }

    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertical());
    }
                                                        //Este script es una capa extra a los inputs que nos da unity que nos permite más flexibilidad a la hora de programarlos
    // - - Buttons - -
    public static bool AButton()
    {
        return Input.GetButton("A_Button");
    }

    public static bool BButton()
    {
        return Input.GetButtonDown("B_Button");
    }

    public static bool XButton()
    {
        return Input.GetButtonDown("X_Button");
    }
    public static bool XButtonGetKey()
    {
        return Input.GetButton("X_Button");
    }

    public static bool YButton()
    {
        return Input.GetButtonDown("Y_Button");
    }
    public static bool YButtonGetKey()
    {
        return Input.GetButton("Y_Button");
    }

    public static bool LeftTrigger()
    {
        return Input.GetButtonDown("LeftTrigger"); //Es posible que en lugar del trigger se use el bumper, al menos en las primeras versiones. Es más sencillo y menos engorroso de programar
    }

    public static bool RightTrigger()
    {
        return Input.GetButtonDown("RightTrigger"); //Es posible que en lugar del trigger se use el bumper, al menos en las primeras versiones. Es más sencillo y menos engorroso de programar
    }

    public static bool MenuButton()
    {
        return Input.GetButtonDown("PauseButton");
    }

    public static bool ResetButton()
    {
        return Input.GetButtonDown("ResetButton");
    }

}
