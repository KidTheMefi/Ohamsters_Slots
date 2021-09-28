using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public bool cheatOn;
    public SlotType[] SlotWillAppear = new SlotType[3];
    private int[] cheatStep = new int[3];
    public TestRow[] rows = new TestRow[3];

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].cheatOn = cheatOn;
        }

        if (cheatOn)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                int n = SlotWillAppear[i] - rows[i].secondSlot;

                if (n == 2 || n == -2)
                    cheatStep[i] = 2;
                else if (n == -1 || n == 3)
                    cheatStep[i] = 1;
                else if (n == 0)
                    cheatStep[i] = 4;
                else if (n == 1 || n == -3)
                    cheatStep[i] = 3;
                rows[i].cheatSteps = cheatStep[i];

            }      
        }
    }
}
