using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;

    public Text score;
    private int scoreValue;

    public GameObject winTextObject;
    public GameObject loseTextObject;

    public Text lives;
    private int livesValue;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        //this sets live value before updating
        livesValue = 3;
        lives.text = livesValue.ToString();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        musicSource.clip = musicClipTwo;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();

            if(scoreValue == 4)
            {
                transform.position = new Vector2(50.0f, 0.0f);
            }

            if(scoreValue == 8)
            {
                winTextObject.SetActive(true);
                musicSource.Stop();
                
                musicSource.clip = musicClipOne;
                musicSource.loop = true;
                musicSource.Play();

                speed = 0;
            }

            Destroy(collision.collider.gameObject);
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();

            if(livesValue == 0)
            {
                loseTextObject.SetActive(true);
                speed = 0;
            }

            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
