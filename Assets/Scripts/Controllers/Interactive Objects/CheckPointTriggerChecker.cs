using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTriggerChecker : MonoBehaviour
{
    public CheckPointMaster cpm;
    public int val ;
    public bool checkPoint1Iman;
    public bool checkPointFor2ImanNotTogether;

    public Transform respawnIman1;
    public Transform respawnIman2;

    [SerializeField] private GameObject disparador1;
    [SerializeField] private GameObject disparador2;

    private bool imanAtouched;
    private bool imanBtouched;

    private Transform imanA;
    private Transform imanB;
   

    void Start()
    {
        cpm = GameObject.FindGameObjectWithTag("CheckPointMaster").GetComponent<CheckPointMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "ImanA")
        {
            imanAtouched = true;
            imanA = col.transform;
        }
        if (col.gameObject.tag == "ImanB")
        {
            imanBtouched = true;
            imanB = col.transform;
        }

        if (imanAtouched && imanBtouched && !checkPoint1Iman)
        {
            cpm.checkPointFor1Iman = false;
            checkPointFor2ImanNotTogether = false;
            cpm.lastCheckPoint = this.transform.position;
            if (GameObject.Find("Main Camera").activeInHierarchy) GameObject.Find("Main Camera").GetComponent<CameraController>().ChangePos(val);
        }

        //if (checkPointFor2ImanNotTogether)
        //{
        //    cpm.checkPointFor1Iman = false;
        //    if (imanAtouched || imanBtouched)
        //    {

        //        if (GameObject.Find("Main Camera").activeInHierarchy) GameObject.Find("Main Camera").GetComponent<CameraController>().ChangePos(val);
        //    }
        //}      

        if (checkPoint1Iman)
        {
            cpm.checkPointFor1Iman = true;

            if (imanAtouched && imanBtouched)
            {
                if (GameObject.Find("Main Camera").activeInHierarchy) GameObject.Find("Main Camera").GetComponent<CameraController>().ChangePos(val);
                //cpm.checkPointFor1Iman = true;
                if (imanA.position.y > imanB.position.y)
                {
                    print("posicion para cada uno");
                    cpm.lastCheckPointImanA = respawnIman1.position;
                    cpm.lastCheckPointImanB = respawnIman2.position;
                    print(respawnIman1.localPosition);
                    print(respawnIman2.localPosition);
                }
                else
                {
                    print("posicion para cada uno");
                    cpm.lastCheckPointImanB = respawnIman1.position;
                    cpm.lastCheckPointImanA = respawnIman2.position;
                    print(respawnIman1.localPosition);
                    print(respawnIman2.localPosition);
                }
            }
        }        

        //if (col.gameObject.tag == "ImanA" || col.gameObject.tag == "ImanB")
        //{
        //    cpm.lastCheckPoint = this.transform.position;
        //    //if (val == 8)
        //    //{
        //    //    cpm.special = true;
        //    //    cpm.specialCheckPoint[0] =new Vector2(336.003f, -138.0957f);
        //    //    cpm.specialCheckPoint[1] = new Vector2(336.003f, -144.08f);                
        //    //}
        //    //else
        //    //{
        //    //    cpm.special = false;
        //    //}
            
        //    //if (val == 10)
        //    //{
        //    //    disparador1.SetActive(true);
        //    //    disparador2.SetActive(true);
        //    //}
        //    //if (val == 11)
        //    //{
        //    //    disparador1.SetActive(false);
        //    //    disparador2.SetActive(false);
        //    //}
        //}
        //if (GameObject.Find("Main Camera").activeInHierarchy) GameObject.Find("Main Camera").GetComponent<CameraController>().ChangePos(val);
        //else if (GameObject.Find("SecondaryCamera").activeInHierarchy) GameObject.Find("SecondaryCamera").GetComponent<CameraController>().ChangePos(val);
        //else if (GameObject.Find("ThirdCamera").activeInHierarchy) GameObject.Find("ThirdCamera").GetComponent<CameraController>().ChangePos(val);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "ImanA")
        {
            imanAtouched = false;
        }
        if (col.gameObject.tag == "ImanB")
        {
            imanBtouched = false;
        }
    }
}
