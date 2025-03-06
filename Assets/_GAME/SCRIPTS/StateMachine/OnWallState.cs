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
        //Debug.Log("OnWall State Enter");
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
    }
}
