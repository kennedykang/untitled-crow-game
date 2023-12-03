using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance; // Static reference to the instance
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool timerActive = false;

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (timerText == null)
        {
            timerText = FindObjectOfType<TextMeshProUGUI>();
        }
        StartTimer();
    }   

    void Update()
    {
        if (timerActive && timerText != null)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00.00");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        if (!timerActive)
        {
            startTime = Time.time;
            timerActive = true;
        }
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
