using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

// Ricky: The veision of our game needed to use TextMeshProUGUI for the text. we need to use "using TMPro;"
using TMPro;
// Ricky: List<> needed "using System.Collections.Generic;"
using System.Collections.Generic;
// Ricky: To swap between scenes.
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    // Ricky: Our unity verision need to use TextMeshProUGUI rather than text.
    public TextMeshProUGUI scoreText;     // Reference to a UI Text element to display the score
    public float scoreMultiplier = 2.0f; // Multiplier for score increase
    public float multiplierDuration = 20.0f; // Duration of the multiplier in seconds

    private float currentMultiplier = 1.0f; // Current multiplier value
    private float multiplierEndTime; // Time when the multiplier will expire
    private int score = 0; // Current score

    public Canvas rankList;
    public TextMeshProUGUI rankListText;

    public Button returnToMainMenuButton;

    public bool rankListOpen = false;

    public LoadEventChannelSO _loadMenuEvent = default;
    public GameSceneSO _menuScene = default;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the score and multiplier
        UpdateScoreText();
        rankListOpen = false;
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

    //Ricky:
    // 1. To save the top 5 score of the game, if a higher score comes in, edit the score rank list. The ranks are TopNo.1 - TopNo.5. 
    // 2. We only record the top 5, if any goes out, we delete the other.
    // 3. The text need to be edit, for each line "first line: Rank list:". The other 5 lines are "Rank: No.X XXX".
    // 4. The text well be updated each time when the savescore() is being called.
    // 5. The UI TextMeshProUGUI we used is called the "rankListText".
    // 6. These should be happen automatically, we shouln't press any button to change score.

    // We call the "SaveScore()" when our player die in the battle scene.
    public void SaveScore()
    {
        // Get the current score and save it in a variable "lastScore".
        int lastScore = score;

        // Create a empty List "highScores".
        List<int> highScores = new List<int>();

        // Save the top 5 scores into the "PlayerPrefs", and added to the highScores list.
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                // PlayerPrefs: this is a class in Unity to store simple key-value pairs locally. Save the information.
                highScores.Add(PlayerPrefs.GetInt("HighScore" + i));
            }
        }
        // Add the new score into the list.
        highScores.Add(lastScore);
        // Create descending order of the list.
        highScores.Sort((a, b) => b.CompareTo(a));
        // If the score has 5 removed the 6th
        if (highScores.Count > 5)
        {
            highScores.RemoveAt(5);
        }
        // Save the new list into the Playerprefs
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        // Display the rank list function
        DisplayRankList();
    }

    public void DisplayRankList()
    {
        rankListOpen = true;

        Time.timeScale = 0.0f;
        // The headder of the rank list.
        string ranklistText = "Rank list:\n";

        // display the current score
        ranklistText += "Your Score: " + score.ToString() + "\n";

        // Print out all the top 5 ranks in the PlayerPrefs and format it out.
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                int score = PlayerPrefs.GetInt("HighScore" + i);
                ranklistText += "Rank: No." + (i + 1) + " " + score + "\n";
            }
        }
        // Set the text into our Text in game.
        rankListText.text = ranklistText;
        // Open our rank list.
        rankList.gameObject.SetActive(true);

        // Set button to automatically selected by the gamepad.
        returnToMainMenuButton.Select();

    }

    public void clearRankList()
    {
        // Clear everything of our ranklist, exclude first line.
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey("HighScore" + i);
        }

        DisplayRankList();
    }

    public void ReturnToMainMenu()
    {
        DialogueLua.SetVariable("PlayedConversations", "[]");
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene("Initialization");
        _loadMenuEvent.RaiseEvent(_menuScene, false);
    }

    public void clearRankListOnly()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey("HighScore" + i);
        }
    }
}
