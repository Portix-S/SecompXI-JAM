using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleRestart : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    public void RestartGame()
    {
        gm.Restart();
    }
}
