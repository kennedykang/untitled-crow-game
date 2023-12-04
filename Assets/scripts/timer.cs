using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Needed to access scene information

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public TextMeshProUGUI timerText;
    private float startTime;
    private bool timerStarted;

    public string endSceneName = "EndScene"; // Name of the end scene

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            startTime = Time.time;
            timerStarted = true;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!timerStarted) return;

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;

        CheckEndScene();
    }

    public void ResetTimer()
    {
        startTime = Time.time;
    }

    private void CheckEndScene()
    {
        if (SceneManager.GetActiveScene().name == endSceneName)
        {
            Destroy(gameObject);
        }
    }
}
