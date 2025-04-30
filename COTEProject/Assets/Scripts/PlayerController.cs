using UnityEngine;
using DefaultNamespace;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator mainAnimator;
    public bool isInsideSafeZone = false;
    public bool IsDashing => isDashing; // Expose private dash flag
    public GameObject armorVisual;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float doubleJumpHeight = 4f;
    [SerializeField] private float rotationSmoothTime = 0.1f;

    [Header("Physics Settings")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 30f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    
    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] public LayerMask enemyLayer;

    [Header("Shield Settings")]
    [SerializeField] private GameObject shieldBubble;
    [SerializeField] private float shieldDuration = 30f;
    [SerializeField] private float shieldCooldown = 60f;
    [SerializeField] private float shieldPulseSpeed = 2f;
    [SerializeField] private float shieldPulseAmount = 0.1f;
    
    [Header("Shield UI")]
    [SerializeField] public PlayerUI playerUI;
    
    [Header("Ability Lock Settings")]
    [SerializeField] private bool dashInitiallyLocked = true;
    [SerializeField] private bool doubleJumpInitiallyLocked = true;
    [SerializeField] private bool shieldInitiallyLocked = true;
    [SerializeField] private bool tripleDmgInitiallyLocked = true;
    
    private bool shieldActive = false;
    private float shieldTimer = 0f;
    private float shieldCooldownTimer = 0f;
    
    private float lastAttackTime;
    public static bool isInPopUp = false;
    private bool isJumping = false;
    private bool canDoubleJump = false;
    private bool isDashing = false;
    private float dashTimer;
    private float dashCooldownTimer;
    private Vector3 dashDirection;

    private Vector3 velocity;
    private bool isGrounded;
    private float movementSpeed;
    private float turnSmoothVelocity;
    private bool isAttacking = false;

    static public bool dialogue = false;
    public float SpeedMultiplier { get; set; } = 1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        mainAnimator = GetComponent<Animator>();

        if (!cameraTransform)
        {
            Debug.LogWarning("Camera Transform is not assigned in PlayerController!");
        }
        
        // Initialize ability states based on unlock status
        if (AbilityLockManager.Instance != null)
        {
            if (AbilityLockManager.Instance.IsShieldUnlocked)
            {
                shieldInitiallyLocked = false;
                playerUI?.UpdateShieldStatus(PlayerUI.ShieldState.Ready,false);
            }
            
            if (AbilityLockManager.Instance.IsDashUnlocked)
            {
                dashInitiallyLocked = false;
            }
            
            if (AbilityLockManager.Instance.IsDoubleJumpUnlocked)
            {
                doubleJumpInitiallyLocked = false;
            }

            if (AbilityLockManager.Instance.IsTripleDmgUnlocked)
            {
                tripleDmgInitiallyLocked = false;
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Checkpoint.isCheckpointActive = true;
        }
        if (Checkpoint.isCheckpointActive)
        {
            controller.enabled = false;
            transform.position = Checkpoint.lastCheckpointPosition;
            controller.enabled = true;
            Checkpoint.isCheckpointActive = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursor();
        }

        if (!isAttacking && !dialogue) // Prevent movement while attacking and in dialogue
        {
            HandleMovement();
            ApplyGravity();
        }

        HandleAnimations();

        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Attack();
        }
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        
        if (!isDashing && Input.GetKeyDown(KeyCode.E) && dashCooldownTimer <= 0)
        {
            StartDash();
        }

        if (isDashing)
        {
            DashMove();
        }
        
        // Handle shield timers
        if (shieldActive)
        {
            shieldTimer -= Time.deltaTime;
            playerUI?.UpdateShieldUI(shieldTimer);
            if (shieldTimer <= 0)
            {
                DeactivateShield();
            }
        }


        if (shieldCooldownTimer > 0)
        {
            shieldCooldownTimer -= Time.deltaTime;
            if (shieldCooldownTimer <= 0)
            {
                playerUI?.UpdateShieldStatus(PlayerUI.ShieldState.Ready, !shieldInitiallyLocked);
            }
        }

    
        // Activate shield with Q key
        if (Input.GetKeyDown(KeyCode.Q) && !shieldActive && shieldCooldownTimer <= 0)
        {
            ActivateShield();
        }
        
        if (shieldActive && shieldBubble != null)
        {
            // Create a pulsing effect
            float scale = 3f + Mathf.Sin(Time.time * shieldPulseSpeed) * shieldPulseAmount;
            shieldBubble.transform.localScale = Vector3.one * scale;
        }
 
        if (dialogue || isInPopUp)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Health Potion
            UsePotion("Health Potion");

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Shield Potion
            UsePotion("Shield Potion");

        if (Input.GetKeyDown(KeyCode.Alpha3)) // Speed Potion
            UsePotion("Speed Potion");
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleArmor();
        }
    }

    void HandleMovement()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            movementSpeed = (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) * SpeedMultiplier;
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
        
        if (doubleJumpInitiallyLocked && AbilityLockManager.Instance != null && !AbilityLockManager.Instance.IsDoubleJumpUnlocked && Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Double jump is locked! Must unlock it first in Air level.");
            return;
        }
        
        //Checking if player is on the ground
        if (isGrounded)
        {
            canDoubleJump = true;
            isJumping = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                mainAnimator.SetTrigger("Jump");
            }
        }
        else if (canDoubleJump && Input.GetKeyDown(KeyCode.B) && 
                 !doubleJumpInitiallyLocked && 
                 AbilityLockManager.Instance != null && 
                 AbilityLockManager.Instance.IsDoubleJumpUnlocked)
        {
            canDoubleJump = false;
            velocity.y = Mathf.Sqrt(doubleJumpHeight * -2f * gravity); 
            mainAnimator.SetTrigger("Jump");
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleAnimations()
    {
        float moveMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        mainAnimator.SetFloat("Walk", moveMagnitude * (movementSpeed / runSpeed));
    }

    void Attack()
    {
        if (dialogue || isAttacking || Time.time < lastAttackTime + attackCooldown)
            return;

        isAttacking = true;
        lastAttackTime = Time.time;

        mainAnimator.SetTrigger("Attack");

        movementSpeed = 0;

        Invoke(nameof(EndAttack), 0.8f); // match your animation length
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1.5f + Vector3.up * 1.4f, attackRange);
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }
    void StartDash()
    {
        if (dashInitiallyLocked && AbilityLockManager.Instance != null && !AbilityLockManager.Instance.IsDashUnlocked)
        {
            Debug.Log("Dash is locked! Must unlock it first in Water level.");
            return;
        }

        if (dashCooldownTimer > 0) return;

        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude == 0)
        {
            inputDirection = transform.forward;
        }
        else
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            dashDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        dashDirection = dashDirection.normalized;
        //mainAnimator.SetTrigger("Dash"); // Optional: Add a "Dash" animation trigger
    }

    void DashMove()
    {
        controller.Move(dashDirection * dashSpeed * Time.deltaTime);

        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            isDashing = false;
        }
    }
    void UsePotion(string potionName)
    {
        var player = Player.User;
        var inventory = player.Inventory;

        // Find the consumable
        Consumable potion = inventory.Consumables.Find(p => p.ItemName == potionName);
    
        if (potion == null || potion.ItemCount <= 0)
        {
            Debug.Log($"No {potionName}s left!");
            return;
        }

        // Use the potion
        potion.Use(player);

        // Remove 1 from inventory
        inventory.RemoveItem(potion);

        // Update UI
        player.UpdateUI();
    }

    public void PerformAttackHit()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(
            transform.position + transform.forward * 1.5f + Vector3.up * 1.4f, 
            attackRange, 
            enemyLayer
        );

        foreach (Collider enemyCollider in hitEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (enemy == null) continue;

            // Apply base damage
            Player.User.DoDamage(enemy);
        
            // Check for triple damage (right mouse button)
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (CanUseTripleDamage())
                {
                    Debug.Log("Triple damage attack");
                    ApplyTripleDamage(enemy);
                }
                else
                {
                    Debug.Log("Triple damage is locked! Unlock in Fire level.");
                }
            }
        }
    }

    private bool CanUseTripleDamage()
    {
        return AbilityLockManager.Instance != null && 
               AbilityLockManager.Instance.IsTripleDmgUnlocked &&
               !tripleDmgInitiallyLocked;
    }

    private void ApplyTripleDamage(Enemy enemy)
    {
        // Apply two additional hits (total 3x damage)
        Player.User.DoDamage(enemy);
        Player.User.DoDamage(enemy);
    }
    
    void ActivateShield()
    {
        if (shieldInitiallyLocked && AbilityLockManager.Instance != null && !AbilityLockManager.Instance.IsShieldUnlocked)
        {
            Debug.Log("Shield is locked! Must unlock it first in Earth level.");
            return;
        }
        shieldActive = true;
        shieldTimer = shieldDuration;
        
        // Debug UI activation
        if (playerUI != null)
        {
            Debug.Log("Attempting to show shield UI");
            playerUI.ShowShieldUI(shieldDuration);
            playerUI.UpdateShieldStatus(PlayerUI.ShieldState.Active, !shieldInitiallyLocked);
        }
        else
        {
            Debug.LogError("PlayerUI reference is missing!");
        }
        
        if (shieldBubble != null)
        {
            shieldBubble.SetActive(true);
        }
 
    }
    void DeactivateShield()
    {
        shieldActive = false;
        shieldCooldownTimer = shieldCooldown;

        if (shieldBubble != null)
        {
            shieldBubble.SetActive(false);
        }
        playerUI?.UpdateShieldStatus(PlayerUI.ShieldState.Cooldown, !shieldInitiallyLocked);
        playerUI?.HideShieldUI();
    }
    public bool IsShieldActive()
    {
        return shieldActive;
    }
    public void UnlockShieldAbility()
    {
        shieldInitiallyLocked = false;
        playerUI?.UpdateShieldStatus(PlayerUI.ShieldState.Ready, true);
    }


    
    void ToggleArmor()
    {
        var player = Player.User;

        if (!player.HasArmor)
        {
            Debug.Log("No armor purchased.");
            return;
        }

        if (player.IsArmorEquipped)
        {
            Debug.Log("Taking off armor.");
            player.Defence -= 2; 
            player.IsArmorEquipped = false;
            armorVisual?.SetActive(false);
        }
        else
        {
            Debug.Log("Putting on armor.");
            player.Defence += 2;
            player.IsArmorEquipped = true;
            armorVisual?.SetActive(true);
        }
    }
    
    public void UnlockDashAbility()
    {
        dashInitiallyLocked = false;
    }
    public void UnlockDoubleJumpAbility()
    {
        doubleJumpInitiallyLocked = false;
    }
    public void UnlockTripleDmgAbility()
    {
        tripleDmgInitiallyLocked = false;
    }
}