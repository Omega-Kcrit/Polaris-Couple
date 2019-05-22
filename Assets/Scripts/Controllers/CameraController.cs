using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 []Posiciones;
    public float []PosSize;
    public float speedCamera = 1;
    public Vector3 nextPosition;


    [HideInInspector] public Transform cameraPos;
    [HideInInspector] private ImanAController ImanA;
    [HideInInspector] private ImanBController ImanB;
    private bool isSecondaryCameraActivated = false;
    private bool isThirdCameraActivated = false;
    private GameObject miniVerticalMovingPlatform;
    private GameObject BossPlatform;
    

    private bool bossPlatform;
    private bool followPlayerInControll;

    void Start()
    {
        cameraPos = GetComponent<Transform>();
        ImanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
        ImanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;
        BossPlatform = GameObject.FindGameObjectWithTag("BossPlatform");
        miniVerticalMovingPlatform = GameObject.FindGameObjectWithTag("PlatfMov");
        nextPosition = this.transform.position;

        bossPlatform = false;
        followPlayerInControll = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovingCamera(nextPosition, bossPlatform);

        

        if (isSecondaryCameraActivated) cameraPos.transform.position = new Vector3((ImanA.transform.position.x + ImanB.transform.position.x) / 2, -138f, -10f); //El -47 es la posición del centro del nivel en la altura
    }

    public void ChangePos(int val)
    {
        switch (val)
        {
            case 0:
                //this.transform.position = Vector3.MoveTowards(this.transform.position, Posiciones[0], speedCamera * Time.deltaTime);
                //transform.position = Posiciones[0];
                nextPosition = Posiciones[0];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[0];
                break;
            case 1:
                //transform.position = Posiciones[1];
                nextPosition = Posiciones[1];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[1];
                break;
            case 2:
                //transform.position = Posiciones[2];
                nextPosition = Posiciones[2];

                gameObject.GetComponent<Camera>().orthographicSize = PosSize[2];
                break;
            case 3:
                //transform.position = Posiciones[3];
                nextPosition = Posiciones[3];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[3];
                break;
            case 4:
                //transform.position = Posiciones[4];
                nextPosition = Posiciones[4];

                gameObject.GetComponent<Camera>().orthographicSize = PosSize[4];
                break;
            case 5:
                //transform.position = Posiciones[5];
                nextPosition = Posiciones[5];

                gameObject.GetComponent<Camera>().orthographicSize = PosSize[5];
                break;
            case 6:
                //this.transform.SetParent(miniVerticalMovingPlatform.transform);
                //transform.position = Posiciones[6];
                nextPosition = Posiciones[6];

                gameObject.GetComponent<Camera>().orthographicSize = PosSize[6];
                break;
            case 7:
                //this.transform.SetParent(null);
                //transform.position = Posiciones[7];
                nextPosition = Posiciones[7];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[7];
                break;
            case 8:
                //isSecondaryCameraActivated = true;
                //cameraPos.transform.position = new Vector3((ImanA.transform.position.x + ImanB.transform.position.x) / 2, -47f, -10f); //El -47 es la posición del centro del nivel en la altura
                //gameObject.GetComponent<Camera>().orthographicSize = PosSize[8];
                //this.transform.SetParent(BossPlatform.transform);
                //transform.position = Posiciones[8];
                nextPosition = Posiciones[8];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[8];
                bossPlatform = true;
                break;
            case 9:
                //isSecondaryCameraActivated = false;
                //transform.position = Posiciones[9];
                nextPosition = Posiciones[9];
                bossPlatform = false;
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[9];
                break;
            case 10:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[10];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[10];
                break;
            case 11:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[11];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[11];
                break;
            case 12:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[12];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[12];
                break;
            case 13:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[13];
                followPlayerInControll = true;
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[13];
                break;
            case 14:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[14];
                followPlayerInControll = false;
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[14];
                break;
            case 15:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[15];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[15];
                break;
            case 16:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[16];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[16];
                break;
            case 17:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[17];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[17];
                break;
            case 18:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[18];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[18];
                break;
            case 19:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[11];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[19];
                break;
            case 20:
                //this.transform.SetParent(VerticalMovingPlatform.transform);
                //transform.position = Posiciones[10];
                nextPosition = Posiciones[20];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[20];
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

    public void MovingCamera(Vector3 position , bool platform)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, position, speedCamera * Time.deltaTime);

        if (platform)
        {
            this.transform.position = GameObject.FindGameObjectWithTag("BossPlatform").transform.position + new Vector3 (8,0,-10); 
        }
        if (followPlayerInControll)
        {
            if (GameObject.FindGameObjectWithTag("ImanA").GetComponent<ImanAController>().InControllA)
            {
                if (this.transform.position.y > GameObject.FindGameObjectWithTag("ImanA").transform.position.y)
                {
                    this.transform.position = new Vector3(this.transform.position.x, GameObject.FindGameObjectWithTag("ImanA").transform.position.y, -10);
                }
            }
            else
            {
                if (this.transform.position.y > GameObject.FindGameObjectWithTag("ImanB").transform.position.y)
                {
                    this.transform.position = new Vector3(this.transform.position.x, GameObject.FindGameObjectWithTag("ImanB").transform.position.y, -10);
                }
            }
        }
    }

    
}
