using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{ JackpotFace, Swim, BuggerOff, Masturbate }

public class TestGameControler : MonoBehaviour
{

    [SerializeField] private Transform handle;
    [SerializeField] private TestRow[] rows;
    [SerializeField] private WinningLine[] winningLine;
    [SerializeField] private AudioManager audioManager;
    
    public List<SlotData> slotData = new List<SlotData>();
    public int secondsBeforeNextSpin;

    private int stoppedCount;
    private bool canSpin;
    private TestParticleSystem particlesForWin;
    private GameDisplay gameDisplay;
    private bool isWin = false;

    // Start is called before the first frame update
    void Start()
    {
        TestSpinMove.MovementStop += StopCheck;
        particlesForWin = GetComponent<TestParticleSystem>();
        gameDisplay = GetComponent<GameDisplay>();
        canSpin = true;
    }

    private void OnDestroy()
    {
        TestSpinMove.MovementStop -= StopCheck;
    }
    // Update is called once per frame
    void Update()
    {

    }
     
    private void MovementStoped()
    {
        audioManager.StopSpinning();
        secondsBeforeNextSpin = 4;
        isWin = false;

        if (ResultsCheck(rows[0].secondSlot, rows[1].secondSlot, rows[2].secondSlot))
        {
            winningLine[1].WinSmth();
        }

        if (ResultsCheck(rows[0].firstSlot, rows[1].secondSlot, rows[2].thirdSlot))
        {
            winningLine[0].WinSmth();
        }

        if (ResultsCheck(rows[0].thirdSlot, rows[1].secondSlot, rows[2].firstSlot))
        {
            winningLine[2].WinSmth();
        }

        if (!isWin)
        {
            secondsBeforeNextSpin = 2;
            audioManager.PlayLose();
            gameDisplay.Lose();
        }
        StartCoroutine(WaitingForSpin(secondsBeforeNextSpin));
    }

    private IEnumerator WaitingForSpin(float s)
    {
        yield return new WaitForSeconds(s);
        gameDisplay.CanSpin();
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
                    gameDisplay.Win(S);
                    audioManager.Play(S.clip);
                    particlesForWin.StartBoom(S.particleSprite);        
                    if (slot1 != SlotType.BuggerOff)
                    { particlesForWin.StartBoom(); }    
                }
            }        
            isWin = true;
            return true;
        }
        return false;
    }

    private void OnMouseDown()
    {
        if (canSpin == true)
        {
            canSpin = false;
            stoppedCount = 0;
            StartCoroutine(PullHandle());
            StartCoroutine(StartSpinning());
            gameDisplay.Hide();
        }
    }

    private void StopCheck()
    {
        stoppedCount++;
        if (stoppedCount == 12)
            MovementStoped();
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
        audioManager.PlaySpin();

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

}

