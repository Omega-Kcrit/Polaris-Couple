using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField]                                      //ESTE CÓDIGO NO ES NECESARIO, NO LO BORRO PORQUE PUEDE SER ÚTIL, PERO PARA
    private GameObject door;                              //LOS BOTONES Y LAS PUERTAS YA ESTÁN LOS SCRIPTS DE BUTTONTRIGGERCHECKER Y
    private ImanAController imanA;                        //DOORSYSTEM -Martí
    private bool alreadyPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        imanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alreadyPressed)
        {
            door.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f * Time.fixedDeltaTime * 100), ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ImanA")
        {
            //player.takeDamage(); Aquí la función que haga que el player vuelva al inicio del nivel o le quite vida
            alreadyPressed = true;
        }
        if (col.gameObject.tag == "ImanB")
        {
            alreadyPressed = true;
        }
    }

}
