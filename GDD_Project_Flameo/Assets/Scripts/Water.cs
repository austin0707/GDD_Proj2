using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Water : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The player object")]
    GameObject Player;
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player) {
            SceneManager.LoadScene("tryagain");
        }
    }
}
