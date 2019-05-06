using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{

    private CheckPointMaster cpm;

    // Start is called before the first frame update
    void Start()
    {
        //cpm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<CheckPointMaster>();   //Este script es una prueba, cuando podamos mergear todo en el mismo playercontroller
        //this.transform.position = cpm.lastCheckPoint;                                               //sin que de problemas el collab, ahí irá -Martí
    }

}
