using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rows : MonoBehaviour
{
    public int steps;
    public bool movementStopped;

    public SlotType firstSlot;
    public SlotType resultSlot;
    public SlotType thirdSlot;

    public bool cheatOn;
    public int cheatSteps = 0;

    private Movements[] slotsInRow;

    public void Start()
    {
        slotsInRow = GetComponentsInChildren<Movements>();

        movementStopped = true;
        resultSlot = SlotType.BuggerOff;
    }
    public void StartSpinning()
    {
        foreach (Movements M in slotsInRow)
        {
            M.StartMovement();
        }

        steps = Random.Range(4, 7);

        if (cheatOn)
        { steps = cheatSteps; }

        resultSlot -= steps%4;
        if ((int)resultSlot < 0)
            resultSlot += 4;

        firstSlot = resultSlot - 1;
        if ((int)firstSlot < 0)
            firstSlot = SlotType.Masturbate;

        thirdSlot = resultSlot + 1;
        if ((int)thirdSlot > 3)
            thirdSlot = SlotType.JackpotFace;

        Debug.Log(firstSlot.ToString() + " " + resultSlot.ToString() + " " + thirdSlot.ToString());
        Debug.Log("_______________");
    }

  

}
