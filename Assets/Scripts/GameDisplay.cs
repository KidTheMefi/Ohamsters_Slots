using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDisplay : MonoBehaviour
{

    [SerializeField] private GameObject prizeImage;
    [SerializeField] private Text prizeText;
    [SerializeField] private Text totalPointsText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject pullImage;

    private int totalPoints;


    // Start is called before the first frame update
    void Start()
    {
        totalPoints = 0;
        totalPointsText.text = "Total points:" + "\n" + totalPoints;
        highScoreText.text = "HighScore" + "\n" + PlayerPrefs.GetInt("HighScore", 0);
        pullImage.SetActive(true);
    }

    public void CanSpin()
    {
        pullImage.SetActive(true);
    }

    public void Hide()
    {
        prizeImage.SetActive(false);
        pullImage.SetActive(false);
    }

    public void Lose()
    { 
        prizeText.text = "Удача не на твоей стороне..." + "\n" + "Поцелуй ирландца!";
        prizeImage.SetActive(true);
    }

    public void Win(SlotData slot)
    {
        prizeText.text = slot.winningText + "\n " + slot.points + " points";
        AddPoints(slot.points);
        prizeImage.SetActive(true);
    }


    private void AddPoints(int p)
    {
        totalPoints += p;
        totalPointsText.text = "Your points:" + "\n" + totalPoints;

        if (PlayerPrefs.GetInt("HighScore", 0) < totalPoints)
        {
            PlayerPrefs.SetInt("HighScore", totalPoints);
            highScoreText.text = "HighScore" + "\n" + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScoreText.text = "HighScore" + "\n" + 0;
    }
}
