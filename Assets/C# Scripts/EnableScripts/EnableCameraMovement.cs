using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class EnableCameraMovement : MonoBehaviour
{
    MoveCamera cameraMovement;
    [SerializeField] GameObject camera;
    public bool GameIsPaused;
    
    void Awake()
    {
        GameIsPaused = true;
        cameraMovement = camera.GetComponent<MoveCamera>();
    }

    //public bool IsGamePaused
    public void reset()
    {
        GameIsPaused = false;
    }

    public void gamePaused()
    {
        if (GameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cameraMovement.enabled = false;
            GameIsPaused = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cameraMovement.enabled = true;
            GameIsPaused = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                cameraMovement.enabled = false;
                GameIsPaused = false;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                cameraMovement.enabled = true;
                GameIsPaused = true;

            }
        }
    }
}
