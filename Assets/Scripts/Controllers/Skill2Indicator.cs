using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Indicator : MonoBehaviour
{

    float input;
    public float rotationSpeed = 100f;
    public Rigidbody2D rb2d;
    public Vector3 rightD;
    public Vector3 forwardD;
    public Vector3 upD;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardD = this.transform.forward;
        rightD = this.transform.right;
        upD = this.transform.up;

        if (Input.GetKey("1"))
        {
            this.transform.rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.rotation;
            this.transform.localPosition = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.localPosition;
        }
         if (Input.GetKey("2"))
        {
            this.transform.rotation = Quaternion.AngleAxis(-rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.rotation;
            this.transform.localPosition = Quaternion.AngleAxis(-rotationSpeed * Time.deltaTime, Vector3.forward) * this.transform.localPosition;
        }

    }
}
