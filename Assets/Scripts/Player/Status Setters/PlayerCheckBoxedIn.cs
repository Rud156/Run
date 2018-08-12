using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCheckBoxedIn : MonoBehaviour
{
    public GameObject policeHolder;
    public float minVelocityThreshold;
    public float minPoliceRange;
    public int maxBustedAmount;

    [Header("Busted")]
    public Animator bustedTextHolder;
    public Slider bustedSlider;
    public GameObject bustedSliderHolder;

    private Rigidbody playerRB;
    private float bustedAmount;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        bustedAmount = 0;
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        CheckBusted();
        UpdateBustedUI();
    }

    private void CheckBusted()
    {
        GameObject nearestPolice = GetNearestPoliceVehicle();
        float normalizedVelocity = Mathf.Abs(playerRB.velocity.magnitude);

        if (nearestPolice == null)
            bustedAmount = 0;
        else
        {
            if (normalizedVelocity < minVelocityThreshold)
                bustedAmount += 1;
            else
                bustedAmount -= 1;
        }

        bustedAmount = bustedAmount < 0 ? 0 : bustedAmount;

        if (bustedAmount >= maxBustedAmount)
            bustedTextHolder.SetTrigger(AnimatorVariables.FadeIn);
    }

    private void UpdateBustedUI()
    {
        if (bustedAmount <= 0)
            bustedSliderHolder.SetActive(false);
        else
            bustedSliderHolder.SetActive(true);

        float bustedRatio = bustedAmount / maxBustedAmount;
        bustedSlider.value = bustedRatio;
    }

    private GameObject GetNearestPoliceVehicle()
    {
        int totalPoliceVehicles = policeHolder.transform.childCount;
        float shortestDistance = minPoliceRange;
        GameObject nearestPolice = null;

        for (int i = 0; i < totalPoliceVehicles; i++)
        {
            float distanceToPolice = Vector3.Distance(playerRB.transform.position,
                policeHolder.transform.GetChild(i).position);

            if (distanceToPolice <= shortestDistance)
            {
                shortestDistance = distanceToPolice;
                nearestPolice = policeHolder.transform.GetChild(i).gameObject;
            }
        }

        return nearestPolice;
    }
}
