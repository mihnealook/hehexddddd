using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterControler : MonoBehaviour
{

    public float COINS_WORTH_POINTS = 1000;

    public GameManager gameManager;

    public float force;
    public int lives = 5;
    public UnityEngine.UI.Text livesText;
    public UnityEngine.UI.Text titleText;
    public UnityEngine.UI.Button quitButton;

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

    public bool isCowActive()
    {
        return this.isActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            this.gameManager.updateScore(COINS_WORTH_POINTS, gameManager.getMultiplier());
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
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

            this.transform.position = new Vector3(this.transform.position.x, 6.5f, this.transform.position.z);
        }
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0) {
            Time.timeScale = 0;
            titleText.text = "You Lost";
            titleText.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }
}
