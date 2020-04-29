using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoSingleton<StarManager>
{
    [SerializeField]private StarPhase[] starPhases;
    [SerializeField] private GameObject[] shipPositions;
    [SerializeField] private GameObject[] pois;


    private int phaseIndex = 0;
    private int shipIndex = 0;

    bool isDoingStuff = false;

    private void Start()
    {
        shipPositions = GameObject.FindGameObjectsWithTag("Ship Pos");

        if(shipPositions.Length > 0) ShuttleController.Instance.MoveToNextPhase(shipPositions[shipIndex], "STELLAR NEBULAR PHASE");

        pois = GameObject.FindGameObjectsWithTag("pois");
        foreach (GameObject poi in pois) poi.SetActive(false);
        if (pois.Length > 0) pois[0].SetActive(true);
    }

    /////////////////////////////////////////////////////////////
    // Update here is just for testing, remove after development!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (isDoingStuff) return;
            if (shipIndex < shipPositions.Length)
            {
                pois[shipIndex].SetActive(false);
                ++shipIndex;
                switch (shipIndex)
                {
                    case 1:
                        ShuttleController.Instance.MoveToNextPhase(shipPositions[shipIndex], "MAIN PHASE");
                        break;
                    case 2:
                        ShuttleController.Instance.MoveToNextPhase(shipPositions[shipIndex], "RED GIANT PHASE");
                        break;
                    case 3:
                        ShuttleController.Instance.MoveToNextPhase(shipPositions[shipIndex], "SUPERNOVA PHASE");
                        break;
                }
                TransitionToNextPhase();
            }
        }
    }
    /////////////////////////////////////////////////////////////

    public void TransitionToNextPhase()
    {
        if (phaseIndex == starPhases.Length - 1)
            return;
        StartCoroutine(PhaseTransition());
    }

    private IEnumerator PhaseTransition()
    {
        isDoingStuff = true;
        // Stop the current system from emmiting
        starPhases[phaseIndex].loopParticleSystem.StopParticles();
        // Play the transition particles
        starPhases[phaseIndex].transitionParticleSystem.StartParticles(starPhases[phaseIndex].loopParticleSystem.GetParticleSystem());
        // Wait for the transition particles to finish
        while(starPhases[phaseIndex].transitionParticleSystem.IsPlaying())
        {
            yield return new WaitForEndOfFrame();
        }
        pois[shipIndex].SetActive(true);


        // Play the next phase loop particles
        phaseIndex++;
        starPhases[phaseIndex].loopParticleSystem.StartParticles();

        yield return new WaitForSeconds(5.0f);
        isDoingStuff = false;
    }
}

[System.Serializable]
public class StarPhase
{
    public ParticleSet loopParticleSystem;
    public ParticleSet transitionParticleSystem;
}

