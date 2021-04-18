using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenButtons : MonoBehaviour
{
    private GameObject victoryScreen;

    private Transform player;
    private PlayerControllerCC playerController;
    private MouseRotate cameraScript;


    private void Awake()
    {
        victoryScreen = GameObject.Find("Victory Screen");
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerControllerCC>();
        cameraScript = player.GetComponentInChildren<MouseRotate>();
    }

    public void ContinueWalking()
    {
        playerController.canMove = true; cameraScript.locked = false; Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        victoryScreen.SetActive(false);
    }

    public void LoadCheckpoint()
    {
        ContinueWalking();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        ContinueWalking();
        SaveSystem.DeleteSave();
        LoadCheckpoint();
    }

    public void MainMenu()
    {
        ContinueWalking();
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}