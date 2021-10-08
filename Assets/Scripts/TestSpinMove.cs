using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpinMove : MonoBehaviour
{

    private TestRow row;
    private TestSlotPosition slot;
    private Vector3 targetPosition;
    private float rotatingSpeed;
    private int sumSteps;

    void Start()
    {
        row = GetComponentInParent<TestRow>();
        slot = GetComponentInParent<TestSlotPosition>();

    }

    public void StartMovement()
    {
        row.movementStopped = false;
        StartCoroutine(Spining());
        rotatingSpeed = slot.rotatingSpeed;
    }

    private IEnumerator Spining()
    {
        sumSteps = slot.minRotationSteps + row.randomRotationSteps;
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < sumSteps; i++)
        {
            targetPosition = transform.position + Vector3.down * slot.distanceBetweenPositionY;

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, rotatingSpeed * Time.deltaTime);
                //rotatingSpeed = Mathf.MoveTowards(rotatingSpeed, 5, 5 * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
                if (i > (sumSteps - slot.stepsToStop))
                {
                    rotatingSpeed = Mathf.MoveTowards(rotatingSpeed, 5, slot.deceleration*Time.deltaTime);
                }
            }
            if (transform.position.y < slot.lowerPositionY)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 4 * slot.distanceBetweenPositionY);              
            }

            /*if (i > (slot.minRotationSteps * 0.75 + row.randomRotationSteps))
            {
                slot.rotatingSpeed = slot.rotatingSpeed * 0.9f;
            }*/
        }
        row.movementStopped = true;
    }

}
