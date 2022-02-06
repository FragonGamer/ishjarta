using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private InputMaster inputMaster;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 movement;

    public Vector2 GetMovementVector() { return movement; }

    private void Awake()
    {
        player = (Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputMaster = new InputMaster();
    }
    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Start()
    {
        inputMaster.Player.Movement.performed += MoveAction;
        inputMaster.Player.Movement.canceled += _ => { 
            movement = Vector2.zero;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        };
        inputMaster.Player.Attack.performed += AttackAction;
        inputMaster.Player.DropItem.performed += DropItemAction;
        inputMaster.Player.SwitchWeapon.performed += SwitchWeaponAction;
        inputMaster.Player.UseActiveItem.performed += UseActiveItemAction;
        inputMaster.Player.DungeonGenerationTest.performed += GenerateStageAction;
    }

    private void MoveAction(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        movement.Normalize();
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    //Temporary
    private void GenerateStageAction(InputAction.CallbackContext context) {
        var stageController = GameObject.FindGameObjectWithTag("StageController");
        stageController.GetComponent<StageController>().CreateStage();
    }


    private void AttackAction(InputAction.CallbackContext context)
    {
        player.Attack(inputMaster.References.MousePosition.ReadValue<Vector2>());
    }
    private void DropItemAction(InputAction.CallbackContext context)
    {
        if (player.inventory.GetActiveItem() != null)
        {
            ActiveItem activeItem = player.inventory.GetActiveItem();
            player.inventory.DropItem(activeItem);
        }
    }
    private void SwitchWeaponAction(InputAction.CallbackContext context)
    {
        player.inventory.ChangeWeapon();
    }
    private void UseActiveItemAction(InputAction.CallbackContext context)
    {
        player.inventory.UseActiveItem();
    }
    private void Move()
    {
        rb.MovePosition(rb.position + movement * player.GetMovementSpeed() * Time.fixedDeltaTime);

    }
    /*
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.Attack(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Dropped Item");
            if (player.inventory.GetActiveItem() != null)
            {
                ActiveItem activeItem = player.inventory.GetActiveItem();
                player.inventory.DropItem(activeItem);
            }
        }
    }
    */
    private void FixedUpdate()
    {
        Move();
    }
}
