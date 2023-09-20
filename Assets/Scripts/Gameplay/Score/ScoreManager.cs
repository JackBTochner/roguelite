using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to a UI Text element to display the score
    public float scoreMultiplier = 2.0f; // Multiplier for score increase
    public float multiplierDuration = 20.0f; // Duration of the multiplier in seconds

    private float currentMultiplier = 1.0f; // Current multiplier value
    private float multiplierEndTime; // Time when the multiplier will expire
    private int score = 0; // Current score

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
        scoreText.text = "Score: " + score.ToString() + "x" + currentMultiplier.ToString("F1");
    }

    // Function to activate the score multiplier for a duration
    public void ActivateMultiplier()
    {
        currentMultiplier = scoreMultiplier; // Set the multiplier
        multiplierEndTime = Time.time + multiplierDuration; // Calculate the multiplier expiration time
        UpdateScoreText(); // Update the displayed score
    }
}
