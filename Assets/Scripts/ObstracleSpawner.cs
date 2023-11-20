using System.Collections;
using UnityEngine;

public class ObstracleSpawner : MonoBehaviour
{
    float timeToSpawn;
    [SerializeField] float leastSpawnPoint,maxSpawnPoint;
    [SerializeField] GameObject[] obstracleList;
    void Start()
    {
        timeToSpawn = GameManager.gameManager.dataManager.blockSpawnSpeed;
        StartCoroutine(spanwObjects());
    }

    IEnumerator spanwObjects()
    {
        yield return new WaitForSeconds(timeToSpawn);
        obstracleSpawner();
        StartCoroutine(spanwObjects());

    }

    private void OnDestroy(){
        StopAllCoroutines();
    }

    void obstracleSpawner()
    {
        int i = Random.Range(0,obstracleList.Length);
        Vector3 position = new Vector3(Random.Range(leastSpawnPoint, maxSpawnPoint),transform.position.y, transform.position.z);
        Instantiate(obstracleList[i],position, Quaternion.identity);
    }
}
