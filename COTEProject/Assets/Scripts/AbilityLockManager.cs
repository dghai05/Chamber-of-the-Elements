using UnityEngine;


public class AbilityLockManager : MonoBehaviour
{
    public static AbilityLockManager Instance { get; private set; }
  
    public bool IsShieldUnlocked { get; private set; } = false;
    public bool IsDashUnlocked { get; private set; } = false;
    public bool IsDoubleJumpUnlocked { get; private set; } = false;
    public bool IsTripleDmgUnlocked { get; private set; } = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void UnlockShield() => IsShieldUnlocked = true;
    public void UnlockDash() => IsDashUnlocked = true;
    public void UnlockDoubleJump() => IsDoubleJumpUnlocked = true;
    public void UnlockTripleDmg() => IsTripleDmgUnlocked = true;
}