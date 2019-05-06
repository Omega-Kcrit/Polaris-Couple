using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTriggerChecker : MonoBehaviour
{
    private CheckPointMaster cpm;
    public int val ;
    

    void Start()
    {
        cpm = GameObject.FindGameObjectWithTag("CheckPointMaster").GetComponent<CheckPointMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ImanA" || col.gameObject.tag == "ImanB")
        {
            cpm.lastCheckPoint = transform.position;
            Debug.Log("Checkpointed");
        }
        GameObject.Find("Main Camera").GetComponent<CameraController>().ChangePos(val);
    }
}
