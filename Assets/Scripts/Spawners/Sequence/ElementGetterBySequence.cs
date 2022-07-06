using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Sequence
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
        
        public int Index { get; private set; }
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
