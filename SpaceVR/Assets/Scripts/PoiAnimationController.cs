using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiAnimationController : MonoBehaviour
{
    public  enum AnimationState {IDLE = 0, SELECTED, CLICKED }
    public AnimationState currentState = AnimationState.IDLE;

    [SerializeField] AnimationClip idleAnim;
    [SerializeField] AnimationClip selectedAnim;
    [SerializeField] AnimationClip clickedAnim;

    [SerializeField] float colourChangeTimeIdle = 5;
    [SerializeField] float colourChangeTimeSelected = 1;

    [SerializeField] float clickTime = 0.0f;

    public Transform childTransform;
    private Animation poiAnim;

    float clickedLength = 0.0f;
    public float currentClickTime = 0.0f;

    public VideoScreen videoScreen;
    public UnityEngine.Video.VideoClip videoClip;

    public Camera camera;
    public bool inVr = false;
    public Transform primaryHandPos;

    private void Start()
    {
        poiAnim = GetComponentInChildren<Animation>();
    }

    private void Update()
    {
        bool hit = GetComponent<BoxCollider>().Raycast(inVr? new Ray(primaryHandPos.position, primaryHandPos.forward) 
            : camera.ScreenPointToRay(Input.mousePosition),out RaycastHit info, Mathf.Infinity);

        //Debug.DrawRay(primaryHandPos.position, primaryHandPos.forward);

        switch (currentState)
        {
            case AnimationState.IDLE:

                if (hit && !videoScreen.GetComponent< UnityEngine.Video.VideoPlayer>().isPlaying)
                {
                    currentState = AnimationState.SELECTED;
                    poiAnim.Stop();
                    poiAnim.clip = selectedAnim;
                    poiAnim.Play();
                }
                break;

            case AnimationState.SELECTED:
                Vector3 pos = childTransform.localPosition;
                childTransform.localPosition = Vector3.Lerp(pos, new Vector3(pos.x, 0, pos.z), 0.05f);
                GetComponentInChildren<ChangeColour>().time = colourChangeTimeSelected;

                bool clicked = inVr ? OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) : Input.GetMouseButtonDown(0);

                if (clicked)
                {
                    currentState = AnimationState.CLICKED;
                    poiAnim.Stop();
                    poiAnim.clip = clickedAnim;
                    poiAnim.Play();
                    GetComponentInChildren<ChangeColour>().enabled = false;
                    videoScreen.OpenVideo(videoClip);
                    clickedLength = (float)videoScreen.GetComponent<UnityEngine.Video.VideoPlayer>().clip.length;
                    break;
                }

                resetState(hit);
                break;

            case AnimationState.CLICKED:
                currentClickTime += Time.deltaTime;
                if (currentClickTime >= clickedLength)
                {
                    resetState(false);
                    currentClickTime = 0;
                }
                break;
        }



    }

    private void resetState(bool hit)
    {
        if (!hit)
        {
            currentState = AnimationState.IDLE;
            poiAnim.Stop();
            poiAnim.clip = idleAnim;
            poiAnim.Play();
            GetComponentInChildren<ChangeColour>().enabled = true;
            GetComponentInChildren<ChangeColour>().time = colourChangeTimeIdle;

        }
    }
}