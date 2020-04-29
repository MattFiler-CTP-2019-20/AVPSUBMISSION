using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MainSeqStart : ParticleSet
{
    [SerializeField] private GameObject sunModel;
    [SerializeField] private ParticleSystem loop;

    public override void StartParticles(ParticleSystem prevPs)
    {
        StartCoroutine(ParticleAnimation(prevPs));
    }

    public override void StopParticles()
    {
        ps.Stop();
        loop.Stop();
        loop.Clear();
    }

    private IEnumerator ParticleAnimation(ParticleSystem prevPs)
    {
        ps.Play();

        yield return new WaitForSeconds(0.6f);
        sunModel.SetActive(true);

        var module = ps.emission;
        module.burstCount = 0;

        loop.Play();
    }
    
}
