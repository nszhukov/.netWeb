using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Ecommerce.Models;
using Microsoft.Data.Sqlite;

namespace Ecommerce.Services
{
    public class SQLiteConnectorService
    {
        private SqliteConnectionStringBuilder connectionStringBuilder;
        private SqliteConnection connection;

        public List<Account> Accounts;
        public List<Category> Categories;
        public List<Invoice> Invoices;
        public List<InvoiceDetails> InvoiceDetails;
        public List<Product> Products;
        public List<Role> Roles;
        public List<RoleAccount> RoleAccounts;
        public List<SlideShow> SlideShows;
        public List<Photo> Photos;

        public SQLiteConnectorService()
        {
            connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Ecommerce.db";

            connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            connection.Open();

            Accounts = GetAccountsPropertiesOnly();
            Categories = GetCategoriesPropertiesOnly();
            Invoices = GetInvoicesPropertiesOnly();
            InvoiceDetails = GetInvoiceDetailsesPropertiesOnly();
            Products = GetProductsPropertiesOnly();
            Roles = GetRolesPropertiesOnly();
            RoleAccounts = GetRoleAccountsPropertiesOnly();
            SlideShows = GetSlideShowsPropertiesOnly();
            Photos = GetPhotosPropertiesOnly();

            AccountsNotMapped();
            CategoriesNotMapped();
            InvoicesNotMapped();
            InvoicesDetailsNotMapped();
            ProductsNotMapped();
            RolesNotMapped();
            RoleAccountsNotMapped();
            PhotosNotMapped();
        }





