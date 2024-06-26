//using System;

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Timer = System.Timers.Timer;
using Update = Unity.VisualScripting.Update;
using Vector2 = System.Numerics.Vector2;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float gameTimer = 0f;
    [SerializeField] private TextMeshProUGUI timerText;
    private bool timerActive = false;
    [SerializeField] GameObject leaderboard;
    [SerializeField] GameObject endScreen;
    


    [Header("Other Settings")]
    
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    [SerializeField] private Transform initialPos;
    private HeadMovement headMovement;
    private LegsMovement legsMovement;
    private ArmsMovement armsMovement;
    private Torso torso;
    public int partsCollected = 2;

    private Rigidbody2D playerRb;

    private float rbVelocity;

    public float parallaxSpeed;
    
    private int currentScene = 0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        timerText = GameObject.FindGameObjectWithTag("GameTimer").GetComponent<TextMeshProUGUI>();
        timerActive = true;
        SceneManager.sceneLoaded += Warmup;
        initialPos = GameObject.FindGameObjectWithTag("Spawn").transform;
        player = Instantiate(playerPrefab, initialPos.position, initialPos.rotation);

        currentScene = SceneManager.GetActiveScene().buildIndex;

        Transform cam = player.transform.Find("PlayerFollow").transform;
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = cam;
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = cam;
        RotationConstraint rotationConstraint = player.GetComponentInChildren<RotationConstraint>();
        Transform rotationSource = GameObject.FindGameObjectWithTag("Constraint").transform;
        rotationConstraint.AddSource(new ConstraintSource() {sourceTransform = rotationSource, weight = 1});

        playerRb = player.GetComponent<Rigidbody2D>();
        player.GetComponent<PlayerManager>().UpdateRespawnPosition(initialPos);

        headMovement = player.GetComponent<HeadMovement>();
        legsMovement = player.GetComponent<LegsMovement>();
        armsMovement = player.GetComponent<ArmsMovement>();
        torso = player.GetComponent<Torso>();

        // headMovement.head.SetActive(true);
        // legsMovement.legs.SetActive(false);
        // armsMovement.arms.SetActive(false);
        // torso.torso.SetActive(false);
        
        headMovement.enabled = true;
        legsMovement.enabled = false;
        armsMovement.enabled = false;
        torso.enabled = false;
        UpdateParts();
    }

     
    private void Warmup(Scene scene, LoadSceneMode loadMode)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene >= SceneManager.sceneCountInBuildSettings - 1)
        {
            if(timerText == null)
                timerText = GameObject.FindGameObjectWithTag("GameTimer").GetComponent<TextMeshProUGUI>();
            timerText.text = "";
            Debug.Log("passou");
            if (endScreen == null)
            {
                Helper help = GameObject.FindGameObjectWithTag("EndScreen").GetComponent<Helper>();
                endScreen = help.child;
                leaderboard = help.leaderboard;
            }

            endScreen.SetActive(true);
            leaderboard.GetComponent<LeaderboardHandler>().SetTimer(gameTimer);
        }
        if (!timerActive) return;


        initialPos = GameObject.FindGameObjectWithTag("Spawn").transform;
        player = Instantiate(playerPrefab, initialPos.position, initialPos.rotation);


        Transform cam = player.transform.Find("PlayerFollow").transform;
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = cam;
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = cam;
        RotationConstraint rotationConstraint = player.GetComponentInChildren<RotationConstraint>();
        Transform rotationSource = GameObject.FindGameObjectWithTag("Constraint").transform;
        rotationConstraint.AddSource(new ConstraintSource() {sourceTransform = rotationSource, weight = 1});
        
        player.GetComponent<PlayerManager>().UpdateRespawnPosition(initialPos);
        playerRb = player.GetComponent<Rigidbody2D>();

        headMovement = player.GetComponent<HeadMovement>();
        legsMovement = player.GetComponent<LegsMovement>();
        armsMovement = player.GetComponent<ArmsMovement>();
        torso = player.GetComponent<Torso>();
        
        headMovement.enabled = true;
        legsMovement.enabled = false;
        armsMovement.enabled = false;
        torso.enabled = false;
        UpdateParts();
    }

    private void Update()
    {
        if(timerActive)
        {
            rbVelocity = playerRb.velocity.x;
            parallaxSpeed = rbVelocity * .001f;
            gameTimer += Time.deltaTime;
            // Show timer as minutes, seconds and milliseconds 
            int miliseconds = ((int)(gameTimer * 100) % 100);
            int seconds = ((int)gameTimer % 60);
            int minutes = ((int)gameTimer / 60);
            timerText.text = string.Format("{0:00}:{1:00}:{2:0}", minutes, seconds, miliseconds);
            // timerText.text = string.Format("{0:00}:{1:00}:{10:00}", minutes, seconds, miliseconds);

        }
        
        // if(Input.GetKeyDown(KeyCode.P))
        //     ChangeScene();
    }

    public void RestorePart()
    {
        player.transform.eulerAngles = Vector3.zero;
        partsCollected++;
        UpdateParts();
    }

    private void UpdateParts(){
        for(int i = 1; i <= partsCollected; i++){
            switch (partsCollected)
            {
                case 1:
                    playerRb.angularDrag = 1f;
                    headMovement.enabled = false;
                    legsMovement.enabled = true;
                    break;
                case 2:
                    //legsMovement.jumpForce *= 1.2f;
                    headMovement.enabled = false;
                    legsMovement.enabled = true;
                    torso.enabled = true;
                    break;
                case 3:
                    headMovement.enabled = false;
                    legsMovement.enabled = true;
                    torso.enabled = true;
                    armsMovement.enabled = true;
                    break;
            }
        }
    }

    public void ChangeScene()
    {
        currentScene++;
        if (currentScene >= SceneManager.sceneCountInBuildSettings - 1)
        {
            timerActive = false;
        }

        // Add Animation?
        SceneManager.LoadScene(currentScene);
        
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
        currentScene = 0;
        Destroy(timerText.transform.parent.gameObject);
        Destroy(endScreen.transform.parent.gameObject);  
        Destroy(this.gameObject);
    }    
}
