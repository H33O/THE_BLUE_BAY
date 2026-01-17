using UnityEngine;
using System.Collections.Generic;

public class FishSchool : MonoBehaviour
{
    [Header("School Settings")]
    [SerializeField] private int fishCount = 20;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private float spawnRadius = 5f;
    
    [Header("Center Point")]
    [SerializeField] private Vector3 centerOffset = Vector3.zero;
    [SerializeField] private float orbitRadius = 10f;
    [SerializeField] private float centerAttractionStrength = 1f;
    
    [Header("Flocking Behavior")]
    [SerializeField] private float separationDistance = 1f;
    [SerializeField] private float separationStrength = 2f;
    [SerializeField] private float alignmentDistance = 3f;
    [SerializeField] private float alignmentStrength = 1f;
    [SerializeField] private float cohesionDistance = 5f;
    [SerializeField] private float cohesionStrength = 1f;
    
    [Header("Randomness")]
    [SerializeField] private float randomForceStrength = 0.5f;
    [SerializeField] private float randomForceInterval = 2f;
    
    [Header("Bounds")]
    [SerializeField] private Vector3 boundsSize = new Vector3(30f, 15f, 30f);
    [SerializeField] private float boundsForceStrength = 3f;
    
    private List<Fish> fishes = new List<Fish>();
    private Vector3 schoolCenter;
    private float randomForceTimer;
    
    private void Start()
    {
        schoolCenter = transform.position + centerOffset;
        SpawnFishes();
    }
    
    private void SpawnFishes()
    {
        for (int i = 0; i < fishCount; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
            randomPos += schoolCenter;
            
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            GameObject fishObj = Instantiate(fishPrefab, randomPos, randomRot, transform);
            fishObj.name = $"Fish_{i}";
            
            Fish fish = fishObj.GetComponent<Fish>();
            if (fish != null)
            {
                fish.Initialize(this);
                fishes.Add(fish);
            }
            else
            {
                Debug.LogWarning($"Fish prefab is missing Fish component!");
            }
        }
        
        Debug.Log($"Spawned {fishes.Count} fishes");
    }
    
    private void Update()
    {
        schoolCenter = transform.position + centerOffset;
        randomForceTimer += Time.deltaTime;
    }
    
    public Vector3 CalculateFishBehavior(Fish fish)
    {
        Vector3 separation = CalculateSeparation(fish);
        Vector3 alignment = CalculateAlignment(fish);
        Vector3 cohesion = CalculateCohesion(fish);
        Vector3 centerAttraction = CalculateCenterAttraction(fish);
        Vector3 randomForce = CalculateRandomForce();
        Vector3 boundsForce = CalculateBoundsForce(fish);
        
        return separation + alignment + cohesion + centerAttraction + randomForce + boundsForce;
    }
    
    private Vector3 CalculateSeparation(Fish fish)
    {
        Vector3 force = Vector3.zero;
        int count = 0;
        
        foreach (Fish other in fishes)
        {
            if (other == fish) continue;
            
            float distance = Vector3.Distance(fish.Position, other.Position);
            if (distance < separationDistance && distance > 0)
            {
                Vector3 diff = fish.Position - other.Position;
                force += diff.normalized / distance;
                count++;
            }
        }
        
        if (count > 0)
        {
            force /= count;
            force *= separationStrength;
        }
        
        return force;
    }
    
    private Vector3 CalculateAlignment(Fish fish)
    {
        Vector3 avgVelocity = Vector3.zero;
        int count = 0;
        
        foreach (Fish other in fishes)
        {
            if (other == fish) continue;
            
            float distance = Vector3.Distance(fish.Position, other.Position);
            if (distance < alignmentDistance)
            {
                avgVelocity += other.Velocity;
                count++;
            }
        }
        
        if (count > 0)
        {
            avgVelocity /= count;
            return (avgVelocity - fish.Velocity) * alignmentStrength;
        }
        
        return Vector3.zero;
    }
    
    private Vector3 CalculateCohesion(Fish fish)
    {
        Vector3 centerOfMass = Vector3.zero;
        int count = 0;
        
        foreach (Fish other in fishes)
        {
            if (other == fish) continue;
            
            float distance = Vector3.Distance(fish.Position, other.Position);
            if (distance < cohesionDistance)
            {
                centerOfMass += other.Position;
                count++;
            }
        }
        
        if (count > 0)
        {
            centerOfMass /= count;
            return (centerOfMass - fish.Position) * cohesionStrength;
        }
        
        return Vector3.zero;
    }
    
    private Vector3 CalculateCenterAttraction(Fish fish)
    {
        Vector3 toCenter = schoolCenter - fish.Position;
        float distance = toCenter.magnitude;
        
        if (distance > orbitRadius)
        {
            return toCenter.normalized * centerAttractionStrength * (distance - orbitRadius) * 0.1f;
        }
        
        Vector3 tangent = Vector3.Cross(toCenter.normalized, Vector3.up);
        if (tangent.magnitude < 0.1f)
        {
            tangent = Vector3.Cross(toCenter.normalized, Vector3.forward);
        }
        
        return tangent.normalized * centerAttractionStrength * 0.3f;
    }
    
    private Vector3 CalculateRandomForce()
    {
        if (randomForceTimer > randomForceInterval)
        {
            randomForceTimer = 0f;
            return Random.insideUnitSphere * randomForceStrength;
        }
        return Vector3.zero;
    }
    
    private Vector3 CalculateBoundsForce(Fish fish)
    {
        Vector3 force = Vector3.zero;
        Vector3 localPos = fish.Position - schoolCenter;
        
        if (Mathf.Abs(localPos.x) > boundsSize.x * 0.5f)
        {
            force.x = -Mathf.Sign(localPos.x) * boundsForceStrength;
        }
        
        if (Mathf.Abs(localPos.y) > boundsSize.y * 0.5f)
        {
            force.y = -Mathf.Sign(localPos.y) * boundsForceStrength;
        }
        
        if (Mathf.Abs(localPos.z) > boundsSize.z * 0.5f)
        {
            force.z = -Mathf.Sign(localPos.z) * boundsForceStrength;
        }
        
        return force;
    }
    
    private void OnDrawGizmos()
    {
        Vector3 center = Application.isPlaying ? schoolCenter : transform.position + centerOffset;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(center, orbitRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, boundsSize);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center, 0.5f);
    }
}
