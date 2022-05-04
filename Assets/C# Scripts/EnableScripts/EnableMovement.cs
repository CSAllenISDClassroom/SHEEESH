using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class EnableMovement : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] GameObject player;
    private bool GameIsPaused = true;
    
    void Awake()
    {
        GameIsPaused = true;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void reset()
    {
        GameIsPaused = true;
    }

    public void gamePaused()
    {
        if (GameIsPaused)
        {
            playerMovement.enabled = false;
            GameIsPaused = false;
        }
        else
        {
            playerMovement.enabled = true;
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
                playerMovement.enabled = false;
                GameIsPaused = false;
            }
            else
            {
                playerMovement.enabled = true;
                GameIsPaused = true;

            }
        }
    }
}
