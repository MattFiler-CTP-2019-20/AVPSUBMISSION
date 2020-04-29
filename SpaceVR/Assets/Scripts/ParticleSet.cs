using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSet : MonoBehaviour
{
    protected ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public virtual void StartParticles(ParticleSystem prevPs = null)
    {
        GetComponent<ParticleSystem>().Play();
    }

    public virtual void StopParticles()
    {
        GetComponent<ParticleSystem>().Stop();
    }

    public virtual bool IsPlaying()
    {
        return ps.isPlaying;
    }

    public ParticleSystem GetParticleSystem()
    {
        return ps;
    }

}
