using System.Collections;
using UnityEngine;

public class OnWallState : State
{
    private Player _player;
    private Animator _animator;
    public OnWallState(StateMachine stateMachine, Player player,Animator animator) : base(stateMachine)
    {
        _player = player;
        _animator = animator;
    }
    public override void Enter()
    {
        _player.isGravityActive = false;
        _player.StopTryHang();
        _animator.SetBool("Hanging", true);
        _animator.SetFloat("Speed", 0);
    }

    public override void Exit()
    {
        _player.isGravityActive = true;
        _animator.SetBool("Hanging", false);
        _player.GetComponent<Hanging>().isOnLedge = false;
        //Debug.Log("OnWall State Exit");
    }

    public override void Update()
    {
        //Debug.Log("OnWall State Update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SetState<OnGroundState>();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            Debug.Log($"Climb {this}");
            _animator.SetTrigger("Climb");
            var newPosition = _player.transform.position + _player.transform.forward;
            newPosition.y += 2.4f;
            _player.Climb(newPosition);
            //stateMachine.SetState<OnGroundState>();
            
        }
    }

    
}
