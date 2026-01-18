using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HoverCarController))]
public class SpeedBoostController : MonoBehaviour
{
    private HoverCarController hoverCar;
    private Coroutine boostCoroutine;
    private float originalMaxSpeed;

    private void Awake()
    {
        hoverCar = GetComponent<HoverCarController>();
    }

    private void Start()
    {
        originalMaxSpeed = hoverCar.MaxSpeed;
    }

    public void ApplyBoost(float multiplier, float duration)
    {
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);
        }

        boostCoroutine = StartCoroutine(BoostSequence(multiplier, duration));
    }

    private IEnumerator BoostSequence(float multiplier, float duration)
    {
        var field = typeof(HoverCarController).GetField("maxSpeed",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        field?.SetValue(hoverCar, originalMaxSpeed * multiplier);

        yield return new WaitForSeconds(duration);

        field?.SetValue(hoverCar, originalMaxSpeed);

        boostCoroutine = null;
    }
}
