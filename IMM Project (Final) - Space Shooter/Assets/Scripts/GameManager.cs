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
    private string URL = "https://github.com/johnnylayson543/IMM_Project_Final_SpaceShooter";

    // TMPro Text Variables
    public TextMeshProUGUI gameOverText; // The game over text
    public TextMeshProUGUI scoreText; // The score text
    public TextMeshProUGUI earthLivesText; 
    private int score; // The score number
    private int earthLives;

    // Buttons
    public Button restartButton; // The restart button

    // Bool to check if the game is still active (initial state of this should set true applied at the Start() method)
    public bool isGameActive;

    // Set the Difficulty Level
    public int difficulty;

    // Call the SpawnManager Script
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>(); // Find the SpawnManager GameObject and get its component the SpawnManager script
        StartGame();
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

    public void GoToSpaceShooter2()
    {
        SceneManager.LoadScene("SpaceShooter2"); // Go to the controls menu
    }

    public void GoToSpaceShooter3()
    {
        SceneManager.LoadScene("SpaceShooter3"); // Go to the difficulty menu
    }

    /*public void EasyDifficulty()
    {
        SceneManager.LoadScene("SpaceShooter1"); // Go to the main game (Play Game)
        difficulty = 100;
    }*/

    public void loadURL()
    {
        Application.OpenURL(URL); // Open the URL which contains the Game Files and Assets from the GitHub Repo
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; // Pass on whatever added score (scoreToAdd) passed via parameters to the score variable
        scoreText.text = "Score: " + score; // Create and concatenate the score number to the Score text on the scoreText.text (the text element)
    }

    public void UpdateEarthLivesCounter(int livesLeft)
    {
        // updates the Earth lives text element
        this.earthLives = livesLeft;
        earthLivesText.text = "Health: " + livesLeft;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true); // If game is over, make the game over text visible
        restartButton.gameObject.SetActive(true); // If game is over, make the restart button visible
        isGameActive = false; // Set the isGameActive to false, in which if applied to a if or while condition, everything stops if the bool value is false
    }

    public void StartGame()
    {
        // feeds the initial state of the Earth to the Earth lives text element
        earthLives = EarthController.getImpactLimit();
        earthLivesText.text = "Health: " + earthLives.ToString();

        // feeds the initial score to the score text element
        scoreText.text = "Score: " + "0";

        StartCoroutine(spawnManager.SpawnEnemy());
    }

}
