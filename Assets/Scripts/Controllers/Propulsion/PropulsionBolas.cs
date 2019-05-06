using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionBolas : MonoBehaviour
{


    public GameObject SiguenteBola;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SiguienteBola",0.5F);
    }

    // Update is called once per frame
    void Update()
    {
        //Invoke("SiguienteBola", 0);
    }

    public void SiguienteBola()
    {
        if(SiguenteBola!=null)
            Instantiate(SiguenteBola, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
