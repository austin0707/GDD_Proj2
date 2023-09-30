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
    private float m_GroundDrag;

    [Header("Keybinds")]
    [SerializeField]
    [Tooltip("Key to press to jump")]
    private KeyCode m_JumpKey = KeyCode.Space;
    #endregion

    #region Cached Components
    private Rigidbody cc_Rb;
    #endregion

    #region GrabVars
    [SerializeField] Transform holdArea;
    private GameObject heldObject;
    private Rigidbody RBheldObject;
    private float pickuprange = 7.0f;
    private float pickupforce = 10.0f; 
    [SerializeField] private LayerMask PickupMask;
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

        p_ReadyToJump = true;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region Main Updates
    private void Update()
    {
        if(Input.GetKey(KeyCode.E)){
            if(heldObject == null){
                RaycastHit hit;
                // Creates a Ray from the center of the viewport
                Ray ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
                if(Physics.Raycast(ray, out hit, pickuprange)){
                    if( hit.transform.gameObject.layer == 6){
                        pickUpObject(hit.transform.gameObject);
                    }
                    
                }

            }
            else{
                dropObject();
            }
        }
        if(heldObject != null){
            MoveObject();
        }

        p_Grounded = CheckBelowTag("Ground");

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
    void pickUpObject(GameObject pickObj){
        if(pickObj.GetComponent<Rigidbody>()){
            RBheldObject = pickObj.GetComponent<Rigidbody>();
            RBheldObject.useGravity = false;
            RBheldObject.drag = 10;
            RBheldObject.constraints = RigidbodyConstraints.FreezeRotation;
            RBheldObject.transform.parent = holdArea;
            heldObject = pickObj;
        }
    }
    void MoveObject(){
        if(Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f){
            Vector3 movedir = (holdArea.position - heldObject.transform.position);
            RBheldObject.AddForce(movedir * pickupforce);
        }
    }
    void dropObject(){
        RBheldObject.useGravity = true;
        RBheldObject.drag = 1;
        RBheldObject.constraints = RigidbodyConstraints.None;
        RBheldObject.transform.parent = null;
        heldObject = null;
        
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

        // When to jump
        if (Input.GetKey(m_JumpKey) && p_ReadyToJump && p_Grounded)
        {
            p_ReadyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), m_JumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //Calculate movement direction
        p_MoveDirection = (new Vector3(m_CameraTransform.forward.x, 0, m_CameraTransform.forward.z)) * p_VerticalInput 
            + (new Vector3(m_CameraTransform.right.x, 0, m_CameraTransform.right.z)) * p_HorizontalInput;

        // on ground
        if (p_Grounded)
        {
            cc_Rb.AddForce(p_MoveDirection.normalized * m_Speed * 10f, ForceMode.Force);
        } else // in air
        {
            cc_Rb.AddForce(p_MoveDirection.normalized * m_Speed * 10f * m_AirMultiplier, ForceMode.Force);
        }
        
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

    #region Miscellaneous Methods
    public bool CheckBelowTag(string tag)
    {
        RaycastHit hit;
        float maxDistance = m_PlayerHeight * 0.5f + 0.2f;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.distance < maxDistance && hit.collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
