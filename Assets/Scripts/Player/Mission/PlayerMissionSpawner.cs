using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissionSpawner : MonoBehaviour
{
    [Header("Mission")]
    public GameObject missionStop;
    public GameObject missionExplosion;
    public List<GameObject> missionSpawnPoints;

    [Header("Player Helper")]
    public Transform playerTransform;
    public Transform arrowTransform;

    [Header("Debug")]
    public bool debugOnStart;

    private GameObject missionBeginObject;
    private GameObject missionEndObject;
    private bool missionBeginValidated;
    private bool missionEndValidated;

    private GameObject missionStopInstance;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (debugOnStart)
            StartMissions();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() => SelectObjectiveToPointAndSetDirection();

    public void StartMissions() =>
        SpawnMission();

    private void SelectObjectiveToPointAndSetDirection()
    {
        if (missionBeginObject == null)
            return;

        if (!missionBeginValidated)
            SetArrowDirection(missionBeginObject);
        else
            SetArrowDirection(missionEndObject);
    }

    private void SetArrowDirection(GameObject objective)
    {
        Vector3 directionToObjective = objective.transform.position -
                playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToObjective);
        arrowTransform.transform.rotation = rotation;
    }

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

        if (missionStopInstance == null)
            missionStopInstance = Instantiate(missionStop,
               missionBeginObject.transform.position,
               missionStop.transform.rotation);
        else
            missionStopInstance.transform.position = missionBeginObject.transform.position;

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
                Instantiate(missionExplosion, missionEndObject.transform.position, Quaternion.identity);
                SpawnMission();
            }
        }
        else if (other == missionBeginObject)
        {
            missionBeginValidated = true;
            Instantiate(missionExplosion, missionBeginObject.transform.position, Quaternion.identity);
            missionStopInstance.transform.position = missionEndObject.transform.position;
        }
    }
}
