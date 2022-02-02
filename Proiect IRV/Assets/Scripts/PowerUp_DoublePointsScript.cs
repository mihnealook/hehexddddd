using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_DoublePointsScript : MonoBehaviour
{
    public GameManager gameManager;
    float POWER_UP_DOUBLE_POINTS_DURATION = 10f;
    float POWER_UP_DOUBLE_POINTS_MULTIPLIER = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cow")
        {
            print("POWERUP DOUBLE COINTS");

            this.gameObject.GetComponent<MeshRenderer>().enabled = false;

            StartCoroutine(doublePointsTimer());

        }

    }
    IEnumerator doublePointsTimer()
    {
        gameManager.incrementMultiplier(POWER_UP_DOUBLE_POINTS_MULTIPLIER);

        gameManager.PowerUpDoublePointsText.gameObject.SetActive(true);

        yield return new WaitForSeconds(POWER_UP_DOUBLE_POINTS_DURATION);

        gameManager.PowerUpDoublePointsText.gameObject.SetActive(false);

        gameManager.incrementMultiplier(-POWER_UP_DOUBLE_POINTS_MULTIPLIER);
    }
}
