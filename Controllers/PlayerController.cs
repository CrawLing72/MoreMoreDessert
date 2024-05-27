using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Rainyk Componenets")]
    public Rigidbody2D RainykRigidbody;
    public Animator RainykAnimator;
    public Collider2D RainykColider;

    [Header("Physics for Rainyk Settings")]
    public float Speed = 5f;
    public float GravityScale = 1f;
    public float JumpScale = 3f;
    public bool isJumped = true;
    public float RunEffect = 0.5f;
    private float RunEffectTime = 0f;

    private void ControllingRainyk()
    {
        // Step 1. Get Inputs and normalize to scale\
        GameManager.Player_X = Input.GetAxis("Horizontal");
        GameManager.Player_Y = Input.GetAxis("Vertical"); // Input 받아오기

        // Step 2. Separate case Topview and Sideview  -> 7일이던 8일이던 해 놓을것!
        if (true)
        {
            RainykRigidbody.gravityScale = 0;

            Vector2 RainykMovement = new Vector2(GameManager.Player_X, GameManager.Player_Y);
            RainykMovement.Normalize(); //Vecotr normalize

            RainykMovement *= Speed;
            RainykRigidbody.velocity = RainykMovement;

            if (RainykMovement.magnitude != 0f)
            {
                GameManager.isRainykMove = true;

                RunEffectTime += Time.deltaTime;
                if ((RunEffectTime > RunEffect) & !isJumped)
                {
                    RunEffectTime = 0f;
                    GameSoundManager.instance.onWalking();
                }
            }
            else GameManager.isRainykMove = false;

            RainykAnimator.SetBool("isMove", GameManager.isRainykMove);
            RainykAnimator.SetFloat("Player_X", GameManager.Player_X);
            RainykAnimator.SetFloat("Player_Y", GameManager.Player_Y);
        }
    }
    private void ControllingRainykSide()
    {
        if (true)
        {
            RainykRigidbody.gravityScale = GravityScale;

            GameManager.Player_X = Input.GetAxis("Horizontal");
            GameManager.Player_Y = 0f;

            Vector2 RainykSideMove = new Vector2(GameManager.Player_X * Speed, RainykRigidbody.velocity.y);


            if (RainykSideMove.magnitude > 0.1f)
            {
                GameManager.isRainykMove = true;

                RunEffectTime += Time.deltaTime;
                if ((RunEffectTime > RunEffect) & !isJumped)
                {
                    RunEffectTime = 0f;
                    GameSoundManager.instance.onWalking();
                }
            }
            else GameManager.isRainykMove = false;

            RainykRigidbody.velocity = RainykSideMove;


            if (Input.GetKeyDown(KeyCode.Space) && !isJumped)
            {
                isJumped = true;
                RainykAnimator.SetBool("isMove", GameManager.isRainykMove);

                RainykRigidbody.velocity = new Vector2(GameManager.Player_X, Speed * JumpScale);
                GameSoundManager.instance.onJump();
            }

            RainykAnimator.SetBool("isMove", GameManager.isRainykMove);
            RainykAnimator.SetFloat("Player_X", GameManager.Player_X);
            RainykAnimator.SetFloat("Player_Y", GameManager.Player_Y);
        }
    }
    
    public void FrozenCheck()
    {
        RainykRigidbody.gravityScale = 0;
        RainykRigidbody.velocity = Vector2.zero;

        GameManager.isRainykMove = false;

        RainykAnimator.SetBool("isMove", GameManager.isRainykMove);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
  

    }

    void Update()
    {
        if (!GameManager.isFrozen)
        {
            if (GameManager.isSideView) ControllingRainykSide();
            else ControllingRainyk();

        }
        else FrozenCheck();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Ground" && isJumped)
        {
            Vector2 collisionNormal = collision.contacts[0].normal;
            float collisionAngle = Vector2.Angle(Vector2.up, collisionNormal);

            if ((-80 < collisionAngle) && (collisionAngle < 80)) isJumped = false;
            else if (collisionAngle == 90) isJumped = false;
        }
    }
}