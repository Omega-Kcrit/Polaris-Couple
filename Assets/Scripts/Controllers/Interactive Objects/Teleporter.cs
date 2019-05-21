using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject ImanA;
    [SerializeField] private GameObject ImanB;

    public Vector2 newPos = new Vector2();

    [SerializeField] private int cameraLeadingTo;
    [HideInInspector] public CameraManager cameraManager;

    void Start()
    {
        cameraManager = FindObjectOfType(typeof(CameraManager)) as CameraManager;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ImanA" || col.gameObject.tag == "ImanB")
        {
            ImanA.transform.position = newPos;
            ImanB.transform.position = newPos + new Vector2(2, 0);
            cameraManager.CameraSwapper(cameraLeadingTo);
        }
    }
}
