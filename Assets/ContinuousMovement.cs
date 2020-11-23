using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public XRNode inputSource;
    public float speed = 1f;
    public float additionalHeight = 0.2f;

    private XRRig rig;
    private Vector2 inputAxis;
    private Rigidbody character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        //CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        //character.Move(direction * Time.fixedDeltaTime * speed);
        gameObject.GetComponent<Rigidbody>().position +=  direction * 1 * Time.deltaTime;
    }

    void CapsuleFollowHeadset()
    {
        gameObject.GetComponent<CapsuleCollider>().height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(capsuleCenter.x, gameObject.GetComponent<CapsuleCollider>().height/2, capsuleCenter.z);
    }
}
