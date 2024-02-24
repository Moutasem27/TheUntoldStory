using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAttackController : MonoBehaviour
{

    protected float speed;
    protected int damage ;
    protected float timeDestroy;
    protected PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        

        


        Destroy(gameObject, timeDestroy);

    }

    // Update is called once per frame
    void Update()
    {

        

    }

    public void Movement()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
    }

   
}
