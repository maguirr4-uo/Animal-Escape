using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] characters;
    public Transform spawn;

    void Start()
    {
        int selection = PlayerPrefs.GetInt("selectedAnimal");
        GameObject playerChar = characters[1];
        GameObject player = Instantiate(playerChar, spawn.position, Quaternion.identity);
    }
}
