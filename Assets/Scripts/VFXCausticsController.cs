using UnityEngine;
using UnityEngine.VFX;

public class VFXCausticsController : MonoBehaviour
{
    [Header("VFX Reference")]
    [Tooltip("Le Visual Effect Graph des caustiques")]
    public VisualEffect causticsVFX;

    [Header("Water Settings")]
    [Tooltip("Niveau Y de la surface de l'eau")]
    public float waterLevel = 0f;

    [Tooltip("Distance verticale couverte par l'effet")]
    public float effectDepth = 50f;

    [Header("Caustics Settings")]
    [Tooltip("Intensité des caustiques")]
    [Range(0f, 2f)]
    public float intensity = 1f;

    [Tooltip("Vitesse d'animation")]
    [Range(0f, 2f)]
    public float animationSpeed = 0.5f;

    [Tooltip("Échelle des motifs")]
    [Range(0.5f, 5f)]
    public float scale = 1.5f;

    [Tooltip("Couleur des caustiques")]
    public Color causticsColor = new Color(0.4f, 0.8f, 1f, 1f);

    [Header("Activation")]
    [Tooltip("Activer les caustiques")]
    public bool enableCaustics = true;

    void Start()
    {
        if (causticsVFX == null)
        {
            causticsVFX = GetComponent<VisualEffect>();
        }

        UpdateVFXProperties();
    }

    void Update()
    {
        UpdateVFXProperties();
    }

    public void UpdateVFXProperties()
    {
        if (causticsVFX == null) return;

        causticsVFX.SetFloat("Intensity", enableCaustics ? intensity : 0f);
        causticsVFX.SetFloat("AnimationSpeed", animationSpeed);
        causticsVFX.SetFloat("Scale", scale);
        causticsVFX.SetVector4("CausticsColor", causticsColor);
        causticsVFX.SetFloat("WaterLevel", waterLevel);
        causticsVFX.SetFloat("EffectDepth", effectDepth);
    }

    public void SetIntensity(float value)
    {
        intensity = Mathf.Clamp01(value);
        UpdateVFXProperties();
    }

    public void SetWaterLevel(float level)
    {
        waterLevel = level;
        UpdateVFXProperties();
    }

    public void Enable(bool enable)
    {
        enableCaustics = enable;
        UpdateVFXProperties();
    }
}
