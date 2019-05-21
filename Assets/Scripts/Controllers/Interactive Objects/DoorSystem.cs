using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{

    public GameObject[] buttonArray;

    private bool canOpenDoor = false;
    [SerializeField] private bool openDownwards = false;
    [SerializeField] private bool staticAppearance = false;

    [SerializeField] private GameObject door;

    //Animaciones
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //door = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpenDoor)
        {
            for(int i = 0; i < buttonArray.Length; i++)
            {
                if (buttonArray[i].GetComponent<ButtonTriggerChecker>().Changed)
                {
                    buttonArray[i].GetComponent<ButtonTriggerChecker>().RestartB();
                }
            }
        }
        switch (buttonArray.Length)
        {
            case 1:
                if (buttonArray[buttonArray.Length - 1].GetComponent<ButtonTriggerChecker>().isGettingTriggered) canOpenDoor = true;
                break;
            case 2:
                if (buttonArray[buttonArray.Length - 1].GetComponent<ButtonTriggerChecker>().isGettingTriggered && buttonArray[buttonArray.Length - 2].GetComponent<ButtonTriggerChecker>().isGettingTriggered) canOpenDoor = true;
                break;


            case 3:
                if (buttonArray[buttonArray.Length - 3].GetComponent<ButtonTriggerChecker>().isGettingTriggered || buttonArray[buttonArray.Length - 1].GetComponent<ButtonTriggerChecker>().isGettingTriggered)
                {
                    canOpenDoor = true;
                    openDownwards = false;
                }
                else
                {
                    canOpenDoor = false;
                    //openDownwards = true;
                }
                break;
            default:
                canOpenDoor = false;
                break;
        }

        //animator.SetBool("IsOpen", canOpenDoor);
    }

    void FixedUpdate()
    {
        //if (canOpenDoor && !openDownwards && !staticAppearance) this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f * Time.fixedDeltaTime * 100f), ForceMode2D.Force);
        //else if (canOpenDoor && openDownwards && !staticAppearance) this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -(10f * Time.fixedDeltaTime * 100f)), ForceMode2D.Force);
        if (canOpenDoor && staticAppearance && door != null) door.SetActive(true);
        if (canOpenDoor && animator != null)
        {
            animator.SetBool("IsOpen", canOpenDoor);
        }
        else if (animator != null)
        {

            animator.SetBool("IsOpen", canOpenDoor);
        }
    }
}


