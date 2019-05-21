using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    //public EnemyModel enemyModel;
    private ImanAController imanA;
    private ImanBController imanB;
    
    [HideInInspector] public CheckPointMaster cpm;
    


    void Start()
    {
        imanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
        imanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;
        cpm = FindObjectOfType(typeof(CheckPointMaster)) as CheckPointMaster;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB")
        {
            imanA.imDeath = true;
            imanB.imDeath = true;
            //print(imanA.imDeath);
            if (cpm/*.special*/ != null)
            {
                imanA.transform.position = cpm.specialCheckPoint[0];
                imanB.transform.position = cpm.specialCheckPoint[1];
                //imanA.imDeath = false;
                //imanB.imDeath = false;

                //print(imanA.imDeath);

            }
            else {
                //imanA.transform.position = cpm.lastCheckPoint + new Vector2(2f, 0);
                //imanB.transform.position = cpm.lastCheckPoint - new Vector2(2f, 0);
                //Debug.Log("has colisionado con el pincho");
                //imanA.imDeath = false;
                //imanB.imDeath = false;
                //print(imanA.imDeath);

            }

        }
    }
}