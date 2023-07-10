using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{


    public int currentLevel = 1;

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Currentlevel", 1);
       

    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt("Currentlevel", currentLevel);
        PlayerPrefs.Save();

        // Load the current level scene
        LoadCurrentLevelScene();
    }

    public void GoToNextLevel()
    {
        // Increment the current level by one
        currentLevel++;
        PlayerPrefs.SetInt("Currentlevel", currentLevel);
        PlayerPrefs.Save();

        // Load the next level scene
        LoadCurrentLevelScene();
    }

    public void LoadCurrentLevelScene()
    {
        string sceneName = "Level" + currentLevel;

        // Load the current level scene
        SceneManager.LoadScene(sceneName);
    }

}
