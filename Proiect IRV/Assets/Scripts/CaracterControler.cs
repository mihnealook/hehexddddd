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
    // Start is called before the first frame update
    void Start()
    {
        lastJumped = Time.time;
    }

    public void SetActive()
    {
        isActive = true;
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
                Debug.Log("caeasd");
                canJump = false;
                lastJumped = Time.time;
                this.GetComponent<Rigidbody>().velocity += Vector3.up * force;
            }

            if (Time.time - lastJumped > timeBetweenJumps)
            {
                canJump = true;
            }
        }
    }
}
