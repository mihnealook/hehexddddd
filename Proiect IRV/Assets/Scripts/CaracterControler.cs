using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterControler : MonoBehaviour
{
    public float force;

    private bool isActive = false;
    private bool canJump = true;
    private float timeBetweenJumps = 1;
    private float lastJumped;

    private MoveLines moveLines;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        lastJumped = Time.time;
        moveLines = GetComponent<MoveLines>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetActive()
    {
        isActive = true;
        moveLines.SetActive();
    }

    public void SetInactive()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                canJump = false;
                lastJumped = Time.time;
                rigidbody.velocity += Vector3.up * force;
            }

            if (Time.time - lastJumped > timeBetweenJumps)
            {
                canJump = true;
            }
        }
    }
}
