using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform respawnPosition;

    private void Start()
    {
        respawnPosition = GameObject.FindGameObjectWithTag("Spawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
    }

    public void Respawn()
    {
        transform.position = respawnPosition.position;
    }
    
    public void UpdateRespawnPosition(Transform newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
        //respawnPosition.position = newRespawnPosition.position;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Respawn();
        }
    }
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("UpdateRespawn"))
    //     {
    //         UpdateRespawnPosition(this.transform);
    //         Destroy(other.gameObject);
    //     }
    // }
}
