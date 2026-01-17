using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UnderwaterEffectController : MonoBehaviour
{
    [Header("Détection Eau")]
    [Tooltip("Niveau Y en dessous duquel le joueur est considéré sous l'eau")]
    [SerializeField] private float waterLevel = 0f;
    
    [Tooltip("Référence au joueur (HoverCar)")]
    [SerializeField] private Transform playerTransform;
    
    [Header("Post-Processing")]
    [Tooltip("Volume de post-processing pour les effets sous-marins")]
    [SerializeField] private Volume underwaterVolume;
    
    [Tooltip("Vitesse de transition des effets (secondes)")]
    [SerializeField] private float transitionSpeed = 1f;
    
    [Header("Caustiques VFX")]
    [Tooltip("Controller de caustiques VFX")]
    [SerializeField] private VFXCausticsController vfxCausticsController;
    
    [Tooltip("Intensité des caustiques sous l'eau")]
    [SerializeField] private float underwaterCausticsIntensity = 1f;
    
    private bool isUnderwater = false;
    private float currentVolumeWeight = 0f;
    private float currentCausticsIntensity = 0f;
    
    public bool IsUnderwater => isUnderwater;
    
    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = transform;
            Debug.LogWarning("UnderwaterEffectController: playerTransform est null, utilisation de transform");
        }
        
        if (underwaterVolume != null)
        {
            underwaterVolume.weight = 0f;
            
            VolumeProfile activeProfile = underwaterVolume.profile != null ? underwaterVolume.profile : underwaterVolume.sharedProfile;
            
            if (activeProfile == null)
            {
                Debug.LogError("UnderwaterEffectController: AUCUN Volume Profile n'est assigné !");
            }
            else if (activeProfile.components == null || activeProfile.components.Count == 0)
            {
                Debug.LogError("UnderwaterEffectController: Le Volume Profile est VIDE !");
            }
            else
            {
                Debug.Log($"UnderwaterEffectController: Volume Profile '{activeProfile.name}' assigné avec {activeProfile.components.Count} effet(s)");
            }
        }
        else
        {
            Debug.LogError("UnderwaterEffectController: underwaterVolume est null !");
        }
        
        if (vfxCausticsController == null)
        {
            vfxCausticsController = FindObjectOfType<VFXCausticsController>();
            if (vfxCausticsController == null)
            {
                Debug.LogWarning("UnderwaterEffectController: VFXCausticsController non trouvé dans la scène");
            }
        }
        
        if (vfxCausticsController != null)
        {
            vfxCausticsController.SetWaterLevel(waterLevel);
        }
        
        Debug.Log($"UnderwaterEffectController initialisé - Water Level: {waterLevel}");
    }
    
    private void Update()
    {
        bool wasUnderwater = isUnderwater;
        isUnderwater = playerTransform.position.y < waterLevel;
        
        if (wasUnderwater != isUnderwater)
        {
            OnWaterStateChanged(isUnderwater);
        }
        
        float targetWeight = isUnderwater ? 1f : 0f;
        currentVolumeWeight = Mathf.MoveTowards(currentVolumeWeight, targetWeight, Time.deltaTime / transitionSpeed);
        
        if (underwaterVolume != null)
        {
            underwaterVolume.weight = currentVolumeWeight;
        }
        
        float targetCausticsIntensity = isUnderwater ? underwaterCausticsIntensity : 0f;
        currentCausticsIntensity = Mathf.MoveTowards(currentCausticsIntensity, targetCausticsIntensity, Time.deltaTime / transitionSpeed);
        
        if (vfxCausticsController != null)
        {
            vfxCausticsController.SetIntensity(currentCausticsIntensity);
        }
    }
    
    private void OnWaterStateChanged(bool underwater)
    {
        if (underwater)
        {
            Debug.Log("Entré dans l'eau - Caustiques activées");
        }
        else
        {
            Debug.Log("Sorti de l'eau - Caustiques désactivées");
        }
    }
}
