
using UnityEngine;
using EzySlice;

public class SlashMove : MonoBehaviour
{
    [SerializeField] float speed,cutForce, destroyTimer;
    [SerializeField] Material crosSectionMarerial;
    [SerializeField] AudioClip clip;
    public bool canSlice = false;

    private void Start() {
        SoundManager.instance.PlaySFX(clip);
        Destroy(gameObject,destroyTimer);
    }
    private void Update() {
       transform.Translate(Vector3.forward * Time.deltaTime* speed);
       
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("obstracle")&&canSlice)
        Slice(other.gameObject);
    }

    void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(transform.position, transform.up);
        if (hull!= null)
        {
            GameObject upperHull = hull.CreateUpperHull(target,crosSectionMarerial);
            setupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target,crosSectionMarerial);
            setupSlicedComponent(lowerHull);
            Destroy(target);
            // combo manager
            GameManager.gameManager.combo();

        }
    }
    void setupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce,slicedObject.transform.position, 1f);
    }

        
}
