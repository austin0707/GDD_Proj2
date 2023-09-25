using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private Transform cameraPosition;
    #endregion

    #region Main Updates
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
    #endregion
}
