using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float jumpAngleInDegrees;
    public int jumpSpeed;

    private CardboardHead head;
    private Rigidbody rb;
    private bool onFloor;
    private float lastJumpRequestTime = 0f;

    // Use this for initialization
    void Start()
    {
        Cardboard.SDK.OnTrigger += PullTrigger;
        head = FindObjectOfType<CardboardHead>();
        rb = GetComponent<Rigidbody>();
    }

    private void PullTrigger()
    {
       RequestJump();
    }

    private void RequestJump()
    {
        lastJumpRequestTime = Time.time;
        rb.WakeUp();
    }

    private void Jump()
    {
        var jumpAngleInRadians = Mathf.Deg2Rad * jumpAngleInDegrees;
        var projectedVector = Vector3.ProjectOnPlane(head.Gaze.direction, Vector3.up);

        var jumpVector = Vector3.RotateTowards(projectedVector, Vector3.up, jumpAngleInRadians, 0);
        rb.velocity = jumpVector * jumpSpeed;
    }

    void OnCollisionStay(Collision collision)
    {
        var delta = Time.time - lastJumpRequestTime;
        if (delta < 0.1)
        {
            Jump();
            lastJumpRequestTime = 0f;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