        private SqliteDataReader ExecuteSelectQuery(string query)
        {
            SqliteCommand cmd = new SqliteCommand(query, connection);
            SqliteDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        private int ExecuteInsertQuery(string query, Object ob)
        {
            SqliteCommand cmd = new SqliteCommand(query, connection);

            PropertyInfo[] properties = ob.GetType().GetProperties();
            foreach (var propertie in properties)
            {
                if (propertie.Name == "Id")
                {
                    continue;
                }

                cmd.Parameters.AddWithValue($"@{propertie.Name}", propertie.GetValue(ob));
            }
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT last_insert_rowid()";
            Int64 LastRowID64 = (Int64)cmd.ExecuteScalar();
            int LastRowID = (int)LastRowID64;

            return LastRowID; 
        }

        private void ExecuteDeleteQuery(string query)
        {
            SqliteCommand cmd = new SqliteCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        private void ExecuteUpdateQuery(string query)
        {
            SqliteCommand cmd = new SqliteCommand(query, connection);
            cmd.ExecuteNonQuery();
        }





        public void AddAccount(Account account)
        {
            account.Id = ExecuteInsertQuery("INSERT INTO Account (UserName, Password, FullName, Email, Status, Address, Phone) VALUES(@UserName, @Password, @FullName, @Email, @Status, @Address, @Phone)", account);            
        }

        public void AddRoleAccount(RoleAccount roleAccount)
        {
            ExecuteInsertQuery("INSERT INTO RoleAccount (RoleId, AccountId, Status) VALUES (@RoleId, @AccountId, @Status)", roleAccount);
        }

        public void AddInvoice(Invoice invoice)
        {
            invoice.Id = ExecuteInsertQuery("INSERT INTO Invoice (Name, Created, Status, AccountId) VALUES (@Name, @Created, @Status, @AccountId)", invoice);
        }

        public void AddInvoiceDetails(InvoiceDetails invoiceDetails)
        {
            ExecuteInsertQuery("INSERT INTO InvoiceDetails (InvoiceId, ProductId, Price, Quantity) VALUES (@InvoiceId, @ProductId, @Price, @Quantity)", invoiceDetails);
        }

        public void AddCategory(Category category)
        {
            if (category.ParrentId == null)
            {
                category.Id = ExecuteInsertQuery("INSERT INTO Category (Name, Status) VALUES (@Name, @Status)", category);
            }
            else
            {
                category.Id = ExecuteInsertQuery("INSERT INTO Category (Name, ParrentId, Status) VALUES (@Name, @ParrentId, @Status)", category);
            }
        }

        public void AddPhoto(Photo photo)
        {
            ExecuteInsertQuery("INSERT INTO Photo (Name, Status, Featured, ProductId) VALUES (@Name, @Status, @Featured, @ProductId)", photo);
        }

        public void AddProduct(Product product)
        {
            ExecuteInsertQuery("INSERT INTO Product (Name, Description, Details, Status, Price, Quantity, CategoryId, Featured) VALUES (@Name, @Description, @Details, @Status, @Price, @Quantity, @CategoryId, @Featured)", product);
        }





        public void DeleteAccount(Account account)
        {
            ExecuteDeleteQuery($"DELETE FROM Account WHERE Id = {account.Id}");
        }

        public void DeleteCategorie(Category category)
        {
            ExecuteDeleteQuery($"DELETE FROM Category WHERE Id = {category.Id}");
        }

        public void DeletePhoto(Photo photo)
        {
            ExecuteDeleteQuery($"DELETE FROM Photo WHERE Id = {photo.Id}");
        }

        




        public void UpdateAccounts(Account account = null)
        {
            if (account != null)
            {
                if (!string.IsNullOrEmpty(account.Password))
                {
                    ExecuteUpdateQuery($"UPDATE Account SET Password = '{account.Password}', FullName = '{account.FullName}', Email = '{account.Email}', Address = '{account.Address}', Phone = '{account.Phone}', Status = {account.Status}  where Id = {account.Id}");
                }
                else
                {
                    ExecuteUpdateQuery($"UPDATE Account SET FullName = '{account.FullName}', Email = '{account.Email}', Address = '{account.Address}', Phone = '{account.Phone}', Status = {account.Status}  where Id = {account.Id}");
                }
            }

            Accounts = GetAccountsPropertiesOnly();
            AccountsNotMapped();
        }

        public void UpdateCategories(Category category = null)
        {
            if (category != null)
            {
                ExecuteUpdateQuery($"UPDATE Category SET Name = '{category.Name}', Status = {category.Status} WHERE Id = {category.Id}");
            }

            Categories = GetCategoriesPropertiesOnly();
            CategoriesNotMapped();
        }

        public void UpdateInvoices(Invoice invoice = null)
        {
            if (invoice != null)
            {
                ExecuteUpdateQuery($"UPDATE Invoice SET Status = {invoice.Status} WHERE Id = {invoice.Id}");
            }

            Invoices = GetInvoicesPropertiesOnly();
            InvoicesNotMapped();
        }

        public void UpdateProducts(Product product = null)
        {
            if (product != null)
            {

            }

            Products = GetProductsPropertiesOnly();
            ProductsNotMapped();
        }

        public void UpdatePhotos(Photo photo = null)
        {
            if (photo != null)
            {

            }

            Photos = GetPhotosPropertiesOnly();
            PhotosNotMapped();
        }






        private List<Account> GetAccountsPropertiesOnly()
        {
            List<Account> accounts = new List<Account>();

            SqliteDataReader reader = ExecuteSelectQuery("SELECT * FROM Account");
            while (reader.Read())
            {
                accounts.Add(new Account
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UserName = reader["UserName"].ToString(),
                    FullName = reader["FullName"].ToString(),
                    Password = reader["Password"].ToString(),
                    Address = reader["Address"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Status = Convert.ToBoolean(reader["Status"])
                });
            }

            return accounts;
        }

        private List<Category> GetCategoriesPropertiesOnly()
        {
            List<Category> categories = new List<Category>();

            SqliteDataReader reader = ExecuteSelectQuery($"SELECT * FROM Category");
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    ParrentId = reader["ParrentId"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(reader["ParrentId"]),
                    Status = Convert.ToBoolean(reader["Status"])
                });
            }

            return categories;
        }

        private List<Invoice> GetInvoicesPropertiesOnly()
        {
            List<Invoice> invoices = new List<Invoice>();
            SqliteDataReader reader = ExecuteSelectQuery("SELECT * FROM Invoice");
            while (reader.Read())
            {
                invoices.Add(new Invoice
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Status = Convert.ToInt32(reader["Status"]),
                    AccountId = Convert.ToInt32(reader["AccountId"]),
                });
            }

            return invoices;
        }

        private List<InvoiceDetails> GetInvoiceDetailsesPropertiesOnly()
        {
            List<InvoiceDetails> invoiceDetailses = new List<InvoiceDetails>();
            SqliteDataReader reader = ExecuteSelectQuery("SELECT * FROM InvoiceDetails");
            while (reader.Read())
            {
                invoiceDetailses.Add(new InvoiceDetails
                {
                    InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                });
            }

            return invoiceDetailses;
        }

        private List<Product> GetProductsPropertiesOnly()
        {
            List<Product> products = new List<Product>();

            SqliteDataReader reader = ExecuteSelectQuery("SELECT * FROM Product");
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Details = reader["Details"].ToString(),
                    Status = Convert.ToBoolean(reader["Status"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    Featured = Convert.ToBoolean(reader["Featured"])
                });
            }

