using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleSystem : MonoBehaviour
{
    public ParticleSystem[] particleSystems;
    public ParticleSystem firework;

    public void StartBoom()
    {
        firework.Play();
    }

    public void StartBoom(Sprite particle)
    {
        foreach (ParticleSystem P in particleSystems)
        {
            P.textureSheetAnimation.SetSprite(0, particle);
            P.Play();
        }

    }



}
