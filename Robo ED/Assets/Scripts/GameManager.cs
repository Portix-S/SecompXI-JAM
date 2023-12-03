using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private HeadMovement headMovement;
    private LegsMovement legsMovement;
    private ArmsMovement armsMovement;
    private Torso torso;
    public int partsCollected = 2;
    // Start is called before the first frame update
    void Start()
    {
        headMovement = player.GetComponent<HeadMovement>();
        legsMovement = player.GetComponent<LegsMovement>();
        armsMovement = player.GetComponent<ArmsMovement>();
        torso = player.GetComponent<Torso>();
        // legsMovement.enabled = false;
        armsMovement.enabled = false;
        // torso.enabled = false;
    }

    public void RestorePart()
    {
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
