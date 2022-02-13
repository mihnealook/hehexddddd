using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject coinPrefab;
    public GameObject boxPrefab;
    public GameManager gameManager;
    public float distanceBetweenSpawns;
    public GameObject cow;
    public float[] lanes;
    public float startingX;
    public float timeBetweenSpawns;

    private float timeLastSpawned = 0;
    private int half;
    private float width;
    private float currPos;
    // Start is called before the first frame update
    void Start() {
        half = cow.GetComponent<MoveLines>().HalfNumberOfLanes;
        width = cow.GetComponent<MoveLines>().OffsetBetweenLines;
        lanes = new float[half * 2 + 1];
        for (int i = -half; i <= half; i++) {
            lanes[i + half] = width * i + cow.transform.position.z; 
        }

        for (int i = 0; i < 100; i++) {
            currPos = startingX - distanceBetweenSpawns * i;
            SpawnLane();
        }

        timeLastSpawned = Time.time;
    }

    void SpawnLane() {
        int boxes = 0;
        for (int j = 0; j < half * 2 + 1; j++) {
            int ran = Random.Range(0, 3);
            switch (ran) {
                case 0:
                    break;
                case 1:
                    GameObject coin = Instantiate(coinPrefab, new Vector3(currPos, 0, lanes[j]), new Quaternion());
                    break;
                case 2:
                    if (boxes == 2) {
                        GameObject backupCoin = Instantiate(coinPrefab, new Vector3(currPos, 0, lanes[j]), new Quaternion());
                    } else {
                        GameObject box = Instantiate(boxPrefab, new Vector3(currPos, 6.5f, lanes[j]), new Quaternion());
                        box.GetComponent<ArionDigital.CrashCrate>().gameManager = gameManager;
                        boxes++;
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeLastSpawned + timeBetweenSpawns) {
            currPos -= distanceBetweenSpawns;
            SpawnLane();
            timeLastSpawned = Time.time;
        }
    }
}
