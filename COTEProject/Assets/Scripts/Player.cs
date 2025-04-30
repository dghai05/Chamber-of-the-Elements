using System;
using DefaultNamespace;
using UnityEngine;
/* This class is an extention for the character class
 * it implements, extends, and overrides its methods
 ****** Lost of commented out methods due to missing classes *****
 */
public class Player:Character
{
    public static Player User { get; private set; }
    
    private int coinBalance;
    private Weapon equippedWeapon;
    private Inventory inventory = new Inventory();
    private PlayerController characterController;
    private Vector3 latestCheckpoint;
    public PlayerUI playerUI;
    public bool HasArmor { get; set; } = false;
    public bool IsArmorEquipped { get; set; } = false;
    public int CoinBalance
    {
        get { return coinBalance; }
        set 
        { 
            coinBalance = Mathf.Max(0, value); 
            playerUI.SetCoins(coinBalance); 
        }
    }
    
    public Weapon EquippedWeapon
    {
        get { return equippedWeapon; }
        set { equippedWeapon = value; }
    }

    public Inventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    public PlayerController CharacterController
    {
        get { return characterController; }
        set { characterController = value; }
    }

    public Vector3 LatestCheckpoint
    {
        get { return latestCheckpoint; }
        set { latestCheckpoint = value; }
    }

    public PlayerUI PlayerUI
    {
        get { return playerUI; }
        set { playerUI = value; }
    }

    public Player(string name, float health, float maxHealth, float attackPower, float defence, Weapon equippedWeapon)
        : base(name, health, maxHealth, attackPower, defence)
    {
        Name = name;
        Health = health;
        MaxHealth = maxHealth;
        coinBalance = 0;
        EquippedWeapon = equippedWeapon;
        HasArmor = false;
        IsArmorEquipped = false;
        Inventory = new Inventory();
        latestCheckpoint = Vector3.zero; 
    }

    public void DoDamage(Enemy enemy)
    {
        float damage = AttackPower;
        enemy.TakeDamage(damage);
    }
    
    public override void Die()
    {
        Health = MaxHealth;
        Checkpoint.isCheckpointActive = true;
        UpdateUI();
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        LatestCheckpoint = newCheckpoint;
    }

    public void PickUpCoin(int amount)
    {
        coinBalance += amount;
        playerUI.SetCoins(coinBalance);
    }

    public void RemoveCoins(int amount)
    {
        coinBalance -= amount;
        playerUI.SetCoins(coinBalance);
    }

    
    public void UpdateUI()
    {
        User.playerUI.SetHealth(User.Health, User.MaxHealth);
        User.playerUI.SetCoins(User.CoinBalance);
        User.playerUI.SetHealthPotions(User.Inventory.GetConsumableCount("Health Potion"));
        User.playerUI.SetShieldPotions(User.Inventory.GetConsumableCount("Shield Potion"));
        User.playerUI.SetSpeedPotions(User.Inventory.GetConsumableCount("Speed Potion"));
        playerUI.CheckArmorVisibility();

    }
    private void Awake()
    {
        if (User != null && User != this)
        {
            Destroy(gameObject);
            return;
        }

        User = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (!characterController.IsShieldActive())
        {
           float reducedDamage = damage * (Defence / 100f);
           float actualDamage = Mathf.Max(0, damage - reducedDamage);
        
           Health = Mathf.Max(0, Health - actualDamage);
        
           UpdateUI();
        
           Debug.Log($"Player took {actualDamage} damage. Current HP: {Health}/{MaxHealth}");
        
           if (Health <= 0)
           {
               Die();
           }
        }
        else
        {
           // Shield blocks damage
           Debug.Log("Damage blocked by shield!");
        }
    }

    private void Start()
    {
        Name = "Ileus";
        MaxHealth = 100;
        Health = 100;
        AttackPower = 10;
        Defence = 0;
        CoinBalance = 0;
        Inventory = new Inventory();
        characterController = GetComponent<PlayerController>();
        UpdateUI();
    }
    
}
