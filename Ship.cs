namespace EclipseSimulatorApp;

public class WeaponLibrary
{
    // Singleton pattern or static class to hold weapon templates
    private static readonly Dictionary<string, Weapon> WeaponTemplates = new();

    public static void RegisterWeapon(string weaponId, Weapon weapon)
    {
        WeaponTemplates[weaponId] = weapon;
    }

    public static Weapon GetWeapon(string weaponId)
    {
        return WeaponTemplates[weaponId];
    }
}

public class Ship
{
    private readonly Random randomTargetingOrder = new Random();

    public string _shipId;
    
    public Ship? _target;
    private readonly string[] _weaponsReferences;
    private float _currentHealth;
    private float _maxHealth;
    public bool isDestroyed;
    public bool isEngaged;
    
    // for now either targeting device is present or not - possible development
    private bool targetingDevice;
    
    // Constructor
    public Ship(string[] weaponIds, float maxHealth = 100, bool targetingDevice = false)
    {
        _weaponsReferences = weaponIds;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        this.targetingDevice = targetingDevice; //using this to avoid naming conflicts
        this.isEngaged = true;
        this.isDestroyed = false;

    }

    public Ship(Ship other)
    {
        _weaponsReferences = other._weaponsReferences;
        _maxHealth = other._maxHealth;
        _currentHealth = _maxHealth;
        this.targetingDevice = other.targetingDevice;
        this.isEngaged = other.isEngaged;
        this.isDestroyed = other.isDestroyed;
    }
    
    public void Attack(List<Ship> queueOfBattle)
    {
        // Seek target only if target is destroyed or not present
        if (_target == null || _target.isDestroyed)
        {
            this._target = FindTarget();
        }
        
        // Fire all weapons at target
        for (int i = 0; i < _weaponsReferences.Length; i++)
        {
            FireWeapon(_weaponsReferences[i]);
        }
        // Currently all ships are tageting themself
        Ship FindTarget()
        {   
            Ship target;
            if (targetingDevice)
            {
                // if targeting device is present
                int targetingRange = randomTargetingOrder.Next(0, queueOfBattle.Count);
                target = queueOfBattle[targetingRange];

            }
            else
            {
                // if targeting device is not present - for now default
                
                    // for now it allows friendly fire XD
                    
                int targetingRange = randomTargetingOrder.Next(0, queueOfBattle.Count);
                target = queueOfBattle[targetingRange];
                
            }
            return target;
        }

        void FireWeapon(string weaponIndex)
        {
            Weapon currentWeapon = WeaponLibrary.GetWeapon(weaponIndex);
            // Only shoot if target is present
                // Prevent null reference exception if target is destroyed right before shooting
            if (this._target != null)
            {
                currentWeapon.Fire(_target);
                
                // Was only for debugging and it clearly works
                // Console.WriteLine($"Weapon {currentWeapon} was fired by {this._shipId} at {this._target._shipId}");
            }
        }
        
    }

    public void GetDamage(float damage, string damageType)
    {
        // for now only damage is important, damage types will be added later. 
        _currentHealth -= damage;
        // return true if ship is destroyed
        if (_currentHealth <= 0)
        {
            isDestroyed = true;
            isEngaged = false;
        }
    }

    public void AssignId(string shipId)
    {
        this._shipId = shipId;
    }
    
    // no real purpose for now
    public void Repair()
    {
        
    }
}

public sealed class Weapon
{
    private float _damage;
    // public int range; // for now it has no purpose
    private double _accuracy;
    private string _damageType;

    // Constructor
    internal Weapon(
        float damage, double accuracy, DamageType damageType)
    {
        this._damage = damage;
        this._accuracy = accuracy;
        this._damageType = damageType.ToString();
    }
    
    public void Fire (Ship target)
    {
        bool hit = Random.Shared.NextDouble() < _accuracy;
        
        if (hit)
        {
            target.GetDamage(_damage, _damageType);
        }
    }
}

public enum DamageType : byte
{
    Energy,
    Kinetic,
    Thermal,
    Explosive,
}
