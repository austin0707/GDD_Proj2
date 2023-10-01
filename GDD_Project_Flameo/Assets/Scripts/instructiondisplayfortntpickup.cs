using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructiondisplayfortntpickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Tooltip("the instruction to show")]
    private GameObject Image;

    private void Start()
    {
        Image.SetActive(false);
    }


    private void Update()
    {
        if (GetComponent<PlayerController> ().Pickedup()) {
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
