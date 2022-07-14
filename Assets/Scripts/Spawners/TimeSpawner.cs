using System;
using UnityEngine;
using System.Collections;

using Random = UnityEngine.Random;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners
{
    [AddComponentMenu("Spawners/Spawners/Time Spawner")]
    public sealed class TimeSpawner : TimeSpawner<Component> { }

    public abstract class TimeSpawner<TComponent> : MonoBehaviour where TComponent : Component
    {
        #region Inspector fields
        [Header("Controllers")]
        [SerializeField] private bool _isPlayOnAwake;
        [SerializeField] private Spawner<TComponent> _spawner;

        [Header("Parameters")]
        [SerializeField] [Min(0)] private float _minStartTime;
        [SerializeField] [Min(0)] private float _maxStartTime;
        [SerializeField] [Min(0)] private float _minTime;
        [SerializeField] [Min(0)] private float _maxTime;
        #endregion

        private IEnumerator _spawning;

        #region Properties
        private bool IsSpawning
        {
            get => _spawning != null;
            set
            {
                if (value == IsSpawning) return;

                if (!value)
                {
                    StopCoroutine(_spawning);
                    _spawning = null;
                }
                else
                {
                    _spawning = Spawning();
                    StartCoroutine(_spawning);
                }
            }
        }

        public float MinStartTime
        {
            get => MinStartTime;
            protected set
            {
                if (value < 0) throw new ArgumentException($"Min start time can't be less than 0; Min start time = {value}");
                if (value > MaxStartTime) throw new AggregateException($"Min start time can't be more than max start time. Min start time = {value} Max start time = {MaxStartTime}");
                MinStartTime = value;
            }
        }

        public float MaxStartTime
        {
            get => MaxStartTime;
            protected set
            {
                if (value < 0) throw new ArgumentException($"Max start time can't be less than 0; Max start time = {value}");
                if (value < MinStartTime) throw new AggregateException($"Max start time can't be less than min start time. Max start time = {value} Min start time = {MinStartTime}");
                MaxStartTime = value;
            }
        }
        
        public float MinTime
        {
            get => _minTime;
            protected set
            {
                if (value < 0) throw new ArgumentException($"Min time can't be less than 0; Min time = {value}");
                if (value > MaxTime) throw new AggregateException($"Min time can't be more than max time. MinTime = {value} Max time = {MaxTime}");
                _minTime = value;
            }
        }
        
        public float MaxTime
        {
            get => _maxTime;
            protected set
            {
                if (value < 0) throw new ArgumentException($"Max time can't be less than 0; Max time = {value}");
                if (value < MinTime) throw new AggregateException($"Max time can't be less than min time. Max time = {value} Min time = {MinTime}");
                _maxTime = value;
            }
        }

        public ISpawnable<TComponent> Spawner => _spawner;
        #endregion

        #region Unity functions
        private void OnValidate()
        {
            if (_maxStartTime < _minStartTime)
                _maxStartTime = _minStartTime;
            
            if (_maxTime < _minTime)
                _maxTime = _minTime;
        }

        private void Awake()
        {
            if (_isPlayOnAwake) StartSpawner();
        }
        #endregion

        #region Spawn functions
        public void StartSpawner()
        {
            if (IsSpawning)
            {
                Debug.LogWarning("You can't start a running spawner");
                return;
            }

            IsSpawning = true;
        }

        public void StopSpawner()
        {
            if (!IsSpawning)
            {
                Debug.LogWarning("You can't stop a stopped spawner");
                return;
            }

            IsSpawning = false;
        }

        private IEnumerator Spawning()
        {
            var startTime = Random.Range(_minStartTime, _maxStartTime);
            yield return new WaitForSeconds(startTime);
            
            while (IsSpawning)
            {
                _spawner.Spawn();
                
                var time = Random.Range(_minTime, _maxTime);
                yield return new WaitForSeconds(time);
            }
        }
        #endregion
    }
}
