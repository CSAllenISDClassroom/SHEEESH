using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class EnableGunFire : MonoBehaviour
{
    GrapplingGun suckGun;
    Gun damageGun;
    [SerializeField] GameObject currentGun;
    private bool GameIsPaused = true;
    
    void Awake()
    {
        GameIsPaused = true;
        suckGun = currentGun.GetComponent<GrapplingGun>();
        //damageGun = currentGun.GetComponent<Gun>();
    }

    public void gamePaused()
    {
        if (GameIsPaused)
        {
            suckGun.enabled = false;
            //damageGun.enabled = false;
            GameIsPaused = false;
        }
        else
        {
            suckGun.enabled = true;
           //damageGun.enabled = true;
            GameIsPaused = true;

        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
               //damageGun.enabled = false;
               suckGun.enabled = false;
                GameIsPaused = false;
            }
            else
            {
               //damageGun.enabled = true;
                suckGun.enabled = true;
                GameIsPaused = true;

            }
        }
    }
}
