using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForWin : MonoBehaviour
{
    public ParticleSystem[] particleSystems;
    public ParticleSystem firework;
    [SerializeField] private Sprite[] sprites;
    //private SlotType winningSlotType;


    public void StartBoom(SlotType winningSlotType)

    {  

        foreach (ParticleSystem P in particleSystems)
        {
            switch (winningSlotType)
            {
                case SlotType.JackpotFace:
                    P.textureSheetAnimation.SetSprite(0, sprites[0]);
                    firework.Play();
                    break;
                case SlotType.Swim:
                    firework.Play();
                    P.textureSheetAnimation.SetSprite(0, sprites[1]);
                    break;
                case SlotType.BuggerOff:
                    P.textureSheetAnimation.SetSprite(0, sprites[2]);
                    break;
                case SlotType.Masturbate:
                    P.textureSheetAnimation.SetSprite(0, sprites[3]);
                    firework.Play();
                    break;

                default:
                    P.textureSheetAnimation.SetSprite(0, sprites[0]);
                    break;
            }

            P.Play();
           
        }
    }
    
   
}
