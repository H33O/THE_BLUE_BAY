using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UnderwaterFogController : MonoBehaviour
{
    [Header("Fog Settings")]
    [SerializeField] private bool enableUnderwaterFog = true;
    
    [Tooltip("Densité du fog sous l'eau")]
    [SerializeField] private float underwaterFogDensity = 0.08f;
    
    [Tooltip("Couleur du fog sous l'eau")]
    [SerializeField] private Color underwaterFogColor = new Color(0.1f, 0.3f, 0.5f, 1f);
    
    [Tooltip("Distance de début du fog")]
    [SerializeField] private float fogStartDistance = 5f;
    
    [Tooltip("Distance de fin du fog")]
    [SerializeField] private float fogEndDistance = 50f;
    
    [Header("References")]
    [SerializeField] private UnderwaterEffectController underwaterController;
    
    private bool originalFogEnabled;
    private Color originalFogColor;
    private float originalFogDensity;
    private FogMode originalFogMode;
    private float originalFogStart;
    private float originalFogEnd;
    
    private bool fogSettingsSaved = false;
    
    private void Start()
    {
        if (underwaterController == null)
        {
            underwaterController = GetComponent<UnderwaterEffectController>();
        }
        
        if (underwaterController == null)
        {
            Debug.LogWarning("UnderwaterFogController: UnderwaterEffectController non trouvé !");
        }
        
        SaveOriginalFogSettings();
    }
    
    private void SaveOriginalFogSettings()
    {
        if (!fogSettingsSaved)
        {
            originalFogEnabled = RenderSettings.fog;
            originalFogColor = RenderSettings.fogColor;
            originalFogDensity = RenderSettings.fogDensity;
            originalFogMode = RenderSettings.fogMode;
            originalFogStart = RenderSettings.fogStartDistance;
            originalFogEnd = RenderSettings.fogEndDistance;
            fogSettingsSaved = true;
        }
    }
    
    private void Update()
    {
        if (!enableUnderwaterFog || underwaterController == null)
            return;
        
        bool isUnderwater = underwaterController.IsUnderwater;
        
        if (isUnderwater)
        {
            ApplyUnderwaterFog();
        }
        else
        {
            RestoreOriginalFog();
        }
    }
    
    private void ApplyUnderwaterFog()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = underwaterFogColor;
        RenderSettings.fogStartDistance = fogStartDistance;
        RenderSettings.fogEndDistance = fogEndDistance;
    }
    
    private void RestoreOriginalFog()
    {
        if (!fogSettingsSaved)
            return;
        
        RenderSettings.fog = originalFogEnabled;
        RenderSettings.fogColor = originalFogColor;
        RenderSettings.fogDensity = originalFogDensity;
        RenderSettings.fogMode = originalFogMode;
        RenderSettings.fogStartDistance = originalFogStart;
        RenderSettings.fogEndDistance = originalFogEnd;
    }
    
    private void OnDisable()
    {
        RestoreOriginalFog();
    }
    
    private void OnDestroy()
    {
        RestoreOriginalFog();
    }
}
