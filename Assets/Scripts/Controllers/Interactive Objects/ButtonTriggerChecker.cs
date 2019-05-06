using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerChecker : MonoBehaviour
{
    [HideInInspector] public bool isGettingTriggered;
    //public Sprite[] Changes;
    private bool Changed = false;
    public bool isLever; //La diferencia entre lever y botón, es que el lever, una vez activado, no va a desactivarse, mientras que para abrir una puerta han de estar todos los botones del nivel pulsaodos -Martí

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ImanA" || col.tag == "ImanB") isGettingTriggered = true;
        //if (col.tag == "ImanA"&&!Changed)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().sprite = Changes[0];
        //    Changed = true;
        //}
        //if (col.tag == "ImanB"&& !Changed)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().sprite = Changes[1];
        //    Changed = true;
        //}
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ImanA" || col.tag == "ImanB") isGettingTriggered = true;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "ImanA" || col.tag == "ImanB" && !isLever) isGettingTriggered = false;
    }
}
