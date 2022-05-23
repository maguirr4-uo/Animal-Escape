using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MapSelection");
    }

    public void FarmLoad()
    {
        SceneManager.LoadScene("FarmScene");
    }

    public void TownLoad()
    {
        SceneManager.LoadScene("TownScene");
    }

    public void ForestLoad()
    {
        SceneManager.LoadScene("ForestScene");
    }

    public void Instrcutions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
