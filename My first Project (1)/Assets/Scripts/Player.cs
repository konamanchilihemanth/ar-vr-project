using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]  private Transform groungCheckTransform = null ;
    [SerializeField] LayerMask playerMask;

    bool jumpKeywasPressed = false;
    float horizontalInput;
    Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;
    //bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        //check if space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeywasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //This method is called once every physics update
    private void FixedUpdate()
    {

        /*if (!isGrounded)
        {
            return; 
        }

        if (Physics.OverlapSphere(groungCheckTransform.position, 0.1f).Length == 1)
        {
            return;
        }*/

        rigidbodyComponent.velocity = new Vector3
            (rigidbodyComponent.velocity.x, rigidbodyComponent.velocity.y,
            horizontalInput);

        if (Physics.OverlapSphere(groungCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeywasPressed)
        {
            float jumpPower = 5f;
            if(superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeywasPressed = false;
        }

    }

    /* private void OnCollisionEnter(Collision collision)
     {
         isGrounded = true;
     }

     private void OnCollisionExit(Collision collision)
     {
         isGrounded = false;
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

}
