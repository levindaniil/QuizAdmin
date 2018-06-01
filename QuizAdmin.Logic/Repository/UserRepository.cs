using QuizAdmin.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository()
        {
            using (var context = new Context())
            {
                _items = context.Users.ToList();
            }
        }

        public override User AddItem(User item)
        {
            using (var context = new Context())
            {
                context.Set<User>().Add(item);
                context.SaveChanges();
            }

            _items.Add(item);
            ItemAdded?.Invoke(item);
            return item;
        }

        public override User EditItem(User item, object id)
        {
            throw new NotImplementedException();
        }

        public override void RemoveItem(User item)
        {
            base.RemoveItem(item);
        }
    }
}
