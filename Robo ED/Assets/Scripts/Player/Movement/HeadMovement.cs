using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public GameObject head;
    Rigidbody2D rb;

    private float input;

    [SerializeField] float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        rb.freezeRotation = false;

        head.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(input * speed * Time.deltaTime, 0), ForceMode2D.Force);
        // If the player is moving right and the input is left, then stop rotation and start rotating left
        if ((rb.velocity.x > 0 && input < 0) || (rb.velocity.x < 0 && input > 0))
        {
            rb.angularVelocity = 0f;
        }
    }
}
