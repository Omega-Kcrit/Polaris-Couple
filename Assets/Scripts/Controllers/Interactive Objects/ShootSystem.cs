using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public Vector3 distanceFromTurret = new Vector3(0, -3f, 0);
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootingFreq = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Shoot", shootingFreq);
        InvokeRepeating("Shoot", 3, shootingFreq);
    }

    public void Shoot()
    {
       GameObject disparo = Instantiate(bullet, this.transform.position + distanceFromTurret, Quaternion.identity);
    }
}
