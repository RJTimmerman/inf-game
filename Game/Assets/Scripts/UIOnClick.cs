using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnClick : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    private PlayerMovementCC playerController;

    public float range = 2;
    public bool exitOnEsc = true;
    public bool lockPlayer = true;


    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerMovementCC>();
    }

    // Update is called once per frame
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
            if (lockPlayer) { playerController.canMove = false; Cursor.lockState = CursorLockMode.Confined; }
        }
    }

    public void CloseUI()
    {
        canvas.SetActive(false);
        if (lockPlayer) { playerController.canMove = true; Cursor.lockState = CursorLockMode.Locked; }
    }
}
