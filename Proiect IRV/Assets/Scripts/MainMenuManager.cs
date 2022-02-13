using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public UnityEngine.UI.Text highscore;
    public GameObject music;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(music.gameObject);
        audioSource = this.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("highscore")) {
            highscore.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();
        } else {
            PlayerPrefs.SetInt("highscore", 0);
            highscore.text = "Highscore: 0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonSound() {
        audioSource.Play();
    }

    public void LoadGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
