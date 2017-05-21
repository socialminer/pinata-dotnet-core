using System.Collections.Generic;

namespace PinataCore.Data
{
    public interface IPinataCoreRepository
    {
        bool Insert(IList<object> list);

        bool Update(IList<object> list);

        bool Delete(IList<object> list);
    }
}
