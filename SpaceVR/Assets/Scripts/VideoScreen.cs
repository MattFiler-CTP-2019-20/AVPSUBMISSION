using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScreen : MonoBehaviour
{
    private bool playVideo = false;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private Animator anim;
    private float animFrameCount = 0;

    private int index = 0;
    [SerializeField] AnimationClip animClip;

    private void Start()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if(playVideo)
        {
            if ((ulong)videoPlayer.frame == videoPlayer.frameCount - animFrameCount - 1) CloseVideo();

            if (Input.GetKeyDown(KeyCode.A))
            {
                CloseVideo();
                videoPlayer.Stop();
                foreach(GameObject poi in GameObject.FindGameObjectsWithTag("poi"))
                {
                    poi.GetComponent<PoiAnimationController>().currentClickTime = float.MaxValue;
                }

            }
        }
    }

    public void OpenVideo(UnityEngine.Video.VideoClip clip)
    {
        if (playVideo) return;

        videoPlayer.clip = clip;
        videoPlayer.Play();
        anim.SetTrigger("Open");
        playVideo = true;
        animFrameCount = animClip.frameRate * animClip.length;
    }

    public void CloseVideo()
    {
        if (!playVideo) return;
        anim.SetTrigger("Close");
        playVideo = false;
    }
}
