using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Animator anim;
    private bool facingRight = true;
    public bool canDoubleJump;
    public bool grounded;
    public bool hurt = false;
    public bool end = false;

    public bool exit;
    public int score;
    public int level;
    public int lives;

    public float speed;

    /*text*/
    public Text scoretext;
    public Text scoretextShadow;
    public Text wintext;
    public Text wintextShadow;
    public Text livestext;
    public Text livestextShadow;
    public Text leveltext;
    public Text leveltextShadow;

    /*music*/
    public AudioClip musicClipOne;
    public AudioClip musicClipWin;
    public AudioClip musicClipThree;
    public AudioSource musicSource;
    public AudioClip sfxClipJump;
    public AudioClip sfxClipDoubleJump;
    public AudioClip sfxClipWallJump;
    public AudioClip sfxClipCoin;
    public AudioClip sfxClipEnemy;
    public AudioSource sfxSource;

    private Rigidbody2D rb2d;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        musicSource.clip = musicClipOne;
        musicSource.Play();

        score = 0;
        level = 1;
        lives = 3;
        exit = false;
        canDoubleJump = false;

        rb2d = GetComponent<Rigidbody2D>();

        wintext.text = " ";
        wintextShadow.text = " ";

        scoretext.text = "Coins " + score.ToString();
        scoretextShadow.text = "Coins " + score.ToString();

        setLevel();

        setLives();

        transform.position = new Vector2(104f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float hozMovement = Input.GetAxis("Horizontal");

        //movement
        if (hurt == false)
        {
            rb2d.AddForce(new Vector2(hozMovement * speed, 0));
        }

        //friction
        rb2d.velocity = new Vector2(rb2d.velocity.x * 0.97f, rb2d.velocity.y * 0.97f);

        //Jump code
        if (Input.GetKeyDown(KeyCode.W) && hurt == false)
        {
            //double jump
            if (canDoubleJump == true && grounded == false)
            {
                canDoubleJump = false;
                anim.SetInteger("State", 3);
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
                sfxSource.clip = sfxClipDoubleJump;
                sfxSource.Play();
            }
        }
        //

        //invulnerability frames
        if (hurt == true)
        {
            anim.SetInteger("State", 6);
            StartCoroutine(HurtFrames());
        }
        //

        //Fall Anim
        if (rb2d.velocity.y < -0.5)
        {
            anim.SetInteger("State", 5);
        }

        //Flip
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        //

        //Fastfall
        if ((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow)))
        {
            rb2d.AddForce(new Vector2(0, -1), ForceMode2D.Impulse);
        }
        //
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //score system
        if (other.gameObject.CompareTag("Coin"))
        {
            score += 1;
            setCoins();
            other.gameObject.SetActive(false);

        }
        //

        //Level 2
        if (score == 4 && level == 1)
        {
            exit = true;

            if (other.gameObject.CompareTag("Exit"))
            {
                exit = false;
                lives = 3;
                level = 2;

                transform.position = new Vector2(0.0f, -0.3f);
                rb2d.velocity = new Vector2(0.0f, 0.0f);

                anim.SetInteger("State", 0);

                setLives();

                setLevel();
            }
        }
        //

        //End
        if (score == 8)
        {
            exit = true;

            if (other.gameObject.CompareTag("Exit"))
            {
                transform.position = new Vector2(0f, 50f);
                rb2d.velocity = new Vector2(0.0f, 0.0f);

                exit = false;
                end = true;

                wintext.text = "You win! Game created by Ryan Fisher!";
                wintextShadow.text = "You win! Game created by Ryan Fisher!";

                musicSource.clip = musicClipWin;
                musicSource.Play();
                musicSource.loop = false;

                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                Time.timeScale = 0f;
            }
        }
        //


        //lives system
        if (other.gameObject.CompareTag("Enemy") && hurt == false)
        {
            lives = lives - 1;
            setLives();
            sfxSource.clip = sfxClipEnemy;
            sfxSource.Play();
            other.gameObject.SetActive(false);
            hurt = true;

            if (other.transform.position.x < this.transform.position.x)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
            }

            if (other.transform.position.x >= this.transform.position.x)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
            }
        }
        //

        //enemy 2
        if (other.gameObject.CompareTag("Enemy2") && hurt == false)
        {
            lives = lives - 1;
            setLives();
            sfxSource.clip = sfxClipEnemy;
            sfxSource.Play();
            hurt = true;

            if (other.transform.position.x < this.transform.position.x)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(15, 5), ForceMode2D.Impulse);
            }

            if (other.transform.position.x >= this.transform.position.x)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(-15, 5), ForceMode2D.Impulse);
            }
        }

        //pit
        if (other.gameObject.CompareTag("Pit"))
        {
            lives = lives - 1;
            setLives();
            sfxSource.clip = sfxClipEnemy;
            sfxSource.Play();
        }
        //
        //Death
        if (lives == 0)
        {
            wintext.text = "You Lose! Better luck next time!";
            wintextShadow.text = "You Lose! Better luck next time!";

            end = true;

            musicSource.clip = musicClipThree;
            musicSource.Play();
            musicSource.loop = false;

            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            Time.timeScale = 0f;
        }
        //
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        //jump system
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        if (collision.collider.tag == "Ground")
        {
            if (!Input.GetKey(KeyCode.W))
            {
                grounded = true;
                canDoubleJump = false;



                //Run anim//
                if (Mathf.Abs(hozMovement) > 0)
                {
                    anim.SetInteger("State", 1);
                }
                //Idle anim//
                if (Mathf.Abs(hozMovement) == 0)
                {
                    anim.SetInteger("State", 0);
                }
            }
            //jump
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State", 2);
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
                sfxSource.clip = sfxClipJump;
                sfxSource.Play();
            }
        }
           

        //Wall Jump
        if (collision.collider.tag == "Wall" && grounded == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                canDoubleJump = true;
                anim.SetInteger("State", 4);
                sfxSource.clip = sfxClipWallJump;
                sfxSource.Play();

                if (collision.transform.position.x < this.transform.position.x)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(new Vector2(5, 20), ForceMode2D.Impulse);
                }

                if (collision.transform.position.x >= this.transform.position.x)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(new Vector2(-5, 20), ForceMode2D.Impulse);
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = false;
            canDoubleJump = true;
        }
    }

            void setLives()
    {
        livestext.text = "Lives: " + lives.ToString();
        livestextShadow.text = "Lives: " + lives.ToString();
    }

    void setLevel()
    {
        leveltext.text = "Level " + level.ToString();
        leveltextShadow.text = "Level " + level.ToString();
    }

    void setCoins()
    {
        scoretext.text = "Coins " + score.ToString();
        scoretextShadow.text = "Coins " + score.ToString();

        sfxSource.clip = sfxClipCoin;
        sfxSource.Play();
    }
    IEnumerator HurtFrames()
    {
        yield return new WaitForSeconds(0.5f);
        hurt = false;
    }
}
