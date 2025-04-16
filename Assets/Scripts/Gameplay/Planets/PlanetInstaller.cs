using Modules.Planets;
using System.Collections.Generic;
using Zenject;

namespace Game.Gameplay
{
    public sealed class PlanetInstaller : Installer<PlanetCatalog, PlanetView[], PlanetInstaller>
    {
        [Inject]
        private readonly PlanetCatalog _catalog;

        [Inject]
        private readonly PlanetView[] planetView;

        public override void InstallBindings()
        {
            foreach (PlanetConfig config in _catalog)
            {
                this.Container
                    .BindInterfacesAndSelfTo<Planet>()
                    .AsCached()
                    .WithArguments(config)
                    .NonLazy();
            }

            List<IPlanet> planet = Container.ResolveAll<IPlanet>();
            for (int i = 0; i < planet.Count; i++)
            {
                this.Container
                    .BindInterfacesTo<PlanetPresenter>()
                    .AsCached()
                    .WithArguments(planet[i], planetView[i])
                    .NonLazy();
            }
        }
    }
}