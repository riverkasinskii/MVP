using System;
using Modules.Utils;
using UnityEngine;
using Zenject;

namespace Modules.Planets
{
    public sealed class Planet : IFixedTickable, IPlanet
    {
        public const int UNDEFINED_PRICE = -1;
        public const int UNDEFINED_LEVEL = -1;

        public event Action OnUnlocked;
        public event Action<bool> OnIncomeReady;
        public event Action<int> OnGathered;
        public event Action<int> OnUpgraded;
        public event Action<int> OnPopulationChanged;
        public event Action<int> OnIncomeChanged;
        public event Action<float> OnIncomeTimeChanged;
                
        public string Name => _config.Name;
                
        public bool CanUpgrade => IsUnlocked && !IsMaxLevel && _moneyAdapter.IsEnough(Price);
                
        public bool CanUnlock => !IsUnlocked && _moneyAdapter.IsEnough(Price);
                
        public bool CanUnlockOrUpgrade => !IsUnlocked ? this.CanUnlock : CanUpgrade;
                
        public bool IsUnlocked { get; internal set; }
                
        public int Price => !IsUnlocked
            ? _config.PurchasePrice
            : !IsMaxLevel
                ? _config.GetUpgradePrice(Level + 1)
                : UNDEFINED_PRICE;
                
        public int Population { get; internal set; }
                
        public bool IsMaxLevel => Level == _config.MaxLevel;
                
        public int Level { get; internal set; }
                
        public int MaxLevel => _config.MaxLevel;
                
        public int NextLevel => !this.IsMaxLevel ? Level + 1 : UNDEFINED_LEVEL;
                
        public float IncomeProgress => 1 - _countdown.RemainingTime / _countdown.Duration;
                
        public bool IsIncomeReady { get; private set; }
                
        public int MinuteIncome => !IsUnlocked ? 0 : _config.GetIncome(Level);
                
        public int NextMinuteIncome => !IsUnlocked
            ? 0
            : !this.IsMaxLevel
                ? _config.GetIncome(Level + 1)
                : 0;

        private readonly PlanetConfig _config;
        private readonly Countdown _countdown;
        private readonly IMoneyAdapter _moneyAdapter;
        private float _populationTime;

        public Planet(PlanetConfig config, IMoneyAdapter moneyAdapter)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _moneyAdapter = moneyAdapter ?? throw new ArgumentNullException(nameof(moneyAdapter));
            _config = config;
            _countdown = new Countdown(config.IncomeDuration);
        }
                
        public bool UnlockOrUpgrade()
        {
            return !IsUnlocked
                ? Unlock()
                : Upgrade();
        }
       
        public bool Unlock()
        {
            if (!CanUnlock)
                return false;

            _moneyAdapter.Spend(Price);

            Level = 1;
            IsUnlocked = true;
            OnUnlocked?.Invoke();
            return true;
        }
               
        public bool Upgrade()
        {
            if (!CanUpgrade)
                return false;

            _moneyAdapter.Spend(Price);

            Level++;

            OnUpgraded?.Invoke(Level);
            OnIncomeChanged?.Invoke(MinuteIncome);
            return true;
        }
                
        public bool GatherIncome()
        {
            if (!IsUnlocked)
                return false;

            if (!IsIncomeReady)
                return false;

            int income = MinuteIncome;
            _moneyAdapter.Earn(income);
            OnGathered?.Invoke(income);

            _countdown.Reset();
            OnIncomeTimeChanged?.Invoke(_countdown.RemainingTime);

            IsIncomeReady = false;
            OnIncomeReady?.Invoke(IsIncomeReady);

            return true;
        }

        public Sprite GetIcon(bool unlocked)
        {
            return _config.GetIcon(unlocked);
        }

        void IFixedTickable.FixedTick()
        {
            if (!IsUnlocked)
                return;

            float deltaTime = Time.fixedDeltaTime;
            this.UpdateIncome(deltaTime);
            this.UpdatePopulation(deltaTime);
        }

        private void UpdatePopulation(float deltaTime)
        {
            _populationTime += deltaTime;
            if (_populationTime < _config.PopulationPeriod)
                return;

            _populationTime -= _config.PopulationPeriod;
            
            Population += Level;
            OnPopulationChanged?.Invoke(Population);
        }

        private void UpdateIncome(float deltaTime)
        {
            if (IsIncomeReady)
                return;

            if (_countdown.IsPlaying())
            {
                _countdown.Tick(deltaTime);
                OnIncomeTimeChanged?.Invoke(_countdown.RemainingTime);
                return;
            }

            IsIncomeReady = true;
            OnIncomeReady?.Invoke(IsIncomeReady);
        }
    }
}