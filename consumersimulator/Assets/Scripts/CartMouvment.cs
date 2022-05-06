using UnityEngine;
using UnityEngine.InputSystem;
public class CartMouvment : MonoBehaviour
{
  
    public Transform hand;
    private float speed = 15f;
    private bool initMove = false;
    private Vector2 handRotX;
    private float handRotZ;
    private bool isBack = false;
    private bool isFront = false;
    private bool isRight = false;
    private bool isLeft = false;
    private float leftTurn;
    private float newYValue = 0;
    private int countMove = 0;
    public InputActionReference inputActionMove;
    public InputAction inputMove;

    private void FixedUpdate()
    {
        //inputActionMove.action.performed += ctx =>
        //{
        //    handRotX = ctx.ReadValue<Vector2>();
        //   // CartMove();
        //};
        //Debug.Log( "performed" + handRotX);

        inputMove.performed += ctx =>
        {
            leftTurn = ctx.ReadValue<float>();
            countMove++;
        };
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
    private void OnEnable()
    {
        inputMove.Enable();
    }
    private void OnDisable()
    {
        inputMove.Disable();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag( "LeftHandController" ))
        {
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
