# Book Store API
Book Store is a backend system designed to manage the operations of a bookstore. The API supports two primary types of users: Admins and Customers, offering tailored functionalities for each role.

## Key Features
**Authentication & Authorization:**

Implemented with **JWT (JSON Web Token)** to ensure secure access. Role-based access control is provided for both Admin and Customer users.

## Identity Integration:
Built on ASP.NET Identity, enabling seamless user account and role management.

## Database Models:
**Customer:** Represents bookstore customers.

**Admin:** Represents administrative users.

**Catalog:** Categories of books.

**Author:** Information about book authors.

**Book:** Details of books available for sale.

**Order:** Tracks customer orders.

**Order_Items:** Links orders to books using a composite key (Order_id, Book_id).
