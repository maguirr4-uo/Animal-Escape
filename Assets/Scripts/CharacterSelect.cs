using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public int selection = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // This is where the selection int will be set depending on the menu selection
        // Check if the option was selected, then assign the number to the selection.
        // Call StartGame once the button to start is pressed

        // Duck = 0
        // Rabbit = 1

        selection = 1;
    }

    // Update is called once per frame
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedAnimal", selection);
        // Change the scene
    }
}
