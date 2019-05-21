using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public ImanAController ImanA;
    [HideInInspector] public ImanBController ImanB;
    [HideInInspector] public CheckPointMaster cpm;

    private Vector2 direction = new Vector2();
    private int objectiveChooser;
    [SerializeField] private float speed = 30f;
    private Rigidbody2D rb2d;


    // Start is called before the first frame update
    void Start()
    {
        ImanA = FindObjectOfType(typeof(ImanAController)) as ImanAController;
        ImanB = FindObjectOfType(typeof(ImanBController)) as ImanBController;
        cpm = FindObjectOfType(typeof(CheckPointMaster)) as CheckPointMaster;
        rb2d = GetComponent<Rigidbody2D>();

        objectiveChooser = Random.Range(0, 1);
        switch (objectiveChooser)
        {
            case 0:
                direction = ImanA.transform.position - this.transform.position;
                break;
            case 1:
                direction = ImanB.transform.position - this.transform.position;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = direction.normalized * 20f* Time.fixedDeltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "ImanA" && collision.gameObject.tag != "ImanB")
        {
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "ImanA" || collision.gameObject.tag == "ImanB")
        {
            ImanA.transform.position = cpm.lastCheckPoint + new Vector2(2f, 0);
            ImanB.transform.position = cpm.lastCheckPoint - new Vector2(2f, 0);
            Debug.Log("has colisionado con el pincho");
        }
    }
}
