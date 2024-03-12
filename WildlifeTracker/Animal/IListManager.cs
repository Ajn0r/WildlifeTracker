using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker
{
    /// <summary>
    /// Interface for list manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IListManager <T>
    {
        #region Properties
        int Count { get; }
        #endregion

        #region Methods
        bool Add(T type);
        bool ChangeAt(int index, T type);
        bool CheckIndex(int index);
        bool DeleteAt(int index);
        void DeleteAll();
        T GetAt(int index);
        string[] ToStringArray();
        List<string> ToStringList();
        #endregion
    }
}
