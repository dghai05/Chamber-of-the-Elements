using System;
using DefaultNamespace;
using Unity.Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character
{

    private int killValue; // Coins recieved for killing enemy
    private GameObject enemyGameObject;
    public EnemyHealthBar healthBar;
    public string elementLevelName;
    public float totalHealth;
    public int value;
    public float attack;
    public float defence;
    
    public int KillValue
    {
        get { return killValue; }
        set { killValue = value; }
    }
    public Enemy(string name, float health, float maxHealth, float attackPower, float defence, int killValue) 
        : base(name, health, maxHealth, attackPower, defence)
    {
        Name = name;
        MaxHealth = maxHealth;
        AttackPower = attackPower;
        Defence = defence;
        KillValue = killValue;
        Health = MaxHealth;
    }

    public virtual void DropItem(int Value)
    {
        Player.User.CoinBalance += Value;
    }

    public override void Die()
    {
        DropItem(KillValue);

        int levelIndex = GetLevelIndexFromScene(SceneManager.GetActiveScene().name);
        if (levelIndex != -1)
        {
            PlayerPrefs.SetInt("LastDefeatedElement", levelIndex);
            PlayerPrefs.SetInt("HighestCompletedLevel", Mathf.Max(PlayerPrefs.GetInt("HighestCompletedLevel", -1), levelIndex));
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("MainHub");
        Destroy(gameObject);
    }
    int GetLevelIndexFromScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Earth": return 0;
            case "Water": return 1;
            case "Air": return 2;
            case "Fire": return 3;
            default: return -1;
        }
    }


    public void TakeDamage(float damage)
    {
        if (IsAlive())
        {
            float damageReduced = damage * (Defence / 100f);
            float actualDamage = Mathf.Max(0, damage - damageReduced);
            Health = Mathf.Max(0, Health - actualDamage);
            
            
            UpdateUI();

            if (Health <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("Enemy already dead");
        }
        Debug.Log($"Enemy {name} took damage. Health now: {Health}/{MaxHealth}");
    }
    
    public void UpdateUI()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(Health, MaxHealth);
        }
    }
    void Start()
    {
        Name = "Enemy";
        MaxHealth = totalHealth;
        Health = MaxHealth;
        AttackPower = attack;
        Defence = defence;
        KillValue = value;

        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<EnemyHealthBar>();
            if (healthBar == null)
                Debug.LogWarning($"{name} has no EnemyHealthBar component in children!");
        }

        if (healthBar != null)
            healthBar.SetTarget(transform);

        UpdateUI();
    }
}