using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizAdmin.Logic.Model;

namespace QuizAdmin.Logic.Repository
{
    public abstract class Repository<T> : IRepository<T>
    {
        public Action<T> ItemAdded { get; set; }

        protected List<T> _items;

        public IEnumerable<T> Data => _items ?? (_items = new List<T>());

        public IEnumerable<T> FindAll(Predicate<T> predicate)
        {
            return _items.FindAll(predicate);
        }

        public virtual T AddItem(T item)
        {
            _items.Add(item);
            return item;
        }

        public virtual void RemoveItem(T item)
        {
            _items.Remove(item);
        }

        public abstract T EditItem(Object id, T item);
    }
}
