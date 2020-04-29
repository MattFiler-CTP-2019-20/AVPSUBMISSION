using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiManager : MonoBehaviour
{

    [SerializeField] Camera camera;
    [SerializeField] bool inVr = false;
    [SerializeField] Transform primaryHandPos;

    void Start()
    {
        foreach(Transform child in transform)
        {
            foreach (PoiAnimationController poi in child.GetComponentsInChildren<PoiAnimationController>())
            {
                poi.camera = camera;
                poi.inVr = inVr;
                poi.primaryHandPos = primaryHandPos;
            }

        }
    }

}
