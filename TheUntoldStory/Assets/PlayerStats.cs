using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHealth = 6;
    public int health = 6;
    public int lives = 3;

    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;

    private SpriteRenderer spriteRenderer;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    //UI
   // public HealthBarUI healthBar;

    // public int coinsCollected = 0;
    // Start is called before the first frame update
    void Start()
    {
      //  healthBar.SetMaxHealth(MaxHealth);
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isImmune == true)
        {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if(immunityTime >= immunityDuration)
            {
                this.isImmune = false;
                this.spriteRenderer.enabled = true;

                //Debug.Log("Immunity has ended")
            }
        }
    }

    void SpriteFlicker()
    {
        if(this.flickerTime < this.flickerDuration)
        {
            this.flickerTime = this.flickerTime + Time.deltaTime;
        }

        else if (this.flickerTime >= this.flickerDuration)
        {
            spriteRenderer.enabled = !(spriteRenderer.enabled);
            this.flickerTime = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("tessst");
        if (this.isImmune == false)
        {
            this.health = this.health - damage;
            if (this.health < 0)
            {
                this.health = 0;
             //   this.healthBar.SetHealth(health);
            }
            if (this.lives > 0 && this.health == 0)
            {
               // FindObjectOfType<levelmanager1>().RespawnPlayer();
                this.health = 6;
                this.lives--;
             //   this.healthBar.SetHealth(health);
            }
            else if (this.lives == 0 && this.health == 0)
            {
                Debug.Log("Gameover");
                Destroy(this.gameObject);
            }

           // Debug.Log("Player Health:" + this.health.ToString());
           // Debug.Log("Player Lives:" + this.lives.ToString());
        }
        PlayHitReaction();
    }
    void PlayHitReaction()
    {
        this.isImmune = true;
        this.immunityTime = 0;
    }
}
