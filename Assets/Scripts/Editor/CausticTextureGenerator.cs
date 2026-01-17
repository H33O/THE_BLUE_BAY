using UnityEngine;
using UnityEditor;
using System.IO;

public class CausticTextureGenerator : EditorWindow
{
    private int textureSize = 512;
    private int frameCount = 8;
    private float intensity = 1.5f;
    private float scale = 5f;
    private string outputPath = "Assets/Textures/Caustics";
    
    [MenuItem("Tools/Generate Caustic Textures")]
    public static void ShowWindow()
    {
        GetWindow<CausticTextureGenerator>("Caustic Generator");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Générateur de Textures de Caustiques", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        
        textureSize = EditorGUILayout.IntSlider("Taille Texture", textureSize, 256, 1024);
        frameCount = EditorGUILayout.IntSlider("Nombre de Frames", frameCount, 4, 16);
        intensity = EditorGUILayout.Slider("Intensité", intensity, 0.5f, 3f);
        scale = EditorGUILayout.Slider("Échelle", scale, 1f, 10f);
        
        EditorGUILayout.Space();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Dossier de Sortie:", outputPath);
        if (GUILayout.Button("Parcourir", GUILayout.Width(80)))
        {
            string path = EditorUtility.OpenFolderPanel("Sélectionner le dossier", "Assets", "");
            if (!string.IsNullOrEmpty(path))
            {
                outputPath = "Assets" + path.Substring(Application.dataPath.Length);
            }
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Générer Textures", GUILayout.Height(40)))
        {
            GenerateCausticTextures();
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "Ce générateur crée des textures de caustiques procédurales.\n" +
            "Les textures seront sauvegardées dans le dossier spécifié.", 
            MessageType.Info
        );
    }
    
    private void GenerateCausticTextures()
    {
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        
        for (int frame = 0; frame < frameCount; frame++)
        {
            Texture2D texture = GenerateCausticFrame(frame);
            
            byte[] bytes = texture.EncodeToPNG();
            string filename = $"{outputPath}/Caustic_{frame:D2}.png";
            File.WriteAllBytes(filename, bytes);
            
            DestroyImmediate(texture);
        }
        
        AssetDatabase.Refresh();
        
        EditorUtility.DisplayDialog(
            "Génération Terminée", 
            $"{frameCount} textures de caustiques ont été générées dans:\n{outputPath}", 
            "OK"
        );
    }
    
    private Texture2D GenerateCausticFrame(int frameIndex)
    {
        Texture2D texture = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
        
        float time = frameIndex / (float)frameCount * Mathf.PI * 2f;
        
        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                float u = x / (float)textureSize;
                float v = y / (float)textureSize;
                
                float caustic = GenerateCausticPattern(u, v, time);
                
                Color color = Color.white * caustic;
                texture.SetPixel(x, y, color);
            }
        }
        
        texture.Apply();
        return texture;
    }
    
    private float GenerateCausticPattern(float u, float v, float time)
    {
        float value = 0f;
        
        float x = u * scale;
        float y = v * scale;
        
        float wave1 = Mathf.Sin(x * 3f + time) * Mathf.Cos(y * 2.5f + time * 0.7f);
        float wave2 = Mathf.Sin(y * 2f - time * 0.8f) * Mathf.Cos(x * 3.5f - time * 1.2f);
        float wave3 = Mathf.Sin((x + y) * 2f + time * 0.5f) * Mathf.Cos((x - y) * 2f + time * 0.9f);
        
        value = (wave1 + wave2 + wave3) / 3f;
        
        value = value * 0.5f + 0.5f;
        
        value = Mathf.Pow(value, 2f) * intensity;
        
        return Mathf.Clamp01(value);
    }
}
