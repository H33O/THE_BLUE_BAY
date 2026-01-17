using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SurfboardsAnimator : MonoBehaviour
{
    [System.Serializable]
    public class SurfboardSettings
    {
        [Header("Surfboard")]
        public Transform surfboard;
        [Tooltip("Position/rotation cible pendant le saut")]
        public Transform targetTransform;
        
        [Header("Looping")]
        [Tooltip("Faire un tour complet sur Z en plus du mouvement")]
        public bool doZLooping = false;
        [Tooltip("Vitesse du looping en degrés par seconde")]
        public float loopingSpeed = 1800f;
    }
    
    [Header("Planches de Surf")]
    [SerializeField] private List<SurfboardSettings> surfboards = new List<SurfboardSettings>();
    
    [Header("Animation Settings")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float rotationSpeed = 600f;
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Return Settings")]
    [Tooltip("Délai après le peak avant de revenir (secondes)")]
    [SerializeField] private float returnDelay = 0.2f;
    [SerializeField] private float returnSpeed = 25f;
    
    [Header("Landing Squash")]
    [SerializeField] private float landingSquashAmount = 0.2f;
    [SerializeField] private float landingSquashDuration = 0.15f;
    [SerializeField] private AnimationCurve squashCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("References")]
    [SerializeField] private HoverCarController hoverCarController;
    
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();
    private Dictionary<Transform, Vector3> originalScales = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, float> currentProgress = new Dictionary<Transform, float>();
    private Dictionary<Transform, float> currentLoopingAngle = new Dictionary<Transform, float>();
    
    private bool isMovingToTarget = false;
    private bool isReturning = false;
    private bool isSquashing = false;
    private float peakTimer = 0f;
    private float squashTimer = 0f;
    
    private void Start()
    {
        if (hoverCarController == null)
        {
            hoverCarController = GetComponentInParent<HoverCarController>();
        }
        
        foreach (var surfboard in surfboards)
        {
            if (surfboard.surfboard != null)
            {
                originalPositions[surfboard.surfboard] = surfboard.surfboard.localPosition;
                originalRotations[surfboard.surfboard] = surfboard.surfboard.localRotation;
                originalScales[surfboard.surfboard] = surfboard.surfboard.localScale;
                currentProgress[surfboard.surfboard] = 0f;
                currentLoopingAngle[surfboard.surfboard] = 0f;
            }
        }
    }
    
    private void Update()
    {
        if (isMovingToTarget)
        {
            UpdateMoveToTarget();
        }
        else if (isReturning)
        {
            peakTimer += Time.deltaTime;
            
            if (peakTimer >= returnDelay)
            {
                UpdateReturn();
            }
        }
        
        if (isSquashing)
        {
            UpdateSquash();
        }
    }
    
    private void UpdateMoveToTarget()
    {
        foreach (var surfboardSettings in surfboards)
        {
            if (surfboardSettings.surfboard == null || surfboardSettings.targetTransform == null) continue;
            
            Transform board = surfboardSettings.surfboard;
            
            currentProgress[board] = Mathf.MoveTowards(
                currentProgress[board], 
                1f, 
                moveSpeed * Time.deltaTime
            );
            
            float curvedProgress = moveCurve.Evaluate(currentProgress[board]);
            
            board.localPosition = Vector3.Lerp(
                originalPositions[board],
                surfboardSettings.targetTransform.localPosition,
                curvedProgress
            );
            
            Quaternion targetRotation = surfboardSettings.targetTransform.localRotation;
            
            if (surfboardSettings.doZLooping)
            {
                if (currentLoopingAngle[board] < 360f)
                {
                    currentLoopingAngle[board] += surfboardSettings.loopingSpeed * Time.deltaTime;
                    currentLoopingAngle[board] = Mathf.Min(currentLoopingAngle[board], 360f);
                }
                
                targetRotation *= Quaternion.Euler(0, 0, currentLoopingAngle[board]);
            }
            
            board.localRotation = Quaternion.Slerp(
                originalRotations[board],
                targetRotation,
                curvedProgress
            );
        }
    }
    
    private void UpdateReturn()
    {
        foreach (var surfboardSettings in surfboards)
        {
            if (surfboardSettings.surfboard == null) continue;
            
            Transform board = surfboardSettings.surfboard;
            
            currentProgress[board] = Mathf.MoveTowards(
                currentProgress[board], 
                0f, 
                returnSpeed * Time.deltaTime
            );
            
            float curvedProgress = moveCurve.Evaluate(currentProgress[board]);
            
            if (surfboardSettings.targetTransform != null)
            {
                board.localPosition = Vector3.Lerp(
                    originalPositions[board],
                    surfboardSettings.targetTransform.localPosition,
                    curvedProgress
                );
                
                board.localRotation = Quaternion.Slerp(
                    originalRotations[board],
                    surfboardSettings.targetTransform.localRotation,
                    curvedProgress
                );
            }
            
            if (surfboardSettings.doZLooping)
            {
                currentLoopingAngle[board] = 0f;
            }
        }
    }
    
    private void UpdateSquash()
    {
        squashTimer += Time.deltaTime;
        float t = Mathf.Clamp01(squashTimer / landingSquashDuration);
        float curvedT = squashCurve.Evaluate(t);
        
        foreach (var surfboard in surfboards)
        {
            if (surfboard.surfboard == null) continue;
            
            Transform board = surfboard.surfboard;
            
            float squashProgress = Mathf.Sin(curvedT * Mathf.PI);
            
            Vector3 targetScale = originalScales[board];
            targetScale.y *= (1f - landingSquashAmount * squashProgress);
            targetScale.x *= (1f + landingSquashAmount * 0.3f * squashProgress);
            targetScale.z *= (1f + landingSquashAmount * 0.3f * squashProgress);
            
            board.localScale = targetScale;
        }
        
        if (t >= 1f)
        {
            isSquashing = false;
            squashTimer = 0f;
            
            foreach (var surfboard in surfboards)
            {
                if (surfboard.surfboard != null)
                {
                    surfboard.surfboard.localScale = originalScales[surfboard.surfboard];
                }
            }
        }
    }
    
    public void OnJumpStart()
    {
        isMovingToTarget = true;
        isReturning = false;
        isSquashing = false;
        peakTimer = 0f;
    }
    
    public void OnPeakReached()
    {
        isMovingToTarget = false;
        isReturning = true;
        peakTimer = 0f;
    }
    
    public void OnLanding()
    {
        isMovingToTarget = false;
        isReturning = false;
        isSquashing = true;
        squashTimer = 0f;
    }
}
