using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioActionController : MonoBehaviour
{
    [Header("GameObjects Affecting")]
    public Transform policeHolderTransform;
    public Transform playerTransform;

    [Header("Audio Pitch")]
    public float maxAudioPitch = 2.5f;
    public float minAudioPitch = 0.5f;
    [Range(0, 1)]
    public float pitchLerpRatio = 0.7f;

    [Header("Player Distance")]
    public float minDistanceToPlayer = 5f;
    public float maxDistanceToPlayer = 15f;

    [Header("Misc")]
    public int maxVehiclesToConsider = 7;

    private AudioSource audioSource;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() => audioSource = GetComponent<AudioSource>();

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() => CalculateAudioPitch();

    private void CalculateAudioPitch()
    {
        if (playerTransform == null)
            return;

        int totalPolice = policeHolderTransform.childCount;
        totalPolice = totalPolice > maxVehiclesToConsider ? maxVehiclesToConsider : totalPolice;
        float currentTotal = 0;

        for (int i = 0; i < totalPolice; i++)
        {
            float currentDistance = Vector3.Distance(playerTransform.position,
                policeHolderTransform.GetChild(i).position);

            if (currentDistance <= maxDistanceToPlayer && currentDistance >= minDistanceToPlayer)
            {
                currentTotal += ExtensionFunctions.Map(currentDistance,
                    minDistanceToPlayer, maxDistanceToPlayer,
                    maxAudioPitch, minAudioPitch);
            }
            else if (currentDistance < minDistanceToPlayer)
                currentTotal += maxAudioPitch;
        }

        float maxValuePossible = maxVehiclesToConsider * maxAudioPitch;
        float audioPitchRatio = currentTotal / maxValuePossible;

        float currentPitch = audioSource.pitch;
        float expectedPitch = ExtensionFunctions.Map(audioPitchRatio, 0, 1, minAudioPitch, maxAudioPitch);

        audioSource.pitch = Mathf.Lerp(currentPitch, expectedPitch, pitchLerpRatio);
    }
}
