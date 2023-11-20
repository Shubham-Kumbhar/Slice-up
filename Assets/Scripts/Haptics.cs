using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

[System.Serializable]
class Haptic{

    [Range(0,1)]
    [SerializeField] float intencity;
    [SerializeField] float duration;
    public void triggerHapptics(XRBaseController controller)
    {
        controller.SendHapticImpulse(intencity,duration);
    }
}