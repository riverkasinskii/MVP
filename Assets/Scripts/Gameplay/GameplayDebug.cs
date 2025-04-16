using Modules.Money;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public sealed class GameplayDebug : MonoBehaviour
    {
        [Inject]        
        private readonly Planet[] _planets;

        [Inject]        
        private readonly MoneyStorage _moneyStorage;
    }
}