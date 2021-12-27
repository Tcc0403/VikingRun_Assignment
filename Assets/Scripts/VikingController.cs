using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class VikingController : MonoBehaviour
{
    public GameObject CanvasGO;
    public int MovementMode = 0;
    public bool isInAir = false;
    public Vector3 MovingDirection = Vector3.forward;
    public float JumpingForce = 50000f;
    bool isMoving = false;
    bool isTurning = false;
    float turningAngle = 0f;
    Rigidbody rb;
    Animator animator;
    [SerializeField] float movingSpeed = 10f;
    [SerializeField] float rotatingSpeed = 360f;



    void updateDirection()
    {
        float degree = (transform.rotation.y + 360) % 360;
        MovingDirection.z = Mathf.Cos(degree * Mathf.Deg2Rad);
        MovingDirection.x = Mathf.Sin(degree * Mathf.Deg2Rad);
    }

    void Awake()
    {


    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            CanvasGO.GetComponent<ScorePanel>().CoinIncreament();
            Destroy(collision.gameObject);
            return;
        }
        //Debug.Log(collision.gameObject.name);
        isInAir = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        isInAir = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        isInAir = true;
    }

    
    // Update is called once per frame
    void Update()
    {
        switch(MovementMode)
        {
            case 0:
                MovingDirection = transform.forward;               
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isMoving = true;

                }
                if (Input.GetKey(KeyCode.S))
                {
                   
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    turningAngle += 90f;
                    isTurning = true;                                                          
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    turningAngle -= 90f;
                    isTurning = true;                      
                }
                if (Input.GetKeyDown(KeyCode.Space) && !isInAir)
                {
                    rb.AddForce(Vector3.up * JumpingForce );
                    isInAir = true;
                }
                if(isTurning)
                {
                    if(turningAngle>5)
                    {
                        transform.Rotate(0, -rotatingSpeed * Time.deltaTime, 0);
                        turningAngle -= rotatingSpeed * Time.deltaTime;
                    }
                    else if(turningAngle<-5)
                    {
                        transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);
                        turningAngle += rotatingSpeed * Time.deltaTime;
                    }
                    else
                    {
                        transform.Rotate(0, -turningAngle, 0);
                        turningAngle = 0;
                        isTurning = false;
                    }
                }
                if(isMoving) transform.position += movingSpeed * Time.deltaTime * MovingDirection;
                
                break;
            case 1:
                MovingDirection = transform.forward;
                isMoving = false;                
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += movingSpeed * Time.deltaTime * MovingDirection;
                    isMoving = true;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position -= movingSpeed * Time.deltaTime * MovingDirection;
                    isMoving = true;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(0, -rotatingSpeed * Time.deltaTime, 0);
                    isMoving = true;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);

                    isMoving = true;
                }
                if (Input.GetKeyDown(KeyCode.Space) && !isInAir)
                {
                    rb.AddForce(Vector3.up * JumpingForce );
                    isInAir = true;
                }
                break;
            
        }
        //Debug.Log("update");
        


        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isInAir", isInAir);
        updateDirection();

    }
}
