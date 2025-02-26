using Module_1.Models;
using System;
using System.Collections.Generic;
using Npgsql;

namespace Module_1.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly string _connectionString;

        public PartnerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public decimal GetTotalSalesQuantity(int partnerId)
        {
            decimal totalQuantity = 0;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(
                    "SELECT COALESCE(SUM(Quantity), 0) FROM saleshistory WHERE PartnerID = @PartnerID", connection))
                {
                    command.Parameters.AddWithValue("@PartnerID", partnerId);
                    totalQuantity = Convert.ToDecimal(command.ExecuteScalar());
                }
            }
            return totalQuantity;
        }
        // Реализованный метод для получения всех партнеров с типом партнера
        public List<Partner> GetAllPartners()
        {
            var partners = new List<Partner>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    SELECT p.PartnerID, pt.TypeName, p.Name, p.DirectorName, p.Email, p.Phone, p.LegalAddress, p.INN, p.Rating 
                    FROM Partners p
                    JOIN PartnerTypes pt ON p.PartnerTypeID = pt.PartnerTypeID";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partners.Add(new Partner
                        {
                            PartnerID = reader.GetInt32(0),
                            PartnerType = new PartnerType { TypeName = reader.GetString(1) }, // Тип партнера
                            Name = reader.GetString(2),
                            DirectorName = reader.GetString(3),
                            Email = reader.GetString(4),
                            Phone = reader.GetString(5),
                            LegalAddress = reader.GetString(6),
                            INN = reader.GetString(7),
                            Rating = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8)
                        });
                    }
                }
            }
            return partners;
        }

        // Реализованный метод для получения партнера по ID
        public Partner GetPartnerById(int partnerId)
        {
            Partner partner = null;
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    SELECT p.PartnerID, pt.TypeName, p.Name, p.DirectorName, p.Email, p.Phone, p.LegalAddress, p.INN, p.Rating 
                    FROM Partners p
                    JOIN PartnerTypes pt ON p.PartnerTypeID = pt.PartnerTypeID
                    WHERE p.PartnerID = @PartnerID";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerID", partnerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            partner = new Partner
                            {
                                PartnerID = reader.GetInt32(0),
                                PartnerType = new PartnerType { TypeName = reader.GetString(1) },
                                Name = reader.GetString(2),
                                DirectorName = reader.GetString(3),
                                Email = reader.GetString(4),
                                Phone = reader.GetString(5),
                                LegalAddress = reader.GetString(6),
                                INN = reader.GetString(7),
                                Rating = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8)
                            };
                        }
                    }
                }
            }
            return partner;
        }

        // Реализованный метод для удаления партнера по ID
        public void DeletePartner(int partnerId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "DELETE FROM Partners WHERE PartnerID = @PartnerID";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerID", partnerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Реализованный метод для получения типов партнеров
        public List<PartnerType> GetPartnerTypes()
        {
            var partnerTypes = new List<PartnerType>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM PartnerTypes";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partnerTypes.Add(new PartnerType
                        {
                            PartnerTypeID = reader.GetInt32(0),
                            TypeName = reader.GetString(1)
                        });
                    }
                }
            }
            return partnerTypes;
        }

        // Метод для добавления или обновления партнера
        public void UpdatePartner(Partner partner)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    INSERT INTO Partners (PartnerID, PartnerTypeID, Name, DirectorName, Email, Phone, LegalAddress, INN, Rating)
                    VALUES (@PartnerID, @PartnerTypeID, @Name, @DirectorName, @Email, @Phone, @LegalAddress, @INN, @Rating)
                    ON CONFLICT (PartnerID) DO UPDATE
                    SET PartnerTypeID = EXCLUDED.PartnerTypeID,
                        Name = EXCLUDED.Name,
                        DirectorName = EXCLUDED.DirectorName,
                        Email = EXCLUDED.Email,
                        Phone = EXCLUDED.Phone,
                        LegalAddress = EXCLUDED.LegalAddress,
                        INN = EXCLUDED.INN,
                        Rating = EXCLUDED.Rating";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerID", partner.PartnerID);
                    cmd.Parameters.AddWithValue("@PartnerTypeID", partner.PartnerTypeID);
                    cmd.Parameters.AddWithValue("@Name", partner.Name);
                    cmd.Parameters.AddWithValue("@DirectorName", partner.DirectorName);
                    cmd.Parameters.AddWithValue("@Email", partner.Email);
                    cmd.Parameters.AddWithValue("@Phone", partner.Phone);
                    cmd.Parameters.AddWithValue("@LegalAddress", partner.LegalAddress);
                    cmd.Parameters.AddWithValue("@INN", partner.INN);
                    cmd.Parameters.AddWithValue("@Rating", partner.Rating);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void AddPartner(Partner partner)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO Partners (Name, DirectorName, Email, Phone, LegalAddress, INN, Rating, PartnerTypeID) VALUES (@Name, @DirectorName, @Email, @Phone, @LegalAddress, @INN, @Rating, @PartnerTypeID)", connection))
                {
                    command.Parameters.AddWithValue("Name", partner.Name);
                    command.Parameters.AddWithValue("DirectorName", partner.DirectorName);
                    command.Parameters.AddWithValue("Email", partner.Email);
                    command.Parameters.AddWithValue("Phone", partner.Phone);
                    command.Parameters.AddWithValue("LegalAddress", partner.LegalAddress);
                    command.Parameters.AddWithValue("INN", partner.INN);
                    command.Parameters.AddWithValue("Rating", partner.Rating.HasValue ? (object)partner.Rating.Value : DBNull.Value);
                    command.Parameters.AddWithValue("PartnerTypeID", partner.PartnerTypeID);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<SalesHistory> GetSalesHistory(int? partnerId = null)
        {
            List<SalesHistory> salesList = new List<SalesHistory>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
            SELECT sh.saleid, sh.partnerid, sh.productid, 
                   sh.quantity, sh.saledate, 
                   p.name AS partnername, 
                   pr.name AS productname, pr.price
            FROM saleshistory sh
            JOIN partners p ON sh.partnerid = p.partnerid
            JOIN products pr ON sh.productid = pr.productid
            WHERE (@partnerId IS NULL OR sh.partnerid = @partnerId)
            ORDER BY sh.saledate DESC";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@partnerId", (object)partnerId ?? DBNull.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sale = new SalesHistory
                            {
                                SaleID = reader.GetInt32(0),
                                PartnerID = reader.GetInt32(1),
                                ProductID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                SaleDate = reader.GetDateTime(4)
                            };

                            // Читаем дополнительные поля прямо здесь
                            string partnerName = reader.GetString(5);
                            string productName = reader.GetString(6);
                            decimal price = reader.GetDecimal(7);

                            // Можно передавать их в DataGridView напрямую
                            salesList.Add(sale);
                        }
                    }
                }
            }
            return salesList;
        }
    }
}

