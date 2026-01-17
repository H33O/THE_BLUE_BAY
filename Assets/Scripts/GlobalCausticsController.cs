using UnityEngine;

public class GlobalCausticsController : MonoBehaviour
{
    [Header("Caustiques Globales")]
    [Tooltip("Activer les caustiques globales")]
    [SerializeField] private bool enableCaustics = true;
    
    [Tooltip("Intensité des caustiques")]
    [SerializeField] private float causticsIntensity = 0.5f;
    
    [Tooltip("Vitesse d'animation des caustiques")]
    [SerializeField] private float causticsSpeed = 0.3f;
    
    [Tooltip("Échelle des caustiques")]
    [SerializeField] private float causticsScale = 1.5f;
    
    [Tooltip("Couleur des caustiques")]
    [SerializeField] private Color causticsColor = new Color(0.5f, 0.8f, 1.0f, 1.0f);
    
    [Header("Zone d'Application")]
    [Tooltip("Niveau Y de l'eau - Les caustiques apparaissent seulement en dessous")]
    [SerializeField] private float waterLevel = 0f;
    
    [Tooltip("Distance de transition (fade) autour du water level")]
    [SerializeField] private float fadeDistance = 2f;
    
    private static readonly int CausticsIntensityID = Shader.PropertyToID("_GlobalCausticsIntensity");
    private static readonly int CausticsSpeedID = Shader.PropertyToID("_GlobalCausticsSpeed");
    private static readonly int CausticsScaleID = Shader.PropertyToID("_GlobalCausticsScale");
    private static readonly int CausticsColorID = Shader.PropertyToID("_GlobalCausticsColor");
    private static readonly int CausticsTimeID = Shader.PropertyToID("_GlobalCausticsTime");
    private static readonly int WaterLevelID = Shader.PropertyToID("_GlobalWaterLevel");
    private static readonly int FadeDistanceID = Shader.PropertyToID("_GlobalCausticsFadeDistance");
    
    private void Update()
    {
        if (enableCaustics)
        {
            Shader.SetGlobalFloat(CausticsIntensityID, causticsIntensity);
            Shader.SetGlobalFloat(CausticsSpeedID, causticsSpeed);
            Shader.SetGlobalFloat(CausticsScaleID, causticsScale);
            Shader.SetGlobalColor(CausticsColorID, causticsColor);
            Shader.SetGlobalFloat(CausticsTimeID, Time.time);
            Shader.SetGlobalFloat(WaterLevelID, waterLevel);
            Shader.SetGlobalFloat(FadeDistanceID, fadeDistance);
        }
        else
        {
            Shader.SetGlobalFloat(CausticsIntensityID, 0f);
        }
    }
    
    public void SetIntensity(float intensity)
    {
        causticsIntensity = Mathf.Clamp(intensity, 0f, 2f);
    }
    
    public void SetWaterLevel(float level)
    {
        waterLevel = level;
    }
    
    public bool IsEnabled => enableCaustics;
    public float Intensity => causticsIntensity;
    public float WaterLevel => waterLevel;
}
