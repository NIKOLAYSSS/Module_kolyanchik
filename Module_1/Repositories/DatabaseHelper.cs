
using Module_1.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_1.Repositories
{
    public class DatabaseHelper
    {
        private readonly string connectionString;
        //private string connectionString = "Host=localhost;Port=5432;Username=your_user;Password=your_password;Database=your_database";
        public DatabaseHelper(string _connectionString)
        {
            connectionString = _connectionString;
        }
        
        public List<Partner> GetPartners()
        {
            var partners = new List<Partner>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT p.partnerid, p.partnertypeid, pt.typename, p.name, p.directorname, 
                       p.email, p.phone, p.legaladdress, p.inn, p.rating
                FROM partners p
                JOIN partnertypes pt ON p.partnertypeid = pt.partnertypeid";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partners.Add(new Partner
                        {
                            PartnerID = reader.GetInt32(0),
                            PartnerTypeID = reader.GetInt32(1),
                            PartnerType = new PartnerType
                            {
                                PartnerTypeID = reader.GetInt32(1),
                                TypeName = reader.GetString(2),
                            },
                            Name = reader.GetString(3),
                            DirectorName = reader.GetString(4),
                            Email = reader.GetString(5),
                            Phone = reader.GetString(6),
                            LegalAddress = reader.GetString(7),
                            INN = reader.GetString(8),
                            Rating = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9)
                        });
                    }
                }
            }

            return partners;
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT productid, producttypeid, productname, article, minimalprice FROM products";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32(0),
                            ProductTypeID = reader.GetInt32(1),
                            ProductName = reader.GetString(2),
                            Article = reader.GetString(3),
                            MinimalPrice = reader.GetDecimal(4)
                        });
                    }
                }
            }

            return products;
        }

        public void AddSaleHistory(int partnerId, int productId, int quantity)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO saleshistory (partnerid, productid, quantity, saledate) VALUES (@partnerid, @productid, @quantity, NOW())";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@partnerid", partnerId);
                    cmd.Parameters.AddWithValue("@productid", productId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<SalesHistory> GetSalesHistory(int? partnerId = null)
        {
            var salesHistory = new List<SalesHistory>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT sh.saleid, sh.partnerid, p.name, sh.productid, pr.productname, sh.quantity, sh.saledate
                FROM saleshistory sh
                JOIN partners p ON sh.partnerid = p.partnerid
                JOIN products pr ON sh.productid = pr.productid";

                if (partnerId.HasValue)
                {
                    query += " WHERE sh.partnerid = @partnerid";
                }

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    if (partnerId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@partnerid", partnerId.Value);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesHistory.Add(new SalesHistory
                            {
                                SaleID = reader.GetInt32(0),
                                PartnerID = reader.GetInt32(1),
                                ProductID = reader.GetInt32(3),
                                Quantity = reader.GetInt32(5),
                                SaleDate = reader.GetDateTime(6)
                            });
                        }
                    }
                }
            }

            return salesHistory;
        }
        public Product GetProductById(int productId)
        {
            Product product = null;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT productid, producttypeid, productname, article, minimalprice FROM products WHERE productid = @productid";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@productid", productId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                ProductID = reader.GetInt32(0),
                                ProductTypeID = reader.GetInt32(1),
                                ProductName = reader.GetString(2),
                                Article = reader.GetString(3),
                                MinimalPrice = reader.GetDecimal(4)
                            };
                        }
                    }
                }
            }

            return product;
        }
    }
}
