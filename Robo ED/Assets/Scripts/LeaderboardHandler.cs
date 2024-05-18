using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// using LitJson;  

public class LeaderboardHandler : MonoBehaviour
{
    [Header("Leaderboard Settings")]
    [SerializeField] private List<LeaderboardEntry> leaderboard = new();
    [SerializeField] private List<TextMeshProUGUI> entryText;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private GameObject entryScreen;
    private float gameTimer;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetTimer(float time)
    {
        gameTimer = time;
        int miliseconds = ((int)(gameTimer * 100) % 100);
        int seconds = ((int)gameTimer % 60);
        int minutes = ((int)gameTimer / 60);
        timer.text = "Seu tempo foi: " + string.Format("{0:00}:{1:00}:{2:0}", minutes, seconds, miliseconds);
    }
    
    public void Top5LeaderBoard(TextMeshProUGUI newName)
    {
        // PlayerPrefs.DeleteKey("Leaderboard");
        // PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            leaderboard.Add(new LeaderboardEntry(){name = newName.text, time = gameTimer});
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    string name = PlayerPrefs.GetString("names " + i);
                    Debug.Log(name + " names " + i);
                    float time = PlayerPrefs.GetFloat("timer " + i);
                    if(name == "") break;
                    leaderboard.Add(new LeaderboardEntry() {name = name, time = time});
                }
                catch (Exception e)
                {
                    break;
                }
            }
            
            leaderboard.Sort((a, b) => a.time.CompareTo(b.time));
            if (leaderboard.Count > 5)
            {
                leaderboard.RemoveAt(5);
            }
            
            Debug.Log(leaderboard[0].name + " - " + leaderboard[0].time);
            // convert leaderboard list to json and save as playerprefs
            // JsonData json = JsonMapper.ToJson(laderboard);

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    PlayerPrefs.SetString("names " + i, leaderboard[i].name);
                    PlayerPrefs.SetFloat("timer " + i, leaderboard[i].time);
                }
                catch (Exception e)
                {
                    break;
                }
            }
            
            // json = JsonUtility.ToJson(leaderboard);
            // PlayerPrefs.SetString("Leaderboard", json);
        }
        else
        {
            Debug.Log("Name " + newName.text + " Seconds " + gameTimer);
            leaderboard.Add(new LeaderboardEntry() {name = newName.text, time = gameTimer});
            PlayerPrefs.SetString("names 0", newName.text);
            PlayerPrefs.SetFloat("timer 0", gameTimer);
            PlayerPrefs.SetString("Leaderboard", "true");
        }
        // Check if showing in correct place
        for (int i = 0; i < leaderboard.Count; i++)
        { 
            float time = leaderboard[i].time;
            int miliseconds = ((int)(time * 1000) % 1000);
            int seconds = ((int)time % 60);
            int minutes = ((int)time / 60);
            
            entryText[i].text = leaderboard[i].name + " - " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);;
        }
        entryScreen.SetActive(false);
    }
    
    [Serializable]
    public class LeaderboardEntry
    {
        public string name;
        public float time;
    }
}
