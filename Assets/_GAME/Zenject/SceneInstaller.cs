using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InputPlayer input;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle();
        Container.Bind<InputPlayer>().FromInstance(input).AsSingle();

    }
}