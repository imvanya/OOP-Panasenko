using System.Collections.Generic;

namespace Lab10v18
{
    public interface IDataSource<T>
    {
        IEnumerable<T> GetData();
    }
}