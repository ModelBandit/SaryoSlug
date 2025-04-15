using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineAnimatorController : MonoBehaviour
{
    enum AnimationIndex
    {
        Crounch = 0, // 0 
        Flutter, // 2
        Idle_Front, // 0
        Idle_Up, // 1
        Jump_Idle_Under, // 둘이 합쳐야함
        Jump_Idle_Up, // 둘이 합쳐야함
        Jump_Move_Under,
        Move_Front,
        NOANIM,
        ShootHG_Crounch,
        ShootHG_Down,
        ShootHG_Front,
        ShootHG_Up
    }



    [SerializeField] SkeletonAnimation SkeletonAnim;

    Spine.Animation[] AnimationArray;
    const float JumpPower = 1;
    float Jump = 0;

    // Start is called before the first frame update
    void Start()
    {
        AnimationArray = SkeletonAnim.state.Data.SkeletonData.Animations.Items;
        SpineBlendTimeSetting();
    }

    // Update is called once per frame
    public void ObjectUpdate()
    {
        if (isPlaying(1))
        {
            AnimationOverrideClear(1);
        }

        InputCheck();

        if (Input.anyKeyDown)
            SkeletonAnim.skeleton.SetToSetupPose();
    }

    private void PlayAnimation(string AnimationName, bool isLoop)
    {
        SkeletonAnim.state.SetAnimation(0, AnimationName, isLoop);
    }
    private void PlayAnimation(Spine.Animation AnimationName, bool isLoop)
    {
        SkeletonAnim.state.SetAnimation(0, AnimationName, isLoop);
    }
    private Spine.TrackEntry PlayAnimation(int index, string AnimationName, bool isLoop)
    {
        return SkeletonAnim.state.SetAnimation(index, AnimationName, isLoop);
    }

    private Spine.TrackEntry PlayAnimation(int index, Spine.Animation AnimationName, bool isLoop)
    {
        if (AnimationClear != null)
            StopCoroutine(AnimationClear);
        SkeletonAnim.state.ClearTrack(index);

        return SkeletonAnim.state.SetAnimation(index, AnimationName, isLoop);
    }

    public void AnimationOverrideClear(int index)
    {   
        string mainTrackName = SkeletonAnim.state.GetCurrent(0).Animation.Name;
        string indexTrackName = SkeletonAnim.state.GetCurrent(index).Animation.Name;

        Spine.TrackEntry track = PlayAnimation(index, SkeletonAnim.state.GetCurrent(0).Animation.Name, true);
        track.TrackTime = SkeletonAnim.state.GetCurrent(0).TrackTime;
        AnimationClear = StartCoroutine(DelayClearing(1, SkeletonAnim.state.GetCurrent(1).AnimationEnd));
        //if (mainTrackName == indexTrackName)
        //    SkeletonAnim.state.ClearTrack(index);
        //else
        //{
        //}
    }

    private bool isPlaying(int index)
    {
        Spine.TrackEntry track = SkeletonAnim.state.GetCurrent(index);
        if (track != null)
        {
            float indexTime = SkeletonAnim.state.GetCurrent(index).AnimationTime;
            float indexEndTime = SkeletonAnim.state.GetCurrent(index).AnimationEnd;
            if (indexTime > indexEndTime)
                return true;
        }
        return false;
    }

    Coroutine AnimationClear = null;
    private IEnumerator DelayClearing(int index, float EndTime)
    {
        float t = 0f;

        while (t < EndTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        SkeletonAnim.state.ClearTrack(index);
        yield break;
    }

    private void SpineBlendTimeSetting()
    {
        SkeletonAnim.state.Data.DefaultMix = 0.1f;
        for(int i = 2;i<8;++i)
        {
            SkeletonAnim.state.Data.SetMix(AnimationArray[i], AnimationArray[11], 0f);
        }
        //SkeletonAnim.state.Data.SetMix(ShootHG, Idle, 0.2f);
        //SkeletonAnim.state.Data.SetMix(ShootHG, Move, 0.2f);
        //SkeletonAnim.state.Data.SetMix(Move, Idle, 0.2f);
        //SkeletonAnim.state.Data.SetMix(Idle, Move, 0.2f);


    }

    Vector3 v3 = Vector3.zero;
    bool isJump = false;

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayAnimation(1, AnimationArray[3], true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            AnimationOverrideClear(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SkeletonAnim.skeleton.ScaleX = 1f;
            PlayAnimation(AnimationArray[7], true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayAnimation(AnimationArray[0], true);
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SkeletonAnim.skeleton.ScaleX = -1f;
            PlayAnimation(AnimationArray[7], true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)
            || Input.GetKeyUp(KeyCode.DownArrow) )
        {
            PlayAnimation(AnimationArray[2], true);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayAnimation(1, AnimationArray[11], false);
        }

        //if (transform.localPosition.y > 0)
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Jump = JumpPower;
            v3 = transform.position;
            isJump = true;
        }
        if (isJump)
        {
            Jump -= Time.deltaTime;
            transform.position = transform.position + new Vector3(0, Jump * Time.deltaTime, 0);
        }

    }

    public bool StopJump()
    {
        if (Jump > 0)
            return false;

        PlayAnimation(AnimationArray[2], true);
        isJump = false;
        return true;

    }
}
