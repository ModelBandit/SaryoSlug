using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Monosingleton<InputManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InputUpdate()
    {
        switch (GameManager.Instance.Player.CharacterState)
        {
            case CharacterController.State.Idle:
                InputIdle();
                break;
            case CharacterController.State.Jump:
                InputJump();
                break;
            case CharacterController.State.Crouch:
                InputCrouch();
                break;
            case CharacterController.State.Dead:
            default:
                break;
        }
        if (Input.anyKeyDown)
            GameManager.Instance.Player.SkeletonAnim.skeleton.SetToSetupPose();
    }
    private void InputIdle()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.Instance.Player.PlayAnimation(1, AnimationList.Aim_Up, true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            GameManager.Instance.Player.ClearTrack(1);
            GameManager.Instance.Player.AnimationOverrideClear(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.Instance.Player.SkeletonAnim.skeleton.ScaleX = 1f;
            GameManager.Instance.Player.PlayAnimation(AnimationList.Move_Front, true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.Instance.Player.CharacterState = CharacterController.State.Crouch;
            GameManager.Instance.Player.PlayAnimation(AnimationList.Crouch, true);
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.Instance.Player.SkeletonAnim.skeleton.ScaleX = -1f;
            GameManager.Instance.Player.PlayAnimation(AnimationList.Move_Front, true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            GameManager.Instance.Player.PlayAnimation(AnimationList.Idle_Front, true);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameManager.Instance.Player.ClearTrack(1);
            GameManager.Instance.Player.PlayAnimation(1, AnimationList.ShootHG_Front, false);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            GameManager.Instance.Player.CharacterState = CharacterController.State.Jump;
            GameManager.Instance.Player.TryJump();
        }
    }

    private void InputJump()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.Instance.Player.ClearTrack(1);
            GameManager.Instance.Player.PlayAnimation(1, AnimationList.Aim_Down, true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.Instance.Player.ClearTrack(1);
            GameManager.Instance.Player.PlayAnimation(1, AnimationList.Aim_Up, true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            GameManager.Instance.Player.AnimationOverrideClear(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(Input.GetKey(KeyCode.DownArrow))
            {
                GameManager.Instance.Player.ClearTrack(1);
                GameManager.Instance.Player.PlayAnimation(1, AnimationList.ShootHG_Down, false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                GameManager.Instance.Player.ClearTrack(1);
                GameManager.Instance.Player.PlayAnimation(1, AnimationList.ShootHG_Up, false);
            }
            else
            {
                GameManager.Instance.Player.ClearTrack(1);
                GameManager.Instance.Player.PlayAnimation(1, AnimationList.ShootHG_Front, false);
            }
        }
    }
    private void InputCrouch()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.Instance.Player.PlayAnimation(AnimationList.Idle_Front, true);
            GameManager.Instance.Player.ClearTrack(1);
            GameManager.Instance.Player.PlayAnimation(1, AnimationList.Aim_Up, true);
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            GameManager.Instance.Player.CharacterState = CharacterController.State.Idle;
            GameManager.Instance.Player.PlayAnimation(AnimationList.Idle_Front, true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.Instance.Player.SkeletonAnim.skeleton.ScaleX = 1f;
            //crouch move
            //GameManager.Instance.Player.PlayAnimation(AnimationList.Move_Front, true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.Instance.Player.SkeletonAnim.skeleton.ScaleX = -1f;
            //courch move
            //GameManager.Instance.Player.PlayAnimation(AnimationList.Move_Front, true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            GameManager.Instance.Player.PlayAnimation(AnimationList.Crouch, true);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameManager.Instance.Player.ClearTrack(1);
            GameManager.Instance.Player.PlayAnimation(1, AnimationList.ShootHG_Crouch, false);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            GameManager.Instance.Player.TryJump();
        }

    }

}
