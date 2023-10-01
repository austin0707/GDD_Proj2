using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    #region
    [SerializeField]
    [Tooltip("the player object")]
    GameObject Player;
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player) {
            Debug.Log("ok");
            SceneManager.LoadScene("endgame");
        }
    }
}
