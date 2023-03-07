using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditor;

public class Player : Entity
{
    
    [SerializeField] AudioSource audiosource;
    [SerializeField] public AudioClip arrowsound;
    [SerializeField] GameObject FirePoint;
    [SerializeField] int rangeModifier;
    [SerializeField] public ItemManager nearestItemManager = null;
    public bool canSeeFullDesc = false;
    [SerializeField] int luck;
    [SerializeField] private float timeRanged = 0.0f;
    [SerializeField] private float timeMelee = 0.0f;
    [SerializeField] public Room currentRoom;
    //[SerializeField] int maxResistance;
    public int visitedRooms = 1;
    [SerializeField] private HealthBar hpBar;
    [SerializeField] private ParticleSystem attackParticleSystem;
    #region SaveSystem
    private bool isPlayerInitialized = false;
    public void Init(PlayerData playerData)
    {
        if (!isPlayerInitialized)
        {
            isPlayerInitialized = true;

            base.Init(playerData);

            this.transform.position = playerData.position;
            inventory.Init(playerData.inventory);
        }
    }
    #endregion SaveSystem

    public void AddToMaxHealth(int health)
    {
        MaxHealth += health;
    }
    public void RemoveFromMaxHealth(int health)
    {
        MaxHealth -= health;
    }
    public void AddToCurrentHealth(int health)
    {
        CurrentHealth += health;
    }
    public int GetBaseDamage() { return BaseDamage; }
    public void SetBaseDamage(int value) { BaseDamage = value; }

    public override void UpdateHealthBar()
    {
        hpBar.SetHealth(CurrentHealth);
    }

