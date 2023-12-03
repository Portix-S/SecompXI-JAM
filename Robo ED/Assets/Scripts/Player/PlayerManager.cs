using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform respawnPosition;


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
        respawnPosition.transform.position = newRespawnPosition.position;
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
