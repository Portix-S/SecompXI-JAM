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
    private static readonly int IsFloating = Animator.StringToHash("isFloating");

    private void Start() {
        torsoAnimator = torso.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        legsRef = GetComponent<LegsMovement>();

        transform.position += Vector3.up * 2f;
        torso.SetActive(true);
        legsRef.legs.transform.position = newLegsPos.position;
    }

    private void OnDisable() {
        torso.SetActive(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && !legsRef.isGrounded){
            rb.velocity = Vector2.zero;
            rb.gravityScale = floatG;
            torsoAnimator.SetBool(IsFloating, true);
        }

        if(Input.GetKeyUp(KeyCode.Space) || legsRef.isGrounded){
            rb.gravityScale = normalG;
            torsoAnimator.SetBool(IsFloating, false);
        }
        
    }
}
