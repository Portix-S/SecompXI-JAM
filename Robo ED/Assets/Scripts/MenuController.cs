using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, howToPlay, credits;

    private void Start() {
        howToPlay.SetActive(false);
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void Menu(){
        SceneManager.LoadScene(0);
    }
    public void Quit(){
        Application.Quit();
    }

    public void BackToMain(){
        howToPlay.SetActive(false);
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenHowToPlay(){
        credits.SetActive(false);
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void OpenCredits(){
        howToPlay.SetActive(false);
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
}
