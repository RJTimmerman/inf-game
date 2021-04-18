using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    private GameObject pauseMenu;

    /*private Transform player;
    private PlayerControllerCC playerController;
    private MouseRotate cameraScript;*/


    private void Awake()
    {
        pauseMenu = GameObject.Find("Pause Menu"); pauseMenu.SetActive(false);
        /*player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerControllerCC>();
        cameraScript = player.GetComponentInChildren<MouseRotate>();*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) PauseGame();
            else ResumeGame();
        }
    }

    private void PauseGame()
    {
        /*playerController.canMove = false; cameraScript.locked = true;*/ Cursor.lockState = CursorLockMode.Confined; Cursor.visible = true;
        //print(currentGun.gameObject.name); currentGun.active = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}
