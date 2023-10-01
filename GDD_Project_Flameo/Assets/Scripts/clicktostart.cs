using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clicktostart : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("Level1");
        }
    }
}
