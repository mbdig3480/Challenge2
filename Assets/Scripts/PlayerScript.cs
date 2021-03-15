using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text winText;
    public Text loseText;
    public Text livesText;
    public Text scoreText;
    private int scoreValue = 0;
    private int livesValue = 3;
    public AudioSource musicSource;
    public AudioClip soundEffect;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scoreText.text = scoreValue.ToString();
        setScoreText();
        setLivesText();
        winText.text = "";
        loseText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Coin") 
        {
            scoreValue += 1;
            Destroy(collision.collider.gameObject);
            setScoreText();
        }

        else if (collision.collider.tag == "Enemy")
        {
            livesValue = livesValue - 1;
            Destroy(collision.collider.gameObject);
            setLivesText();
        }
    }

    void OnCollisionStay2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W)) 
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void setScoreText() {
        scoreText.text="Score: " + scoreValue.ToString();

        if (scoreValue==8) {
            winText.text="You win! By Mallory Bronson";
            DestroyGameObject();
            musicSource.clip=soundEffect;
            musicSource.Play();
        }

        if (scoreValue==4) 
        {
            transform.position = new Vector2(-10.85f, 0.5772f);
            livesValue = 3;
            livesText.text = "Lives: " +livesValue.ToString();
        }
    }

    void setLivesText() {
        livesText.text="Lives: " + livesValue.ToString();

        if (livesValue==0) {
            loseText.text="You lose! By Mallory Bronson";
            DestroyGameObject();
        }
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void Animation () {
        if (Input.GetKeyDown(KeyCode.D))
        {
           anim.SetInteger("State", 4);
         }
    }
}