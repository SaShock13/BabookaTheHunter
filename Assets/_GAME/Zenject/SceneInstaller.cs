using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerInteractor playerInteractor;
    [SerializeField] private InputPlayer input;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    [SerializeField] private Movement2_5_D movement2d;
    [SerializeField] private Movement3_D movement3d;



    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle();
        Container.Bind<InputPlayer>().FromInstance(input).AsSingle();
        Container.Bind<PlayerStateMachine>().FromInstance(playerStateMachine).AsSingle();
        Container.Bind<PlayerInteractor>().FromInstance(playerInteractor).AsSingle();
        Container.Bind<IMovable>().FromInstance(movement2d).AsSingle();


    }
}