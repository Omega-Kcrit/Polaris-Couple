using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMaster : MonoBehaviour
{
    [HideInInspector]public Vector2 lastCheckPoint;

    private static CheckPointMaster checkPointMaster;

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
        if (lastCheckPoint == null)
        {
            lastCheckPoint = new Vector2(0, -6.5f);
        }
    }
}
