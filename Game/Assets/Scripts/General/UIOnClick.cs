using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnClick : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private Transform player;
    private PlayerControllerCC playerController;
    private MouseRotate cameraScript;

    [SerializeField] private float range = 2;
    [SerializeField] private bool exitOnEsc = true;
    [SerializeField] private bool lockPlayer = true;

    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerControllerCC>();
        cameraScript = player.GetComponentInChildren<MouseRotate>();
    }

    private void Update()
    {
        if (exitOnEsc && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(player.position, transform.position) <= range)
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
