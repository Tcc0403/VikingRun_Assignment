using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class VikingController : MonoBehaviour
{
    public bool isInAir = false;
    public Vector3 MovingDirection = Vector3.forward;
    public float JumpingForce = 50000f;
    bool isMoving = false;
    Rigidbody rb;
    Animator animator;
    [SerializeField] float movingSpeed = 10f;
    [SerializeField] float rotatingSpeed = 180f;



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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Bridge");
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update");
        MovingDirection = transform.forward;
        isMoving = false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movingSpeed = 20f;
            rotatingSpeed = 360f;
        }
        else
        {
            movingSpeed = 10f;
            rotatingSpeed = 180f;
        }
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


        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isInAir", isInAir);
        updateDirection();


        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayCastHit;

            if (Physics.Raycast(r, out rayCastHit))
            {
                GameObject x = rayCastHit.collider.gameObject;
                Debug.Log(rayCastHit.collider.name);
                if (!x.transform.IsChildOf(GameObject.Find("landscapes").transform) && !x.transform.IsChildOf(transform))
                    Destroy(x);
            }
        }

    }
}
