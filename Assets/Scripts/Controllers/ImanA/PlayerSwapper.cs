using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapper : MonoBehaviour
{

    public GameObject[] ballArray;

    public GameObject currentControlledBall;
    private int ballIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        ballIndex = 0;
        currentControlledBall = ballArray[ballIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") || InputManager.LeftTrigger() || InputManager.RightTrigger())
        {
            this.change();
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
