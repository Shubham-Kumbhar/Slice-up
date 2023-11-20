using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("obstracle"))
        {
            Destroy(other.gameObject);
        }else{
             Destroy(other.gameObject);
        }
    }
}
