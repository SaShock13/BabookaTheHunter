using UnityEngine;

public class OnGroundState : State
{
    private InputPlayer _input;
    private Player _player;

    public OnGroundState(StateMachine stateMachine,Player player, InputPlayer input) : base(stateMachine)
    {
        _input = input;
        _player = player;
    }

    public override void Enter()
    {
        InputPlayer.OnTorchPressedEvent += _player.GetTorch;
        InputPlayer.OnAttackEvent += _player.Attack;
        InputPlayer.OnJumpEvent += _player.Jump;
        InputPlayer.OnMoveEvent += _player.MovePlayer;
        InputPlayer.OnInteractEvent += _player.Interact;
        //Debug.Log("Onground State Enter");
    }

    public override void Exit()
    {
        //Debug.Log("Onground State Exit");
        InputPlayer.OnTorchPressedEvent -= _player.GetTorch;
        InputPlayer.OnAttackEvent -= _player.Attack;
        InputPlayer.OnJumpEvent -= _player.Jump;
        InputPlayer.OnMoveEvent -= _player.MovePlayer;
        InputPlayer.OnInteractEvent -= _player.Interact;
    }

    public override void Update()
    {
        _player.LookCamera();
        //Debug.Log("Onground State Update");
    }
}
