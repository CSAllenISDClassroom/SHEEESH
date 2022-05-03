using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnPoint;

    

    void OnTriggerEnter(Collider other)
    {


            Player.transform.position = respawnPoint.transform.position;

    }


}
