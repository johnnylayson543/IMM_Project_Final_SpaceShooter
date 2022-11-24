using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This script manages the main elements of the game such as Start, Restart and GameOver.
public class GameManager : MonoBehaviour
{

    // URL String
    public string URL = "https://github.com/johnnylayson543/IMM_Project_Alpha_SpaceShooter";

    // TMPro Text Variables
    public TextMeshProUGUI gameOverText; // The game over text

    // Buttons
    public Button restartButton; // The restart button

    // Bool to check if the game is still active (initial state of this should set true applied at the Start() method)
    public bool isGameActive;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSpaceShooter0()
    {
        SceneManager.LoadScene("SpaceShooter0"); // Go to the main menu 
    }
    public void GoToSpaceShooter1()
    {
        SceneManager.LoadScene("SpaceShooter1"); // Go to the main game (Play Game)
    }

    public void loadURL()
    {
        Application.OpenURL(URL); // Open the URL which contains the Game Files and Assets from the GitHub Repo
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true); // If game is over, make the game over text visible
        restartButton.gameObject.SetActive(true); // If game is over, make the restart button visible
        isGameActive = false; // Set the isGameActive to false, in which if applied to a if or while condition, everything stops if the bool value is false
    }

}
