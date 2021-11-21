using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public GameObject reticle;
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;

    public XRRayInteractor leftInteractionRay;
    public XRRayInteractor rightInteractionRay;

    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.05f;

    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    void Update()
    {
        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;

        if(leftTeleportRay)
        {
            bool isLeftInteractorRayHovering = leftInteractionRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            bool isLeftGrabbing = leftInteractionRay.isSelectActive;
            leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering && !isLeftGrabbing);
        }

        if (rightTeleportRay)
        {
            bool isRightInteractorRayHovering = rightInteractionRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            bool isRightGrabbing = rightInteractionRay.isSelectActive;
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering && !isRightGrabbing);
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    } 
}
