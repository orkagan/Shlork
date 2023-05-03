using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When loading and unloading scenes
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void NextScene()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Load the next scene after the current scene
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
    public void Restart()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Load the next scene after current scene
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void Quit()
    {
        // Closes the game
        Application.Quit();
    }

    public void Menu()
    {
        // We return to the menu
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        // When the player hits escape
        if (Input.GetKey("escape"))
        {
            // We return to the menu
            SceneManager.LoadScene("Menu");
        }
    }
}
