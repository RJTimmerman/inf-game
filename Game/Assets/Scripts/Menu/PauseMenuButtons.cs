using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public void ResumeGame()
    {
        GetComponentInParent<OpenPauseMenu>().ResumeGame();
    }

    public void LoadCheckpoint()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        ResumeGame();
        SaveSystem.DeleteSave();
        LoadCheckpoint();
    }

    public void MainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}