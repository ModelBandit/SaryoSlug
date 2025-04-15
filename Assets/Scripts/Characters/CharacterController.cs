using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterController : MonoBehaviour
{
    public enum State
    {
        Idle,       // Idle, F/U Aim_Shoot, Move
        Jump,       // Jump, F/U/D Aim_Shoot, 
        Crouch,    // Crouch, FC Shoot
        Dead
    }
    State m_State;
    public State CharacterState
    {
        get => m_State;
        set => m_State = value;
    }
    //shoot if is Idle or Jump


    [SerializeField] public SkeletonAnimation SkeletonAnim;
    [SerializeField] BoxCollider2D m_BodyCollider;
    public BoxCollider2D BodyCollider
    {
        get => m_BodyCollider;
    }
    [SerializeField] BoxCollider2D m_FootCollider;
    public BoxCollider2D FootCollider
    {
        get => m_FootCollider;
    }

    const float JumpPower = 1;
    float Jump = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpineBlendTimeSetting();
        m_State = State.Idle;

        //GameManager.Instance.AddUpdate(ObjectUpdate());
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlaying(1))
        {
            AnimationOverrideClear(1);
        }

        if (m_State == State.Jump)
        {
            Jump -= Time.deltaTime;
            transform.position = transform.position + new Vector3(0, Jump * Time.deltaTime, 0);
        }
    }


    ///PlayAnimation
    public Spine.TrackEntry PlayAnimation(string AnimationName, bool isLoop)
    {
        return SkeletonAnim.state.SetAnimation(0, AnimationName, isLoop);
    }
    public Spine.TrackEntry PlayAnimation(Spine.Animation AnimationName, bool isLoop)
    {
        return SkeletonAnim.state.SetAnimation(0, AnimationName, isLoop);
    }

    ///Index PlayAnimation
    public Spine.TrackEntry PlayAnimation(int index, string AnimationName, bool isLoop)
    {
        return SkeletonAnim.state.SetAnimation(index, AnimationName, isLoop);
    }

    public Spine.TrackEntry PlayAnimation(int index, Spine.Animation AnimationName, bool isLoop)
    {
        ClearTrack(index);
        SkeletonAnim.state.ClearTrack(index);

        return SkeletonAnim.state.SetAnimation(index, AnimationName, isLoop);
    }

    Coroutine[] CleanerArr = { null, null, null };
    /// erase animation in delay
    public void AnimationOverrideClear(int index)
    {
        string mainTrackName = SkeletonAnim.state.GetCurrent(0).Animation.Name;
        string indexTrackName;

        Spine.TrackEntry subTrack = SkeletonAnim.state.GetCurrent(index);
        if (subTrack == null)
            return;

        indexTrackName = indexTrackName = SkeletonAnim.state.GetCurrent(index).Animation.Name;
        

        Spine.TrackEntry track = PlayAnimation(index, SkeletonAnim.state.GetCurrent(0).Animation.Name, true);
        track.TrackTime = SkeletonAnim.state.GetCurrent(0).TrackTime;
        CleanerArr[index] = StartCoroutine(DelayClearing(index, track.AnimationEnd));
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

    private IEnumerator DelayClearing(int index, float EndTime)
    {
        float t = 0f;

        while (t < EndTime)
        {
            t += GameManager.Instance.deltaTime;
            yield return null;
        }

        SkeletonAnim.state.ClearTrack(index);
        yield break;
    }

    private void SpineBlendTimeSetting()
    {
        SkeletonAnim.state.Data.DefaultMix = 0.1f;
        //¸®ÆÑÅä¸µ
        //for (int i = 2; i < 8; ++i)
        //{
        //    SkeletonAnim.state.Data.SetMix(AnimationArray[i], AnimationArray[11], 0f);
        //}
        //SkeletonAnim.state.Data.SetMix(ShootHG, Idle, 0.2f);
        //SkeletonAnim.state.Data.SetMix(ShootHG, Move, 0.2f);
        //SkeletonAnim.state.Data.SetMix(Move, Idle, 0.2f);
        //SkeletonAnim.state.Data.SetMix(Idle, Move, 0.2f);
    }

    public void ClearTrack(int index)
    {
        if (CleanerArr[index] != null &&
            SkeletonAnim.state.GetCurrent(index) != null)
        {
            StopCoroutine(CleanerArr[index]);
            Spine.TrackEntry track = PlayAnimation(SkeletonAnim.state.GetCurrent(0).Animation.Name, true);
            track.TrackTime = SkeletonAnim.state.GetCurrent(0).TrackTime;
            SkeletonAnim.state.ClearTrack(index);
        }
    }
    public void TryJump()
    {
        ClearTrack(2);

        Jump = JumpPower;
        m_State = State.Jump;
        PlayAnimation(AnimationList.Jump_Idle, false);
        PlayAnimation(2, AnimationList.Flutter, true);
    }
    public bool StopJump()
    {
        if (Jump > 0 || m_State != State.Jump)
            return false;

        Spine.TrackEntry track = PlayAnimation(AnimationList.Idle_Front, true);
        m_State = State.Idle;
        PlayAnimation(2, AnimationList.Idle_Front, true);
        SkeletonAnim.state.ClearTrack(2);
        return true;

    }
}
