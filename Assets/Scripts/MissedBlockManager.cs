using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedBlockManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameManager.gameManager.dataManager.missedBlock +=1;
        Destroy(other.gameObject);
        GameManager.gameManager.commboEnd();
    }
}
