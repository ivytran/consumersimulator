using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hands : MonoBehaviour
{
    [SerializeField] ActionBasedController handsController;
    [SerializeField] float positionSpeed = 30f;
    [SerializeField] float rotationSpeed = 100f;
    public Vector3 posOffset;
    public Vector3 rotOffset;
    private Transform _targetTransform;
    private Rigidbody _body;

    [SerializeField] private Transform handPalm;
    [SerializeField] private float reachDistance = 0.2f, jointDistance = 0.05f;
    [SerializeField] LayerMask grabLayer;

    private bool _handGrabed;
    private GameObject _thisHeldObj;
    private Transform _thisGrabPoint;
    private FixedJoint _jointOne, _jointTwo;
    private GameObject tempObject;
    private void Start()
    {
        tempObject = new GameObject();
        MoveObject();
    }
    private void Update()
    {
       MovePhysics();
    }
    public void MoveObject()
    {
        _targetTransform = handsController.gameObject.transform;
        _body = GetComponent<Rigidbody>();
        handsController.selectAction.action.started += GrabItem;
        handsController.selectAction.action.canceled += LeaveItem;
        _body.position = handsController.transform.position;
        _body.rotation = handsController.transform.rotation;
        _body.maxAngularVelocity = 20f;
    }

    private void LeaveItem(InputAction.CallbackContext obj)
    {
        Debug.Log( "released" );
        if (_jointOne != null)
        {
            Debug.Log( "joinOne" );
            Destroy( _jointOne);
        }
        if (_jointTwo != null)
        {
            Debug.Log( "jointwo" );
            Destroy( _jointTwo );
        }
        if (_thisGrabPoint != null)
        {
            Debug.Log( "grabPoint" );
            _thisGrabPoint.gameObject.SetActive(false);
        }
        if (_thisHeldObj != null)
        {
            Debug.Log( "thisHeldObject" );
            var _thf = _thisHeldObj.GetComponent<Rigidbody>();
            _thf.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _thf.interpolation = RigidbodyInterpolation.None;
            _thisHeldObj = null;
        }

        _handGrabed = false;
        _targetTransform = handsController.gameObject.transform;

    }

    private IEnumerator GrabObject(Collider collider, Rigidbody thisRgd)
    {
        _handGrabed = true;
        _thisGrabPoint = tempObject.transform;
        _thisGrabPoint.position = collider.ClosestPoint( handPalm.position );
        _thisGrabPoint.parent = _thisHeldObj.transform;
        _targetTransform = _thisGrabPoint;
        while (!_handGrabed && Vector3.Distance(_thisGrabPoint.position, handPalm.position) > jointDistance && _handGrabed)
        {
            yield return new WaitForEndOfFrame();
        }
        _body.velocity = Vector2.zero;
        _body.angularVelocity = Vector2.zero;
        thisRgd.velocity = Vector2.zero;
        thisRgd.angularVelocity = Vector2.zero;

        thisRgd.collisionDetectionMode = CollisionDetectionMode.Continuous;
        thisRgd.interpolation = RigidbodyInterpolation.Interpolate;
        _jointOne = gameObject.AddComponent<FixedJoint>();
        _jointOne.connectedBody = thisRgd;
        _jointOne.breakForce = float.PositiveInfinity;
        _jointOne.breakTorque = float.PositiveInfinity;
        _jointOne.connectedMassScale = 1f;
        _jointOne.massScale = 1f;
        _jointOne.enableCollision = false;
        _jointOne.enablePreprocessing = false;

        _jointTwo = _thisHeldObj.AddComponent<FixedJoint>();
        _jointTwo.connectedBody = _body;
        _jointTwo.breakForce = float.PositiveInfinity;
        _jointTwo.breakTorque = float.PositiveInfinity;
        _jointTwo.connectedMassScale = 1f;
        _jointTwo.connectedMassScale = 1f;
        _jointTwo.massScale = 1f;
        _jointTwo.enableCollision = false;
        _jointTwo.enablePreprocessing = false;

        _targetTransform = handsController.gameObject.transform;
    }

    private void GrabItem(InputAction.CallbackContext obj)
    {
        if (_handGrabed || _thisHeldObj)
            return;
        Collider[] allHandColliders = Physics.OverlapSphere( handPalm.position , reachDistance , grabLayer );
        if (allHandColliders.Length < 1)
            return;
        var objectTogRab = allHandColliders[0].transform.gameObject;
        var objectGrabRgd = objectTogRab.GetComponent<Rigidbody>();
        if (objectGrabRgd != null)
            _thisHeldObj = objectGrabRgd.gameObject;
        else
            objectGrabRgd = objectTogRab.GetComponentInParent<Rigidbody>();
            if (objectGrabRgd != null)
                _thisHeldObj = objectGrabRgd.gameObject;
            else
                return;

     StartCoroutine( GrabObject( allHandColliders[0] , objectGrabRgd ) );
    }

    public void MovePhysics()
    {
        var startPosOffset = _targetTransform.TransformPoint(posOffset);
        var diffPos = Vector2.Distance( startPosOffset , transform.position );
       _body.velocity = ( startPosOffset - transform.position ).normalized * (positionSpeed * diffPos);

        var startRotOffset = _targetTransform.rotation * Quaternion.Euler( rotOffset );
        var quoaternion = startRotOffset * Quaternion.Inverse( _body.rotation );       
        quoaternion.ToAngleAxis( out float angle , out Vector3 axis );
       _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotationSpeed);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log( "collision detected " + collision.gameObject.name );
    //}
}
