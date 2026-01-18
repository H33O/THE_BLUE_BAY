using UnityEngine;

public class RingGlow : MonoBehaviour
{
    [Header("Glow Effect")]
    public bool enablePulse = true;
    public float pulseSpeed = 2f;
    public float minIntensity = 1f;
    public float maxIntensity = 3f;

    [Header("Rotation")]
    public bool rotate = true;
    public float rotationSpeed = 30f;
    public Vector3 rotationAxis = Vector3.up;

    private Material material;
    private Color emissionColor;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;

            if (material.HasProperty("_EmissionColor"))
            {
                emissionColor = material.GetColor("_EmissionColor");
            }
        }
    }

    private void Update()
    {
        if (rotate)
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }

        if (enablePulse && material != null)
        {
            float intensity = Mathf.Lerp(minIntensity, maxIntensity,
                (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);

            material.SetColor("_EmissionColor", emissionColor * intensity);
        }
    }
}
