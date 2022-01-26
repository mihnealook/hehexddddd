using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLines : MonoBehaviour
{
    public int HalfNumberOfLanes = 1;
    public float TimeBetweenLaneChanges = 2.0f;
    public float OffsetBetweenLines = 10f;
    public float LineChangingSpeed = 2.0f;
    public float RotationAngle = Mathf.PI / 4;

    private float TimeSinceLastLaneChange;

    private bool isActive = false;
    private int currentLane = 0;
    private int nextLane = 0;

    private float transitionTime = 0.0f;
    private float initialZCoordinate;

    private float currentZCoordinate;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the user can change the lane from the start 
        TimeSinceLastLaneChange = TimeBetweenLaneChanges;

        rigidbody = GetComponent<Rigidbody>();
        initialZCoordinate = rigidbody.position.z;
        currentZCoordinate = initialZCoordinate;

        for (int i = -HalfNumberOfLanes; i <= HalfNumberOfLanes; ++i)
        {
            Debug.Log("Z position for index " + i + " = " + GetZCoordinateForIndex(i));
        }
    }

    public void SetActive()
    {
        isActive = true;
    }

    public void SetInactive()
    {
        isActive = false;
    }

    float GetZCoordinateForIndex(int index)
    {
        return initialZCoordinate + index * OffsetBetweenLines;
    }

    void UpdatePosition()
    {
        if (nextLane == currentLane)
            return;

        float lastZCoordinate = GetZCoordinateForIndex(currentLane);
        float futureZCoordinate = GetZCoordinateForIndex(nextLane);

        int direction = nextLane > currentLane ? -1 : 1;

        currentZCoordinate = Mathf.Lerp(lastZCoordinate, futureZCoordinate, transitionTime);
        Quaternion currentRotation = rigidbody.rotation;
        if (transitionTime <= 0.5f)
        {
            currentRotation *= Quaternion.Euler(new Vector3(0.0f, 0.0f, transitionTime * direction * RotationAngle));
        }
        else
        {
            currentRotation *= Quaternion.Euler(new Vector3(0.0f, 0.0f, (transitionTime - 0.5f) * -1 * direction * RotationAngle));
        }
        rigidbody.rotation = currentRotation;

        transitionTime += Time.deltaTime * LineChangingSpeed;
        if (transitionTime > 1.0f)
        {
            currentLane = nextLane;
            var eulerAngles = rigidbody.rotation.eulerAngles;
            eulerAngles.z = 0.0f;
            rigidbody.rotation = Quaternion.Euler(new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z));

        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastLaneChange += Time.deltaTime;
        if (!isActive)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow) && TimeSinceLastLaneChange > TimeBetweenLaneChanges)
        {
            nextLane = currentLane + 1;
            nextLane = Mathf.Clamp(nextLane, -HalfNumberOfLanes, HalfNumberOfLanes);
            transitionTime = 0.0f;
            TimeSinceLastLaneChange = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && TimeSinceLastLaneChange > TimeBetweenLaneChanges)
        {
            nextLane = currentLane - 1;
            nextLane = Mathf.Clamp(nextLane, -HalfNumberOfLanes, HalfNumberOfLanes);
            transitionTime = 0.0f;
            TimeSinceLastLaneChange = 0.0f;
        }

        UpdatePosition();
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = rigidbody.position;
        currentPosition.z = currentZCoordinate;
        rigidbody.MovePosition(currentPosition);
    }
}
