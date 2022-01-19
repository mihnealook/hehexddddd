using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject cow;

    private bool isActive = false;
    private Vector3 offset = Vector3.right * 25 + Vector3.up * 15;
    private Vector3 cowOffset = Vector3.up * 5;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(isActive) {
            this.transform.position = cow.transform.position + offset;
            transform.LookAt(cow.transform.position + cowOffset);
        }
    }
}
