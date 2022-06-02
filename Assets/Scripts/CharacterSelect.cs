using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public static int selection = 0;

    public void duckSelect()
    {
        selection = 0;
        SceneManager.LoadScene("MapSelection");

    }
    public void rabbitSelect()
    {
        selection = 1;
        SceneManager.LoadScene("MapSelection");

    }
}
