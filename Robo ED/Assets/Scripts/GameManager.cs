using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;
using Vector2 = System.Numerics.Vector2;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private HeadMovement headMovement;
    private LegsMovement legsMovement;
    private ArmsMovement armsMovement;
    private Torso torso;
    public int partsCollected = 2;

    private Rigidbody2D playerRb;

    private float rbVelocity;

    public float parallaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        headMovement = player.GetComponent<HeadMovement>();
        legsMovement = player.GetComponent<LegsMovement>();
        // armsMovement = player.GetComponent<ArmsMovement>();
        // torso = player.GetComponent<Torso>();
        legsMovement.enabled = false;
        // armsMovement.enabled = false;
        // torso.enabled = false;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rbVelocity = playerRb.velocity.x;
        parallaxSpeed = rbVelocity * .1f;
    }

    public void RestorePart()
    {
        player.transform.eulerAngles = Vector3.zero;
        partsCollected++;
        switch (partsCollected)
        {
            case 1:
                headMovement.enabled = false;
                legsMovement.enabled = true;
                break;
            case 2:
                torso.enabled = true;
                break;
            case 3:
                armsMovement.enabled = true;
                break;
        }
    }
}
