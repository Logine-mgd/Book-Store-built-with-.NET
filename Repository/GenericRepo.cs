using Project1.Models;

namespace Project1.Repository
{
    public class GenericRepo<T> where T : class
    {
            public BookStoreContext db;
            public GenericRepo(BookStoreContext _db)
            {
                db = _db;
            }

            public List<T> SelectAll()
            {
                return db.Set<T>().ToList();
            }

            public T SelectbyId(int id)
            {
                return db.Set<T>().Find(id);
            }

            public void Add(T ent)
            {
                db.Set<T>().Add(ent);
            }
            public void Edit(T ent)
            {
                db.Entry(ent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            public void Delete(int id)
            {
                db.Set<T>().Remove(SelectbyId(id));
            }

            public void Save()
            {
                db.SaveChanges();
            }

        }
    }
