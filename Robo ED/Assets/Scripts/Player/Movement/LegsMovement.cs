using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class LegsMovement : MonoBehaviour
{
    public GameObject legs;
    private Animator legsAnimator;

    [SerializeField] private SpriteRenderer[] bodySprites;
    private bool isFacingRight = true;

    [Header("Leg Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 input;
    private Rigidbody2D rb;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform[] groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    public bool isGrounded = true;

    private void Start() {
        input = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        legsAnimator = legs.GetComponent<Animator>();
        rb.freezeRotation = true;
        legs.SetActive(true);
    }

    private void Awake()
    {
    }

    private void Update() {
        input.x = Input.GetAxis("Horizontal") * moveSpeed;

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            //input.y = jumpForce;
            Jump();
        }
        else if(!isGrounded && input.y > 0f)
            input.y -= Time.deltaTime * 100f;

        // Handles sprite flip
        if(input.x < 0f && isFacingRight){
            foreach(SpriteRenderer sprite in bodySprites){
                sprite.flipX = true;
            }
            isFacingRight = !isFacingRight;
        }
        else if(input.x > 0f && !isFacingRight){
            foreach(SpriteRenderer sprite in bodySprites){
                sprite.flipX = false;
            }
            isFacingRight = !isFacingRight;
        }
            
        // Handles Animator
        if(input.x != 0f && isGrounded){
            legsAnimator.SetBool("isWalking", true);
        }
        else legsAnimator.SetBool("isWalking", false);

    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck[0].position, 0.4f, whatIsGround) || Physics2D.OverlapCircle(groundCheck[1].position, 0.4f, whatIsGround);

        // rb.AddForce(input * (1000f * Time.deltaTime), ForceMode2D.Force);
        rb.AddForce(new Vector2(0f,(input.y * (1000f * Time.deltaTime))), ForceMode2D.Force);
        rb.velocity = new Vector2(input.x * Time.deltaTime * 100f, rb.velocity.y);
        if ((rb.velocity.x > 0 && input.x < 0) || (rb.velocity.x < 0 && input.x > 0))
            rb.velocity = Vector2.zero;
        
    }

    private void Jump(){
        if(input.y <= jumpForce){
            input.y += Time.deltaTime;
            Jump();
        }
        else return;
    } 
}
