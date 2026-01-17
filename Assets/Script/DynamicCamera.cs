using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HoverCarController carController;
    
    [Header("FOV Settings")]
    [SerializeField] private float baseFov = 60f;
    [SerializeField] private float maxSpeedFov = 80f;
    [SerializeField] private float brakeFov = 50f;
    [SerializeField] private float fovSmoothSpeed = 5f;
    
    [Header("Position Settings")]
    [SerializeField] private Vector3 baseLocalPosition = new Vector3(0f, 3.25f, -7.09f);
    [SerializeField] private float accelerateZOffset = -1.5f;
    [SerializeField] private float brakeZOffset = 1f;
    [SerializeField] private float positionSmoothSpeed = 4f;
    
    [Header("Rotation Settings")]
    [SerializeField] private float turnRotationAmount = 5f;
    [SerializeField] private float rotationSmoothSpeed = 3f;
    
    private Camera cam;
    private float targetFov;
    private Vector3 targetLocalPosition;
    private float targetYRotation;
    
    private void Awake()
    {
        cam = GetComponent<Camera>();
        targetFov = baseFov;
        targetLocalPosition = baseLocalPosition;
        targetYRotation = 0f;
    }
    
    private void LateUpdate()
    {
        if (carController == null) return;
        
        float speedNormalized = Mathf.Clamp01(carController.CurrentSpeed / carController.MaxSpeed);
        bool isBraking = carController.IsBraking;
        float turnInput = carController.TurnInput;
        
        if (isBraking)
        {
            targetFov = brakeFov;
            targetLocalPosition = baseLocalPosition + new Vector3(0f, 0f, brakeZOffset);
        }
        else
        {
            targetFov = Mathf.Lerp(baseFov, maxSpeedFov, speedNormalized);
            float zOffset = Mathf.Lerp(0f, accelerateZOffset, speedNormalized);
            targetLocalPosition = baseLocalPosition + new Vector3(0f, 0f, zOffset);
        }
        
        targetYRotation = -turnInput * turnRotationAmount;
        
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, Time.deltaTime * fovSmoothSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPosition, Time.deltaTime * positionSmoothSpeed);
        
        Vector3 currentRotation = transform.localEulerAngles;
        float smoothedYRotation = Mathf.LerpAngle(currentRotation.y, targetYRotation, Time.deltaTime * rotationSmoothSpeed);
        transform.localEulerAngles = new Vector3(currentRotation.x, smoothedYRotation, currentRotation.z);
    }
}
