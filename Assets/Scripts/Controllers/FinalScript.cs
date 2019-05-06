using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScript : MonoBehaviour
{

    public GameObject endTriggerer;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ImanA" || col.gameObject.tag == "ImanB")
        {
            Debug.Log("u win");
            endTriggerer.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
