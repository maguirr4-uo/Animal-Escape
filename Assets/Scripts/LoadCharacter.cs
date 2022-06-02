using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class LoadCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] characters;
    public Transform spawn;

    //public GameObject player;
    public GameObject cameraRoot;

    void Start()
    {
        int selection = PlayerPrefs.GetInt("selectedAnimal");
        GameObject playerChar = characters[0];
        GameObject playerInstance = Instantiate(playerChar, spawn.position, Quaternion.identity);

        playerInstance.tag = "Player";

        //player.transform.parent = playerInstance.transform;

        // Set Cinemachine for new object
        cameraRoot.transform.parent = playerInstance.transform;
        playerInstance.GetComponent<ThirdPersonController>().CinemachineCameraTarget = cameraRoot;

    }
}