            return products;
        }

        private List<Role> GetRolesPropertiesOnly()
        {
            List<Role> roles = new List<Role>();

            SqliteDataReader reader = ExecuteSelectQuery("SELECT * FROM Role");
            while (reader.Read())
            {
                roles.Add(new Role
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Status = Convert.ToBoolean(reader["Status"])
                });
            }

            return roles;
        }

        private List<RoleAccount> GetRoleAccountsPropertiesOnly()
        {
            List<RoleAccount> roleAccounts = new List<RoleAccount>();

            SqliteDataReader reader = ExecuteSelectQuery("SELECT * FROM RoleAccount");
            while (reader.Read())
            {
                roleAccounts.Add(new RoleAccount
                {
                    RoleId = Convert.ToInt32(reader["RoleId"]),
                    AccountId = Convert.ToInt32(reader["AccountId"]),
                    Status = Convert.ToBoolean(reader["Status"])
                });
            }

            foreach (var roleAccount in roleAccounts)
            {
                roleAccount.Role = GetRolesPropertiesOnly().Find(r => r.Id == roleAccount.RoleId);
            }

            return roleAccounts;
        }

        private List<SlideShow> GetSlideShowsPropertiesOnly()
        {
            List<SlideShow> slideShows = new List<SlideShow>();

            SqliteDataReader reader = ExecuteSelectQuery($"SELECT * FROM SlideShow");
            while (reader.Read())
            {
                slideShows.Add(new SlideShow
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Status = Convert.ToBoolean(reader["Status"]),
                    Description = reader["Description"].ToString(),
                    Title = reader["Title"].ToString()
                });
            }

            return slideShows;
        }

        private List<Photo> GetPhotosPropertiesOnly()
        {
            List<Photo> photos = new List<Photo>();

            SqliteDataReader reader = ExecuteSelectQuery("SELECT * FROM Photo");
            while (reader.Read())
            {
                photos.Add(new Photo
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Status = Convert.ToBoolean(reader["Status"]),
                    Featured = Convert.ToBoolean(reader["Featured"]),
                    ProductId = Convert.ToInt32(reader["ProductId"])
                });
            }

            return photos;
        }





        private void AccountsNotMapped()
        {
            foreach (var account in Accounts)
            {
                account.RoleAccounts = RoleAccounts.Where(ra => ra.AccountId == account.Id).ToList();
                account.Invoices = Invoices.Where(i => i.AccountId == account.Id).ToList();
            }
        }

        private void CategoriesNotMapped()
        {
            foreach (var category in Categories)
            {
                category.Products = Products.Where(p => p.Status && p.CategoryId == category.Id).ToList();
                category.InverseParent = Categories.Where(c => c.ParrentId == category.Id).ToList();
                if (category.InverseParent.Count == 0)
                {
                    category.Parent = Categories.Find(c => c.Id == category.ParrentId);
                }
            }
        }

        private void InvoicesNotMapped()
        {
            foreach (var invoice in Invoices)
            {
                invoice.Account = Accounts.Find(a => a.Id == invoice.AccountId);
                invoice.InvoiceDetailses = InvoiceDetails.Where(id => id.InvoiceId == invoice.Id).ToList();
            }
        }

        private void InvoicesDetailsNotMapped()
        {
            foreach (var invoiceDetail in InvoiceDetails)
            {
                invoiceDetail.Invoice = Invoices.Find(i => i.Id == invoiceDetail.InvoiceId);
                invoiceDetail.Product = Products.Find(p => p.Id == invoiceDetail.ProductId);
            }
        }

        private void ProductsNotMapped()
        {
            foreach (var product in Products)
            {
                product.Category = Categories.Find(c => c.Id == product.CategoryId);
                product.Photos = Photos.Where(p => p.ProductId == product.Id).ToList();
                product.InvoiceDetailses = InvoiceDetails.Where(id => id.ProductId == product.Id).ToList();
            }
        }

        private void RolesNotMapped()
        {
            foreach (var role in Roles)
            {
                role.RoleAccounts = RoleAccounts.Where(ra => ra.RoleId == role.Id).ToList();
            }
        }

        private void RoleAccountsNotMapped()
        {
            foreach (var roleAccount in RoleAccounts)
            {
                roleAccount.Account = Accounts.Find(a => a.Id == roleAccount.AccountId);
                roleAccount.Role = Roles.Find(r => r.Id == roleAccount.RoleId);
            }
        }

        private void PhotosNotMapped()
        {
            foreach (var photo in Photos)
            {
                photo.Product = Products.Find(p => p.Id == photo.ProductId);
            }
        }

    }
}
