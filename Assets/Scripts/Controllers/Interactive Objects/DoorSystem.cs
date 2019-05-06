using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{

    public GameObject[] buttonArray;

    private bool canOpenDoor = false;

    //Animaciones
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (buttonArray.Length)
        {
            case 1:
                if (buttonArray[buttonArray.Length - 1].GetComponent<ButtonTriggerChecker>().isGettingTriggered) canOpenDoor = true;
                break;
            case 2:
                if (buttonArray[buttonArray.Length - 1].GetComponent<ButtonTriggerChecker>().isGettingTriggered && buttonArray[buttonArray.Length - 2].GetComponent<ButtonTriggerChecker>().isGettingTriggered) canOpenDoor = true;
                break;
            default:
                canOpenDoor = false;
                break;
        }
        animator.SetBool("IsOpen", canOpenDoor);
    }

    void FixedUpdate()
    {

    }
}
