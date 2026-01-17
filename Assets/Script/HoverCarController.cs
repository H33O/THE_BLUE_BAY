using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class HoverCarController : MonoBehaviour
{
    [Header("⚙️ Réglages Lévitation")]
    [Tooltip("Hauteur de vol au-dessus du sol")]
    [SerializeField] private float hoverHeight = 1f;
    [Tooltip("Force du ressort qui maintient le van en l'air")]
    [SerializeField] private float hoverSpringStrength = 100f;
    [Tooltip("Amortissement du ressort (réduit les oscillations)")]
    [SerializeField] private float hoverDamping = 15f;
    [Tooltip("Couches de sol détectées")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Distance max du raycast pour détecter le sol")]
    [SerializeField] private float maxRayDistance = 5f;
    [Tooltip("Distance pour vérifier si on est au-dessus du sol")]
    [SerializeField] private float groundCheckDistance = 20f;
    
    [Header("🚗 Réglages Déplacement")]
    [Tooltip("Vitesse maximale du van")]
    [SerializeField] private float maxSpeed = 20f;
    [Tooltip("Accélération (vitesse de montée en vitesse)")]
    [SerializeField] private float acceleration = 10f;
    [Tooltip("Décélération (vitesse de freinage)")]
    [SerializeField] private float deceleration = 15f;
    [Tooltip("Vitesse de rotation (virages)")]
    [SerializeField] private float turnSpeed = 100f;
    
    [Header("🦘 Réglages Saut")]
    [Tooltip("Hauteur maximale du saut")]
    [SerializeField] private float jumpHeight = 5f;
    [Tooltip("Durée du saut (secondes)")]
    [SerializeField] private float jumpDuration = 1f;
    [Tooltip("Délai entre deux sauts (secondes)")]
    [SerializeField] private float jumpCooldown = 0.8f;
    [Tooltip("Écrasement avant le saut (0 = pas d'écrasement, 1 = écrasé à fond)")]
    [SerializeField] private float preJumpSquashAmount = 0.25f;
    [Tooltip("Durée de l'écrasement avant le saut (secondes)")]
    [SerializeField] private float preJumpSquashDuration = 0.1f;
    [Tooltip("Écrasement à l'atterrissage (0 = pas d'écrasement, 1 = écrasé à fond)")]
    [SerializeField] private float landingSquashAmount = 0.3f;
    [Tooltip("Durée de l'écrasement à l'atterrissage (secondes)")]
    [SerializeField] private float landingSquashDuration = 0.15f;
    [Tooltip("Accélération de la descente (multiplieur de gravité)")]
    [SerializeField] private float descentAcceleration = 2f;
    
    [Header("📐 Réglages Inclinaison")]
    [Tooltip("Inclinaison avant/arrière selon vitesse (degrés)")]
    [SerializeField] private float pitchTiltAmount = 15f;
    [Tooltip("Inclinaison gauche/droite dans les virages (degrés)")]
    [SerializeField] private float rollTiltAmount = 20f;
    [Tooltip("Vitesse de transition des inclinaisons")]
    [SerializeField] private float tiltSpeed = 3f;
    
    [Header("🎨 Corps Visuel")]
    [Tooltip("Transform du modèle 3D du van (pour inclinaisons et squash)")]
    [SerializeField] private Transform visualBody;
    
    [Header("📍 Positions des Roues")]
    [Tooltip("Position de la roue avant gauche")]
    [SerializeField] private Transform frontLeftWheelPos;
    [Tooltip("Position de la roue avant droite")]
    [SerializeField] private Transform frontRightWheelPos;
    [Tooltip("Position de la roue arrière gauche")]
    [SerializeField] private Transform rearLeftWheelPos;
    [Tooltip("Position de la roue arrière droite")]
    [SerializeField] private Transform rearRightWheelPos;
    
    [Header("🛞 Modèles des Roues")]
    [Tooltip("Mesh de la roue avant gauche")]
    [SerializeField] private GameObject frontLeftWheelMesh;
    [Tooltip("Mesh de la roue avant droite")]
    [SerializeField] private GameObject frontRightWheelMesh;
    [Tooltip("Mesh de la roue arrière gauche")]
    [SerializeField] private GameObject rearLeftWheelMesh;
    [Tooltip("Mesh de la roue arrière droite")]
    [SerializeField] private GameObject rearRightWheelMesh;
    
    [Header("⚙️ Réglages Roues")]
    [Tooltip("Décalage vertical des roues par rapport au sol")]
    [SerializeField] private float wheelOffset = 0.1f;
    [Tooltip("Angle maximum de braquage des roues avant (degrés)")]
    [SerializeField] private float maxSteerAngle = 30f;
    [Tooltip("Vitesse de rotation du braquage des roues")]
    [SerializeField] private float wheelSteerSpeed = 5f;
    [Tooltip("Distance de repli si pas de sol détecté")]
    [SerializeField] private float wheelFallbackDistance = 0.5f;
    
    [Header("🏄 Planches de Surf")]
    [Tooltip("Contrôleur de l'animation des planches")]
    [SerializeField] private SurfboardsAnimator surfboardsAnimator;
    
    [Header("🔍 Debug Visuel")]
    [Tooltip("Afficher tous les gizmos de debug")]
    [SerializeField] private bool showGizmos = true;
    [Tooltip("Afficher les rayons des roues")]
    [SerializeField] private bool showWheelRays = true;
    [Tooltip("Afficher la détection du sol")]
    [SerializeField] private bool showGroundCheck = true;
    [Tooltip("Afficher la hauteur de lévitation")]
    [SerializeField] private bool showHoverHeight = true;
    
    private Rigidbody rb;
    private float currentSpeed;
    private float targetPitch;
    private float targetRoll;
    private Vector2 moveInput;
    private float turnInput;
    private float currentSteerAngle;
    private bool isGrounded;
    private float groundDistance;
    private bool canJump = true;
    private bool isInAir = false;
    private bool peakReached = false;
    private Vector3 originalBodyScale;
    private float airTimer = 0f;
    private bool isBraking = false;
    
    public float CurrentSpeed => currentSpeed;
    public float MaxSpeed => maxSpeed;
    public bool IsBraking => isBraking;
    public float TurnInput => turnInput;
    
    private Transform[] wheelPositions;
    private RaycastHit[] wheelHits;
    private bool[] wheelGrounded;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.linearDamping = 0f;
        rb.angularDamping = 5f;
        
        if (visualBody != null)
        {
            originalBodyScale = visualBody.localScale;
        }
        
        wheelPositions = new Transform[4];
        wheelPositions[0] = frontLeftWheelPos;
        wheelPositions[1] = frontRightWheelPos;
        wheelPositions[2] = rearLeftWheelPos;
        wheelPositions[3] = rearRightWheelPos;
        
        wheelHits = new RaycastHit[4];
        wheelGrounded = new bool[4];
    }
    
    private void FixedUpdate()
    {
        CheckGrounded();
        
        if (!isInAir)
        {
            MaintainGroundPosition();
            HandleMovement();
        }
        
        ApplyTilts();
    }
    
    private void Update()
    {
        UpdateWheelPositions();
        UpdateWheelRotations();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump && isGrounded && !isInAir)
        {
            StartCoroutine(PerformJump());
        }
    }
    
    private IEnumerator PerformJump()
    {
        canJump = false;
        
        if (surfboardsAnimator != null)
        {
            surfboardsAnimator.OnJumpStart();
        }
        
        if (visualBody != null)
        {
            float squashTime = 0f;
            while (squashTime < preJumpSquashDuration)
            {
                squashTime += Time.deltaTime;
                float t = squashTime / preJumpSquashDuration;
                float smoothT = Mathf.SmoothStep(0f, 1f, t);
                float squash = Mathf.Lerp(1f, 1f - preJumpSquashAmount, smoothT);
                visualBody.localScale = new Vector3(
                    originalBodyScale.x * (1f + preJumpSquashAmount * 0.3f * smoothT),
                    originalBodyScale.y * squash,
                    originalBodyScale.z * (1f + preJumpSquashAmount * 0.3f * smoothT)
                );
                yield return null;
            }
        }
        
        isInAir = true;
        peakReached = false;
        
        if (visualBody != null)
        {
            visualBody.localScale = originalBodyScale;
        }
        
        Vector3 startPosition = transform.position;
        float peakHeight = startPosition.y + jumpHeight;
        
        float ascentTime = jumpDuration * 0.4f;
        float elapsedTime = 0f;
        
        while (elapsedTime < ascentTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / ascentTime;
            float easedT = 1f - Mathf.Pow(1f - t, 2f);
            
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(startPosition.y, peakHeight, easedT);
            transform.position = pos;
            
            yield return null;
        }
        
        Debug.Log("SOMMET ATTEINT! Début de la descente accélérée!");
        peakReached = true;
        
        if (surfboardsAnimator != null)
        {
            surfboardsAnimator.OnPeakReached();
        }
        
        float descentSpeed = 0f;
        
        while (isInAir)
        {
            descentSpeed += descentAcceleration * Time.deltaTime;
            
            Vector3 pos = transform.position;
            pos.y -= descentSpeed * Time.deltaTime;
            transform.position = pos;
            
            CheckGrounded();
            
            if (isGrounded && groundDistance < hoverHeight + 0.5f)
            {
                Debug.Log("LANDING!");
                isInAir = false;
                peakReached = false;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                
                if (surfboardsAnimator != null)
                {
                    surfboardsAnimator.OnLanding();
                }
                
                break;
            }
            
            yield return null;
        }
        
        if (visualBody != null)
        {
            float landTime = 0f;
            while (landTime < landingSquashDuration)
            {
                landTime += Time.deltaTime;
                float t = landTime / landingSquashDuration;
                
                float squashT = Mathf.Sin(t * Mathf.PI);
                float squash = Mathf.Lerp(1f, 1f - landingSquashAmount, squashT);
                
                visualBody.localScale = new Vector3(
                    originalBodyScale.x * (1f + landingSquashAmount * 0.5f * squashT),
                    originalBodyScale.y * squash,
                    originalBodyScale.z * (1f + landingSquashAmount * 0.5f * squashT)
                );
                yield return null;
            }
            visualBody.localScale = originalBodyScale;
        }
        
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
    
    private void CheckGrounded()
    {
        isGrounded = false;
        float closestDistance = float.MaxValue;
        
        for (int i = 0; i < 4; i++)
        {
            if (wheelPositions[i] == null) continue;
            
            RaycastHit hit;
            if (Physics.Raycast(wheelPositions[i].position, Vector3.down, out hit, groundCheckDistance, groundLayer))
            {
                isGrounded = true;
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    groundDistance = hit.distance;
                }
            }
        }
        
        if (!isGrounded)
        {
            groundDistance = groundCheckDistance;
        }
    }
    
    private void MaintainGroundPosition()
    {
        float lowestWheelHeight = float.MaxValue;
        int groundedCount = 0;
        
        for (int i = 0; i < 4; i++)
        {
            if (wheelPositions[i] == null) continue;
            
            Vector3 wheelPos = wheelPositions[i].position;
            wheelGrounded[i] = Physics.Raycast(wheelPos, Vector3.down, out wheelHits[i], maxRayDistance, groundLayer);
            
            if (wheelGrounded[i])
            {
                if (wheelHits[i].distance < lowestWheelHeight)
                {
                    lowestWheelHeight = wheelHits[i].distance;
                }
                groundedCount++;
            }
        }
        
        if (groundedCount > 0)
        {
            float targetY = transform.position.y + (hoverHeight - lowestWheelHeight);
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, targetY, Time.fixedDeltaTime * 10f);
            transform.position = pos;
            
            Vector3 vel = rb.linearVelocity;
            vel.y = 0f;
            rb.linearVelocity = vel;
        }
    }
    
    private void HandleMovement()
    {
        float targetSpeed = moveInput.y * maxSpeed;
        
        isBraking = (moveInput.y < -0.1f && currentSpeed > 0.5f) || (moveInput.y > 0.1f && currentSpeed < -0.5f);
        
        if (Mathf.Abs(moveInput.y) > 0.1f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }
        
        Vector3 targetVelocity = transform.forward * currentSpeed;
        Vector3 currentHorizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        Vector3 velocityDiff = targetVelocity - currentHorizontalVelocity;
        
        rb.AddForce(velocityDiff * 10f, ForceMode.Force);
        
        turnInput = moveInput.x;
        if (Mathf.Abs(currentSpeed) > 0.5f)
        {
            float steeringDirection = currentSpeed >= 0 ? 1f : -1f;
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime * steeringDirection;
            transform.Rotate(0, turn, 0);
        }
        
        targetPitch = -moveInput.y * pitchTiltAmount;
        targetRoll = -turnInput * rollTiltAmount * Mathf.Clamp01(Mathf.Abs(currentSpeed) / maxSpeed);
    }
    
    private void ApplyTilts()
    {
        if (visualBody == null) return;
        
        Quaternion baseTilt = Quaternion.Euler(targetPitch, 0, targetRoll);
        visualBody.localRotation = Quaternion.Slerp(visualBody.localRotation, baseTilt, Time.fixedDeltaTime * tiltSpeed);
    }
    
    private void UpdateWheelPositions()
    {
        if (isInAir) return;
        
        PositionWheel(frontLeftWheelPos, frontLeftWheelMesh, 0);
        PositionWheel(frontRightWheelPos, frontRightWheelMesh, 1);
        PositionWheel(rearLeftWheelPos, rearLeftWheelMesh, 2);
        PositionWheel(rearRightWheelPos, rearRightWheelMesh, 3);
    }
    
    private void PositionWheel(Transform wheelPos, GameObject wheelMesh, int wheelIndex)
    {
        if (wheelPos == null || wheelMesh == null) return;
        
        if (wheelGrounded[wheelIndex])
        {
            wheelMesh.transform.position = wheelHits[wheelIndex].point + Vector3.up * wheelOffset;
        }
        else
        {
            wheelMesh.transform.position = wheelPos.position + Vector3.down * wheelFallbackDistance;
        }
    }
    
    private void UpdateWheelRotations()
    {
        float targetSteerAngle = turnInput * maxSteerAngle;
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, Time.deltaTime * wheelSteerSpeed);
        
        if (frontLeftWheelMesh != null)
        {
            RotateFrontWheel(frontLeftWheelMesh);
        }
        
        if (frontRightWheelMesh != null)
        {
            RotateFrontWheel(frontRightWheelMesh);
        }
    }
    
    private void RotateFrontWheel(GameObject wheelMesh)
    {
        wheelMesh.transform.localRotation = Quaternion.Euler(0, currentSteerAngle, 90);
    }
    
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        
        Vector3 position = transform.position;
        
        if (showWheelRays && Application.isPlaying)
        {
            for (int i = 0; i < 4; i++)
            {
                if (wheelPositions[i] == null) continue;
                
                Vector3 wheelPos = wheelPositions[i].position;
                
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(wheelPos, 0.1f);
                
                if (wheelGrounded[i])
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(wheelPos, wheelHits[i].point);
                    Gizmos.DrawWireSphere(wheelHits[i].point, 0.15f);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(wheelPos, wheelPos + Vector3.down * maxRayDistance);
                }
            }
        }
        
        if (showGroundCheck)
        {
            if (Application.isPlaying && isGrounded)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(position, position + Vector3.down * groundDistance);
                Gizmos.DrawWireSphere(position + Vector3.down * groundDistance, 0.2f);
            }
            else
            {
                Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
                Gizmos.DrawLine(position, position + Vector3.down * groundCheckDistance);
            }
        }
        
        if (showHoverHeight)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
            Vector3 targetHeightPos = position + Vector3.down * hoverHeight;
            Gizmos.DrawWireCube(targetHeightPos, new Vector3(3f, 0.02f, 3f));
        }
        
        Gizmos.color = isInAir ? Color.magenta : Color.white;
        Gizmos.DrawWireCube(position, Vector3.one * 0.3f);
        
        if (Application.isPlaying && isInAir)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(position, 0.5f);
        }
    }
}
