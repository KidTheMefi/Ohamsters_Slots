using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField] private GameObject prizeImage;
    [SerializeField] private Text prizeText;
    [SerializeField] private Transform handle;
    [SerializeField] private Rows[] rows;
    [SerializeField] private Text totalPointsText;
    [SerializeField] private Text highScoreText; 
    [SerializeField] private WinningLine[] winningLine;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject PullImage; 


    private bool canSpin;
    private ParticleForWin particlesForWin;
    private int totalPoints;
    private bool resultsChecked = true;
    bool isWin = false;
    // Start is called before the first frame update
    void Start()
    {
        particlesForWin = GetComponent<ParticleForWin>();
        totalPoints = 0;
        totalPointsText.text = "Total points:" + "\n" + totalPoints;
        highScoreText.text = "HighScore" + "\n" + PlayerPrefs.GetInt("HighScore", 0);
        canSpin = true;
        PullImage.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].movementStopped || !rows[1].movementStopped || !rows[2].movementStopped)
        {
            prizeImage.SetActive(false);

            resultsChecked = false;
        }

        if (rows[0].movementStopped && rows[1].movementStopped && rows[2].movementStopped && !resultsChecked)
        {
            audioManager.Stop("Spinning");
            isWin = false;

            ResultsCheck(rows[0].firstSlot, rows[1].resultSlot, rows[2].thirdSlot);
            if (isWin)
            {
                winningLine[0].WinSmth();
                return;
            }

            ResultsCheck(rows[0].resultSlot, rows[1].resultSlot, rows[2].resultSlot);
            if (isWin)
            {
                winningLine[1].WinSmth();
                return;
            }

            ResultsCheck(rows[0].thirdSlot, rows[1].resultSlot, rows[2].firstSlot);
            if (isWin)
            {
                winningLine[2].WinSmth();
                return;
            }

            if (!isWin)
            {
                StartCoroutine(WaitingForSpin(1.9f));
                audioManager.Play("Nothing");
                prizeText.text = "Удача не на твоей стороне..." + "\n" + "Поцелуй ирландца!";
                prizeImage.SetActive(true);
            }

        }

    }

    private IEnumerator WaitingForSpin(float s) 
    {
        yield return new WaitForSeconds(s);
        PullImage.SetActive(true);
        canSpin = true;

    }

    void ResultsCheck(SlotType slot1, SlotType slot2, SlotType slot3)

    {
        resultsChecked = true;
        if (slot1 == slot2 && slot2 == slot3)
        {
            switch (slot1)
            {
                case SlotType.BuggerOff:
                    prizeText.text = "По домам, ублюдки, по домам!!" + "\n" + "-500 pt";
                    audioManager.Play("Lose");                   
                    AddPoints(-500);
                    break;
                case SlotType.JackpotFace:
                    prizeText.text = "!JACKPOT!" + "\n" + "МЫ ПЛЫЛИ И ДРОЧИЛИ!!!!" + "\n" + "+1000 pt";
                    audioManager.Play("Jackpot");
                    AddPoints(1000);
                    break;
                case SlotType.Masturbate:
                    prizeText.text = "Мы дрочили!" + "\n" + "+300 pt";
                    audioManager.Play("Win");
                    AddPoints(300);
                    break;
                case SlotType.Swim:
                    prizeText.text = "Мы плыли!" + "\n" + "+100 pt";
                    audioManager.Play("Win");
                    AddPoints(100);
                    break;
            }
            prizeImage.SetActive(true);
            particlesForWin.StartBoom(slot1);
            StartCoroutine(WaitingForSpin(4));
            isWin = true;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Click");
        if (rows[0].movementStopped && rows[1].movementStopped && rows[2].movementStopped && canSpin == true)
            StartCoroutine(PullHandle());
    }

    private IEnumerator PullHandle()
    {
        audioManager.Play("Spinning");
        audioManager.Play("Pull");
        canSpin = false;
        PullImage.SetActive(false);
        rows[0].StartSpinning();

        foreach (WinningLine W in winningLine)
        { W.resultsChecked = false; }

        for (int i = 0; i < 15; i += 3)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.05f);
        }

        rows[1].StartSpinning();

        for (int i = 0; i < 15; i += 3)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.06f);
        }

        rows[2].StartSpinning();
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
