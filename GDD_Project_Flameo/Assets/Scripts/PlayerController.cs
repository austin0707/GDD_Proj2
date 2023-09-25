using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How fast the player should move when running around.")]
    private float m_Speed;

    [SerializeField]
    [Tooltip("The transform of the camera following the player.")]
    private Transform m_CameraTransform;

    [SerializeField]
    [Tooltip("Force applied to the player when jumping")]
    private float m_JumpForce;

    [SerializeField]
    [Tooltip("Cooldown until next jump")]
    private float m_JumpCooldown;

    [SerializeField]
    private float m_AirMultiplier;

    [Header("Ground Check")]
    [SerializeField]
    private float m_PlayerHeight;
    [SerializeField]
    private LayerMask m_IsGround;
    [SerializeField]
    private float m_GroundDrag;

    [Header("Keybinds")]
    private KeyCode m_JumpKey = KeyCode.Space;
    #endregion

    #region Cached Components
    private Rigidbody cc_Rb;
    #endregion

    #region Private Variables
    private float p_HorizontalInput;
    private float p_VerticalInput;
    private Vector3 p_MoveDirection;
    private bool p_Grounded;
    private bool p_ReadyToJump;
    #endregion

    #region Initialization
    private void Awake()
    {
        cc_Rb = GetComponent<Rigidbody>();
        cc_Rb.freezeRotation = true;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region Main Updates
    private void Update()
    {
        p_Grounded = Physics.Raycast(transform.position, Vector3.down, m_PlayerHeight * 0.5f + 0.2f, m_IsGround);

        MyInput();
        SpeedControl();

        // Handles drag
        if (p_Grounded)
        {
            cc_Rb.drag = m_GroundDrag;
        } else
        {
            cc_Rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    #endregion

    #region Movement Methods
    private void MyInput()
    {
        p_HorizontalInput = Input.GetAxisRaw("Horizontal");
        p_VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //Calculate movement direction
        p_MoveDirection = m_CameraTransform.forward * p_VerticalInput + m_CameraTransform.right * p_HorizontalInput;

        cc_Rb.AddForce(p_MoveDirection.normalized * m_Speed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(cc_Rb.velocity.x, 0f, cc_Rb.velocity.z);

        if (flatVel.magnitude > m_Speed)
        {
            Vector3 limitedVel = flatVel.normalized * m_Speed;
            cc_Rb.velocity = new Vector3(limitedVel.x, cc_Rb.velocity.y, limitedVel.z);
        }
    }
    
    private void Jump()
    {
        cc_Rb.velocity = new Vector3(cc_Rb.velocity.x, 0f, cc_Rb.velocity.z);

        cc_Rb.AddForce(transform.up * m_JumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        p_ReadyToJump = true;
    }
    #endregion
}
