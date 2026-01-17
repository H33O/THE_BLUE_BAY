using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ImprovedUnderwaterSetup : EditorWindow
{
    private VolumeProfile volumeProfile;
    private Texture2D[] causticTextures;
    
    [MenuItem("Tools/Improved Underwater System")]
    public static void ShowWindow()
    {
        GetWindow<ImprovedUnderwaterSetup>("Underwater Setup Pro");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Configuration Système Sous-Marin Immersif", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "Version améliorée avec fog, visibilité réduite et effets immersifs.", 
            MessageType.Info
        );
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Créer Volume Profile Immersif", GUILayout.Height(40)))
        {
            CreateImprovedVolumeProfile();
        }
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Ajouter Fog Controller à la Caméra", GUILayout.Height(30)))
        {
            AddFogController();
        }
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Créer Surface d'Eau Animée", GUILayout.Height(30)))
        {
            CreateWaterSurface();
        }
    }
    
    private void CreateImprovedVolumeProfile()
    {
        string path = "Assets/Settings/UnderwaterVolumeProfile.asset";
        
        VolumeProfile existingProfile = AssetDatabase.LoadAssetAtPath<VolumeProfile>(path);
        if (existingProfile != null)
        {
            if (!EditorUtility.DisplayDialog(
                "Volume Profile Existe",
                "Un Volume Profile existe déjà. Voulez-vous le remplacer ?",
                "Remplacer",
                "Annuler"))
            {
                return;
            }
            AssetDatabase.DeleteAsset(path);
        }
        
        System.IO.Directory.CreateDirectory("Assets/Settings");
        
        VolumeProfile profile = ScriptableObject.CreateInstance<VolumeProfile>();
        
        ColorAdjustments colorAdj = profile.Add<ColorAdjustments>();
        colorAdj.colorFilter.overrideState = true;
        colorAdj.colorFilter.value = new Color(0.3f, 0.55f, 0.75f, 1f);
        colorAdj.saturation.overrideState = true;
        colorAdj.saturation.value = -30f;
        colorAdj.contrast.overrideState = true;
        colorAdj.contrast.value = -20f;
        
        Vignette vignette = profile.Add<Vignette>();
        vignette.color.overrideState = true;
        vignette.color.value = new Color(0f, 0.12f, 0.3f, 1f);
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0.5f;
        vignette.smoothness.overrideState = true;
        vignette.smoothness.value = 0.5f;
        
        WhiteBalance whiteBalance = profile.Add<WhiteBalance>();
        whiteBalance.temperature.overrideState = true;
        whiteBalance.temperature.value = -25f;
        whiteBalance.tint.overrideState = true;
        whiteBalance.tint.value = -12f;
        
        DepthOfField dof = profile.Add<DepthOfField>();
        dof.mode.overrideState = true;
        dof.mode.value = DepthOfFieldMode.Gaussian;
        dof.gaussianStart.overrideState = true;
        dof.gaussianStart.value = 15f;
        dof.gaussianEnd.overrideState = true;
        dof.gaussianEnd.value = 50f;
        dof.gaussianMaxRadius.overrideState = true;
        dof.gaussianMaxRadius.value = 2f;
        
        ChromaticAberration chromatic = profile.Add<ChromaticAberration>();
        chromatic.intensity.overrideState = true;
        chromatic.intensity.value = 0.2f;
        
        AssetDatabase.CreateAsset(profile, path);
        AssetDatabase.SaveAssets();
        
        GameObject underwaterVolume = GameObject.Find("Underwater Volume");
        if (underwaterVolume != null)
        {
            Volume volume = underwaterVolume.GetComponent<Volume>();
            if (volume != null)
            {
                volume.sharedProfile = profile;
                EditorUtility.SetDirty(underwaterVolume);
            }
        }
        
        EditorUtility.DisplayDialog(
            "Volume Profile Créé",
            "Volume Profile immersif créé avec succès!\n\n" +
            "Effets ajoutés:\n" +
            "• Teinte bleue profonde\n" +
            "• Saturation et contraste réduits\n" +
            "• Vignette renforcée\n" +
            "• Depth of Field (visibilité réduite)\n" +
            "• Chromatic Aberration\n\n" +
            "Le profile a été assigné au Volume automatiquement.",
            "OK"
        );
    }
    
    private void AddFogController()
    {
        GameObject camera = GameObject.Find("Main Camera");
        if (camera == null)
        {
            camera = Camera.main?.gameObject;
        }
        
        if (camera == null)
        {
            EditorUtility.DisplayDialog("Erreur", "Caméra principale non trouvée!", "OK");
            return;
        }
        
        if (camera.GetComponent<UnderwaterFogController>() != null)
        {
            EditorUtility.DisplayDialog("Info", "Le Fog Controller est déjà présent sur la caméra.", "OK");
            return;
        }
        
        camera.AddComponent<UnderwaterFogController>();
        EditorUtility.SetDirty(camera);
        
        EditorUtility.DisplayDialog(
            "Fog Controller Ajouté",
            "Le contrôleur de fog sous-marin a été ajouté à la caméra!\n\n" +
            "Il activera automatiquement le fog quand vous êtes sous l'eau pour réduire la visibilité.",
            "OK"
        );
    }
    
    private void CreateWaterSurface()
    {
        GameObject waterSurface = GameObject.Find("Water Surface");
        if (waterSurface == null)
        {
            waterSurface = GameObject.CreatePrimitive(PrimitiveType.Plane);
            waterSurface.name = "Water Surface";
            waterSurface.transform.position = new Vector3(0, 0, 0);
            waterSurface.transform.localScale = new Vector3(50, 1, 50);
            
            DestroyImmediate(waterSurface.GetComponent<Collider>());
        }
        
        string shaderPath = "Assets/Scripts/WaterSurface.shader";
        Shader waterShader = AssetDatabase.LoadAssetAtPath<Shader>(shaderPath);
        
        if (waterShader == null)
        {
            EditorUtility.DisplayDialog(
                "Erreur",
                "Le shader WaterSurface n'a pas été trouvé!\n\n" +
                "Assurez-vous que le fichier WaterSurface.shader existe dans Assets/Scripts/",
                "OK"
            );
            return;
        }
        
        System.IO.Directory.CreateDirectory("Assets/Materials");
        string matPath = "Assets/Materials/WaterSurface.mat";
        
        Material waterMaterial = AssetDatabase.LoadAssetAtPath<Material>(matPath);
        if (waterMaterial == null)
        {
            waterMaterial = new Material(waterShader);
            AssetDatabase.CreateAsset(waterMaterial, matPath);
            AssetDatabase.SaveAssets();
        }
        else
        {
            waterMaterial.shader = waterShader;
        }
        
        waterMaterial.SetColor("_Color", new Color(0.1f, 0.4f, 0.7f, 0.75f));
        waterMaterial.SetColor("_DeepColor", new Color(0.0f, 0.1f, 0.3f, 0.85f));
        waterMaterial.SetColor("_FresnelColor", new Color(0.7f, 0.9f, 1.0f, 1.0f));
        waterMaterial.SetFloat("_WaveSpeed", 0.8f);
        waterMaterial.SetFloat("_WaveScale", 0.15f);
        waterMaterial.SetFloat("_WaveHeight", 0.3f);
        
        MeshRenderer renderer = waterSurface.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = waterMaterial;
        }
        
        EditorUtility.SetDirty(waterSurface);
        EditorUtility.SetDirty(waterMaterial);
        
        Selection.activeGameObject = waterSurface;
        
        EditorUtility.DisplayDialog(
            "Surface d'Eau Créée",
            "Une surface d'eau animée a été créée!\n\n" +
            "Caractéristiques:\n" +
            "• Vagues animées en temps réel\n" +
            "• Transparence réaliste\n" +
            "• Effet Fresnel\n" +
            "• Reflets et spéculaire\n\n" +
            "Ajustez la position Y pour correspondre à votre Water Level.",
            "OK"
        );
    }
}
