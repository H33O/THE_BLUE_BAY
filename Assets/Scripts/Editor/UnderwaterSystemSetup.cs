using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UnderwaterSystemSetup : EditorWindow
{
    private VolumeProfile volumeProfile;
    private Texture2D[] causticTextures;
    
    [MenuItem("Tools/Setup Underwater System")]
    public static void ShowWindow()
    {
        GetWindow<UnderwaterSystemSetup>("Underwater Setup");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Configuration du Système Sous-Marin", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "Cet outil vous aide à finaliser la configuration du système sous-marin.", 
            MessageType.Info
        );
        
        EditorGUILayout.Space();
        
        DrawVolumeProfileSection();
        
        EditorGUILayout.Space();
        
        DrawCausticTexturesSection();
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Appliquer la Configuration", GUILayout.Height(40)))
        {
            ApplyConfiguration();
        }
    }
    
    private void DrawVolumeProfileSection()
    {
        EditorGUILayout.LabelField("1. Volume Profile", EditorStyles.boldLabel);
        
        volumeProfile = (VolumeProfile)EditorGUILayout.ObjectField(
            "Volume Profile", 
            volumeProfile, 
            typeof(VolumeProfile), 
            false
        );
        
        if (volumeProfile == null)
        {
            EditorGUILayout.HelpBox(
                "Créez un Volume Profile:\n" +
                "Clic droit dans Assets/Settings → Create → Rendering → URP Volume Profile\n" +
                "Nommez-le 'UnderwaterVolumeProfile'",
                MessageType.Warning
            );
            
            if (GUILayout.Button("Créer et Configurer Automatiquement"))
            {
                CreateVolumeProfile();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Volume Profile assigné ✓", MessageType.None);
        }
    }
    
    private void DrawCausticTexturesSection()
    {
        EditorGUILayout.LabelField("2. Textures de Caustiques", EditorStyles.boldLabel);
        
        SerializedObject so = new SerializedObject(this);
        SerializedProperty texturesProperty = so.FindProperty("causticTextures");
        EditorGUILayout.PropertyField(texturesProperty, true);
        so.ApplyModifiedProperties();
        
        if (causticTextures == null || causticTextures.Length == 0)
        {
            EditorGUILayout.HelpBox(
                "Assignez des textures de caustiques ou utilisez le générateur:\n" +
                "Tools → Generate Caustic Textures",
                MessageType.Warning
            );
        }
        else
        {
            EditorGUILayout.HelpBox($"{causticTextures.Length} textures assignées ✓", MessageType.None);
        }
    }
    
    private void CreateVolumeProfile()
    {
        string path = "Assets/Settings/UnderwaterVolumeProfile.asset";
        
        VolumeProfile profile = ScriptableObject.CreateInstance<VolumeProfile>();
        
        if (!profile.Has<ColorAdjustments>())
        {
            ColorAdjustments colorAdj = profile.Add<ColorAdjustments>();
            colorAdj.colorFilter.overrideState = true;
            colorAdj.colorFilter.value = new Color(0.4f, 0.7f, 0.9f, 1f);
            colorAdj.saturation.overrideState = true;
            colorAdj.saturation.value = -20f;
            colorAdj.contrast.overrideState = true;
            colorAdj.contrast.value = -10f;
        }
        
        if (!profile.Has<Vignette>())
        {
            Vignette vignette = profile.Add<Vignette>();
            vignette.color.overrideState = true;
            vignette.color.value = new Color(0f, 0.2f, 0.4f, 1f);
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0.35f;
            vignette.smoothness.overrideState = true;
            vignette.smoothness.value = 0.4f;
        }
        
        if (!profile.Has<WhiteBalance>())
        {
            WhiteBalance whiteBalance = profile.Add<WhiteBalance>();
            whiteBalance.temperature.overrideState = true;
            whiteBalance.temperature.value = -15f;
            whiteBalance.tint.overrideState = true;
            whiteBalance.tint.value = -5f;
        }
        
        AssetDatabase.CreateAsset(profile, path);
        AssetDatabase.SaveAssets();
        
        volumeProfile = profile;
        
        EditorUtility.DisplayDialog(
            "Volume Profile Créé", 
            $"Le Volume Profile a été créé avec succès à:\n{path}\n\nIl est configuré avec les effets sous-marins.", 
            "OK"
        );
    }
    
    private void ApplyConfiguration()
    {
        bool success = true;
        
        GameObject underwaterVolume = GameObject.Find("Underwater Volume");
        if (underwaterVolume != null && volumeProfile != null)
        {
            Volume volume = underwaterVolume.GetComponent<Volume>();
            if (volume != null)
            {
                volume.profile = volumeProfile;
                EditorUtility.SetDirty(underwaterVolume);
                Debug.Log("Volume Profile assigné à 'Underwater Volume'");
            }
        }
        else if (volumeProfile == null)
        {
            EditorUtility.DisplayDialog("Erreur", "Veuillez créer ou assigner un Volume Profile d'abord.", "OK");
            success = false;
        }
        
        GameObject causticProjector = GameObject.Find("Caustic Projector");
        if (causticProjector != null && causticTextures != null && causticTextures.Length > 0)
        {
            CausticProjector causticScript = causticProjector.GetComponent<CausticProjector>();
            if (causticScript != null)
            {
                SerializedObject so = new SerializedObject(causticScript);
                SerializedProperty texturesProp = so.FindProperty("causticTextures");
                
                texturesProp.arraySize = causticTextures.Length;
                for (int i = 0; i < causticTextures.Length; i++)
                {
                    texturesProp.GetArrayElementAtIndex(i).objectReferenceValue = causticTextures[i];
                }
                
                so.ApplyModifiedProperties();
                EditorUtility.SetDirty(causticProjector);
                Debug.Log($"{causticTextures.Length} textures assignées au Caustic Projector");
            }
        }
        
        if (success)
        {
            EditorUtility.DisplayDialog(
                "Configuration Appliquée", 
                "Le système sous-marin a été configuré avec succès !\n\n" +
                "Vous pouvez maintenant tester le jeu en déplaçant le HoverCar au-dessus et en dessous du niveau d'eau.", 
                "OK"
            );
        }
    }
}
