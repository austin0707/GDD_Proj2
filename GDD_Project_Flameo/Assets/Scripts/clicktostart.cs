using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clicktostart : MonoBehaviour
{
    [SerializeField]
    [Tooltip("the scene to load")]
    string scenename;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene(scenename);
        }
    }
}
