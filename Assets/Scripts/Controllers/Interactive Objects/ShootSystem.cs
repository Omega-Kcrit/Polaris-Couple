using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public Vector3 posDis;//Posicion editabel de donde quiere que spawne el clavo

    public GameObject Prefa;
    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("Shoot", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
       GameObject disparo= Instantiate(Prefa, transform.position+posDis, Quaternion.identity);
        Invoke("Shoot", 2);
    }


}
