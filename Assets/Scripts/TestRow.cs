using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRow : MonoBehaviour
{
    public int randomRotationSteps;
   
    public SlotType firstSlot;
    public SlotType secondSlot;
    public SlotType thirdSlot;

    public bool cheatOn;
    public int cheatSteps = 0;

    public bool movementStopped;

    private TestSpinMove[] slotsInRow;

    public void Start()
    {
        slotsInRow = GetComponentsInChildren<TestSpinMove>();

        movementStopped = true;
        secondSlot = SlotType.BuggerOff;

    }
    public void StartSpinning()
    {
        randomRotationSteps = Random.Range(4, 7);

        if (cheatOn)
        { randomRotationSteps = cheatSteps; }

        foreach (TestSpinMove M in slotsInRow)
        {
            M.StartMovement();
        }


        secondSlot -= randomRotationSteps % 4;
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
