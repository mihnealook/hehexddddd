using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotControler : MonoBehaviour
{
    public GameObject[] lineRenderers;
    public Transform[] lineStart;
    public Transform middle;
    public Transform idleBack;
    public GameObject cow;
    public float maxLen;
    public float force;
    public Camera mainCamera;

    private bool isMouseDown;
    private Vector3 currPos;
    private Vector3 correction = Vector3.up * 4;
    private Rigidbody cowRigidBody;
    private CapsuleCollider cowCapsuleCollider;
    void Start()
    {
        cowRigidBody = cow.GetComponent<Rigidbody>();
        cowCapsuleCollider = cow.GetComponent<CapsuleCollider>();

        //cowCapsuleCollider.enabled = false;
        cowRigidBody.useGravity = false;

        for (int i = 0; i < 2; i++)
        {
            lineRenderers[i].GetComponent<LineRenderer>().positionCount = 2;
            lineRenderers[i].GetComponent<LineRenderer>().SetPosition(0, lineStart[i].position);
            lineRenderers[i].GetComponent<LineRenderer>().SetWidth(1.0f, 1.0f);
        }

        UpdateLines(idleBack.position);
        UpdateCow(middle.position);
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        Vector3 forceVec = (currPos - (middle.position + correction)) * force * -1;
        cowRigidBody.velocity = forceVec;
        cowRigidBody.useGravity = true;
        mainCamera.GetComponent<CameraFollower>().SetActive();
        cow.GetComponent<CaracterControler>().SetActive();
    }

    void UpdateLines(Vector3 position)
    {
        for (int i = 0; i < 2; i++)
        {
            lineRenderers[i].GetComponent<LineRenderer>().SetPosition(1, position);
        }

        currPos = position;
    }

    void UpdateCow(Vector3 position)
    {
        cow.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown) {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.WorldToViewportPoint(mousePos);
            mousePos.z = this.transform.position.z;
            mousePos = (middle.position + correction) + Vector3.ClampMagnitude(mousePos - (middle.position + correction), maxLen);
            mousePos.x = Mathf.Clamp(mousePos.x, middle.position.x, 1000);
            UpdateLines(mousePos);
            UpdateCow(mousePos - idleBack.position + middle.position);
            /*if(cowCapsuleCollider)
                cowCapsuleCollider.enabled = true;*/
        } else {
            UpdateLines(idleBack.position);
            //UpdateCow(middle.position);
        }
    }
}
