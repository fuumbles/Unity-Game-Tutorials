using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            

    Vector3 movement;                   
    Animator anim;                      
    Rigidbody playerRigidbody;          
    int floorMask;                      
    float camRayLength = 100f;          

    void Awake()
    {
        this.tag = "Player";
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // movement variables
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //calling on update of location



        Animating(h, v);
        Move(h, v);        
        Turning();
    }

    void Move(float h, float v)
    {
        
        movement.Set(h, 0f, v);//set movement        
        movement = movement.normalized * speed * Time.deltaTime;//normalize movement for speed        
        playerRigidbody.MovePosition(transform.position + movement);//move player to new position w/ rigidbody
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }

}
