using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRow : MonoBehaviour
{
    public int steps;
    public bool movementStopped;

    public float timeInterval;
    [Range (0.001f, 0.01f)] public float timeSlowdown; 

    public SlotType firstSlot;
    public SlotType secondSlot;
    public SlotType thirdSlot;

    public bool cheatOn;
    public int cheatSteps = 0;

    private TestSpinMove[] slotsInRow;

    public void Start()
    {
        slotsInRow = GetComponentsInChildren<TestSpinMove>();

        movementStopped = true;
        secondSlot = SlotType.BuggerOff;

    }
    public void StartSpinning()
    {
        steps = Random.Range(4, 7);

        if (cheatOn)
        { steps = cheatSteps; }

        foreach (TestSpinMove M in slotsInRow)
        {
            M.StartMovement();
        }

       

        secondSlot -= steps % 4;
        if ((int)secondSlot < 0)
            secondSlot += 4;

        firstSlot = secondSlot - 1;
        if ((int)firstSlot < 0)
            firstSlot = SlotType.Masturbate;

        thirdSlot = secondSlot + 1;
        if ((int)thirdSlot > 3)
            thirdSlot = SlotType.JackpotFace;

        /*Debug.Log(firstSlot.ToString() + " " + secondSlot.ToString() + " " + thirdSlot.ToString());
        Debug.Log("_______________");*/
    }


}