    private void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        hpBar.SetMaxHealth(MaxHealth);
        attackParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private ItemManager GetNearestItemManager()
    {
        var managers = FindObjectsOfType<ItemManager>().Where(item => item);
        if (managers != null)
        {
            ItemManager closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (var manager in managers)
            {
                manager.isNearest = false;
                Vector3 diff = manager.gameObject.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = manager;
                    distance = curDistance;
                }
            

            
            }

            if (closest != null && closest.isInRange)
            {
                closest.isNearest = true;

            }
            
            return closest;
        }
        else
        {
            return null;
        }
    }
    private void Update()
    {
        UpdateHealthBar();
        nearestItemManager = GetNearestItemManager();
        

        HandleEffects();
        if (inventory.GetMeleeWeapon() != null && timeMelee < inventory.GetMeleeWeapon().AttackRate)
        {
            timeMelee += Time.deltaTime;
        }
        if (inventory.GetRangedWeapon() != null && timeRanged < inventory.GetRangedWeapon().AttackRate)
        {
            timeRanged += Time.deltaTime;
        }
    }

    public void CalcResistence()
    {
        int armorAmount = inventory.GetArmor().Amount;
        Resistance = (1 * armorAmount) / (2.5f + armorAmount) * 0.3f;
        CurrentResistance = Resistance;
    }

    public float GetResistence()
    {
        return CurrentResistance;
    }
    public float GetMovementSpeed()
    {
        return MovementSpeed * SpeedModifier;
    }
    public Inventory inventory;

    public Inventory GetInventory()
    {
        return inventory;
    }
    public float GetDamageModifierer()
    {
        return DamageModifier;
    }
    public void AddDamageModifierer(float DM)
    {
        DamageModifier += DM;
    }
    public void RemoveDamageModifierer(float DM)
    {
        DamageModifier -= DM;

    }
 public void AddSpeedModifierer(float sp)
    {
        this.SpeedModifier += sp;
    }
    public void RemoveSpeedModifierer(float sp)
    {
        SpeedModifier -= sp;

    }
    public void AddAttackRate(int ar)
    {
        AttackRate += ar;
    }
    public void RemoveAttackRate(int ar)
    {
        AttackRate -= ar;

    }

    public List<BaseEffect> GetCurrentEffects =>
        inventory.CurrentWeapon is MeleeWeapon || inventory.CurrentWeapon is RangedWeapon ?
        inventory.CurrentWeapon.EmitEffects : null;

    private void MeeleeAttack(Vector2 mousePos, MeleeWeapon melWeapon)
    {
        if (GetComponent<PolygonCollider2D>() == null)
        {
            if (timeMelee >= melWeapon.AttackRate)
            {
                timeMelee = 0f;

                float angle = CalculateDegreesInRad(mousePos);

                Vector2[] v = new Vector2[]
                {
                    new Vector2 {x = 0, y = 0},
                    RotateVector2(new Vector2 {x = melWeapon.Range * (0.7f), y = melWeapon.Width}, angle),
                    RotateVector2(new Vector2 {x = melWeapon.Range, y = 0}, angle),
                    RotateVector2(new Vector2 {x = melWeapon.Range * (0.7f), y = melWeapon.Width * (-1)}, angle)
                };

                PolygonCollider2D pc = this.gameObject.AddComponent<PolygonCollider2D>();
                pc.isTrigger = true;
                pc.points = v;
                var angleOfAttack = Vector2.Angle(v[1],v[3]);

                attackParticleSystem.transform.localRotation = Quaternion.Euler(0, 0, (angle * Mathf.Rad2Deg)-angleOfAttack/2);
                var shape = attackParticleSystem.shape;
                shape.arc = angleOfAttack;
                attackParticleSystem.Play();
                Destroy(pc, 0.2f);
            }
        }
    }
    private void RangedAttack(Vector2 mousePos, float damageChargeModifier, RangedWeapon curWeapon)
    {
        if (timeRanged >= curWeapon.AttackRate)
        {
            timeRanged = 0f;

            Vector2 lookdir = (Vector2)Camera.main.ScreenToWorldPoint(mousePos) - GetComponent<Rigidbody2D>().position;
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;


            FirePoint.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            var arrowvariant = "ArrowBasic";
            var projectiles = Utils.LoadIRessourceLocations<GameObject>(new string[] { "Projectile" });
            var projectileObject = Utils.LoadGameObjectByName(projectiles, arrowvariant);
            GameObject projectile = Instantiate(projectileObject,
                (Quaternion.Euler(0f, 0f, angle) * (FirePoint.transform.position - transform.position)) + transform.position, FirePoint.transform.rotation);

            projectile.GetComponent<Projectile>().DealingDamage = Mathf.FloorToInt(DealingDamage * damageChargeModifier);
            projectile.GetComponent<Projectile>().EmitEffects = GetCurrentEffects;
            projectile.GetComponent<Projectile>().Owner = this.gameObject;
            projectile.GetComponent<Rigidbody2D>().AddForce((FirePoint.transform.up) * curWeapon.ProjectileVelocity, ForceMode2D.Impulse);

            audiosource.clip = arrowsound;
            audiosource.Play();

            Debug.Log("shot arrow");

            Debug.Log(projectile.GetComponent<Projectile>().DealingDamage);

            Destroy(projectile, 10f);
        }
    }
    public override void Attack(Vector2 mousePos, float damageChargeModifier)
    {
        if (GetComponent<PolygonCollider2D>() == null && inventory.CurrentWeapon is MeleeWeapon melWeapon)
        {
            MeeleeAttack(mousePos,melWeapon);
        }
        //Ranged Attack
        else if (inventory.CurrentWeapon is RangedWeapon curWeapon)
        {

            RangedAttack(mousePos,damageChargeModifier,curWeapon);
        }

    }

    protected override void Die()
    {
        SaveManager.DeleteSave(SceneManager.GetActiveScene().name);
        Debug.Log("Died");
        AssetBundle.UnloadAllAssetBundles(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void ReceiveDamage(int damage)
    {
        damage = (damage - ((int)(damage * Resistance)));
        CurrentHealth -= damage;

        Debug.Log(name + " is being attacked");

        hpBar.SetHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    private float CalculateDegreesInRad(Vector2 vec)
    {
        Vector2 lookdir = (Vector2)Camera.main.ScreenToWorldPoint(vec) - GetComponent<Rigidbody2D>().position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x);
        if (angle < 0)
            angle = (float)(Math.PI - Math.Abs(angle) + Math.PI);

        return angle;
    }
}