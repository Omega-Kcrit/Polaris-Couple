using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 []Posiciones;
    public float []PosSize;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                transform.position = Posiciones[6];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[6];
                break;
            case 7:
                transform.position = Posiciones[7];
                gameObject.GetComponent<Camera>().orthographicSize = PosSize[7];
                break;
            default:
                
                transform.position = new Vector2(3.2f, 0.1f);
                gameObject.GetComponent<Camera>().orthographicSize = 8.833425f;
                break;


        }
    }



}
