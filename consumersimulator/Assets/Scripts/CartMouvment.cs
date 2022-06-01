using UnityEngine;
using UnityEngine.InputSystem;
public class CartMouvment : MonoBehaviour
{
  
    //public Transform hand;
    private float speed = 15f;
    private bool initMove = false;
    private Vector2 handRotX;
    private float handRotZ;
    private bool isBack = false;
    private bool isFront = false;
    private bool isRight = false;
    private bool isLeft = false;
    private float leftTurn;
    private float rightTurn;
    private float newYValue = 0;
    private int countMove = 0;
    public InputActionReference leftInputActionMove;
    public InputAction leftInputMove;
    public InputActionReference rightInputActionMove;
    public InputAction rightInputMove;
    public StringData leftControllerValue;
    public StringData rightControllerValue;
    private bool isPressedCart = false;
    private void FixedUpdate()
    {
        if (isPressedCart)
        {
            if (leftInputActionMove && leftControllerValue.RuntimeValue == "On" && rightControllerValue.RuntimeValue == "Off")
            {
                leftInputMove.performed += ctx =>
                {
                    leftTurn = ctx.ReadValue<float>();
                    countMove++;
                };
            }
            else if (rightInputActionMove && rightControllerValue.RuntimeValue == "On" && leftControllerValue.RuntimeValue == "Off")
            {
                Debug.Log( "RightHandsCalled..." );
                rightInputMove.performed += ctx =>
                {
                    rightTurn = ctx.ReadValue<float>();
                    Debug.Log( "RightHandsCalledValue..." + rightTurn );
                    countMove++;
                };
            }
            else if (( rightInputActionMove && leftInputActionMove && rightControllerValue.RuntimeValue == "On" && leftControllerValue.RuntimeValue == "On" ) || rightControllerValue || leftControllerValue)
            {
                Debug.Log( "bothHandsCalled..." );
                rightInputMove.performed += ctx =>
                {
                    rightTurn = ctx.ReadValue<float>();
                    countMove++;
                };
                leftInputMove.performed += ctx =>
                {
                    leftTurn = ctx.ReadValue<float>();
                    Debug.Log( "lStart" + leftTurn );
                    countMove++;
                };
            }
        }
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
    private void OnEnable()
    {
        if (leftInputActionMove && leftControllerValue.RuntimeValue == "On" && rightControllerValue.RuntimeValue == "Off")
        {
            leftInputMove.Enable();
        }
        if (rightInputActionMove && leftControllerValue.RuntimeValue == "Off" && rightControllerValue.RuntimeValue == "On")
        {
            rightInputMove.Enable();
        }
        if ((rightInputActionMove && leftInputActionMove && leftControllerValue.RuntimeValue == "On" && rightControllerValue.RuntimeValue == "On" ) || rightControllerValue || leftControllerValue)
        {
            rightInputMove.Enable();
            leftInputMove.Enable();
        }
    }
    private void OnDisable()
    {
        if (leftInputActionMove && leftControllerValue.RuntimeValue == "On" && rightControllerValue.RuntimeValue == "Off")
        {
            leftInputMove.Disable();
        }
        if (rightInputActionMove && leftControllerValue.RuntimeValue == "Off" && rightControllerValue.RuntimeValue == "On")
        {
            rightInputMove.Disable();
        }
        if ((rightInputActionMove && leftInputActionMove && leftControllerValue.RuntimeValue == "On" && rightControllerValue.RuntimeValue == "On") || rightControllerValue || leftControllerValue)
        {
            rightInputMove.Disable();
            leftInputMove.Disable();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag( "LeftHandController" ) || other.CompareTag( "RightHandController" ))
        {
            isPressedCart = true;
            //    if (hand)
            //    {
            //        if (!initMove)
            //        {
            //            Carts.cart = gameObject.name;
            //            gameObject.transform.position = new Vector3( hand.transform.position.x , 0 , hand.transform.position.z + 1f );

            //            if (gameObject.transform.rotation.y != 90)
            //            {
            //                gameObject.transform.rotation = Quaternion.Euler( 0 , 90 , 0 );
            //            }
            //            initMove = true;
            //        }
            //        else
            //        {
            //            gameObject.transform.rotation = Quaternion.Euler( 0 , newYValue , 0 );
            //            if (newYValue != 90 && newYValue != -90 && newYValue != 180)
            //            {
            //                gameObject.transform.position = new Vector3( hand.transform.position.x - 1f , 0 , hand.transform.position.z );
            //                gameObject.transform.rotation = Quaternion.Euler( 0 , newYValue , 0 );
            //                if (countMove > 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , -90 , 0 );
            //                    newYValue = -90;
            //                    countMove = 0;
            //                }
            //                else if (countMove == 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , 90 , 0 );
            //                    newYValue = 90;
            //                    countMove = 0;
            //                }
            //            }
            //            else if (newYValue == 90)
            //            {
            //                gameObject.transform.position = new Vector3( hand.transform.position.x , 0 , hand.transform.position.z + 1f );
            //                //gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( hand.transform.position.x , 0 , hand.transform.position.z + 1f ) * speed );
            //                if (countMove > 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , 0 , 0 );
            //                    newYValue = 0;
            //                    countMove = 0;
            //                }
            //                else if (countMove == 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , 180 , 0 );
            //                    newYValue = 180;
            //                    countMove = 0;
            //                }
            //            }
            //            else if (newYValue == -90)
            //            {
            //                gameObject.transform.position = new Vector3( hand.transform.position.x , 0 , hand.transform.position.z - 1f );
            //                //gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( hand.transform.position.x , 0 , hand.transform.position.z - 1f ) * speed );
            //                if (countMove > 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , 180 , 0 );
            //                    newYValue = 180;
            //                    countMove = 0;
            //                }
            //                else if (countMove == 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , 0 , 0 );
            //                    newYValue = 0;
            //                    countMove = 0;
            //                }
            //            }
            //            else if (newYValue == 180)
            //            {
            //                gameObject.transform.position = new Vector3( hand.transform.position.x + 1 , 0 , hand.transform.position.z );
            //                //gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( hand.transform.position.x + 1 , 0 , hand.transform.position.z ) * speed );
            //                if (countMove > 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , 90 , 0 );
            //                    newYValue = 90;
            //                    countMove = 0;
            //                }
            //                else if (countMove == 1)
            //                {
            //                    gameObject.transform.rotation = Quaternion.Euler( 0 , -90 , 0 );
            //                    newYValue = -90;
            //                    countMove = 0;
            //                }
            //            }
            //        }
            //    }
            //}


            //// Add Force
            // change x and z by rotation
            // + x backward

            //if (hand)
            //{
            //    if (!initMove)
            //    {
            //        Carts.cart = gameObject.name;
            //        gameObject.transform.position = new Vector3( hand.transform.position.x , 0 , hand.transform.position.z + 1f );
            //        gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( 12 , 0 , 0 ) , ForceMode.VelocityChange );

            //        //if (gameObject.transform.rotation.y != 90)
            //        //{
            //        //    gameObject.transform.rotation = Quaternion.Euler( 0 , 90 , 0 );
            //        //}
            //        initMove = true;
            //    }
            //    else
            //    {
            gameObject.transform.rotation = Quaternion.Euler( 0 , newYValue , 0 );
            Carts.cart = gameObject.name;
            if (newYValue != 90 && newYValue != -90 && newYValue != 180)
            {
                //gameObject.transform.position = new Vector3( hand.transform.position.x - 1f , 0 , hand.transform.position.z );
                gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( -12 , 0 , 0 ) , ForceMode.VelocityChange );
                
                gameObject.transform.rotation = Quaternion.Euler( 0 , newYValue , 0 );
                if (countMove > 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , -90 , 0 );
                    newYValue = -90;
                    countMove = 0;
                }
                else if (countMove == 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , 90 , 0 );
                    newYValue = 90;
                    countMove = 0;
                }
            }
            else if (newYValue == 90)
            {
                //gameObject.transform.position = new Vector3( hand.transform.position.x , 0 , hand.transform.position.z + 1f );
                gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( 0 , 0 , 12 ) , ForceMode.VelocityChange );
                if (countMove > 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , 0 , 0 );
                    newYValue = 0;
                    countMove = 0;
                }
                else if (countMove == 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , 180 , 0 );
                    newYValue = 180;
                    countMove = 0;
                }
            }
            else if (newYValue == -90)
            {
                //gameObject.transform.position = new Vector3( hand.transform.position.x , 0 , hand.transform.position.z - 1f );
                gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( 0 , 0 , -12 ) , ForceMode.VelocityChange );
                if (countMove > 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , 180 , 0 );
                    newYValue = 180;
                    countMove = 0;
                }
                else if (countMove == 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , 0 , 0 );
                    newYValue = 0;
                    countMove = 0;
                }
            }
            else if (newYValue == 180)
            {
                //gameObject.transform.position = new Vector3( hand.transform.position.x + 1 , 0 , hand.transform.position.z );
                gameObject.GetComponent<Rigidbody>().AddForce( new Vector3( 12 , 0 , 0 ) , ForceMode.VelocityChange );
                if (countMove > 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , 90 , 0 );
                    newYValue = 90;
                    countMove = 0;
                }
                else if (countMove == 1)
                {
                    gameObject.transform.rotation = Quaternion.Euler( 0 , -90 , 0 );
                    newYValue = -90;
                    countMove = 0;
                }
            }
        }
    }
  
    private bool CartMove(){
        //var value when value = 
      if (handRotX.x < 0)
        {
            return isLeft = true;
        }else if(handRotX.x > 0 && handRotX.y > 0)
        {
            return isFront = true;
        }else if (handRotX.x < 0 && handRotX.y < 0)
        {
            return isBack = true;
        }
       else if(handRotX.y < 0)
        {
            return isRight = true;
        }
        return false;
    }

}
