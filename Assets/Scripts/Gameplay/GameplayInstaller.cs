using Game.Gameplay;
using Modules.Planets;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public sealed class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private int _initialMoney = 300;

        [SerializeField]
        private PlanetCatalog _catalog;

        [SerializeField]
        private MoneyView _moneyView;

        [SerializeField]
        private PlanetView[] _planetView;

        [SerializeField]
        private ParticleAnimator _particleAnimator;

        public override void InstallBindings()
        {
            PlanetPopupInstaller.Install(this.Container);
            MoneyInstaller.Install(this.Container, _initialMoney, _moneyView);
            PlanetInstaller.Install(this.Container, _catalog, _planetView);
            ParticleAnimatorInstaller.Install(this.Container, _particleAnimator);
        }
    }
}