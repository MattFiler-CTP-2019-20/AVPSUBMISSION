using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MainSeqTransition : ParticleSet
{
    [SerializeField] private float transitionDuration = 2;
    [SerializeField] private float transitionFrameRate = 60;

    [SerializeField] private float sizeMultiplier = 2.0f;
    [SerializeField] private GameObject mainSeqStar;
    [SerializeField] private GameObject redGiantStar;

    private Vector3 msStartScale = Vector3.zero;
    private Vector3 rgStartScale = Vector3.zero;

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
        mainSeqStar.SetActive(true);
        redGiantStar.SetActive(true);
        prevPs.GetComponent<ParticleSet>().StopParticles();

        int frames = (int)(transitionFrameRate * transitionDuration);
        float frameLength = 1.0f / (float)transitionFrameRate;
        Vector3 msStartScale = mainSeqStar.transform.localScale;
        Vector3 rgStartScale = redGiantStar.transform.localScale;
        Vector3 msEndScale = msStartScale * sizeMultiplier;
        Vector3 rgEndScale = rgStartScale * sizeMultiplier;

        for (int i = 0; i <= frames; i++)
        {
            float t = (float)i / (float)frames;
            mainSeqStar.transform.localScale = Vector3.Lerp(msStartScale, msEndScale, t);
            mainSeqStar.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, (byte)(255 - ((float)255 * t)));
            redGiantStar.transform.localScale = Vector3.Lerp(rgStartScale, rgEndScale, t);
            redGiantStar.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, (byte)((float)255 * t));
            yield return new WaitForSeconds(frameLength);
        }
        yield return new WaitForEndOfFrame();
        mainSeqStar.SetActive(false);
        isPlaying = false;
    }
}
