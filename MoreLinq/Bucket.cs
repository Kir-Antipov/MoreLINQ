#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2020 Kir_Antipov. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace MoreLinq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a subsequence of objects generated by a user predicate.
    /// </summary>
    /// <typeparam name="TElement">The type of the values in the <see cref="IBucket{TElement}"/>.</typeparam>

    public interface IBucket<TElement> : IList<TElement> { }

    internal class Bucket<TElement> : IBucket<TElement>
    {
        public TElement[] Elements = new TElement[1];
        public int Count = 0;

        int ICollection<TElement>.Count => Count;

        bool ICollection<TElement>.IsReadOnly => true;

        public void Add(TElement element)
        {
            if (Elements.Length == Count)
                Array.Resize(ref Elements, checked(Count << 1));

            Elements[Count++] = element;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            for (var i = 0; i < Count; ++i)
                yield return Elements[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<TElement>.Contains(TElement item)
        {
            for (var i = 0; i < Count; ++i)
                if (Equals(Elements[i], item))
                    return true;

            return false;
        }

        void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex) => Array.Copy(Elements, 0, array, arrayIndex, Count);

        int IList<TElement>.IndexOf(TElement item) => Array.IndexOf(Elements, item, 0, Count);

        TElement IList<TElement>.this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return Elements[index];
            }

            set => throw new NotSupportedException();
        }

        void ICollection<TElement>.Add(TElement item) => throw new NotSupportedException();

        void ICollection<TElement>.Clear() => throw new NotSupportedException();

        bool ICollection<TElement>.Remove(TElement item) => throw new NotSupportedException();

        void IList<TElement>.Insert(int index, TElement item) => throw new NotSupportedException();

        void IList<TElement>.RemoveAt(int index) => throw new NotSupportedException();
    }
}
