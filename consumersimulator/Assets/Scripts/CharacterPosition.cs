using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterPosition : MonoBehaviour
{
    private XROrigin m_XROrigin;
    private CharacterControllerDriver m_CharacterController;
    private CharacterController characterController;
    private ItemUI itemUI;
    public StringData lftHand;
    public StringData rgtHand;
    public FloatData volume;
    public IntData performanceSettings;
    public IntData nDifficulty;
    //public IntData hDifficulty;
    public GameObject rightHandController;
    public GameObject leftHandController;
    public GameObject actualLeftHand;
    public GameObject actualRightHand;

    private void Start()
    {
        m_XROrigin = GetComponent<XROrigin>();
        m_CharacterController = GetComponent<CharacterControllerDriver>();
        characterController = GetComponent<CharacterController>();
        itemUI = FindObjectOfType<ItemUI>();
        if (itemUI)
        {
            itemUI.DiactivateCanvas();
        }
        if (lftHand || rgtHand)
        {
            ChangeHandControllers();
        }
        if (performanceSettings)
        {
            ChangePerformanceSetting();
        }
        if (volume)
        {
            GetComponent<AudioSource>().volume = volume.RuntimeValue;
        }
        if (nDifficulty)
        {
            if (nDifficulty.RuntimeValue == 1)
            {
                GameObject.FindGameObjectWithTag( "Cart" ).transform.position = new Vector3( 12 , 0 , -12 );
                GameObject.FindGameObjectWithTag( "Cart" ).SetActive( false );
            }
        }
    }
    private void Update()
    {
        UpdateCharacterController();
        if (leftHandController || rightHandController || actualLeftHand || actualLeftHand)
        {
            if (leftHandController.activeSelf && !rightHandController.activeSelf)
            {
                SwitchHandControllers( actualLeftHand );
            }
            else if (rightHandController.activeSelf && !leftHandController.activeSelf)
            {
                SwitchHandControllers( actualRightHand );
            }
            else if (rightHandController.activeSelf && leftHandController.activeSelf)
            {
                SwitchHandControllers( actualRightHand );
                SwitchHandControllers( actualLeftHand );
            }
        }
    }
    private void ChangeHandControllers()
    {
        if (lftHand.RuntimeValue == "On" && rgtHand.RuntimeValue == "On")
        {
            Debug.Log( "leftandRightaretrue" );
            rightHandController.SetActive( true );
            leftHandController.SetActive( true );
        }
        else if (lftHand.RuntimeValue == "On" && rgtHand.RuntimeValue == "Off")
        {
            rightHandController.SetActive( false );
            leftHandController.SetActive( true );
        }
        else if (lftHand.RuntimeValue == "Off" && rgtHand.RuntimeValue == "On")
        {
            rightHandController.SetActive( true );
            leftHandController.SetActive( false );
        }
    }
    private void ChangePerformanceSetting()
    {
        Debug.Log( "PerformanceValue" + performanceSettings.RuntimeValue );
        if (performanceSettings.RuntimeValue == 0)
        {
            QualitySettings.SetQualityLevel( 0 , true );
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
        }
        else if (performanceSettings.RuntimeValue == 1)
        {
            QualitySettings.SetQualityLevel( 1 , true );
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
        }
        else if (performanceSettings.RuntimeValue == 2)
        {
            QualitySettings.SetQualityLevel( 2 , true );
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
        }
    }
    private void SwitchHandControllers(GameObject handController)
    {
        if (handController)
        {
            if (handController.transform.rotation.y == 0 || handController.transform.rotation.y < 0)
            {
                if (itemUI)
                {
                    if (itemUI.itemCanvas)
                    {
                        if (itemUI.itemCanvas.activeInHierarchy)
                        {
                            itemUI.itemCanvas.gameObject.transform.position = new Vector3( handController.transform.position.x - 1f , handController.transform.position.y , handController.transform.position.z );
                            itemUI.itemCanvas.gameObject.transform.rotation = Quaternion.Euler( 0 , -90f , 0 );
                        }
                    }
                }

            }
            else
            {
                if (itemUI)
                {
                    if (itemUI.itemCanvas)
                    {
                        if (itemUI.itemCanvas.activeInHierarchy)
                        {
                            itemUI.itemCanvas.gameObject.transform.position = new Vector3( handController.transform.position.x + 1f , handController.transform.position.y , handController.transform.position.z );
                            itemUI.itemCanvas.gameObject.transform.rotation = Quaternion.Euler( 0 , 90f , 0 );
                        }
                    }
                }
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
