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
    private int earthLives; // Earth lives remaining
    Scene currentScene; // the current scene as an object

    // Buttons
    public Button restartButton; // The restart button

    // Bool to check if the game is still active (initial state of this should set true applied at the Start() method)
    public static bool isGameActive = true;

    // Set the game's difficulty
    public static int difficulty;

    // Call the SpawnManager Script
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene(); // gets the current scene from the scene
        isGameActive = currentScene.name == "SpaceShooter1";
        // checks if the game has been activated before letting the sequence start
        if (isGameActive)
        {
            spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>(); // Find the SpawnManager GameObject and get its component the SpawnManager script
            StartGame(difficulty);
        }
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

    public void loadURL()
    {
        Application.OpenURL(URL); // Open the URL which contains the Game Files and Assets from the GitHub Repo
    }

    public void EasyDifficulty()
    {
        SceneManager.LoadScene("SpaceShooter1"); // Go to the main game (Play Game) in Easy Difficulty
        difficulty = 1; // Set the initial value of difficulty to 1
    }

    public void MediumDifficulty()
    {
        SceneManager.LoadScene("SpaceShooter1"); // Go to the main game (Play Game) in Medium Difficulty
        difficulty = 2; // Set the initial value of difficulty to 2
    }

    public void HardDifficulty()
    {
        SceneManager.LoadScene("SpaceShooter1"); // Go to the main game (Play Game) in Hard Difficulty
        difficulty = 3; // Set the initial value of difficulty to 3
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

    public void StartGame(int difficulty)
    {
        // feeds the initial state of the Earth to the Earth lives text element
        earthLives = EarthController.getImpactLimit();
        earthLivesText.text = "Health: " + earthLives.ToString();

        // feeds the initial score to the score text element
        scoreText.text = "Score: " + "0";

        spawnManager.setSpawnRate(difficulty); // calls a function from the spawn manager to set the spawn rate from the difficulty level
        StartCoroutine(spawnManager.SpawnEnemy()); // starts the coroutine to spawn enemies using the spawn manager 
    }

    

}
