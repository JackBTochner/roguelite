using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;     // Reference to a UI Text element to display the score
    public float scoreMultiplier = 2.0f; // Multiplier for score increase
    public float multiplierDuration = 20.0f; // Duration of the multiplier in seconds

    private float currentMultiplier = 1.0f; // Current multiplier value
    private float multiplierEndTime; // Time when the multiplier will expire
    private int score = 0; // Current score

    public Canvas rankList;
    public TextMeshProUGUI rankListText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the score and multiplier
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the multiplier has expired
        if (Time.time >= multiplierEndTime)
        {
            currentMultiplier = 1.0f; // Reset the multiplier to 1
            UpdateScoreText(); // Update the displayed score
        }
    }

    // Function to increase the score
    public void IncreaseScore(int amount)
    {
        // Calculate the actual score increase with the current multiplier
        int increasedScore = Mathf.RoundToInt(amount * currentMultiplier);

        // Add the increased score to the total score
        score += increasedScore;

        // Update the displayed score
        UpdateScoreText();
    }

    // Function to update the displayed score
    void UpdateScoreText()
    {
        // Display the score with the current multiplier
        scoreText.text = "Score: " + score.ToString();// + "x" + currentMultiplier.ToString("F1");
    }

    // Function to activate the score multiplier for a duration
    public void ActivateMultiplier()
    {
        currentMultiplier = scoreMultiplier; // Set the multiplier
        multiplierEndTime = Time.time + multiplierDuration; // Calculate the multiplier expiration time
        UpdateScoreText(); // Update the displayed score
    }


    // 1. To save the top 5 score of the game, if a higher score comes in, edit the score rank list. The ranks are TopNo.1 - TopNo.5. 
    // 2. We only record the top 5, if any goes out, we delete the other.
    // 3. The text need to be edit, for each line "first line: Rank list:". The other 5 lines are "Rank: No.X XXX".
    // 4. The text well be updated each time when the savescore() is being called.
    // 5. The UI TextMeshProUGUI we used is called the "rankListText".
    // 6. These should be happen automatically, we shouln't press any button to change score.
    public void SaveScore()
    {
        int lastScore = score;


        List<int> highScores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                highScores.Add(PlayerPrefs.GetInt("HighScore" + i));
            }
        }

        highScores.Add(lastScore);  // Add the current score to the list
        highScores.Sort((a, b) => b.CompareTo(a));  // Sort in descending order
        if (highScores.Count > 5)
        {
            highScores.RemoveAt(5);  // Remove the 6th score to keep only the top 5
        }

        // Save updated scores
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }



        DisplayRankList();
    }
    public void DisplayRankList()
    {
        string rankText = "Rank list:\n";

        // Add each high score to the rank text
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                int score = PlayerPrefs.GetInt("HighScore" + i);
                rankText += "Rank: No." + (i + 1) + " " + score + "\n";
            }
        }
        rankListText.text = rankText;

        rankList.gameObject.SetActive(true);
    }

    //public void ClearHighScores()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        PlayerPrefs.DeleteKey("HighScore" + i);
    //    }
    //    PlayerPrefs.DeleteKey("LastScore");

    //    // Optional: Display a debug message to know the operation was successful
    //    Debug.Log("High scores cleared!");
    //}

}
