using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Unity version needed to use "TextMeshProUGUI".
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    // Set the total count down time in the inspector.
    public float totalTime;
    // A type for checking time running.
    public bool timeIsRunning = false;
    // The text type, in this unity version we use "TextMeshProUGUI".
    public TextMeshProUGUI timetext;

    // Start is called before the first frame update
    void Start()
    {
        // Set to true at the start of the game, means we want it start counting start of the game.
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check it is running
        if (timeIsRunning)
        {
            // If time more than 0
            if (totalTime > 0)
            {
                // - the total time by the time passed since last frame.
                totalTime -= Time.deltaTime;
                // Display the time on the screen. Pass in the time as a argument.
                DisplayTime(totalTime);
            }
            else
            {
                // Set time to 0.
                totalTime = 0;
                // Stop the timmer.
                timeIsRunning = false;
                // Call the end function.
                TimeEnd();
                // Display the current 0 timmer, so it won't display negative values in the timer.
                DisplayTime(totalTime);
            }
        }
    }

    // Get the parameter time.
    void DisplayTime(float timeToDisplay)
    {
        // "timeToDisplay / 60" get the minute for the total time in seconds.
        // "Mathf.FloorToInt" take a float number and rounds it down to the nearst integer.
        // Example: 120 / 60 = 2, 125 / 60 = 2....a float number
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);

        // "timeToDisplay % 60" returns the remainder.
        // Example: 123 / 60 = 2 and remainder 3.
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // This conversion multiplies timeToDisplay by 100 to shift the decimal two places to the right,
        // then takes the modulus by 100 to get the remainder, which represents the hundredths of a second.
        float hundredths = (int)((timeToDisplay * 100) % 100);

        // Set the text that displayed.
        // string.Format is to format display the text.
        // "0:00"  first 0 means the first argument which is minutes,
        // :00 specifies that it should be displayed as a two-digit number, padding with zeros if necessary.

        // "1:00"  first 1 means the second argument which is seconds, 
        // :00 specifies that it should also be displayed as a two-digit number, padding with zeros if necessary.

        // "2:00" will be the same but we divided 100.
        timetext.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    void TimeEnd()
    {
        Debug.Log("Timer has ended!");

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.SaveScore();
    }
}
