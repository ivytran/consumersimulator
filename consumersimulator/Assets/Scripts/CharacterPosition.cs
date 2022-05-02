using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterPosition : MonoBehaviour
{
    private XROrigin m_XROrigin;
    private CharacterControllerDriver m_CharacterController;
    private CharacterController characterController;
    private GameObject itemUI;
    private GameObject leftHandController;

    private void Start()
    {
        m_XROrigin = GetComponent<XROrigin>();
        m_CharacterController = GetComponent<CharacterControllerDriver>();
        characterController = GetComponent<CharacterController>();
        itemUI = GameObject.Find( "ItemCanvas" );
        leftHandController = GameObject.Find( "LeftHand Controller" );
        itemUI.transform.position = new Vector3( leftHandController.transform.position.x, leftHandController.transform.position.y, leftHandController.transform.position.z + 1f);
    }
    private void Update()
    {
        UpdateCharacterController();
        itemUI.transform.position = new Vector3( leftHandController.transform.position.x , leftHandController.transform.position.y , leftHandController.transform.position.z + 1f );

    }
    protected virtual void UpdateCharacterController()
    {
        if (m_XROrigin == null || m_CharacterController == null)
            return;

        var height = Mathf.Clamp( m_XROrigin.CameraInOriginSpaceHeight , m_CharacterController.minHeight , m_CharacterController.maxHeight );

        Vector3 center = m_XROrigin.CameraInOriginSpacePos;
        center.y = height / 2f + characterController.skinWidth;

        characterController.height = height;
        characterController.center = center;
    }
}
