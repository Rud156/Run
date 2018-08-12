using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissionSpawner : MonoBehaviour
{
    public List<GameObject> missionSpawnPoints;
    public bool debugOnStart;

    private GameObject missionBeginObject;
    private GameObject missionEndObject;
    private bool missionBeginValidated;
    private bool missionEndValidated;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (debugOnStart)
            StartMissions();
    }

    public void StartMissions() =>
        SpawnMission();


    private void SpawnMission()
    {
        int randomNumber = Random.Range(0, 1000);
        int randomStartIndex = randomNumber % missionSpawnPoints.Count;

        randomNumber = Random.Range(0, 1000);
        int randomEndIndex = randomNumber % missionSpawnPoints.Count;

        while (randomEndIndex == randomStartIndex)
        {
            randomNumber = Random.Range(0, 1000);
            randomEndIndex = randomNumber % missionSpawnPoints.Count;
        }

        missionBeginObject = missionSpawnPoints[randomStartIndex];
        missionEndObject = missionSpawnPoints[randomEndIndex];

        missionBeginObject.GetComponent<Renderer>().material.color = Color.red;
        missionEndObject.GetComponent<Renderer>().material.color = Color.green;

        missionBeginValidated = false;
        missionEndValidated = false;
    }

    public void CheckMissionBeginAndEnd(GameObject other)
    {
        if (missionBeginValidated)
        {
            if (!missionEndValidated && other == missionEndObject)
            {
                missionEndValidated = true;
                missionEndObject.GetComponent<Renderer>().material.color = Color.white;
                SpawnMission();
            }
        }
        else if (other == missionBeginObject)
        {
            missionBeginValidated = true;
            missionBeginObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
