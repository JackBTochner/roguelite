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
                totalTime = 0;
                timeIsRunning = false;
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

        // Set the text that displayed.
        // string.Format is to format display the text.
        // "0:00"  first 0 means the first argument which is minutes,
        // :00 specifies that it should be displayed as a two-digit number, padding with zeros if necessary.

        // "1:00"  first 1 means the second argument which is seconds, 
        // :00 specifies that it should also be displayed as a two-digit number, padding with zeros if necessary.
        timetext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
