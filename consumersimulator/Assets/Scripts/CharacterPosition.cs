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
        if (leftHandController)
        {
            if (leftHandController.transform.rotation.y == 0 || leftHandController.transform.rotation.y < 0)
            {
                itemUI.transform.position = new Vector3( leftHandController.transform.position.x - 1f , leftHandController.transform.position.y , leftHandController.transform.position.z );
                itemUI.transform.rotation = Quaternion.Euler( 0 , -90f , 0 );
            }
            else
            {
                itemUI.transform.position = new Vector3( leftHandController.transform.position.x + 1f , leftHandController.transform.position.y , leftHandController.transform.position.z );
                itemUI.transform.rotation = Quaternion.Euler( 0 , 90f , 0 );
            }
        }

    }
    private void Update()
    {
        UpdateCharacterController();
        if (leftHandController)
        {
            if (leftHandController.transform.rotation.y == 0 || leftHandController.transform.rotation.y < 0)
            {
                itemUI.transform.position = new Vector3( leftHandController.transform.position.x - 1f , leftHandController.transform.position.y , leftHandController.transform.position.z );
                itemUI.transform.rotation = Quaternion.Euler( 0 , -90f , 0 );
            }
            else
            {
                itemUI.transform.position = new Vector3( leftHandController.transform.position.x + 1f , leftHandController.transform.position.y , leftHandController.transform.position.z );
                itemUI.transform.rotation = Quaternion.Euler( 0 , 90f , 0 );
            }
        }
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
