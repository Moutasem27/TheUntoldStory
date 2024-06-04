using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;


    public float moveSpeed;
    public float jumpHeight;
    public bool isFacingRight;
    Rigidbody2D rb;
    //Keybindings
    public KeyCode Spacebar;
    public KeyCode L;
    public KeyCode R;
    public KeyCode S;  //Shooting
    public KeyCode A;  //Attacking
    public KeyCode X;  //Dash
    public KeyCode Z;  //Shield

    //Wind gameObjects
    public GameObject Shield;
    public GameObject projectile;
    public GameObject megaProjectile;
    public GameObject windBeam;
    public Transform firePoint;
    public float windChargeTime = 0;

    //Ground
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool grounded;


    public bool isAttacking = false;
    public bool isShooting = false;
    public bool isShielding = false;
    public bool canMove = true;
    public static PlayerController instance;

    public Transform shieldPoint;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    //Dash
    float doubleTapTime;
    KeyCode lastKeyCode;
    public float dashspeed;
    private float dashCount;
    public float startDashCount;
    int side;
    
    void flip()
    {
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //transform.Rotate(0f, 180f, 0f);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }


    
    private void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    }
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dashCount = startDashCount;
    }

    // Update is called once per frame
    void Update()
    {
        //Shadows.me.PlayerSkill();
        Attack();
        Shoot();
        Shielding();
        ActiveShield();
        if (Input.GetKeyDown(Spacebar) && grounded && canMove)
        {

            Jump();
        }




        if (Input.GetKey(L) && canMove)
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (isFacingRight)
            {
                flip();
                isFacingRight = false;
            }
        }

        if (Input.GetKey(R))//&& canMove)
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (!isFacingRight)
            {
                flip();
                isFacingRight = true;
            }
        }

        //dash
        if (side == 0)
        {


            if (Input.GetKeyDown(KeyCode.X) && !isFacingRight)
            {
                if (doubleTapTime > Time.time && lastKeyCode == L)
                {
                    side = 1;
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = L;
            }

            else if (Input.GetKeyDown(KeyCode.X) && isFacingRight)
            {
                if (doubleTapTime > Time.time && lastKeyCode == R)
                {
                    side = 2;

                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = R;
            }
        }
        else
        {
            if (dashCount <= 0)
            {
                side = 0;
                dashCount = startDashCount;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashCount -= Time.deltaTime;
                if (side == 2)
                {
                    Shadows.me.PlayerSkill();
                    rb.velocity = Vector2.right * dashspeed;
                }
                else if (side == 1)
                {
                    Shadows.me.PlayerSkill();
                    rb.velocity = Vector2.left * dashspeed;
                }
            }
        }
        




        //if (Input.GetKeyDown(A))
        //{
        //    anim.SetTrigger("Attack");

        //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //    foreach(Collider2D enemy in hitEnemies)
        //    {
        //        Debug.Log("We hit " + enemy.name);
        //    }
        //}
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("Grounded", grounded);

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Attack()
    {
        if (Input.GetKeyDown(A) && !isAttacking)
        {
            isAttacking = true;
        }

        if(Input.GetKeyDown(S) && isAttacking)
        {
            Instantiate(windBeam, firePoint.position, firePoint.rotation);
            Destroy(windBeam, 1f);

        }
    }

    public void Shoot()
    {
        if (Input.GetKey(S) && !isShooting && !isAttacking)
        {
            //windChargeTime += Time.deltaTime;
            windChargeTime += 1;
            isShooting = true;
            Debug.Log(windChargeTime);
        }

        else if (Input.GetKeyUp(S) && windChargeTime >= 3 && !isAttacking)
        {
            Instantiate(megaProjectile, firePoint.position, firePoint.rotation);
            windChargeTime = 0;
        }

        else if (Input.GetKeyUp(S) && windChargeTime < 3 && !isAttacking)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            windChargeTime = 0;
        }

    }
    public void ActiveShield()
    {
        if (Input.GetKey(Z) && isShielding)
        {
            Shadows.me.PlayerSkill();
        }
        else if (Input.GetKeyUp(Z))
        {
            isShielding = false;
        }
    }
    public void Shielding()
    {
        if (Input.GetKeyDown(Z) && !isShielding)
        {
            Instantiate(Shield, shieldPoint.position, shieldPoint.rotation);

            isShielding = true;
        }
    }

}



    //}

