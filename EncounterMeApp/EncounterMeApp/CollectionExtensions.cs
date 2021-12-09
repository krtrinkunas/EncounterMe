using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp
{
    public static class CollectionExtensions
    {
        public static void Sort<T>(this ObservableRangeCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);
            sortableList.Reverse();

            for (int i = 0; i < sortableList.Count; ++i)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
    }
}
