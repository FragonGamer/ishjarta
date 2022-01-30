using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField] GameObject FirePoint;
    [SerializeField] int rangeModifier;
    [SerializeField] int luck;
    [SerializeField]private float timeRanged = 0.0f;
    [SerializeField]private float timeMelee = 0.0f;
    //[SerializeField] int maxResistance;

    [SerializeField] private HealthBar hpBar;

    public int GetBaseDamage() { return baseDamage; }
    public void SetBaseDamage(int value) { baseDamage = value; }

    private void Awake()
    {
        hpBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
    }
    void Start()
    {
        hpBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
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
            resistance = (1 * armorAmount) / (2.5f + armorAmount) * 0.25f;
    }
    
    public float GetResistence()
    {
        return resistance;
    }
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
    public Inventory inventory;

    public int DealingDamage
    {
        get
        {
            return (int)(this.baseDamage * damageModifier);
        }
    }



    public override void Attack(Vector2 mousePos)
    {
        //MeleeAttack
        if (GetComponent<PolygonCollider2D>() == null && inventory.CurrentWeapon is MeleeWeapon melWeapon)
        {
            if (timeMelee >= melWeapon.AttackRate)
            {
                timeMelee = 0f;

                melWeapon.Range = 1.2f;
                melWeapon.Width = 0.3f;
                float angle = CalculateDegreesInRad(mousePos);

                Vector2[] v = new Vector2[]
                {
                    RotateVector2(new Vector2 {x = 0, y = 0}, angle),
                    RotateVector2(new Vector2 {x = melWeapon.Range * (0.7f), y = melWeapon.Width}, angle),
                    RotateVector2(new Vector2 {x = melWeapon.Range, y = 0}, angle),
                    RotateVector2(new Vector2 {x = melWeapon.Range * (0.7f), y = melWeapon.Width * (-1)}, angle)
                };

                PolygonCollider2D pc = this.gameObject.AddComponent<PolygonCollider2D>();
                pc.isTrigger = true;
                pc.points = v;

                Destroy(pc, 0.2f);
            }
        }
        //Ranged Attack
        else if (inventory.CurrentWeapon is RangedWeapon curWeapon)
        {
            if (timeRanged >= curWeapon.AttackRate)
            {
                timeRanged = 0f;
                
                Vector2 lookdir = (Vector2)Camera.main.ScreenToWorldPoint(mousePos) - GetComponent<Rigidbody2D>().position;
                float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;


                FirePoint.transform.rotation = Quaternion.Euler( 0f ,0f , angle);

                GameObject arrow = Instantiate((GameObject)Resources.Load($"Prefabs/ArrowBasic") as GameObject,
                    (Quaternion.Euler(0f, 0f, angle)*(FirePoint.transform.position - transform.position))+transform.position, FirePoint.transform.rotation);
            
                arrow.GetComponent<Arrow>().DealingDammage = DealingDamage;
                arrow.GetComponent<Rigidbody2D>().AddForce((FirePoint.transform.up) * curWeapon.ProjectileVelocity, ForceMode2D.Impulse);

                Destroy(arrow, 10f);
            }
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
    private Vector2 RotateVector2(Vector2 vec, float angle)
    {
        Vector2 result = new Vector2();

        result.x = (float)(Math.Cos(angle) * vec.x - Math.Sin(angle) * vec.y);
        result.y = (float)(Math.Sin(angle) * vec.x + Math.Cos(angle) * vec.y);

        return result;
    }

    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    new public void ReceiveDamage(int damage)
    {
        damage = (damage - ((int)(damage * resistance)));
        currentHealth -= damage;

        Debug.Log(name + " is being attacked");

        hpBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
}