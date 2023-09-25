using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The player to follow")]
    private Transform m_PlayerTransform;

    [SerializeField]
    [Tooltip("The offset from the player's origin to the camera")]
    private Vector3 m_Offset;

    [SerializeField]
    private float sensX;

    [SerializeField]
    private float sensY;

    [SerializeField]
    private Transform orientation;
    #endregion

    #region Private Variables
    private float p_yRotation;
    private float p_xRotation;
    #endregion

    #region Main Updates
    private void LateUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        p_yRotation += mouseX;
        p_xRotation -= mouseY;
        p_xRotation = Mathf.Clamp(p_xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(p_xRotation, p_yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, p_yRotation, 0);

    }
    #endregion  
}
