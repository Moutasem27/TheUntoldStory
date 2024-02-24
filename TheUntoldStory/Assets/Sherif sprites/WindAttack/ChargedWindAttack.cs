using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedWindAttack : WindAttackController
{
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        speed = 10;
        damage = 10;
        timeDestroy = 5f;

        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
            GetComponent<Rigidbody2D>().transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Movement", 0.5f);
       

    }
}
