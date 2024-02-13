using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed;
    public float jumpHeight;
    public float FlyingHeight;
    public bool isFacingRight;
    public KeyCode Spacebar;
    public KeyCode L;
    public KeyCode R;
    public KeyCode F;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform airCheck;
    public float airCheckRadius;
    public LayerMask whatIsAir;
    public bool grounded;
    public bool flying;
    void flip()
    {
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

    void Fly()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FlyingHeight);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        flying = Physics2D.OverlapCircle(airCheck.position, airCheckRadius, whatIsAir);

    }
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Spacebar) && grounded)
        {
            
            Jump();
        }
        if (Input.GetKeyDown(F) && flying)
        {

            Fly();
        }



        if (Input.GetKey(L))
        {
            
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            
            if (isFacingRight)
            {
                flip();
                isFacingRight = false;
            }
        }

        if (Input.GetKey(R))
        {
            
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (!isFacingRight)
            {
                flip();
                isFacingRight = true;
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Flying", flying);

    }
}
