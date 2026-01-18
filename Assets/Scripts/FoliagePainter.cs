using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
public class FoliagePainter : MonoBehaviour
{
    [Header("Prefabs à Peindre")]
    [Tooltip("Liste des prefabs de plantes/décors à placer")]
    public List<GameObject> foliagePrefabs = new List<GameObject>();

    [Header("Paramètres de Peinture")]
    [Tooltip("Taille du pinceau")]
    [Range(1f, 50f)]
    public float brushSize = 10f;

    [Tooltip("Densité (nombre d'objets par clic)")]
    [Range(1, 50)]
    public int density = 10;

    [Tooltip("Variation de rotation aléatoire sur Y")]
    public bool randomRotation = true;

    [Tooltip("Rotation fixe sur l'axe X (degrés)")]
    public float rotationX = 89.98f;

    [Header("Offset de Position")]
    [Tooltip("Décalage de position après placement (ex: Y pour hauteur)")]
    public Vector3 positionOffset = Vector3.zero;

    [Tooltip("Multiplicateur d'échelle global (x1 = taille normale, x2 = double taille)")]
    [Range(0.1f, 10f)]
    public float scaleMultiplier = 2f;

    [Tooltip("Variation d'échelle aléatoire")]
    [Range(0f, 0.5f)]
    public float scaleVariation = 0.2f;

    [Header("Parent")]
    [Tooltip("Dossier parent pour organiser les objets créés")]
    public Transform parentFolder;

    [Header("Mode Effacement")]
    [Tooltip("Mode effacement : supprime les objets au lieu d'en créer")]
    public bool eraseMode = false;

    [Tooltip("Tag des objets à effacer")]
    public string eraseTag = "Foliage";

    private void OnDrawGizmosSelected()
    {
        if (Selection.activeGameObject == this.gameObject)
        {
            Handles.color = eraseMode ? Color.red : Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, brushSize);
        }
    }
}

[CustomEditor(typeof(FoliagePainter))]
public class FoliagePainterEditor : Editor
{
    private FoliagePainter painter;
    private bool isPainting = false;

    private void OnEnable()
    {
        painter = (FoliagePainter)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("CTRL + Clic gauche sur la surface pour peindre\nCTRL + Shift + Clic pour effacer", MessageType.Info);

        if (painter.foliagePrefabs.Count == 0)
        {
            EditorGUILayout.HelpBox("Ajoutez des prefabs dans la liste 'Foliage Prefabs' !", MessageType.Warning);
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Créer Dossier Parent", GUILayout.Height(30)))
        {
            CreateParentFolder();
        }
    }

    private void OnSceneGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && e.control)
        {
            isPainting = true;
            PaintFoliage(e);
            e.Use();
        }
        else if (e.type == EventType.MouseDrag && e.control && isPainting)
        {
            PaintFoliage(e);
            e.Use();
        }
        else if (e.type == EventType.MouseUp)
        {
            isPainting = false;
        }

        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    }

    private void PaintFoliage(Event e)
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (painter.eraseMode || e.shift)
            {
                EraseFoliage(hit.point);
            }
            else
            {
                PlaceFoliage(hit.point, hit.normal);
            }
        }
    }

    private void PlaceFoliage(Vector3 center, Vector3 normal)
    {
        if (painter.foliagePrefabs.Count == 0)
        {
            Debug.LogWarning("Aucun prefab assigné !");
            return;
        }

        Transform parent = painter.parentFolder;
        if (parent == null)
        {
            GameObject folder = GameObject.Find("_Foliage");
            if (folder == null)
            {
                folder = new GameObject("_Foliage");
            }
            parent = folder.transform;
        }

        for (int i = 0; i < painter.density; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * painter.brushSize;
            Vector3 spawnPos = center + new Vector3(randomCircle.x, 0, randomCircle.y);

            Ray ray = new Ray(spawnPos + Vector3.up * 100f, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200f))
            {
                GameObject prefab = painter.foliagePrefabs[Random.Range(0, painter.foliagePrefabs.Count)];

                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                instance.transform.position = hit.point + painter.positionOffset;
                instance.transform.parent = parent;

                if (painter.randomRotation)
                {
                    float randomYRotation = Random.Range(0f, 360f);
                    instance.transform.rotation = Quaternion.Euler(painter.rotationX, randomYRotation, 0);
                }
                else
                {
                    instance.transform.rotation = Quaternion.Euler(painter.rotationX, 0, 0);
                    instance.transform.up = hit.normal;
                }

                float finalScale = painter.scaleMultiplier;
                if (painter.scaleVariation > 0)
                {
                    finalScale *= 1f + Random.Range(-painter.scaleVariation, painter.scaleVariation);
                }
                instance.transform.localScale *= finalScale;

                instance.tag = "Foliage";

                Undo.RegisterCreatedObjectUndo(instance, "Paint Foliage");
            }
        }
    }

    private void EraseFoliage(Vector3 center)
    {
        GameObject[] foliageObjects = GameObject.FindGameObjectsWithTag(painter.eraseTag);

        foreach (GameObject obj in foliageObjects)
        {
            if (Vector3.Distance(obj.transform.position, center) <= painter.brushSize)
            {
                Undo.DestroyObjectImmediate(obj);
            }
        }
    }

    private void CreateParentFolder()
    {
        GameObject folder = new GameObject("_Foliage");
        painter.parentFolder = folder.transform;
        Undo.RegisterCreatedObjectUndo(folder, "Create Foliage Folder");
        EditorUtility.SetDirty(painter);
    }
}
#endif
