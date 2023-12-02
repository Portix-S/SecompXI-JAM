using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torso : MonoBehaviour
{
    public GameObject torso;
    private Animator torsoAnimator;

    private Rigidbody2D rb;
    private LegsMovement legsRef;

    // [SerializeField] private float floatingForce = .5f;
    // private float curFloat;
    [SerializeField] private float normalG = 9.81f, floatG = 4.9f; 
    private bool isFloating;

    private void Start() {
        torsoAnimator = torso.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        legsRef = GetComponent<LegsMovement>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && !legsRef.isGrounded){
            rb.gravityScale = floatG;
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            rb.gravityScale = normalG;
        }
        
    }
}
