using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Unity version needed to use "TextMeshProUGUI".
using TMPro;
using DG.Tweening;

public class CountDownTimer : MonoBehaviour
{
    // Set the total count down time in the inspector.
    public float totalTime;
    // A type for checking time running.
    public bool timeIsRunning = false;
    // The text type, in this unity version we use "TextMeshProUGUI".
    public TextMeshProUGUI timetext;
    // The previous time of total time to second 
    private int previousSeconds;
    private int previousMinutes;

    private ScoreManager ScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager = FindObjectOfType<ScoreManager>();
        // Set to true at the start of the game, means we want it start counting start of the game.
        timeIsRunning = true;
        // Get the total time to seconds from the start.
        previousSeconds = Mathf.FloorToInt(totalTime % 60);
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

                int currentMinutes = Mathf.FloorToInt(totalTime / 60);

                // Get the current second.
                int currentSeconds = Mathf.FloorToInt(totalTime % 60);
                // calculate the current hundredths of second.
                float hundredths = (totalTime * 100) % 100;

                // If the second is 10 and hundreths is 0 so it means 00:10:00 
                // The result of currentSeconds compare with the previousSeconds, and they both full integers so only be true when - 
                // - it's full integer and when second is smaller than 9.
                if ((currentMinutes == 0 && currentSeconds == 10 && Mathf.FloorToInt(hundredths) == 0) || currentMinutes == 0 && currentSeconds < previousSeconds && currentSeconds <= 9)
                {
                    // Run the flashing funciton
                    StartCoroutine(FlashingText());
                    // DOShakeScale(float duration, float strength, int vibrato, float randomness, bool fadeOut)
                    // Method from DOTween uses DOShakeScale to shakes an object's scale back and forth for a given duration.
                    timetext.transform.DOShakeScale(0.5f, 0.3f, 5, 5, true);
                }
                previousMinutes = currentMinutes;
                // Set the current second to previous second.
                previousSeconds = currentSeconds;

                // Display the time on the screen. Pass in the time as a argument.
                DisplayTime(totalTime);
            }
            else
            {
                // Set time to 0.
                totalTime = 0;
                // Stop the timmer.
                timeIsRunning = false;

                StopCoroutine("FlashingText");
                timetext.color = Color.white;



                // Call the end function.
                TimeEnd();
                // Display the current 0 timmer, so it won't display negative values in the timer.
                DisplayTime(totalTime);
            }
        }
        if (ScoreManager.rankListOpen)
        {
            return;
        }
    }

    IEnumerator FlashingText()
    {
        // Half red and half white color time when hits 10 seconds.
        while (timeIsRunning && totalTime <= 10)
        {
            timetext.color = Color.red;
            yield return new WaitForSeconds(0.5f); 
            timetext.color = Color.white;
            yield return new WaitForSeconds(0.5f); 
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
