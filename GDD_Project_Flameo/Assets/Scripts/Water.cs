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
            GetComponent<AudioSource>().Play();

            StartCoroutine(Reset());
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("tryagain");
    }
}
