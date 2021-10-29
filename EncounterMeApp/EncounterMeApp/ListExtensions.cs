using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp
{
    public static class ListExtensions
    {
        public static void SortDesc<T>(this List<T> list)
        {
            list.Sort();
            list.Reverse();
        }
    }
}
