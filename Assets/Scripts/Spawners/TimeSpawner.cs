using UnityEngine;
using System.Collections;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners
{
    [AddComponentMenu("Spawners/Spawners/Time Spawner")]
    public sealed class TimeSpawner : TimeSpawner<Component> { }

    public abstract class TimeSpawner<TComponent> : MonoBehaviour where TComponent : Component
    {
        #region Inspector fields
        [SerializeField] private bool _isPlayOnAwake;
        [SerializeField] private Spawner<TComponent> _spawner;

        [Header("Parameters")]
        [SerializeField] [Min(0)] private float _startTime;
        [SerializeField] [Min(0)] private float _minTime;
        [SerializeField] [Min(0)] private float _maxTime;
        #endregion

        private IEnumerator _spawning;

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

        #region Unity functions
        private void OnValidate()
        {
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
                Debug.LogWarning("You cannot start a running spawner");
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
            yield return new WaitForSeconds(_startTime);
            
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
