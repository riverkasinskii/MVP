using Modules.UI;
using Zenject;

public sealed class ParticleAnimatorInstaller : Installer<ParticleAnimator, ParticleAnimatorInstaller>
{
    private readonly ParticleAnimator _particleAnimator;

    public ParticleAnimatorInstaller(ParticleAnimator particleAnimator)
    {
        _particleAnimator = particleAnimator;
    }

    public override void InstallBindings()
    {
        IMoneyPresenter moneyPresenter = Container.Resolve<IMoneyPresenter>();

        this.Container
            .BindInterfacesAndSelfTo<ParticleAnimatorShower>()
            .AsSingle()
            .WithArguments(_particleAnimator, moneyPresenter)
            .NonLazy();
    }
}
