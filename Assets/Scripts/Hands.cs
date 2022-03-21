using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] GameObject handsController;
    [SerializeField] float positionSpeed = 30f;
    [SerializeField] float rotationSpeed = 100f;
    public Vector3 posOffset;
    public Vector3 rotOffset;
    private Transform _targetTransform;
    private Rigidbody _body;

    private void Start()
    {
        MoveObject();
    }
    private void Update()
    {
       MovePhysics();
    }
    public void MoveObject()
    {
        _targetTransform = handsController.transform;
        _body = GetComponent<Rigidbody>();
        _body.position = handsController.transform.position;
        _body.rotation = handsController.transform.rotation;
    }
    public void MovePhysics()
    {
        var startPosOffset = _targetTransform.position + posOffset;
        var diffPos = Vector2.Distance( startPosOffset , transform.position );
       _body.velocity = ( startPosOffset - transform.position ).normalized * (positionSpeed * diffPos);

        var startRotOffset = _targetTransform.rotation * Quaternion.Euler( rotOffset );
        var quoaternion = startRotOffset * Quaternion.Inverse( _body.rotation );       
        quoaternion.ToAngleAxis( out float angle , out Vector3 axis );
       _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotationSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log( "collision detected " + collision.gameObject.name );
    }
}
