using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public Text gazeText;

    public float jumpAngleInDegrees;
    public int jumpSpeed;

    private CardboardHead head;
    private Rigidbody rb;
    private bool onFloor;


    private bool showText = true;

    // Use this for initialization
    void Start()
    {
        Cardboard.SDK.OnTrigger += PullTrigger;
        head = FindObjectOfType<CardboardHead>();
        rb = GetComponent<Rigidbody>();


    }

    private void PullTrigger()
    {
        if (onFloor)
        {
            var jumpAngleInRadians = Mathf.Deg2Rad * jumpAngleInDegrees;
            var projectedVector = Vector3.ProjectOnPlane(head.Gaze.direction, Vector3.up);

            var jumpVector = Vector3.RotateTowards(projectedVector, Vector3.up, jumpAngleInRadians, 0);
            rb.velocity = jumpVector * jumpSpeed;
        }

        //rb.AddForce(head.Gaze.direction * 1000);
        //showText = !showText;
    }

    void OnCollisionEnter(Collision collision)
    {
        onFloor = true;
    }

    void OnCollisionExit(Collision collision)
    {
        onFloor = false;
    }

    // Update is called once per frame
    void Update()
    {
        gazeText.enabled = showText;
        gazeText.text = head.Gaze.ToString();
    }
}
