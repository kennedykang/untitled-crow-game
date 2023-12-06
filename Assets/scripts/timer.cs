using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public TextMeshProUGUI timerText;
    private float startTime;
    private bool timerStarted;
    private bool timerStopped; 

    private float finalTime; 
    public string endSceneName = "EndScene"; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            startTime = Time.time;
            timerStarted = true;
            timerStopped = false;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (!timerStarted || timerStopped) return;

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;

        CheckEndScene();
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        timerStopped = false;
    }

    private void CheckEndScene()
    {
        if (SceneManager.GetActiveScene().name == endSceneName && !timerStopped)
        {
            finalTime = Time.time - startTime;
            timerStopped = true;
            
            StartCoroutine(DisplayCongratulatoryMessage());
        }
    }

    private IEnumerator DisplayCongratulatoryMessage()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Set the font size to 5
        timerText.fontSize = 20;

        // Display the final time with a congratulatory message
        string finalMinutes = ((int)finalTime / 60).ToString();
        string finalSeconds = (finalTime % 60).ToString("f2");
        timerText.text = "Congratulations, you completed the game in " + finalMinutes + ":" + finalSeconds;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Destroy(gameObject);
        }
    }
}
