using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerMissionSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct MissionPoint
    {
        public GameObject missionSpawnPoint;
        public Transform missionExplosionPoint;
    };

    [Header("Mission")]
    public GameObject missionIndicator;
    public GameObject missionExplosion;
    public List<MissionPoint> missionPoints;
    public float explosionWaitTime = 0.5f;

    [Header("Player Helper")]
    public Transform playerTransform;
    public Transform arrowTransform;

    [Header("Camera Shaker Stats")]
    public float magnitude = 5;
    public float roughness = 5;
    public float fadeInTime = 0.15f;
    public float fadeOutTime = 0.15f;

    [Header("Global Effectors")]
    public EnemySpawner policeSpawner;
    public Light directionalLight;
    public float lightIntensityDecreaseRate = 0.01f;
    public float policeSpawnerTimeDecreaseRate = 0.3f;

    private MissionPoint missionBeginObject;
    private MissionPoint missionEndObject;
    private bool missionBeginValidated;
    private bool missionEndValidated;

    private GameObject missionIndicatorInstance;
    private bool firstMissionActivated;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        firstMissionActivated = false;
        StartMissions();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!firstMissionActivated)
            return;

        float currentLightIntensity = directionalLight.intensity;
        if (currentLightIntensity > 0.3)
            directionalLight.intensity -= lightIntensityDecreaseRate * Time.deltaTime;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() => SelectObjectiveToPointAndSetDirection();

    public void StartMissions() =>
        SpawnMission();

    private void SelectObjectiveToPointAndSetDirection()
    {
        if (missionBeginObject.Equals(default(MissionPoint)))
            return;

        if (!missionBeginValidated)
            SetArrowDirection(missionBeginObject.missionSpawnPoint);
        else
            SetArrowDirection(missionEndObject.missionSpawnPoint);
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
        int randomStartIndex = randomNumber % missionPoints.Count;

        randomNumber = Random.Range(0, 1000);
        int randomEndIndex = randomNumber % missionPoints.Count;

        while (randomEndIndex == randomStartIndex)
        {
            randomNumber = Random.Range(0, 1000);
            randomEndIndex = randomNumber % missionPoints.Count;
        }

        missionBeginObject = missionPoints[randomStartIndex];
        missionEndObject = missionPoints[randomEndIndex];

        if (missionIndicatorInstance == null)
            missionIndicatorInstance = Instantiate(missionIndicator,
               missionBeginObject.missionSpawnPoint.transform.position,
               missionIndicator.transform.rotation);
        else
            missionIndicatorInstance.transform.position = missionBeginObject
                .missionSpawnPoint.transform.position;

        missionBeginValidated = false;
        missionEndValidated = false;
    }

    public void CheckMissionBeginAndEnd(GameObject other)
    {
        if (missionBeginValidated)
        {
            if (!missionEndValidated && other == missionEndObject.missionSpawnPoint)
                MissionEnd();
        }
        else if (other == missionBeginObject.missionSpawnPoint)
            MissionBegin();
    }

    private void MissionBegin()
    {
        if (!firstMissionActivated)
        {
            firstMissionActivated = true;
            policeSpawner.StartSpawn();
        }

        missionBeginValidated = true;
        directionalLight.intensity = 1.5f;

        StartCoroutine(CauseExplosionAndShake(missionBeginObject.missionExplosionPoint));

        missionIndicatorInstance.transform.position = missionEndObject.missionSpawnPoint.transform.position;
    }

    private void MissionEnd()
    {
        missionEndValidated = true;

        float currentWaitTime = policeSpawner.waitBeteweenEffectAndSpawn;
        currentWaitTime = currentWaitTime <= 0.5 ? currentWaitTime :
            currentWaitTime - policeSpawnerTimeDecreaseRate;
        policeSpawner.waitBeteweenEffectAndSpawn -= currentWaitTime;

        SpawnMission();
    }

    private IEnumerator CauseExplosionAndShake(Transform explosionPoint)
    {
        int randomNumber = (Random.Range(0, 1000) % 3) + 1;

        for (int i = 0; i < randomNumber; i++)
        {
            Instantiate(missionExplosion, explosionPoint.transform.position,
                      Quaternion.identity);
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            yield return new WaitForSeconds(explosionWaitTime);
        }
    }
}
