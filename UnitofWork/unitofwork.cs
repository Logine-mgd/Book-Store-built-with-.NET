using Project1.Models;
using Project1.Repository;

namespace Project1.UnitofWork
{
    public class unitofwork
    {
        BookStoreContext db;
        public unitofwork(BookStoreContext _db)
        {
            db = _db;
        }

        GenericRepo<Order> rep_order;
        GenericRepo<Order_Items> rep_orderitems;
        GenericRepo<Catalog> rep_catalog;
        GenericRepo<Book> rep_books;
        GenericRepo<Author> rep_authors;

        public GenericRepo<Order> Rep_order
        {
            get
            {
                if (rep_order == null)
                    rep_order = new GenericRepo<Order>(db);

                return rep_order;
            }
        }

        public GenericRepo<Order_Items> Rep_orderitems
        {
            get
            {
                if (rep_orderitems == null)
                    rep_orderitems = new GenericRepo<Order_Items>(db);

                return rep_orderitems;
            }
        }

        public GenericRepo<Book> Rep_books
        {
            get
            {
                if (rep_books == null)
                    rep_books = new GenericRepo<Book>(db);

                return rep_books;
            }
        }

        public GenericRepo<Catalog> Rep_catalog
        {
            get
            {
                if (rep_catalog == null)
                    rep_catalog = new GenericRepo<Catalog>(db);

                return rep_catalog;
            }
        }
        public GenericRepo<Author> Rep_authors
        {
            get
            {
                if (rep_authors == null)
                    rep_authors = new GenericRepo<Author>(db);

                return rep_authors;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

    }
}
