using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestorePart : MonoBehaviour
{
    private GameManager _gm;
    [SerializeField] private float delayToCall = 1f;
    bool hasRestored = false;
    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasRestored)
        {
            hasRestored = true;
            Invoke(nameof(RestorePartCall), delayToCall);
        }
    }

    private void RestorePartCall()
    {
        _gm.RestorePart();
        Destroy(gameObject);
    }
}
