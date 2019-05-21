using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerChecker : MonoBehaviour
{
    [HideInInspector] public bool isGettingTriggered;
    public Sprite[] Changes;
    public bool Changed = false;
    public bool isLever; //La diferencia entre lever y botón, es que el lever, una vez activado, no va a desactivarse, mientras que para abrir una puerta han de estar todos los botones del nivel pulsaodos -Martí

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ImanA" || col.tag == "ImanB") isGettingTriggered = true;
        else isGettingTriggered = false;
        if (col.tag == "ImanA" && !Changed)
        {
            print("Iman A");
            gameObject.GetComponent<SpriteRenderer>().sprite = Changes[0];
            
        }
        if (col.tag == "ImanB" && !Changed)
        {
            print("Iman B");
            gameObject.GetComponent<SpriteRenderer>().sprite = Changes[1];
            
        }
    }

   public void RestartB()
    {
        Changed = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = Changes[2];
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ImanA" || col.tag == "ImanB") isGettingTriggered = true;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "ImanA" || col.tag == "ImanB" /*&& !isLever*/) isGettingTriggered = false;
        Changed = true;
    }
}
