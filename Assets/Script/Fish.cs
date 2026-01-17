using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("References")]
    private FishSchool school;
    
    [Header("Movement")]
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 4f;
    
    [Header("Animation")]
    [SerializeField] private Transform fishBody;
    [SerializeField] private float swimFrequency = 2f;
    [SerializeField] private float swimAmplitude = 0.1f;
    
    private Vector3 velocity;
    private float swimTimer;
    
    public Vector3 Velocity => velocity;
    public Vector3 Position => transform.position;
    
    public void Initialize(FishSchool fishSchool)
    {
        school = fishSchool;
        velocity = Random.insideUnitSphere.normalized * baseSpeed;
        swimTimer = Random.Range(0f, Mathf.PI * 2f);
    }
    
    private void Update()
    {
        if (school == null) return;
        
        Vector3 acceleration = school.CalculateFishBehavior(this);
        
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        
        if (velocity.magnitude < baseSpeed * 0.5f)
        {
            velocity = velocity.normalized * baseSpeed;
        }
        
        transform.position += velocity * Time.deltaTime;
        
        if (velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
        AnimateSwimming();
    }
    
    private void AnimateSwimming()
    {
        if (fishBody == null) return;
        
        swimTimer += Time.deltaTime * swimFrequency * velocity.magnitude;
        
        float bendAngle = Mathf.Sin(swimTimer) * swimAmplitude * 30f;
        fishBody.localRotation = Quaternion.Euler(0, bendAngle, 0);
    }
}
