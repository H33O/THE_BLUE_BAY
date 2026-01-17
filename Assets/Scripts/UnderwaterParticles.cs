using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class UnderwaterParticles : MonoBehaviour
{
    [Header("Détection Eau")]
    [Tooltip("Référence au contrôleur d'effets sous-marins")]
    [SerializeField] private UnderwaterEffectController underwaterController;
    
    [Header("Particules")]
    [Tooltip("Activer automatiquement les particules sous l'eau")]
    [SerializeField] private bool autoActivate = true;
    
    private ParticleSystem particleSystem;
    private bool wasUnderwater = false;
    
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        
        if (underwaterController == null)
        {
            underwaterController = Camera.main.GetComponent<UnderwaterEffectController>();
        }
        
        if (!autoActivate && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }
    
    private void Update()
    {
        if (underwaterController == null || !autoActivate)
            return;
        
        bool isUnderwater = underwaterController.IsUnderwater;
        
        if (isUnderwater && !wasUnderwater)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else if (!isUnderwater && wasUnderwater)
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }
        
        wasUnderwater = isUnderwater;
    }
}
