using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum SlotType
{ JackpotFace, Swim, BuggerOff, Masturbate  }

public class Movements : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;
    private Rows row;


   
    void Start()
    {
        row = GetComponentInParent<Rows>();
        //GameControl.HandlePulled += StartMovement;

    }

    public void StartMovement()
    {

        row.movementStopped = false;
        timeInterval = 0.003f;
        StartCoroutine(Spining());
    }

    private IEnumerator Spining()
    {
        //yield return new WaitForEndOfFrame();

        for (int i = 0; i < 20 + row.steps; i++)
            {
            for (int j = 0; j < 9; j++)
            {
                //yield return new WaitForSeconds(timeInterval);
                yield return new WaitForSecondsRealtime(timeInterval);
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

                if (transform.position.y <= -4.3f)
                    transform.position = new Vector2(transform.position.x, transform.position.y + 9f);
            }

        

            if (i > (16 + row.steps) )
            { timeInterval += 0.003f; }
        }

        row.movementStopped = true;

    }

    private void OnDestroy()
    {
        //GameControl.HandlePulled -= StartMovement;
    }

    void Update()
    {
        
    }
}
