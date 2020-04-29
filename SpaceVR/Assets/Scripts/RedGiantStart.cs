using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RedGiantStart : ParticleSet
{
    [SerializeField] private GameObject prevRedGiantModel;
    [SerializeField] private GameObject redGiantModel;
    [SerializeField] private ParticleSystem loop;

    public override void StartParticles(ParticleSystem prevPs)
    {

        prevRedGiantModel.SetActive(false);
        redGiantModel.SetActive(true);
        Vector3 scaleDif = new Vector3(prevRedGiantModel.transform.localScale.x / redGiantModel.transform.localScale.x,
            prevRedGiantModel.transform.localScale.y / redGiantModel.transform.localScale.y,
            prevRedGiantModel.transform.localScale.z / redGiantModel.transform.localScale.z);
        loop.transform.localScale = new Vector3(loop.transform.localScale.x * scaleDif.x,
            loop.transform.localScale.y * scaleDif.y,
            loop.transform.localScale.z * scaleDif.z);

        redGiantModel.transform.position = prevRedGiantModel.transform.position;
        redGiantModel.transform.rotation = prevRedGiantModel.transform.rotation;
        redGiantModel.transform.localScale = prevRedGiantModel.transform.localScale;

        loop.Play();
    }

    public override void StopParticles()
    {
        ps.Stop();
        loop.Stop();
        loop.Clear();
    }
}
