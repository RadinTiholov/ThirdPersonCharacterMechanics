using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float fightSpeed = 1, fightBackSpeed = 1;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;
    public float acceleration = 5f;
    public float deceleration = 5f;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput, vInput;
    CharacterController controller;
    #endregion

    [SerializeField]
    float groundYOffset;
    [SerializeField]
    LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    public FightState Fight = new FightState();

    [HideInInspector] public Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        float targetSpeed = dir.magnitude * currentMoveSpeed;
        if (targetSpeed > currentMoveSpeed)
        {
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, targetSpeed, deceleration * Time.deltaTime);
        }

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool isGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        return Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask);
    }

    void Gravity()
    {
        if (!isGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
