using UnityEngine;

[RequireComponent(typeof(Projector))]
public class CausticProjector : MonoBehaviour
{
    [Header("Animation Caustiques")]
    [Tooltip("Textures de caustiques pour l'animation")]
    [SerializeField] private Texture2D[] causticTextures;
    
    [Tooltip("Vitesse d'animation des caustiques (images par seconde)")]
    [SerializeField] private float framesPerSecond = 15f;
    
    [Tooltip("Intensité des caustiques")]
    [SerializeField] private float intensity = 1f;
    
    [Tooltip("Vitesse de défilement des caustiques")]
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.01f, 0.01f);
    
    private Projector projector;
    private Material causticMaterial;
    private int currentFrame = 0;
    private float frameTimer = 0f;
    private Vector2 currentOffset = Vector2.zero;
    
    private void Start()
    {
        projector = GetComponent<Projector>();
        
        if (projector.material != null)
        {
            causticMaterial = new Material(projector.material);
            projector.material = causticMaterial;
        }
        
        if (causticTextures != null && causticTextures.Length > 0)
        {
            UpdateCausticTexture();
        }
    }
    
    private void Update()
    {
        if (causticTextures == null || causticTextures.Length == 0 || causticMaterial == null)
            return;
        
        frameTimer += Time.deltaTime;
        
        if (frameTimer >= 1f / framesPerSecond)
        {
            frameTimer = 0f;
            currentFrame = (currentFrame + 1) % causticTextures.Length;
            UpdateCausticTexture();
        }
        
        currentOffset += scrollSpeed * Time.deltaTime;
        causticMaterial.SetTextureOffset("_ShadowTex", currentOffset);
        causticMaterial.SetFloat("_Intensity", intensity);
    }
    
    private void UpdateCausticTexture()
    {
        if (causticMaterial != null && currentFrame < causticTextures.Length)
        {
            causticMaterial.SetTexture("_ShadowTex", causticTextures[currentFrame]);
        }
    }
    
    private void OnDestroy()
    {
        if (causticMaterial != null)
        {
            Destroy(causticMaterial);
        }
    }
}
