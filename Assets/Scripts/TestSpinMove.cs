using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpinMove : MonoBehaviour
{
    
    private TestRow row;
    private float timeInterval;

    void Start()
    {
        row = GetComponentInParent<TestRow>();
       // timeInterval = row.timeInterval;
    }

    public void StartMovement()
    {

        row.movementStopped = false;

        StartCoroutine(Spining());
    }

    private IEnumerator Spining()
    {
        timeInterval = row.timeInterval;
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < 16 + row.steps; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                //yield return new WaitForSeconds(timeInterval);
                yield return new WaitForSecondsRealtime(timeInterval);
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

                if (transform.position.y <= -4.3f)
                    transform.position = new Vector2(transform.position.x, transform.position.y + 9f);
            }



            if (i > (12 + row.steps))
            { timeInterval += row.timeSlowdown; }
        }

        row.movementStopped = true;

    }

}
