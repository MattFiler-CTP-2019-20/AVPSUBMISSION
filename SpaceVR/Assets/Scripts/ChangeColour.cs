using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Color[] colours;
    public float time = 2.0f;

    private int currentIndex = 0;
    private int nextIndex;
    private float lastChange = 0.0f;
    private float timer = 0.0f;

    void Start()
    {
        if (colours == null || colours.Length < 2)  Debug.Log("Need to setup colors array in inspector");

        nextIndex = (currentIndex + 1) % colours.Length;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > time)
        {
            currentIndex = (currentIndex + 1) % colours.Length;
            nextIndex = (currentIndex + 1) % colours.Length;
            timer = 0.0f;

        }

        Color currentColour = Color.Lerp(colours[currentIndex], colours[nextIndex], timer / time);
        GetComponent<Renderer>().materials[1].color = currentColour;

        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            ParticleSystem.TrailModule trail = ps.trails;
            trail.colorOverTrail = currentColour;
        }
    }
}
