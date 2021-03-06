using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnClick : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    private PlayerControllerCC playerController;
    public MouseRotate cameraScript;

    public float range = 2;
    public bool exitOnEsc = true;
    public bool lockPlayer = true;

    
    void Awake()
    {
        playerController = player.GetComponent<PlayerControllerCC>();
    }

    void Update()
    {
        if (exitOnEsc && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= range)
        {
            canvas.SetActive(true);
            if (lockPlayer) { playerController.canMove = false; cameraScript.locked = true; Cursor.lockState = CursorLockMode.Confined; }
        }
    }

    public void CloseUI()
    {
        canvas.SetActive(false);
        if (lockPlayer) { playerController.canMove = true; cameraScript.locked = false; Cursor.lockState = CursorLockMode.Locked; }
    }
}
