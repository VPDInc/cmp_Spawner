using System;
using UnityEngine;
using System.Collections.Generic;

using Random = UnityEngine.Random;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.Sequence
{
    public class ElementGetterBySequence<TElement>
    {
        #region Fields
        public readonly SequenceType Sequence;
        private readonly TElement[] _elements;

        private int _index;
        #endregion

        #region Properties
        public IReadOnlyList<TElement> Elements => _elements;

        public int Index
        {
            get
            {
#if UNITY_EDITOR
                if (Sequence != SequenceType.Queue)
                    Debug.LogWarning($"With this Sequence ({Sequence}), the index does not matter");
#endif
                return _index;
            }
            private set => _index = value;
        }
        #endregion

        public ElementGetterBySequence(SequenceType sequence, params TElement[] elements)
        {
            Sequence = sequence;
            _elements = elements;
        }
        
        public TElement Get()
        {
            switch (Sequence)
            {
                case SequenceType.Queue:
                    if (Index >= _elements.Length) Index = 0;
                    return _elements[Index++];
                
                case SequenceType.Random: return _elements[Random.Range(0, _elements.Length)];
                
                default: throw new ArgumentOutOfRangeException(nameof(Sequence), Sequence, null);
            }
        }
    }
}
