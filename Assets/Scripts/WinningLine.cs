using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningLine : MonoBehaviour
{
    private SpriteRenderer winSprite;
    private float timeToChage = 0.01f; 
    public bool resultsChecked;

    private void Start()
    {
        winSprite = GetComponent<SpriteRenderer>();
        //WinSmth(); 
    }

    public void WinSmth()
    {
        resultsChecked = true;
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (resultsChecked)
        {
            for (int i = 0; i < 100; i += 5)
            {
                winSprite.color = new Color32(0, 255, 0, (byte)i);
                yield return new WaitForSeconds(timeToChage);
            }
            for (int i = 100; i > 0; i -= 5)
            {
                winSprite.color = new Color32(0, 255, 0, (byte)i);
                yield return new WaitForSeconds(timeToChage);
            }
        }

        winSprite.color = new Color32(0, 255, 0, 0);
    }
}
