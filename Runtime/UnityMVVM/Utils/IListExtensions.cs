using System;
using System.Collections.Generic;


namespace System.Linq {
    /// <summary>
    /// These extension methods are in the same convention as System.Linq and can be viewed as an
    /// addition to those methods, so they are placed in the System.Linq namespace
    /// </summary>
    public static class IListExtensions {

        public static T GetElementAt<T>(this IList<T> _list, int _index) {
            if(_list == null || _list.Count == 0 || _index >= _list.Count || _index < 0) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogWarning("[ERR] Can't access index " + _index + " for list " + _list + (_list == null ? "" : " (size = " + _list.Count + ")"));
#endif
                return default(T);
            }
            return _list[_index];
        }
        


        public static T NextOrFirst<T>(this IList<T> list, T current) {
            int index = list.IndexOf(current);
            if(index == -1) {
                throw new IndexOutOfRangeException("Can't found the object " + current + " inside list: " + list);
            }
            return list.NextOrFirst(index);
        }
        public static T PreviousOrLast<T>(this IList<T> list, T current) {
            int index = list.IndexOf(current);
            if(index == -1) {
                throw new IndexOutOfRangeException("Can't found the object " + current + " inside list: " + list);
            }
            return list.PreviousOrLast(index);
        }

        public static T NextOrNull<T>(this IList<T> list, T current) {
            int index = list.IndexOf(current);
            return list.HasNext(current) ? list[(index + 1)] : default(T);
        }

        public static T PreviousOrNull<T>(this IList<T> list, T current) {
            int index = list.IndexOf(current);
            return list.HasPrevious(current) ? list[(index - 1)] : default(T);
        }


        public static T NextOrFirst<T>(this IList<T> list, int index) {
            int listCount = list.Count;
            if(index < 0 || index > (listCount - 1)) {
                throw new IndexOutOfRangeException("Index is outside list range: " + list);
            }
            return list[(index + 1) % listCount];
        }

        public static T PreviousOrLast<T>(this IList<T> list, int index) {
            int listCount = list.Count;
            if(index < 0 || index > (listCount - 1)) {
                throw new IndexOutOfRangeException("Index is outside list range: " + list);
            }
            return list[(index + listCount - 1) % listCount];
        }



        public static bool HasNext<T>(this IList<T> list, T current) {
            int index = list.IndexOf(current);
            if(index == -1) {
                return false;
            }
            return index + 1 < list.Count;
        }

        public static bool HasPrevious<T>(this IList<T> list, T current) {
            int index = list.IndexOf(current);
            if(index == -1) {
                return false;
            }
            return index - 1 >= 0;
        }

        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> ts) {
            var count = ts.Count;
            var last = count - 1;
            for(var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }
}

public static class ListExtensions {

    private static Random random = new Random();

    public static T GetElementAt<T>(this List<T> _list, int _index) {
        if(_list == null || _list.Count == 0 || _index >= _list.Count || _index < 0) {
#if UNITY_EDITOR
            UnityEngine.Debug.LogWarning("[ERR] Can't access index " + _index + " for list " + _list + (_list == null ? "" : " (size = " + _list.Count + ")"));
#endif
            return default(T);
        }
        return _list[_index];
    }

    public static T GetElementAt<T>(this T[] _array, int _index) {
        if(_array == null || _array.Length == 0 || _index >= _array.Length || _index < 0) {
#if UNITY_EDITOR
            UnityEngine.Debug.LogWarning("[ERR] Can't access index " + _index + " for built-in array " + _array + (_array == null ? "" : " (size = " + _array.Length + ")"));
#endif
            return default(T);
        }
        return _array[_index];
    }


    public static T Random<T>(this List<T> list) {
        if(list.Count == 0) {
            throw new IndexOutOfRangeException("Cannot take random element from an empty list");
        }
        return list[random.Next(list.Count)];
    }

    public static T Random<T>(this T[] list) {
        if(list.Length == 0) {
            throw new IndexOutOfRangeException("Cannot take random element from an empty list");
        }
        return list[random.Next(list.Length)];
    }
}