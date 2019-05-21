using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCameraController : MonoBehaviour
{
    [HideInInspector] public Transform cameraPos;

    private ImanAController ImanA;
    private ImanBController ImanB;
    [SerializeField] private float yFixedPos = -77f;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GetComponent<Transform>();
        ImanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
        ImanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;

        cameraPos.transform.position = new Vector3((ImanA.transform.position.x + ImanB.transform.position.x) / 2, yFixedPos, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos.transform.position = new Vector3((ImanA.transform.position.x + ImanB.transform.position.x) / 2, yFixedPos, -10f);
    }
}
