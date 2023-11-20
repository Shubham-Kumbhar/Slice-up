
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerController : MonoBehaviour
{
    [SerializeField] private InputActionReference leftGrip,rightGrip;
    [SerializeField] private XRBaseController leftController ,rightController;
    [SerializeField] private SliceObjects leftSaber ,rightSaber;
    [SerializeField] private GameObject fireSlashAbility;
    [SerializeField] private Transform fireSlashAbilityAnchor;
    [SerializeField] ParticleSystem leftAbilityPartlce, rightAbilityPartlce;
    [SerializeField] Haptic haptic;
    bool fireSlashCanRotat;
    private void Awake() 
    {
        leftAbilityPartlce.Stop();
        rightAbilityPartlce.Stop();
    }
    private void  OnEnable()     
    {
        leftGrip.action.performed += leftGripPressed;
        leftGrip.action.canceled+=leftGripUnPressed;
        rightGrip.action.performed+= rightGripPressed;
        rightGrip.action.canceled+=rightGripUnPressed;
    }
    private void  OnDisable()   
    {
        leftGrip.action.performed -= leftGripPressed;
        leftGrip.action.canceled-=leftGripUnPressed;
        rightGrip.action.performed-= rightGripPressed;
        rightGrip.action.canceled-=rightGripUnPressed;
    }

    private void Update() {
        //
        if(leftGrip.action.inProgress) leftSaber.abilityActivated= true;
        else leftSaber.abilityActivated= false;

        if(rightGrip.action.inProgress) rightSaber.abilityActivated= true;
        else rightSaber.abilityActivated= false;

        if(GO!= null && fireSlashCanRotat)
        {
            RotatePlane();
        }
        //left Saber partilce activate 
        if(leftSaber.power>80)
        {
            if(!leftAbilityPartlce.isPlaying && !leftGrip.action.inProgress)
            {
                leftAbilityPartlce.Play();
            }
        }
        //power check 
        if(rightSaber.power<0)
        {
            rightPowerDeactivated();
            rightSaber.power= 0;

        }else if (rightSaber.power>100)
        {
            rightSaber.power= 100;
        }
        if(leftSaber.power<0)
        {
            leftPowerDeactivated();
            leftSaber.power= 0;

        }else if(leftSaber.power>100){
            leftSaber.power= 100;
        }
    }
//--------------------- ICE POWERS -------------------------
    void leftGripPressed(InputAction.CallbackContext obj)
    {
        haptic.triggerHapptics(leftController);
        if(leftSaber.power>0)
        {
            leftAbilityPartlce.Stop();
            leftPowerActivated();
        }    
    }
    void leftGripUnPressed(InputAction.CallbackContext obj)
    {
        haptic.triggerHapptics(leftController);
        leftPowerDeactivated();
    }
    
     public void leftPowerActivated()
    {
        
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale* 0.005f;
        //--------------------------------------------------------------------
        leftSaber.startPoweUp();
        
    }

    public void leftPowerDeactivated()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        //---------------------------------------------------------------------      
        leftSaber.enddPowerUp();
       

    }
//-------------------------- FIRE POWERS -------------------------------
    void rightGripPressed(InputAction.CallbackContext obj)
    {
        haptic.triggerHapptics(rightController);

        if(rightSaber.power == 100)
        {
            rightPowerActivated();
        }
    }
    void rightGripUnPressed(InputAction.CallbackContext obj)
    {
        haptic.triggerHapptics(rightController);
        rightPowerDeactivated();
    }
    public GameObject GO;
    void rightPowerActivated()
    {
        
        rightAbilityPartlce.Play();
        //spawn Slash
        GO = Instantiate(fireSlashAbility,fireSlashAbilityAnchor);
        fireSlashCanRotat=true;
        rightSaber.startPoweUp();
    }
    void rightPowerDeactivated()
    {
        rightAbilityPartlce.Stop();
        //spawn dash move 
        if(GO!= null)
        {
            GO.transform.SetParent(null);
            fireSlashCanRotat=false;
            //GO.GetComponent<RotateSlash>().enabled = false;
            GO.GetComponent<SlashMove>().enabled = true;
            GO.GetComponent<SlashMove>().canSlice = true;
        }

        //saber power to zero
            rightSaber.power = 0;
            rightSaber.powerBar.value = 0;
            rightSaber.enddPowerUp();
        

    }

    public void RotatePlane()
    {
        GO.transform.eulerAngles += new Vector3(0, 0, -rightController.transform.position.x* 8);
    }
   

}