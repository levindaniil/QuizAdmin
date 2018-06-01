using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizAdmin.Logic.Model;

namespace QuizAdmin.Logic.Repository
{
   public interface IRepository<T>
    {
        IEnumerable<T> Data { get; }
        IEnumerable<T> FindAll(Predicate<T> predicate);
        T AddItem(T item);
        void RemoveItem(T item);
        T EditItem(T item, object id);
        Action<T> ItemAdded { get; set; }

    }
}
