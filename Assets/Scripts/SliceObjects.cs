using UnityEngine;
using EzySlice;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class SliceObjects : MonoBehaviour
{
    [SerializeField] Transform startSlicePoint, endSlicePoint;
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] VelocityEstimator velocityEstimator;
    [SerializeField] Material crosSectionMarerial;
    [SerializeField] float cutForce;
   // [SerializeField] GameObject blastParticle;
    [SerializeField] Haptic haptic;
    [HideInInspector] public bool abilityActivated;
    [SerializeField] AudioClip clip;

    XRBaseController controller;
    private void Start() {
        controller=GetComponentInParent<XRBaseController>();
    }
    private void FixedUpdate() 
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position,out RaycastHit hit ,sliceableLayer);
        if(hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }
    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetAngularVelocityEstimate();
        SlicedHull hull = target.Slice(endSlicePoint.position, velocity);
        if (hull!= null)
        {
            haptic.triggerHapptics(controller);
            SoundManager.instance.PlaySFX(clip);
            GameObject upperHull = hull.CreateUpperHull(target,crosSectionMarerial);
            setupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target,crosSectionMarerial);
            setupSlicedComponent(lowerHull);
            Destroy(target);
            if(!abilityActivated)
            power+=10;
            perPower= power / 100;
            powerBar.value = perPower;

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
        // GameObject go = Instantiate(blastParticle);
        // go.transform.SetParent(slicedObject.transform);
        slicedObject.tag = "obstracle";
    }

    //------------------------------------------- slider --------------------------------

    public float power = 0f;
    bool isPowerUp = false;
    bool isDirectionUp = false;
    [SerializeField] private float powerSpeed= 100f;
    float perPower;
    public Slider powerBar;
    private void LateUpdate()
    {
        
        if(isPowerUp)
        {
            powerActivate();
        }
        
    }
    public void startPoweUp()
    {
        isPowerUp = true;
        isDirectionUp = true;
        //power = 0f;
    }
    public void powerActivate()
    {
        if (isDirectionUp)
        {
            power -= Time.deltaTime * powerSpeed;
            if (power > 100f)
            {
                Debug.Log(" gerter than 100 ");
                isDirectionUp = false;
                power = 100;
            }
        }
        perPower= power / 100;
        powerBar.value = perPower;

    }

    
    public void enddPowerUp()
    {
        isPowerUp = false;
    }
}
