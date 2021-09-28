using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGameControler : MonoBehaviour
{

    [SerializeField] private GameObject prizeImage;
    [SerializeField] private Text prizeText;
    [SerializeField] private Transform handle;
    [SerializeField] private TestRow[] rows;
    [SerializeField] private Text totalPointsText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private WinningLine[] winningLine;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject PullImage;
 
    public List<SlotData> slotData = new List<SlotData>();

    private bool canSpin;
    private TestParticleSystem particlesForWin;
    private int totalPoints;
    private bool resultsChecked = true;
    bool isWin = false;
    // Start is called before the first frame update
    void Start()
    {
        particlesForWin = GetComponent<TestParticleSystem>();
        totalPoints = 0;
        totalPointsText.text = "Total points:" + "\n" + totalPoints;
        highScoreText.text = "HighScore" + "\n" + PlayerPrefs.GetInt("HighScore", 0);
        PullImage.SetActive(true);
        canSpin = true;
        

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
            StartCoroutine(WaitingForSpin(4));
            isWin = false;

            
            if (ResultsCheck(rows[0].secondSlot, rows[1].secondSlot, rows[2].secondSlot))
            {
                winningLine[1].WinSmth();
            }

            if(ResultsCheck(rows[0].firstSlot, rows[1].secondSlot, rows[2].thirdSlot))          
            {
                winningLine[0].WinSmth();      
            }

            if (ResultsCheck(rows[0].thirdSlot, rows[1].secondSlot, rows[2].firstSlot))
            {
                winningLine[2].WinSmth();            
            }           

            if (!isWin)
            {
                audioManager.Play("Nothing");
                prizeText.text = "Удача не на твоей стороне..." + "\n" + "Поцелуй ирландца!";
                prizeImage.SetActive(true);
            }

            resultsChecked = true;
        }

    }

    private IEnumerator WaitingForSpin(float s)
    {
        yield return new WaitForSeconds(s);
        PullImage.SetActive(true);
        canSpin = true;

    }

    bool ResultsCheck(SlotType slot1, SlotType slot2, SlotType slot3)

    {       
        if (slot1 == slot2 && slot2 == slot3)
        {
            foreach (SlotData S in slotData)
            {
                if (S.slotType == slot1)
                {
                    prizeText.text = S.winningText + "\n " + S.points + " points"; ;
                    particlesForWin.StartBoom(S.particleSprite);

                    if (slot1 != SlotType.BuggerOff)
                    { particlesForWin.StartBoom(); } 

                    AddPoints(S.points);
                    audioManager.Play(S.clip);         
                }
            }
            prizeImage.SetActive(true);          
            isWin = true;
            return true;
        }
        return false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Click");
        if (rows[0].movementStopped && rows[1].movementStopped && rows[2].movementStopped && canSpin == true)
        {
            StartCoroutine(PullHandle());
            StartCoroutine(StartSpinning());
        }
    }


    private IEnumerator StartSpinning()

    {   foreach (TestRow R in rows)
        {
            R.StartSpinning();
            yield return new WaitForSeconds(0.5f);
        }

    }

    private IEnumerator PullHandle()
    {
        audioManager.Play("Spinning");
        audioManager.Play("Pull");
        canSpin = false;
        PullImage.SetActive(false);
        
        foreach (WinningLine W in winningLine)
        { W.resultsChecked = false; }

         for (int i = 0; i < 15; i += 3)
         {
             handle.Rotate(0f, 0f, i);
             yield return new WaitForSeconds(0.05f);
         }

        for (int i = 0; i < 15; i += 3)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.06f);
        }
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

