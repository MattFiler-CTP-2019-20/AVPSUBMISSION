using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class RedGiantTransition : ParticleSet
{
    [SerializeField] private float transitionDuration = 2;
    [SerializeField] private float transitionFrameRate = 60;

    [SerializeField] private float sizeMultiplier = 0.9f;
    [SerializeField] private GameObject redGiantStar;
    [SerializeField] private GameObject glowObj;
    [SerializeField] private Image whiteout;

    [SerializeField] private ParticleSystem glow;
    [SerializeField] private ParticleSystem explosion;


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
        glowObj.SetActive(true);
        prevPs.GetComponent<ParticleSet>().StopParticles();

        int frames = (int)(transitionFrameRate * transitionDuration);
        int halfFrames = frames / 2;
        int quaterFrames = halfFrames / 2;
        float frameLength = 1.0f / (float)transitionFrameRate;
        Vector3 rgStartScale = redGiantStar.transform.localScale;
        Vector3 rgEndScale = rgStartScale * sizeMultiplier;

        glow.Play();


        for (int i = 0; i <= halfFrames; i++)
        {
            glowObj.transform.rotation = redGiantStar.transform.rotation;
            float t = (float)i / (float)halfFrames;
            glowObj.transform.localScale = Vector3.Lerp(rgStartScale, rgEndScale, t);
            glowObj.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, (byte)((float)200 * t));
            redGiantStar.transform.localScale = Vector3.Lerp(rgStartScale, rgEndScale, t);
            redGiantStar.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, (byte)(255 - ((float)200 * t)));
            yield return new WaitForSeconds(frameLength);
        }

        for (int i = 0; i <= quaterFrames; i++)
        {
            float t = (float)i / (float)quaterFrames;
            whiteout.color = new Color32(255, 255, 255, (byte)((float)255 * t));
            yield return new WaitForSeconds(frameLength);
        }

        yield return new WaitForEndOfFrame();
        glowObj.SetActive(false);
        redGiantStar.SetActive(false);
        isPlaying = false;

        for (int i = 0; i <= quaterFrames; i++)
        {
            float t = (float)i / (float)quaterFrames;
            whiteout.color = new Color32(255, 255, 255, (byte)(255 - ((float)255 * t)));
            yield return new WaitForSeconds(frameLength);
        }

    }
}
