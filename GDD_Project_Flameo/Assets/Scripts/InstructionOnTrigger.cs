using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionOnTrigger : MonoBehaviour
{
    
    [SerializeField]
    [Tooltip("the instruction to show")]
    private GameObject Image;

    private void Start()
    {
        Image.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ran");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ran2");
            Image.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        Destroy(Image);

    }
}
