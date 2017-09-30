using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpScript : MonoBehaviour {

    public static PlayerJumpScript instance;

    private Rigidbody2D myBody;
    private Animator anim;

    [SerializeField]
    private float forceX, forceY;
    private float maxForceX = 6.5f, maxForceY = 13.5f;
    private float thresholdX = 5f;
    private float thresholdY = 10f;

    private float distanceToGround;
    private float width;
    private bool setPower, didJump;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        width = GetComponent<BoxCollider2D>().bounds.extents.x;
        Singleton();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PrivateSetPower();
    }


    void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void PrivateSetPower()
    {
        if(setPower)
        {
            forceX += thresholdX * Time.deltaTime;
            forceY += thresholdY * Time.deltaTime;

            if (forceX > 6.5f) forceX = maxForceX;
            if (forceY > 13.5f) forceY = maxForceY;

        }
    }

    public float GetJumpPower()
    {
        return forceX / maxForceX;
    }


    public void SetPower(bool setPower)
    {
        this.setPower = setPower;
        if (!setPower)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!IsGrounded())
        {
            return;
        }
        myBody.velocity = new Vector2(forceX, forceY);
        forceX = forceY = 0f;
        didJump = true;
        anim.SetBool("Jump", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (didJump)
        {
            if (collision.gameObject.tag == "Ground" && IsGrounded())
            {
                didJump = false;
                GameManager.instance.CreateNewPlatformAndLerp(collision.transform.position.x);
                anim.SetBool("Jump",false);
                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.IncrementScore();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (didJump)
        {
            if (collision.tag == "Dead")
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.GameOverActions();
                }
                Destroy(gameObject); //player Dead
            }
        }
    }


    private bool IsGrounded()
    {
        //RaycastHit2D hitLeft;
        //Vector2 positionLeft = new Vector2(transform.position.x - width/2, transform.position.y);
        //RaycastHit2D hitRight;
        //Vector2 positionRight = new Vector2(transform.position.x + width/2, transform.position.y);
        //hitLeft = Physics2D.Raycast(positionLeft, Vector2.down, distanceToGround+1f, LayerMask.GetMask("Ground"));
        //hitRight = Physics2D.Raycast(positionRight, Vector2.down, distanceToGround + 1f, LayerMask.GetMask("Ground"));
        //Debug.DrawRay(positionLeft, Vector2.down, Color.black, 5f);
        //Debug.DrawRay(positionRight, Vector2.down, Color.black, 5f);
        //return (hitLeft || hitRight);
        
        //Debug.DrawRay(transform.position, Vector2.down, Color.black, 5f);
        return Physics2D.Raycast(transform.position, -Vector2.up, distanceToGround+1f, LayerMask.GetMask("Ground"));
    }

}
