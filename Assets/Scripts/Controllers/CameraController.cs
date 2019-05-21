using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 []Posiciones;
    public float []PosSize;


    [HideInInspector] public Transform cameraPos;
    [HideInInspector] private ImanAController ImanA;
    [HideInInspector] private ImanBController ImanB;
    private bool isSecondaryCameraActivated = false;
    private bool isThirdCameraActivated = false;
    private GameObject miniVerticalMovingPlatform;
    private GameObject VerticalMovingPlatform;

    void Start()
    {
        cameraPos = GetComponent<Transform>();
        ImanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
        ImanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;
        VerticalMovingPlatform = GameObject.FindGameObjectWithTag("VerticalMovingPlatform1");
        miniVerticalMovingPlatform = GameObject.FindGameObjectWithTag("PlatfMov");
    }

    // Update is called once per frame
    void Update()
    {
        if (isSecondaryCameraActivated) cameraPos.transform.position = new Vector3((ImanA.transform.position.x + ImanB.transform.position.x) / 2, -138f, -10f); //El -47 es la posición del centro del nivel en la altura
    }

    public void ChangePos(int val)
    {
        switch (val)
        {
            case 0:
                transform.position = Posiciones[0];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[0];
                break;
            case 1:
                transform.position = Posiciones[1];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[1];
                break;
            case 2:
                transform.position = Posiciones[2];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[2];
                break;
            case 3:
                transform.position = Posiciones[3];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[3];
                break;
            case 4:
                transform.position = Posiciones[4];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[4];
                break;
            case 5:
                transform.position = Posiciones[5];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[5];
                break;
            case 6:
                this.transform.SetParent(miniVerticalMovingPlatform.transform);
                transform.position = Posiciones[6];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[6];
                break;
            case 7:
                this.transform.SetParent(null);
                transform.position = Posiciones[7];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[7];
                break;
            case 8:
                isSecondaryCameraActivated = true;
                cameraPos.transform.position = new Vector3((ImanA.transform.position.x + ImanB.transform.position.x) / 2, -47f, -10f); //El -47 es la posición del centro del nivel en la altura
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[8];
                break;
            case 9:
                isSecondaryCameraActivated = false;
                transform.position = Posiciones[9];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[9];
                break;
            case 10:
                this.transform.SetParent(VerticalMovingPlatform.transform);
                transform.position = Posiciones[10];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[10];
                break;
            //case 11:
            //    this.transform.SetParent(null);
            //    transform.position = Posiciones[11];
            //    gameObject.GetComponent<Camera>().orthographicSize = PosSize[11];
            //    break;
            default:
                transform.position = new Vector2(3.2f, 0.1f);
                gameObject.GetComponent<Camera>().orthographicSize = 8.833425f;
                break;


        }
    }



}
