using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsMovement : MonoBehaviour
{
    public GameObject legs;
    private Animator legsAnimator;

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
    }

    private void Update() {
        input.x = Input.GetAxis("Horizontal") * moveSpeed;

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            //input.y = jumpForce;
            Jump();
        }
        else if(!isGrounded && input.y > 0f)
            input.y -= Time.deltaTime * 100f;
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck[0].position, 0.4f, whatIsGround) || Physics2D.OverlapCircle(groundCheck[1].position, 0.4f, whatIsGround);

        rb.AddForce(input * (100f * Time.deltaTime), ForceMode2D.Force);
        // rb.AddForce(new Vector2(0f,(input.y * (1000f * Time.deltaTime))), ForceMode2D.Force);
        // rb.velocity = new Vector2(input.x * Time.deltaTime, rb.velocity.y);
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
