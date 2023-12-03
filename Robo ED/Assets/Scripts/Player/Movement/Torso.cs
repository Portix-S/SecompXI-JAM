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

    [SerializeField] private Transform newLegsPos;

    [SerializeField] private float normalG = 9.81f, floatG = 4.9f; 
    private void Start() {
        torsoAnimator = torso.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        legsRef = GetComponent<LegsMovement>();

        transform.position += Vector3.up * 2f;
        torso.SetActive(true);
        legsRef.legs.transform.position = newLegsPos.position;
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
