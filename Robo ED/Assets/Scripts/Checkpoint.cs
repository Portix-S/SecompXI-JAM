using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerManager>().UpdateRespawnPosition(this.transform);
            this.GetComponent<Collider2D>().enabled = false;
            //Destroy(this.gameObject, 2f);
        }
    }
}
