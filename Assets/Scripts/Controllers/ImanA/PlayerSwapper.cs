using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapper : MonoBehaviour
{

    public GameObject[] ballArray;


    private ImanAController imanA;
    private ImanBController imanB;
    [HideInInspector] public CheckPointMaster cpm;

    public GameObject currentControlledBall;
    private int ballIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        ballIndex = 0;
        currentControlledBall = ballArray[ballIndex];
        imanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
        imanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;
        cpm = FindObjectOfType(typeof(CheckPointMaster)) as CheckPointMaster;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") || InputManager.LeftTrigger() || InputManager.RightTrigger())
        {
            this.change();
        }

        if (Input.GetKeyDown("r") || InputManager.ResetButton()){
            imanA.transform.position = cpm.lastCheckPoint + new Vector2(2f, 0);
            imanB.transform.position = cpm.lastCheckPoint - new Vector2(2f, 0);
        }



    }

    public void change()
    {
        ballIndex++;
        if (ballIndex > 1)
        {
            ballIndex = 0;
        }
        if (currentControlledBall.GetComponent<ImanAController>() != null)
        {
            currentControlledBall.GetComponent<ImanAController>().InControllA = false;
            currentControlledBall.GetComponent<Atraccion>().enabled = true;

            ballArray[ballIndex].GetComponent<ImanBController>().InControllB = true;
            ballArray[ballIndex].GetComponent<Atraccion>().enabled = false;
        }
        else
        {
            currentControlledBall.GetComponent<ImanBController>().InControllB = false;
            currentControlledBall.GetComponent<Atraccion>().enabled = true;

            ballArray[ballIndex].GetComponent<ImanAController>().InControllA = true;
            ballArray[ballIndex].GetComponent<Atraccion>().enabled = false;
        }

        currentControlledBall = ballArray[ballIndex];

        
    }
}
