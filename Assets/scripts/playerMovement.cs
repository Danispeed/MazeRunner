using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class playerMovement : MonoBehaviour
{
    public static playerMovement Inst;
    public float speed = 10f; 

    private Rigidbody rb;
    private string horizontalInputAxis;
    private string verticalInputAxis;

    private int destroy_count = 2;
    private bool destroy_now = false;
    private KeyCode destroy;
    private KeyCode block;
    private bool moving = true;
    private int direction;
    private int blockCount = 3;
    private float spinSpeed = 50000000000000000f;
    private bool isBlocking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
 
        // set the correct movement controls, based on which player it is
        if (gameObject.tag == "Player1")
        {
            // Debug.LogError("player1");
            horizontalInputAxis = "Horizontal";
            verticalInputAxis = "Vertical";
            destroy = KeyCode.RightShift;
            block = KeyCode.Space;
        }
        else
        {
            // Debug.LogError("player2");
            horizontalInputAxis = "Horizontal_P2";
            verticalInputAxis = "Vertical_P2";
            destroy = KeyCode.E;
            block = KeyCode.Q;
        }

        playerManager.Ins.RegisterPlayer(this);   
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            string loser = "";
            if (gameObject.tag == "Player1")
            {
                loser = "Player 2 is the winner!";
            }
            else if (gameObject.tag == "Player2")
            {
                loser = "Player 1 is the winner!";
            }
            uiManager.Instance.ShowMenu(loser);
        }

        if (Input.GetKeyDown(destroy)){
            if (destroy_count > 0){
                destroy_now = true;

                if (gameObject.tag == "Player1"){
                    Debug.LogError("player1 can now collide");
                    uiManagerPlayer1.Instance.UpdateImage(destroy_now);
                }
                if (gameObject.tag == "Player2"){
                    uiManagerPlayer2.Instance.UpdateImage(destroy_now);
                    Debug.LogError("player2 can now collide");
                }
            }
        }
        HandleBlocking();
    }

    void HandleBlocking()
    {
        bool isMoving = rb.velocity.magnitude > 0.1f;
        
        if (Input.GetKeyDown(block) && blockCount > 0 && !isBlocking && !isMoving)
        {
            StartBlocking();
        }
        else if ((Input.GetKeyUp(block) || isMoving) && isBlocking)
        {
            StopBlocking();
        }
    }

    void StartBlocking()
    {
        isBlocking = true;
        blockCount--;
        Debug.Log("Blocking. Blocks left: " + blockCount);
        // Start spinning to indicate blocking
        rb.angularVelocity = Vector3.up * spinSpeed;
    }

    void StopBlocking()
    {
        isBlocking = false;
        rb.angularVelocity = Vector3.zero; // Stop spinning
    }

    void FixedUpdate()
    {
        if (moving)
        {
            float moveHorizontal = Input.GetAxis(horizontalInputAxis);
            float moveVertical = Input.GetAxis(verticalInputAxis);

            Vector3 movement = new Vector3(moveHorizontal * speed, 0, moveVertical * speed);
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }
        else
        {
            Vector3 movement = new Vector3(0, 0, 0);;
            if (direction == 0){
                movement = new Vector3(0, 0, -speed * 3);
            }
            if (direction == 1){
                movement = new Vector3(0, 0, speed * 3);
            }
            if (direction == 2){
                movement = new Vector3(speed * 3, 0, 0);
            }
            if (direction == 3){
                movement = new Vector3(-speed * 3, 0, 0);
            }
            
            rb.velocity = movement;
        }
    }

    // IEnumerator ApplyPushBack(Vector3 direction)
    // {
    //     Vector3 originalVelocity = rb.velocity;
    //     rb.velocity = direction * -100; // Adjust the multiplier as needed for desired push-back strength
    //     yield return new WaitForSeconds(0.2f); // Adjust time as needed
    //     rb.velocity = originalVelocity;
    // }

    void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        // {
        //     playerMovement otherPlayer = collision.gameObject.GetComponent<playerMovement>();

        //     if (otherPlayer != null && otherPlayer.isBlocking)
        //     {
        //         Vector3 directionToOther = collision.transform.position - transform.position;
        //         directionToOther.y = 0; // Keep the push-back on the horizontal plane
        //         directionToOther = directionToOther.normalized; 

        //         // Apply a controlled push-back
        //         StartCoroutine(ApplyPushBack(directionToOther));
        //     }
        // }

        if (collision.gameObject.tag == "wall")
        {
            if (destroy_now){
                Destroy(collision.gameObject);
                destroy_now = false;
                destroy_count -= 1;

                if (gameObject.tag == "Player1"){
                    // player1_destroy.enabled = destroy_now;
                    uiManagerPlayer1.Instance.UpdateImage(destroy_now);
                    Debug.LogError("player1 finished destroying");
                }
                if (gameObject.tag == "Player2"){
                    // player2_destroy.enabled = destroy_now;
                    uiManagerPlayer2.Instance.UpdateImage(destroy_now);
                    Debug.LogError("player2 finished destroying");
                }
            }
        }

        if (gameObject.tag == "Player1"){
            if (collision.gameObject.tag == "Player2"){
                StopBlocking();
            }
        }

        if (gameObject.tag == "Player2"){
            if (collision.gameObject.tag == "Player1"){
                StopBlocking();
            }
        }
    }

    public void canMove(int direct)
    {
        direction = direct;
        StartCoroutine(waitForMovement());
    }

    IEnumerator waitForMovement()
    {
        moving = false;
        yield return new WaitForSeconds(3);
        moving = true;
    }
}
