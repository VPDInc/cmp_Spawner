using System;
using System.Collections.Generic;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace Pool.Core
{
    public class ObjectPool<T> : IDisposable where T : class
    {
        #region Fields
        private readonly Queue<T> _queue;

        private readonly Func<T> _createFunc;
        private readonly Action<T> _actionOnGet;
        private readonly Action<T> _actionOnRelease;
        private readonly Action<T> _actionOnDestroy;

        private readonly int _maxSize;

        private readonly bool _collectionCheck;
        #endregion

        #region Properties
        public int CountAll { get; private set; }
        public int CountInactive => _queue.Count;
        public int CountActive => CountAll - CountInactive;
        #endregion

        public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null,
            bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            if (maxSize <= 0)
                throw new ArgumentException("Max Size must be greater than 0", nameof (maxSize));
            
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _queue = new Queue<T>(defaultCapacity);
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;

            _collectionCheck = collectionCheck;
            _maxSize = maxSize;
        }

        public T Get()
        {
            T obj;
            if (_queue.Count == 0)
            {
                obj = _createFunc();
                ++CountAll;
            }
            else obj = _queue.Dequeue();
            
            _actionOnGet?.Invoke(obj);
            return obj;
        }

        public void Release(T element)
        {
            if (_collectionCheck && _queue.Count > 0 && _queue.Contains(element))
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
            
            _actionOnRelease?.Invoke(element);

            if (CountInactive < _maxSize) _queue.Enqueue(element);
            else _actionOnDestroy?.Invoke(element);
        }
        
        public void Clear()
        {
            if (_actionOnDestroy != null)
                foreach (var obj in _queue)
                    _actionOnDestroy(obj);
            
            _queue.Clear();
            CountAll = 0;
        }

        public void Dispose() => Clear();
    }
}
