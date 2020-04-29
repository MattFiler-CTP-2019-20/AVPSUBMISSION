using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class NebulaTransition : ParticleSet
{
    [SerializeField] private float transitionDuration = 2;
    [SerializeField] private float transitionFrameRate = 60;
    [SerializeField] private Vector3 focusPoint;

    private List<Vector3> particleStarts = new List<Vector3>();
    private bool isPlaying = true;

    public override bool IsPlaying()
    {
        return isPlaying;
    }

    public override void StartParticles(ParticleSystem prevPs)
    {
        StartCoroutine(ParticleAnimation(prevPs));
    }

    private IEnumerator ParticleAnimation(ParticleSystem prevPs)
    {
        ps.Play();
        yield return new WaitForSeconds(0.1f);
        Particle[] particles = new Particle[prevPs.particleCount];
        prevPs.GetParticles(particles);

        // Record the start position of all the particles
        for (int i = 0; i < particles.Length; i++)
        {
            particleStarts.Add(particles[i].position);
        }
        ps.SetParticles(particles);
        prevPs.SetParticles(new Particle[0]);

        // Loop through each frame and animate the particles
        int frames = (int)(transitionFrameRate * transitionDuration);
        float frameLength = 1.0f / (float)transitionFrameRate;
        int halfPoint = (int)(frames / 2);
        float halfTransition = transitionDuration / 2.0f;

        for (int i = 0; i <= frames; i++)
        {
            if(i == halfPoint)
            {
                for (int j = 0; j < particles.Length; j++)
                {
                    if(particles[j].remainingLifetime > halfTransition)
                    {
                        particles[j].remainingLifetime = halfTransition;
                    }
                }
            }
            float t = (float)i / (float)frames;
            for (int j = 0; j < particles.Length; j++)
            {
                particles[j].position = Vector3.Lerp(particleStarts[j], focusPoint, t);
            }
            ps.SetParticles(particles);
            yield return new WaitForSeconds(frameLength);
            ps.GetParticles(particles);
        }
        isPlaying = false;

    }
}
