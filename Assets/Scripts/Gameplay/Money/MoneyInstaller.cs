using Modules.Money;
using Zenject;

namespace Game.Gameplay
{
    public sealed class MoneyInstaller : Installer<int, MoneyView, MoneyInstaller>
    {
        [Inject]
        private readonly int _initialMoney;

        [Inject]
        private readonly MoneyView _moneyView;

        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<MoneyStorage>()
                .AsSingle()
                .WithArguments(_initialMoney)
                .NonLazy();

            this.Container
                .BindInterfacesTo<MoneyAdapter>()
                .AsSingle()
                .NonLazy();

            this.Container
                .BindInterfacesTo<MoneyPresenter>()
                .AsSingle()
                .WithArguments(_moneyView)
                .NonLazy();
        }
    }
}