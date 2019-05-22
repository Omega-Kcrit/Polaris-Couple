using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMaster : MonoBehaviour
{
    public Vector2 lastCheckPoint;
    public Vector2 lastCheckPointImanA;
    public Vector2 lastCheckPointImanB;

    public bool checkPointFor1Iman;

    private static CheckPointMaster checkPointMaster;
    public bool special = false;
    public Vector2 []specialCheckPoint;

    void Awake()
    {
        if (checkPointMaster == null)
        {
            checkPointMaster = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
       // specialCheckPoint = new Vector2[2];
        if (lastCheckPoint == null)
        {
            lastCheckPoint = this.transform.position;            
        }
        //checkPointFor1Iman = false;
    }

    void Update()
    {

        //if (Input.GetKeyDown("1"))
        //{
        //    lastCheckPoint = new Vector2(126.69f, -148.3f);
        //}
        //if (Input.GetKeyDown("2"))
        //{
        //    lastCheckPoint = new Vector2(199, -151.3f);
        //}
        //if (Input.GetKeyDown("3"))
        //{
        //    lastCheckPoint = new Vector2(267.3f, -130.3f);
        //}
        //if (Input.GetKeyDown("4"))
        //{
        //    lastCheckPoint = new Vector2(535.4f, -87.8f);
        //}
        //if (Input.GetKeyDown("5"))
        //{
        //    lastCheckPoint = new Vector2(472.1f, -144f);
        //}

    }
}
