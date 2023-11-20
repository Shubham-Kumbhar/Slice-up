using UnityEngine;

public class Obstracle : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update() {
         transform.Translate(-Vector3.forward * Time.deltaTime* speed);
    }

}
