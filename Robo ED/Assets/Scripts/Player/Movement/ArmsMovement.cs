using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsMovement : MonoBehaviour
{
    public GameObject arms;
    private Rigidbody2D rb;
    private float baseGravity;

    private float input;

    private bool isClimbing;

    private bool isOnLadder;
    [SerializeField] float climbSpeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;

        arms.SetActive(true);
    }

    private void OnDisable() {
        arms.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Vertical");
        
        if(isOnLadder && input != 0)
            isClimbing = true;
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, input * climbSpeed);
            rb.gravityScale = 0;
        }
        // else
        // {
        //     rb.gravityScale = baseGravity;
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") && this.enabled == true)
        {
            isOnLadder = false;
            isClimbing = false;
            rb.gravityScale = baseGravity;
        }
    }
}
