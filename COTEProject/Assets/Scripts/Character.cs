using UnityEngine;
/* This is a an abstract class for different character types
 * Types include Player and Enemy 
 */
public abstract class Character:MonoBehaviour
{
    // These are private for shared character attributes
    private string name;
    private float health; 
    private float maxHealth;
    private float defence;
    private float attackPower;
    private Vector3 position;
    
    // These are public properties to be able to access and modify the above private fields
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float Defence
    {
        get { return defence; }
        set { defence = value; }
    }
    
    public float AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    // This is a construction use to initalize the characters attributes
    public Character(string name, float health, float maxHealth, float attackPower, float defence)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.attackPower = attackPower;
        this.defence = defence;
        this.position = Vector3.zero;
    }
    
    /*
    public virtal int AdjustAttackPower()
    {
        attackpower 
        return attackpower;
        ???
        Also need to add cool down for weapons
        need to overide in player if we use it
    }
    */  
    
    // A virtual method for taking damage, can be overriden in derived classes
    public virtual void TakeDamage(int damage)
    {
        if (damage > defence)
        {
            health -= (damage - defence);
        }

        IsAlive();
    }
    
    // This is a method for healing the characters
    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    
    // This method is used to check if the character is alive
    public bool IsAlive()
    {
        if (health <= 0)
        {
            return false;
        }
        return true;
    }
    
    // An abstract 'Die' method the will be implementment in derived classes
    public abstract void Die();
}
