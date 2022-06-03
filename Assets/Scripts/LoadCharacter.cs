using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;
using TMPro;


public class LoadCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] characters;
    public GameObject[] FarmerTypes;
    public Transform spawn;

    public GameObject player;
    public GameObject cameraRoot;

    public GameObject self;

    // Farmer Spawn management
    private float time;
    private float interval = 1.0f;
    private Vector3 farmerSpawnPos;

    // Wave Management
    private int waveCount = 0;
    private bool waveCleared = false;
    private int curHumanCount = 0;
    private int tarHumanCount = 0;

    public int humansDown = 0;

    // Text
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;

    int score = 0;

    void Start()
    {
        int selection = PlayerPrefs.GetInt("selectedAnimal");
        GameObject playerChar = characters[CharacterSelect.selection];
        GameObject playerInstance = Instantiate(playerChar, spawn.position, Quaternion.identity);

        playerInstance.tag = "Player";

        player.transform.parent = playerInstance.transform;

        // Set Cinemachine for new object
        cameraRoot.transform.parent = playerInstance.transform;
        playerInstance.GetComponent<ThirdPersonController>().CinemachineCameraTarget = cameraRoot;

        waveCount = 1;
        tarHumanCount = waveCount * 5;

        StartCoroutine(DisplayTransitionText());
    }

    private void Update()
    {
        ManageWave();

        scoreText.text = "Score: " + score.ToString();
    }

    private void ManageWave()
    {        
        if (waveCleared)
        {
            // Load text that tells player they cleared wave for 3 secs
            waveCount++;

            StartCoroutine(DisplayTransitionText());

            // Load text that tells player new wave is starting
            curHumanCount = 0;
            tarHumanCount = waveCount * 5;

            waveCleared = false;
        }
        else        
            while(curHumanCount < tarHumanCount)
            {
                // Load farmers randomly
                time += Time.deltaTime;
                while (time >= interval)
                {
                    LoadFarmer();
                    time -= interval;
                }
            }

            // Check if all humans are dead
            if(humansDown == tarHumanCount)
            {
                waveCleared = true;
                score += humansDown * 100;
                humansDown = 0;
            }
    }

   private void LoadFarmer()
   {
        bool hit = RandomNavmeshLocation(player.transform.position, 25.0f, out farmerSpawnPos);

        if(hit)
        {
            int type = Random.Range(0, 3);
            GameObject FarmerInstance = Instantiate(FarmerTypes[type], farmerSpawnPos, Quaternion.identity);

            FarmerInstance.GetComponent<HumanController>().gameManager = self;
            curHumanCount++;
        }
   }

    // Following function from
    // https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html

    bool RandomNavmeshLocation(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


    IEnumerator DisplayTransitionText()
    {
        // Displays the text between waves
        waveText.text = "Wave " + waveCount.ToString() + "!";
        yield return new WaitForSeconds(5);
        waveText.text = "";
    }

}
