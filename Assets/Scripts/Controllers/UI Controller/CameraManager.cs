using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cameraArray;
    private int activeCamera;

    private ImanAController ImanA;
    private ImanBController ImanB;

    // Start is called before the first frame update
    void Start()
    {
        ImanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
        ImanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraSwapper(int cameraNumber)
    {
        for(int i = 0; i < cameraArray.Length; i++)
        {
            Debug.Log("Entrando en el for");
            if (i == cameraNumber)
            {
                cameraArray[i].SetActive(true);
            }
            else
            {
                cameraArray[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB") CameraSwapper(1); //no funciona porque cuando se ejecuta este codigo no está colisionando con el teleporter
        else if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB") CameraSwapper(2);
    }
}
