using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    // The game object of the cow assigned from the inspector.
    public GameObject cowGameObject;

    // Used to invert the direction x (cow is moving towards -x);
    float POSITION_X_DIRECTION = -1.0f;

    string SCORE_TEXT = "Score: ";
    public TextMeshProUGUI scoreTextMesh;

    public TextMeshProUGUI PowerUpDoublePointsText;

    // Actual score is a float to accurately keep track of the score.
    private float actualScore;

    // Visible score is an int that is printed to the user.
    // Visible score is the floor of the actual score.
    private int visibleScore;

    // Variables used to calculate how much the cow has moved forward.
    private float cowPreviousPosX;
    private float cowCurrentPosX;

    private float cowMovementPoints;

    // The multiplier used to multiply the points aquired.
    private float scoreMultiplier;

    // One score point every 100 units the cow moves.
    private float COW_MOVEMENT_POINTS_MULTIPLIER = 1/200f;

    // Used to get values from the character controller.
    private CaracterControler characterController;

    // Start is called before the first frame update
    void Start()
    {

        characterController = cowGameObject.GetComponent<CaracterControler>();

        this.scoreMultiplier = 1.0f;
        updateScore(0, 0);

        // Initialize the cow movement variables.
        this.cowPreviousPosX = getCowPositionX();
        this.cowCurrentPosX = getCowPositionX();

    }

    public void incrementMultiplier(float value)
    {
        this.scoreMultiplier += value;
    }

    float getCowPositionX()
    {
        float res = this.cowGameObject.transform.position.x * POSITION_X_DIRECTION;

        return res;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Check if the cow has been launched.
        if (characterController.isCowActive())
        {
            // Get the movement points.
            this.cowCurrentPosX = getCowPositionX();
            this.cowMovementPoints = getPosXDifference(this.cowPreviousPosX, this.cowCurrentPosX) * COW_MOVEMENT_POINTS_MULTIPLIER;

            // Update the previous position to the current one.
            this.cowPreviousPosX = this.cowCurrentPosX;

            updateScore(this.cowMovementPoints, this.scoreMultiplier);
        }
        
    }

    float getPosXDifference(float startPosX, float endPosX)
    {
        float res = 0.0f;

        res = endPosX - startPosX;
        if (res <= 0)
        {
            res = 0.0f;
        }

        return res;
    }

    public float getMultiplier()
    {
        return this.scoreMultiplier;
    }

    public void updateScore(float scoreToAdd, float multiplier=1)
    {

        // Calculate the score and multiplier.
        this.actualScore += (scoreToAdd * multiplier);

        // Calculate an int value from the score.
        this.visibleScore = Mathf.FloorToInt(this.actualScore);

        // Update the score gui.
        this.scoreTextMesh.text = SCORE_TEXT + this.visibleScore;
    }
}
