using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTriggerChecker : MonoBehaviour
{
    private CheckPointMaster cpm;
    public int val ;

    [SerializeField] private GameObject disparador1;
    [SerializeField] private GameObject disparador2;



    void Start()
    {
        cpm = GameObject.FindGameObjectWithTag("CheckPointMaster").GetComponent<CheckPointMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ImanA" || col.gameObject.tag == "ImanB")
        {
            cpm.lastCheckPoint = transform.position;
            if (val == 8)
            {
                cpm.special = true;
                cpm.specialCheckPoint[0] =new Vector2(336.003f, -138.0957f);
                cpm.specialCheckPoint[1] = new Vector2(336.003f, -144.08f);


            }
            else
            {
                cpm.special = false;
            }
            
            if (val == 10)
            {
                disparador1.SetActive(true);
                disparador2.SetActive(true);
            }
            if (val == 11)
            {
                disparador1.SetActive(false);
                disparador2.SetActive(false);
            }
        }
        if (GameObject.Find("Main Camera").activeInHierarchy) GameObject.Find("Main Camera").GetComponent<CameraController>().ChangePos(val);
        else if (GameObject.Find("SecondaryCamera").activeInHierarchy) GameObject.Find("SecondaryCamera").GetComponent<CameraController>().ChangePos(val);
        else if (GameObject.Find("ThirdCamera").activeInHierarchy) GameObject.Find("ThirdCamera").GetComponent<CameraController>().ChangePos(val);
    }
}
