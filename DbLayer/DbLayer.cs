using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace Clover.DbLayer
{
    /// <summary>
    /// Acceso a los parámetros de la capa base de datos.
    /// </summary>
    /// 





    public static class DbLayerSettings
    {
        public const int DefaultRecordLimit = 300;
        public static string ConnectionString { get; set; }
        public static int UserID { get; set; }





        public static void SetConnectionString(string Server, uint Port, string Database, string UserID, string Password)
        {
            var builder = new MySqlConnectionStringBuilder()
            {
                Server = Server,
                Port = Port,
                Database = Database,
                UserID = UserID,
                Password = Password,
                DefaultCommandTimeout = 7
            };
            ConnectionString = builder.ToString();
        }
        public static void SetUserID(int UserID)
        {
            DbLayerSettings.UserID = UserID;
        }
    }

    /// <summary>
    /// Clase de funciones auxiliares.
    /// </summary>
    public static class DbHelpers
    {
        public static bool CheckFKConstraintViolation(Exception exception)
        {
            int[] fkViolationErrorNumbers = { 1216, 1217, 1451, 1452 };
            return exception is MySqlException && fkViolationErrorNumbers.Contains(((MySqlException)exception).Number);
        }

    }

    public class DbConfig
    {
        // La propiedad miCadenaConexion ya no es necesaria
        // porque vamos a utilizar la cadena de conexión de DbLayerSettings

        // Elimina el constructor estático
        // static DbConfig()
        // {
        //     miCadenaConexion = ConfigurationManager.ConnectionStrings["MiCadenaConexion"].ConnectionString;
        // }

        // Cambia la propiedad CadenaConexion para que obtenga la cadena de conexión desde DbLayerSettings
        public static string CadenaConexion
        {
            get { return DbLayerSettings.ConnectionString; }
        }
    }

    /// <summary>
    /// Provee soporte para transacciones.
    /// </summary>
    public class DbTransactionHandler : IDisposable
    {
        public MySqlConnection DbConnection { get; set; }
        public MySqlTransaction DbTransaction { get; }

        private bool disposedValue = false;

        public DbTransactionHandler()
        {
            DbConnection = new MySqlConnection(DbLayerSettings.ConnectionString);
            DbConnection.Open();
            DbTransaction = DbConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            DbTransaction.Commit();
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DbTransaction.Dispose();
                    DbConnection.Close();
                    DbConnection.Dispose();
                }
                disposedValue = true;
            }
        }
    }

    /// <summary>
    /// Clase de funciones auxiliares para manejo de base de datos.
    /// </summary>
    public static class DbManager
    {
        /// <summary>
        /// Ejecuta un comando SQL y devuelve una lista del tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objecto para mapeo.</typeparam>
        /// <param name="handler">Soporte para transacciones.</param>
        /// <param name="sqlCommand">Comando SQL.</param>
        /// <param name="parameters">Objecto anónimo con los parámetros del comando.</param>
        /// <returns></returns>
        public static List<T> DbQueryList<T>(DbTransactionHandler handler, string sqlCommand, object parameters = null)
        {
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    return dbConnection.Query<T>(sqlCommand, parameters).ToList();
                }
            }
            else
            {
                return handler.DbConnection.Query<T>(sqlCommand, parameters, handler.DbTransaction).ToList();
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL y devuelve un único objecto del tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objecto para mapeo.</typeparam>
        /// <param name="handler">Soporte para transacciones.</param>
        /// <param name="sqlCommand">Comando SQL.</param>
        /// <param name="parameters">Objecto anónimo con los parámetros del comando.</param>
        /// <returns></returns>
        public static T DbQuerySingle<T>(DbTransactionHandler handler, string sqlCommand, object parameters)
        {
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    return dbConnection.QuerySingle<T>(sqlCommand, parameters);
                }
            }
            else
            {
                return handler.DbConnection.QuerySingle<T>(sqlCommand, parameters, handler.DbTransaction);
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL y devuelve un único objecto del tipo especificado. En caso de no encontrarse, devuelve el valor por defecto del tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objecto para mapeo.</typeparam>
        /// <param name="handler">Soporte para transacciones.</param>
        /// <param name="sqlCommand">Comando SQL.</param>
        /// <param name="parameters">Objecto anónimo con los parámetros del comando.</param>
        /// <returns></returns>
        internal static T DbQuerySingleOrDefault<T>(DbTransactionHandler handler, string sqlCommand, object parameters)
        {
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    return dbConnection.QuerySingleOrDefault<T>(sqlCommand, parameters);
                }
            }
            else
            {
                return handler.DbConnection.QuerySingleOrDefault<T>(sqlCommand, parameters, handler.DbTransaction);
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL de inserción y devuelve el valor del PK asignado.
        /// </summary>
        /// <param name="handler">Soporte para transacciones.</param>
        /// <param name="sqlCommand">Comando SQL.</param>
        /// <param name="parameters">Objecto anónimo con los parámetros del comando.</param>
        /// <returns></returns>
        internal static int DbInsert(DbTransactionHandler handler, string sqlCommand, object parameters)
        {
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    using (var dbTransaction = dbConnection.BeginTransaction())
                    {
                        dbConnection.Execute(sqlCommand, parameters, dbTransaction);
                        int lastInsertedId = GetLastInsertedId(dbConnection, dbTransaction);
                        dbTransaction.Commit();
                        return lastInsertedId;
                    }
                }
            }
            else
            {
                handler.DbConnection.Execute(sqlCommand, parameters, handler.DbTransaction);
                return GetLastInsertedId(handler.DbConnection, handler.DbTransaction);
            }
        }

        internal static int DbInsert(DbTransactionHandler handler, string sqlCommand, object parameters, int eventCode)
        {
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    using (var dbTransaction = dbConnection.BeginTransaction())
                    {
                        // Inserta y obtiene ID del elemento.
                        dbConnection.Execute(sqlCommand, parameters, dbTransaction);
                        int lastInsertedId = GetLastInsertedId(dbConnection, dbTransaction);
                        // Registra el evento.
                        const string sqlCommand2 = "INSERT INTO record (Time, UserID, EventCode, RefID) VALUES (@Time, @UserID, @EventCode, @RefID);";
                        object parameters2 = new { Time = DateTime.Now, UserID = DbLayerSettings.UserID, EventCode = eventCode, RefID = lastInsertedId };
                        dbConnection.Execute(sqlCommand2, parameters2, dbTransaction);
                        // Concreta transacción.
                        dbTransaction.Commit();
                        return lastInsertedId;
                    }
                }
            }
            else
            {
                // Inserta y obtiene ID del elemento.
                handler.DbConnection.Execute(sqlCommand, parameters, handler.DbTransaction);
                int lastInsertedId = GetLastInsertedId(handler.DbConnection, handler.DbTransaction);
                // Registra el evento.
                const string sqlCommand2 = "INSERT INTO record (Time, UserID, EventCode, RefID) VALUES (@Time, @UserID, @EventCode, @RefID);";
                object parameters2 = new { Time = DateTime.Now, UserID = DbLayerSettings.UserID, EventCode = eventCode, RefID = lastInsertedId };
                handler.DbConnection.Execute(sqlCommand2, parameters2, handler.DbTransaction);
                return lastInsertedId;
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL.
        /// </summary>
        /// <param name="handler">Soporte para transacciones.</param>
        /// <param name="sqlCommand">Comando SQL.</param>
        /// <param name="parameters">Objecto anónimo con los parámetros del comando.</param>
        public static void DbExecuteNonQuery(DbTransactionHandler handler, string sqlCommand, object parameters)
        {
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    dbConnection.Execute(sqlCommand, parameters);
                }
            }
            else
            {
                handler.DbConnection.Execute(sqlCommand, parameters, handler.DbTransaction);
            }
        }
        internal static void DbExecuteNonQuery(DbTransactionHandler handler, string sqlCommand, object parameters, int eventCode, int? refID = null)
        {
            // Comando para registrar el evento.
            const string sqlCommand2 = "INSERT INTO record (Time, UserID, EventCode, RefID) VALUES (@Time, @UserID, @EventCode, @RefID);";
            object parameters2 = new { Time = DateTime.Now, UserID = DbLayerSettings.UserID, EventCode = eventCode, RefID = refID };
            
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    using (var dbTransaction = dbConnection.BeginTransaction())
                    {
                        dbConnection.Execute(sqlCommand, parameters, dbTransaction);
                        dbConnection.Execute(sqlCommand2, parameters2, dbTransaction);
                        dbTransaction.Commit();
                    }
                }
            }
            else
            {
                handler.DbConnection.Execute(sqlCommand, parameters, handler.DbTransaction);
                handler.DbConnection.Execute(sqlCommand2, parameters2, handler.DbTransaction);
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL y devuelve un único resultado del tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de resultado.</typeparam>
        /// <param name="handler">Soporte para transacciones.</param>
        /// <param name="sqlCommand">Comando SQL.</param>
        /// <param name="parameters">Objecto anónimo con los parámetros del comando.</param>
        /// <returns></returns>
        internal static T DbExecuteScalar<T>(DbTransactionHandler handler, string sqlCommand, object parameters)
        {
            if (handler == null)
            {
                using (var dbConnection = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    dbConnection.Open();
                    return dbConnection.ExecuteScalar<T>(sqlCommand, parameters);
                }
            }
            else
            {
                return handler.DbConnection.ExecuteScalar<T>(sqlCommand, parameters, handler.DbTransaction);
            }
        }

        /// <summary>
        /// Devuelve el valor del "primary key" del último registro.
        /// </summary>
        /// <param name="dbConnection">Conexión SQL.</param>
        /// <param name="dbTransaction">Transacción SQL.</param>
        /// <returns></returns>
        private static int GetLastInsertedId(MySqlConnection dbConnection, MySqlTransaction dbTransaction)
        {
            const string sqlCommand = "SELECT LAST_INSERT_ID();";
            var lastInsertedId = dbConnection.ExecuteScalar(sqlCommand, transaction: dbTransaction);
            return Convert.ToInt32(lastInsertedId);
        }
    }

    /// <summary>
    /// Clase base que permite obtener el ID principal del elemento sin conocer el tipo específico.
    /// </summary>
    [Serializable]
    public abstract class DbEntity
    {
        public abstract int PrimaryKeyID { get; }
    }

    public class Business : DbEntity
    {
        public override int PrimaryKeyID { get { return BusinessID; } }

        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public byte[] BusinessLogo { get; set; }
        public byte[] MailLogo { get; set; }
        public byte[] SocialMediaLogo1 { get; set; }
        public byte[] SocialMediaLogo2 { get; set; }
        public byte[] SocialMediaLogo3 { get; set; }
        public byte[] SocialMediaLogo4 { get; set; }
        public string SocialMediaLink1 { get; set; }
        public string SocialMediaLink2 { get; set; }
        public string SocialMediaLink3 { get; set; }
        public string SocialMediaLink4 { get; set; }

        public static List<Business> GetBusiness(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " business.BusinessID,"
                                    + " business.BusinessName,"
                                    + " business.BusinessLogo,"
                                    + " business.MailLogo,"
                                    + " business.SocialMediaLogo1,"
                                    + " business.SocialMediaLogo2,"
                                    + " business.SocialMediaLogo3,"
                                    + " business.SocialMediaLogo4,"
                                    + " business.SocialMediaLink1,"
                                    + " business.SocialMediaLink2,"
                                    + " business.SocialMediaLink3,"
                                    + " business.SocialMediaLink4"
                                    + " FROM business"
                                    + " ORDER BY business.BusinessID ASC;";
            return DbManager.DbQueryList<Business>(handler, sqlCommand);
        }
        public static List<Business> GetBusinessLight(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " business.BusinessID,"
                                    + " business.BusinessName"
                                    + " FROM business"
                                    + " ORDER BY business.BusinessID ASC;";
            return DbManager.DbQueryList<Business>(handler, sqlCommand);
        }
        public static Business GetBusinessById(int BusinessID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " business.BusinessID,"
                                    + " business.BusinessName,"
                                    + " business.BusinessLogo,"
                                    + " business.MailLogo,"
                                    + " business.SocialMediaLogo1,"
                                    + " business.SocialMediaLogo2,"
                                    + " business.SocialMediaLogo3,"
                                    + " business.SocialMediaLogo4,"
                                    + " business.SocialMediaLink1,"
                                    + " business.SocialMediaLink2,"
                                    + " business.SocialMediaLink3,"
                                    + " business.SocialMediaLink4"
                                    + " FROM business"
                                    + " WHERE business.BusinessID = @BusinessID;";
            object parameters = new { BusinessID };
            return DbManager.DbQuerySingle<Business>(handler, sqlCommand, parameters);
        }
    }




    public class ChatMessage : DbEntity
    {
        public override int PrimaryKeyID { get { return MessageID; } }

        public int MessageID { get; set; }
        public int OwnerID { get; set; }
        public int SenderID { get; set; }
        public int? AddresseeID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Body { get; set; }

        // Campos de soporte (no pertenecen a la tabla "chat_message").
        public string UserName { get; set; }
        public string ChatColor { get; set; }



        public static List<ChatMessage> GetMessagesBetweenTwoUsers(int OwnerUserID, int SecondUserID, DateTime OldestDateAllowed, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " chat_message.MessageID,"
                                    + " chat_message.OwnerID,"
                                    + " chat_message.SenderID,"
                                    + " chat_message.AddresseeID,"
                                    + " chat_message.Timestamp,"
                                    + " chat_message.Body,"
                                    + " user.UserName,"
                                    + " user.ChatColor"
                                    + " FROM chat_message"
                                    + " INNER JOIN user ON user.UserID = chat_message.SenderID"
                                    + " WHERE chat_message.OwnerID = @OwnerUserID"
                                    + " AND ((chat_message.SenderID = @SecondUserID AND chat_message.AddresseeID = @OwnerUserID)"
                                    + " OR (chat_message.SenderID = @OwnerUserID AND chat_message.AddresseeID = @SecondUserID))"
                                    + " AND chat_message.Timestamp >= @OldestDateAllowed"
                                    + " ORDER BY chat_message.Timestamp ASC;";
            object parameters = new { OwnerUserID, SecondUserID, OldestDateAllowed };
            return DbManager.DbQueryList<ChatMessage>(handler, sqlCommand, parameters);
        }
        public static List<ChatMessage> GetPublicMessages(int OwnerUserID, DateTime OldestDateAllowed, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " chat_message.MessageID,"
                                    + " chat_message.OwnerID,"
                                    + " chat_message.SenderID,"
                                    + " chat_message.AddresseeID,"
                                    + " chat_message.Timestamp,"
                                    + " chat_message.Body,"
                                    + " user.UserName,"
                                    + " user.ChatColor"
                                    + " FROM chat_message"
                                    + " INNER JOIN user ON user.UserID = chat_message.SenderID"
                                    + " WHERE chat_message.OwnerID = @OwnerUserID"
                                    + " AND chat_message.AddresseeID IS NULL"
                                    + " AND chat_message.Timestamp >= @OldestDateAllowed"
                                    + " ORDER BY chat_message.Timestamp ASC;";
            object parameters = new { OwnerUserID, OldestDateAllowed };
            return DbManager.DbQueryList<ChatMessage>(handler, sqlCommand, parameters);
        }
        public static void DeleteMessagesBetweenTwoUsers(int OwnerUserID, int SecondUserID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM chat_message"
                                    + " WHERE OwnerID = @OwnerUserID AND ((SenderID = @SecondUserID AND AddresseeID = @OwnerUserID) OR (SenderID = @OwnerUserID AND AddresseeID = @SecondUserID));";
            object parameters = new { OwnerUserID, SecondUserID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO chat_message (OwnerID, SenderID, AddresseeID, Timestamp, Body)"
                                    + " VALUES (@OwnerID, @SenderID, @AddresseeID, @Timestamp, @Body);";
            this.MessageID = DbManager.DbInsert(handler, sqlCommand, this);
        }


        public static List<ChatMessage> GetMessagesByGroup(int GroupID, DateTime OldestDateAllowed, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT m.MessageID, m.OwnerID, m.SenderID, m.GroupID, m.Timestamp, m.Body, u.UserName, u.ChatColor"
                                     + " FROM chat_message m"
                                     + " INNER JOIN user u ON u.UserID = m.SenderID"
                                     + " WHERE m.GroupID = @GroupID AND m.Timestamp >= @OldestDateAllowed"
                                     + " ORDER BY m.Timestamp ASC;";
            object parameters = new { GroupID, OldestDateAllowed };
            return DbManager.DbQueryList<ChatMessage>(handler, sqlCommand, parameters);
        }
    }
    public class Currency : DbEntity
    {
        public override int PrimaryKeyID { get { return CurrencyID; } }

        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string AfipCode { get; set; }

        public static List<Currency> GetCurrencies(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " currency.CurrencyID,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol,"
                                    + " currency.AfipCode"
                                    + " FROM currency"
                                    + " ORDER BY currency.CurrencyID ASC;";
            return DbManager.DbQueryList<Currency>(handler, sqlCommand);
        }
    }
    public class Customer : DbEntity
    {
        public override int PrimaryKeyID { get { return CustomerID; } }

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string IdentityNumber { get; set; }
        public bool IsCUIT { get; set; }
        public string TaxGroup { get; set; }
        public int PaymentID { get; set; }
        public int PaymentTerm { get; set; }
        public int BusinessID { get; set; }
        public int? CountryID { get; set; }
        public string Country { get; set; }
        public int? DistrictID { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime RegistryDate { get; set; }

        // Campos de soporte (no pertenecen a la tabla "customer")
        public string PaymentName { get; set; }
        public string BusinessName { get; set; }

        public static List<Customer> GetCustomers(string viewSettingsString, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " customer.CustomerID,"
                              + " customer.CustomerName,"
                              + " customer.IdentityNumber,"
                              + " customer.IsCUIT,"
                              + " customer.TaxGroup,"
                              + " customer.PaymentID,"
                              + " customer.PaymentTerm,"
                              + " customer.BusinessID,"
                              + " customer.CountryID,"
                              + " customer.Country,"
                              + " customer.DistrictID,"
                              + " customer.District,"
                              + " customer.City,"
                              + " customer.Address,"
                              + " customer.RegistryDate,"
                              + " payment.PaymentName,"
                              + " business.BusinessName"
                              + " FROM customer"
                              + " INNER JOIN payment ON payment.PaymentID = customer.PaymentID"
                              + " INNER JOIN business ON business.BusinessID = customer.BusinessID"
                              + viewSettingsString
                              + $" LIMIT {DbLayerSettings.DefaultRecordLimit};";
            return DbManager.DbQueryList<Customer>(handler, sqlCommand);
        }
        public static List<Customer> GetCustomersLight(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " customer.CustomerID,"
                              + " customer.CustomerName,"
                              + " customer.PaymentID,"
                              + " customer.BusinessID"
                              + " FROM customer"
                              + " ORDER BY customer.CustomerName ASC;";
            return DbManager.DbQueryList<Customer>(handler, sqlCommand);
        }
        public static Customer GetCustomerById(int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " customer.CustomerID,"
                                    + " customer.CustomerName,"
                                    + " customer.IdentityNumber,"
                                    + " customer.IsCUIT,"
                                    + " customer.TaxGroup,"
                                    + " customer.PaymentID,"
                                    + " customer.PaymentTerm,"
                                    + " customer.BusinessID,"
                                    + " customer.CountryID,"
                                    + " customer.Country,"
                                    + " customer.DistrictID,"
                                    + " customer.District,"
                                    + " customer.City,"
                                    + " customer.Address,"
                                    + " customer.RegistryDate,"
                                    + " payment.PaymentName,"
                                    + " business.BusinessName"
                                    + " FROM customer"
                                    + " INNER JOIN payment ON payment.PaymentID = customer.PaymentID"
                                    + " INNER JOIN business ON business.BusinessID = customer.BusinessID"
                                    + " WHERE customer.CustomerID = @CustomerID;";
            object parameters = new { CustomerID };
            return DbManager.DbQuerySingle<Customer>(handler, sqlCommand, parameters);
        }
        public static void DeleteCustomerById(int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM customer WHERE CustomerID = @CustomerID;";
            object parameters = new { CustomerID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO customer"
                                    + " (CustomerName, IdentityNumber, IsCUIT, TaxGroup, PaymentID, PaymentTerm, BusinessID,"
                                    + " CountryID, Country, DistrictID, District, City, Address, RegistryDate)"
                                    + " VALUES (@CustomerName, @IdentityNumber, @IsCUIT, @TaxGroup, @PaymentID, @PaymentTerm,"
                                    + " @BusinessID, @CountryID, @Country, @DistrictID, @District, @City, @Address, @RegistryDate);";
            this.CustomerID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE customer SET"
                                    + " CustomerName = @CustomerName,"
                                    + " IdentityNumber = @IdentityNumber,"
                                    + " IsCUIT = @IsCUIT,"
                                    + " TaxGroup = @TaxGroup,"
                                    + " PaymentID = @PaymentID,"
                                    + " PaymentTerm = @PaymentTerm,"
                                    + " BusinessID = @BusinessID,"
                                    + " CountryID = @CountryID,"
                                    + " Country = @Country,"
                                    + " DistrictID = @DistrictID,"
                                    + " District = @District,"
                                    + " City = @City,"
                                    + " Address = @Address,"
                                    + " RegistryDate = @RegistryDate"
                                    + " WHERE CustomerID = @CustomerID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class CustomerContact : DbEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override int PrimaryKeyID { get { return ContactID; } }

        public int ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; OnPropertyChanged("ContactID"); }
        }
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; OnPropertyChanged("CustomerID"); }
        }
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; OnPropertyChanged("ContactName"); }
        }
        public string Greeting
        {
            get { return _Greeting; }
            set { _Greeting = value; OnPropertyChanged("Greeting"); }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; OnPropertyChanged("Phone"); }
        }
        public string SecondaryPhone
        {
            get { return _SecondaryPhone; }
            set { _SecondaryPhone = value; OnPropertyChanged("SecondaryPhone"); }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged("Email"); }
        }

        private int _ContactID;
        private int _CustomerID;
        private string _ContactName;
        private string _Greeting;
        private string _Phone;
        private string _SecondaryPhone;
        private string _Email;

        public static List<CustomerContact> GetContactsByCustomerId(int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " customer_contact.ContactID,"
                                    + " customer_contact.CustomerID,"
                                    + " customer_contact.ContactName,"
                                    + " customer_contact.Greeting,"
                                    + " customer_contact.Phone,"
                                    + " customer_contact.SecondaryPhone,"
                                    + " customer_contact.Email"
                                    + " FROM customer_contact"
                                    + " WHERE customer_contact.CustomerID = @CustomerID"
                                    + " ORDER BY customer_contact.ContactName ASC;";
            object parameters = new { CustomerID };
            return DbManager.DbQueryList<CustomerContact>(handler, sqlCommand, parameters);
        }
        public static CustomerContact GetContactById(int ContactID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " customer_contact.ContactID,"
                                    + " customer_contact.CustomerID,"
                                    + " customer_contact.ContactName,"
                                    + " customer_contact.Greeting,"
                                    + " customer_contact.Phone,"
                                    + " customer_contact.SecondaryPhone,"
                                    + " customer_contact.Email"
                                    + " FROM customer_contact"
                                    + " WHERE customer_contact.ContactID = @ContactID;";
            object parameters = new { ContactID };
            return DbManager.DbQuerySingle<CustomerContact>(handler, sqlCommand, parameters);
        }
        public static void DeleteContactById(int ContactID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM customer_contact WHERE ContactID = @ContactID;";
            object parameters = new { ContactID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO customer_contact"
                                    + " (CustomerID, ContactName, Greeting, Phone, SecondaryPhone, Email)"
                                    + " VALUES (@CustomerID, @ContactName, @Greeting, @Phone, @SecondaryPhone, @Email)";
            this.ContactID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE customer_contact SET"
                                    + " CustomerID = @CustomerID,"
                                    + " ContactName = @ContactName,"
                                    + " Greeting = @Greeting,"
                                    + " Phone = @Phone,"
                                    + " SecondaryPhone = @SecondaryPhone,"
                                    + " Email = @Email"
                                    + " WHERE ContactID = @ContactID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    public class CustomerPayment : DbEntity
    {
        public override int PrimaryKeyID { get { return CustomerPaymentID; } }

        public int CustomerPaymentID { get; set; }
        public int BusinessID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public int AccountID { get; set; }
        public decimal TotalAmount { get; set; }
        public int CurrencyID { get; set; }
        public string AdditionalInformation { get; set; }
        public bool IsUnmarked { get; set; }
        public DateTime FechaChequeDiferido { get; set; }

        // Campos de soporte (no pertenecen a la tabla "customer_payment")
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }
        public string AccountName { get; set; }
        public string CurrencySymbol { get; set; }
        public string IsUnmarkedText { get; set; }

        public static List<CustomerPayment> GetPayments(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " customer_payment.CustomerPaymentID,"
                              + " customer_payment.BusinessID,"
                              + " customer_payment.CustomerID,"
                              + " customer_payment.Date,"
                              + " customer_payment.FechaChequeDiferido,"
                              + " customer_payment.AccountID,"
                              + " customer_payment.TotalAmount,"
                              + " customer_payment.CurrencyID,"
                              + " customer_payment.AdditionalInformation,"
                              + " customer_payment.IsUnmarked,"
                              + " IF(customer_payment.IsUnmarked,'N','B') AS IsUnmarkedText,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " account.AccountName,"
                              + " currency.CurrencySymbol"
                              + " FROM customer_payment"
                              + " JOIN business ON business.BusinessID = customer_payment.BusinessID"
                              + " JOIN customer ON customer.CustomerID = customer_payment.CustomerID"
                              + " JOIN account ON account.AccountID = customer_payment.AccountID"
                              + " JOIN currency ON currency.CurrencyID = customer_payment.CurrencyID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<CustomerPayment>(handler, sqlCommand);
        }
        public static List<CustomerPayment> GetPaymentsByCustomerId(int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " customer_payment.CustomerPaymentID,"
                              + " customer_payment.BusinessID,"
                              + " customer_payment.CustomerID,"
                              + " customer_payment.Date,"
                              + " customer_payment.FechaChequeDiferido,"
                              + " customer_payment.AccountID,"
                              + " customer_payment.TotalAmount,"
                              + " customer_payment.CurrencyID,"
                              + " customer_payment.AdditionalInformation,"
                              + " customer_payment.IsUnmarked,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " account.AccountName,"
                              + " currency.CurrencySymbol"
                              + " FROM customer_payment"
                              + " JOIN business ON business.BusinessID = customer_payment.BusinessID"
                              + " JOIN customer ON customer.CustomerID = customer_payment.CustomerID"
                              + " JOIN account ON account.AccountID = customer_payment.AccountID"
                              + " JOIN currency ON currency.CurrencyID = customer_payment.CurrencyID"
                              + " WHERE customer_payment.CustomerID = @CustomerID"
                              + " ORDER BY customer_payment.Date DESC;";
            object parameters = new { CustomerID };
            return DbManager.DbQueryList<CustomerPayment>(handler, sqlCommand, parameters);
        }
        public static List<CustomerPayment> GetPaymentsByCustomerId(int CustomerID, int CurrencyID, DateTime startDate, DateTime endDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " customer_payment.CustomerPaymentID,"
                              + " customer_payment.BusinessID,"
                              + " customer_payment.CustomerID,"
                              + " customer_payment.Date,"
                              + " customer_payment.FechaChequeDiferido,"
                              + " customer_payment.AccountID,"
                              + " customer_payment.TotalAmount,"
                              + " customer_payment.CurrencyID,"
                              + " customer_payment.AdditionalInformation,"
                              + " customer_payment.IsUnmarked,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " account.AccountName,"
                              + " currency.CurrencySymbol"
                              + " FROM customer_payment"
                              + " JOIN business ON business.BusinessID = customer_payment.BusinessID"
                              + " JOIN customer ON customer.CustomerID = customer_payment.CustomerID"
                              + " JOIN account ON account.AccountID = customer_payment.AccountID"
                              + " JOIN currency ON currency.CurrencyID = customer_payment.CurrencyID"
                              + " WHERE customer_payment.CustomerID = @CustomerID"
                              + " AND customer_payment.CurrencyID = @CurrencyID"
                              + " AND customer_payment.Date >= @startDate"
                              + " AND customer_payment.Date <= @endDate"
                              + " ORDER BY customer_payment.Date ASC;";
            object parameters = new { CustomerID, CurrencyID, startDate, endDate };
            return DbManager.DbQueryList<CustomerPayment>(handler, sqlCommand, parameters);
        }
        public static decimal GetTotalByCustomerId(int CustomerID, int CurrencyID, DateTime endDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " SUM(customer_payment.TotalAmount)"
                                    + " FROM customer_payment"
                                    + " WHERE customer_payment.CustomerID = @CustomerID"
                                    + " AND customer_payment.CurrencyID = @CurrencyID"
                                    + " AND customer_payment.Date <= @endDate;";
            object parameters = new { CustomerID, CurrencyID, endDate };
            return DbManager.DbExecuteScalar<decimal>(handler, sqlCommand, parameters);
        }
        public static CustomerPayment GetPaymentById(int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " customer_payment.CustomerPaymentID,"
                              + " customer_payment.BusinessID,"
                              + " customer_payment.CustomerID,"
                              + " customer_payment.Date,"
                              + " customer_payment.FechaChequeDiferido,"
                              + " customer_payment.AccountID,"
                              + " customer_payment.TotalAmount,"
                              + " customer_payment.CurrencyID,"
                              + " customer_payment.AdditionalInformation,"
                              + " customer_payment.IsUnmarked,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " account.AccountName,"
                              + " currency.CurrencySymbol"
                              + " FROM customer_payment"
                              + " JOIN business ON business.BusinessID = customer_payment.BusinessID"
                              + " JOIN customer ON customer.CustomerID = customer_payment.CustomerID"
                              + " JOIN account ON account.AccountID = customer_payment.AccountID"
                              + " JOIN currency ON currency.CurrencyID = customer_payment.CurrencyID"
                              + " WHERE customer_payment.CustomerPaymentID = @CustomerPaymentID;";
            object parameters = new { CustomerPaymentID };
            return DbManager.DbQuerySingle<CustomerPayment>(handler, sqlCommand, parameters);
        }
        public static void DeletePaymentById(int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            // Comando para actualizar el estado de las facturas que solo tienen este cobro.
            const string sqlCommand1 = "UPDATE (sale_invoice INNER JOIN customer_payment_link ON sale_invoice.SaleInvoiceID = customer_payment_link.SaleInvoiceID)"
                                     + " SET sale_invoice.Status = 'Pendiente' WHERE customer_payment_link.CustomerPaymentID = @CustomerPaymentID"
                                     + " AND sale_invoice.SaleInvoiceID IN (SELECT customer_payment_link.SaleInvoiceID FROM customer_payment_link GROUP BY customer_payment_link.SaleInvoiceID HAVING COUNT(1) = 1);";
            // Comando para actualizar el estado de las ventas que solo tienen este cobro (por medio de factura).
            const string sqlCommand2 = "UPDATE (sale INNER JOIN sale_invoice_link ON sale.SaleID = sale_invoice_link.SaleID INNER JOIN customer_payment_link ON sale_invoice_link.SaleInvoiceID = customer_payment_link.SaleInvoiceID)"
                                     + " SET sale.HasPayments = 0 WHERE customer_payment_link.CustomerPaymentID = @CustomerPaymentID"
                                     + " AND sale.SaleID IN (SELECT sale_invoice_link.SaleID FROM (sale_invoice_link INNER JOIN customer_payment_link ON sale_invoice_link.SaleInvoiceID = customer_payment_link.SaleInvoiceID) GROUP BY sale_invoice_link.SaleID, sale_invoice_link.SaleInvoiceID HAVING COUNT(1) = 1);";
            // Comando para actualizar el estado de las ventas que solo tienen este cobro (por vía directa).
            const string sqlCommand3 = "UPDATE (sale INNER JOIN sale_payment_link ON sale.SaleID = sale_payment_link.SaleID)"
                                     + " SET sale.HasPayments = 0 WHERE sale_payment_link.CustomerPaymentID = @CustomerPaymentID"
                                     + " AND sale.SaleID IN (SELECT sale_payment_link.SaleID FROM sale_payment_link GROUP BY sale_payment_link.SaleID HAVING COUNT(1) = 1);";
            // Comando para eliminar el cobro.
            const string sqlCommand4 = "DELETE FROM customer_payment WHERE CustomerPaymentID = @CustomerPaymentID;";
            object parameters = new { CustomerPaymentID };
            // Ejecución de todos los comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand3, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand4, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand3, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand4, parameters);
            }
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO customer_payment (BusinessID, CustomerID, Date, AccountID, TotalAmount, CurrencyID, AdditionalInformation, IsUnmarked, FechaChequeDiferido)"
                              + " VALUES (@BusinessID, @CustomerID, @Date, @AccountID, @TotalAmount, @CurrencyID, @AdditionalInformation, @IsUnmarked, @FechaChequeDiferido);";
            this.CustomerPaymentID = DbManager.DbInsert(handler, sqlCommand, this);
        }




        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE customer_payment SET"
                              + " BusinessID = @BusinessID,"
                              + " CustomerID = @CustomerID,"
                              + " Date = @Date,"
                              + " AccountID = @AccountID,"
                              + " TotalAmount = @TotalAmount,"
                              + " CurrencyID = @CurrencyID,"
                              + " AdditionalInformation = @AdditionalInformation,"
                              + " IsUnmarked = @IsUnmarked,"
                              + " FechaChequeDiferido = @FechaChequeDiferido"
                              + " WHERE CustomerPaymentID = @CustomerPaymentID;";

            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }


    }
    public class DeliveryNote : DbEntity
    {
        public override int PrimaryKeyID { get { return DeliveryNoteID; } }

        public int DeliveryNoteID { get; set; }
        public int SaleID { get; set; }
        public string Number { get; set; }
        public DateTime PrintingDate { get; set; }
        public string InvoiceType { get; set; }
        public string MaskedInvoiceNumber { get; set; }

        public static List<DeliveryNote> GetDeliveryNotesBySaleId(int SaleID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " delivery_note.DeliveryNoteID,"
                                    + " delivery_note.SaleID,"
                                    + " delivery_note.Number,"
                                    + " delivery_note.PrintingDate,"
                                    + " delivery_note.InvoiceType,"
                                    + " delivery_note.MaskedInvoiceNumber"
                                    + " FROM delivery_note"
                                    + " WHERE delivery_note.SaleID = @SaleID"
                                    + " ORDER BY delivery_note.PrintingDate DESC;";
            object parameters = new { SaleID };
            return DbManager.DbQueryList<DeliveryNote>(handler, sqlCommand, parameters);
        }
        public static DeliveryNote GetDeliveryNoteById(int DeliveryNoteID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " delivery_note.DeliveryNoteID,"
                                    + " delivery_note.SaleID,"
                                    + " delivery_note.Number,"
                                    + " delivery_note.PrintingDate,"
                                    + " delivery_note.InvoiceType,"
                                    + " delivery_note.MaskedInvoiceNumber"
                                    + " FROM delivery_note"
                                    + " WHERE delivery_note.DeliveryNoteID = @DeliveryNoteID;";
            object parameters = new { DeliveryNoteID };
            return DbManager.DbQuerySingle<DeliveryNote>(handler, sqlCommand, parameters);
        }
        public static void DeleteDeliveryNoteById(int DeliveryNoteID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM delivery_note WHERE DeliveryNoteID = @DeliveryNoteID;";
            object parameters = new { DeliveryNoteID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO delivery_note (SaleID, Number, PrintingDate, InvoiceType, MaskedInvoiceNumber)"
                                    + " VALUES (@SaleID, @Number, @PrintingDate, @InvoiceType, @MaskedInvoiceNumber);";
            this.DeliveryNoteID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE delivery_note SET"
                                    + " SaleID = @SaleID,"
                                    + " Number = @Number,"
                                    + " PrintingDate = @PrintingDate,"
                                    + " InvoiceType = @InvoiceType,"
                                    + " MaskedInvoiceNumber = @MaskedInvoiceNumber"
                                    + " WHERE DeliveryNoteID = @DeliveryNoteID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class Department : DbEntity
    {
        public override int PrimaryKeyID { get { return DepartmentID; } }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public static List<Department> GetDepartments(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " department.DepartmentID,"
                                    + " department.DepartmentName"
                                    + " FROM department"
                                    + " ORDER BY department.DepartmentID ASC;";
            return DbManager.DbQueryList<Department>(handler, sqlCommand);
        }
    }
    public class Estimate : DbEntity
    {
        public override int PrimaryKeyID { get { return EstimateID; } }

        public int EstimateID { get; set; }
        public int BusinessID { get; set; }
        public int CustomerID { get; set; }
        public int? ContactID { get; set; }
        public DateTime Date { get; set; }
        public DateTime FechaChequeDiferido { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public int CurrencyID { get; set; }
        public int PaymentID { get; set; }
        public int? DeliveryDelay { get; set; }
        public int WarrantyMonths { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
        public int DepartmentID { get; set; }
        public bool IsUnmarked { get; set; }
        public bool DontTotalize { get; set; }

        // Campos de soporte (no pertenecen a la tabla "estimate")
        public string DepartmentName { get; set; }
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string PaymentName { get; set; }
        public string IsUnmarkedText { get; set; }

        public static List<Estimate> GetEstimates(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " estimate.EstimateID,"
                              + " estimate.BusinessID,"
                              + " estimate.CustomerID,"
                              + " estimate.ContactID,"
                              + " estimate.Date,"
                              + " estimate.Description,"
                              + " estimate.Discount,"
                              + " estimate.TotalBeforeTax,"
                              + " estimate.CurrencyID,"
                              + " estimate.PaymentID,"
                              + " estimate.DeliveryDelay,"
                              + " estimate.WarrantyMonths,"
                              + " estimate.ExpirationDate,"
                              + " estimate.Status,"
                              + " estimate.DepartmentID,"
                              + " estimate.IsUnmarked,"
                              + " estimate.DontTotalize,"
                              + " IF(estimate.IsUnmarked,'N','B') AS IsUnmarkedText,"
                              + " department.DepartmentName,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol,"
                              + " payment.PaymentName"
                              + " FROM estimate"
                              + " INNER JOIN department ON department.DepartmentID = estimate.DepartmentID"
                              + " INNER JOIN business ON business.BusinessID = estimate.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = estimate.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = estimate.CurrencyID"
                              + " INNER JOIN payment ON payment.PaymentID = estimate.PaymentID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<Estimate>(handler, sqlCommand);
        }
        public static List<Estimate> GetEstimatesLight(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " estimate.EstimateID,"
                              + " customer.CustomerName"
                              + " FROM estimate"
                              + " INNER JOIN customer ON customer.CustomerID = estimate.CustomerID"
                              + " ORDER BY estimate.EstimateID DESC;";
            return DbManager.DbQueryList<Estimate>(handler, sqlCommand);
        }
        public static List<Estimate> GetAvailableEstimates(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " estimate.EstimateID,"
                              + " customer.CustomerName"
                              + " FROM estimate"
                              + " INNER JOIN customer ON customer.CustomerID = estimate.CustomerID"
                              + " WHERE (estimate.Status = 'Activo' OR estimate.Status = 'Vendido') AND estimate.TotalBeforeTax > 0"
                              + " ORDER BY estimate.EstimateID DESC;";
            return DbManager.DbQueryList<Estimate>(handler, sqlCommand);
        }
        public static List<Estimate> GetEstimatesByCustomerId(int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " estimate.EstimateID,"
                              + " estimate.BusinessID,"
                              + " estimate.CustomerID,"
                              + " estimate.ContactID,"
                              + " estimate.Date,"
                              + " estimate.Description,"
                              + " estimate.Discount,"
                              + " estimate.TotalBeforeTax,"
                              + " estimate.CurrencyID,"
                              + " estimate.PaymentID,"
                              + " estimate.DeliveryDelay,"
                              + " estimate.WarrantyMonths,"
                              + " estimate.ExpirationDate,"
                              + " estimate.Status,"
                              + " estimate.DepartmentID,"
                              + " estimate.IsUnmarked,"
                              + " estimate.DontTotalize,"
                              + " department.DepartmentName,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol,"
                              + " payment.PaymentName"
                              + " FROM estimate"
                              + " INNER JOIN department ON department.DepartmentID = estimate.DepartmentID"
                              + " INNER JOIN business ON business.BusinessID = estimate.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = estimate.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = estimate.CurrencyID"
                              + " INNER JOIN payment ON payment.PaymentID = estimate.PaymentID"
                              + " WHERE estimate.CustomerID = @CustomerID"
                              + " ORDER BY estimate.EstimateID DESC;";
            object parameters = new { CustomerID };
            return DbManager.DbQueryList<Estimate>(handler, sqlCommand, parameters);
        }
        public static Estimate GetEstimateById(int EstimateID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " estimate.EstimateID,"
                              + " estimate.BusinessID,"
                              + " estimate.CustomerID,"
                              + " estimate.ContactID,"
                              + " estimate.Date,"
                              + " estimate.Description,"
                              + " estimate.Discount,"
                              + " estimate.TotalBeforeTax,"
                              + " estimate.CurrencyID,"
                              + " estimate.PaymentID,"
                              + " estimate.DeliveryDelay,"
                              + " estimate.WarrantyMonths,"
                              + " estimate.ExpirationDate,"
                              + " estimate.Status,"
                              + " estimate.DepartmentID,"
                              + " estimate.IsUnmarked,"
                              + " estimate.DontTotalize,"
                              + " department.DepartmentName,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol,"
                              + " payment.PaymentName"
                              + " FROM estimate"
                              + " INNER JOIN department ON department.DepartmentID = estimate.DepartmentID"
                              + " INNER JOIN business ON business.BusinessID = estimate.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = estimate.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = estimate.CurrencyID"
                              + " INNER JOIN payment ON payment.PaymentID = estimate.PaymentID"
                              + " WHERE estimate.EstimateID = @EstimateID;";
            object parameters = new { EstimateID };
            return DbManager.DbQuerySingle<Estimate>(handler, sqlCommand, parameters);
        }
        public static void UpdateStatus(int EstimateID, string Status, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE estimate SET Status = @Status WHERE EstimateID = @EstimateID;";
            object parameters = new { EstimateID, Status };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateStatusSingleLinkBySaleId(int SaleID, string Status, DbTransactionHandler handler = null)
        {
            // Actualiza los presupuestos asociados únicamente a la venta especificada (no asi para los presupuestos con mas de una venta asociada).
            const string sqlCommand = "UPDATE (estimate INNER JOIN sale ON sale.EstimateID = estimate.EstimateID)"
                                    + " SET estimate.Status = @Status WHERE sale.SaleID = @SaleID"
                                    + " AND estimate.EstimateID IN (SELECT sale.EstimateID FROM sale GROUP BY sale.EstimateID HAVING COUNT(1) = 1);";
            object parameters = new { SaleID, Status };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void DeleteEstimateById(int EstimateID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM estimate WHERE EstimateID = @EstimateID;";
            object parameters = new { EstimateID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO estimate (BusinessID, CustomerID, ContactID, Date, Description,"
                                    + " Discount, TotalBeforeTax, CurrencyID, PaymentID, DeliveryDelay, WarrantyMonths,"
                                    + " ExpirationDate, Status, DepartmentID, IsUnmarked, DontTotalize) VALUES (@BusinessID, @CustomerID, @ContactID,"
                                    + " @Date, @Description, @Discount, @TotalBeforeTax, @CurrencyID, @PaymentID,"
                                    + " @DeliveryDelay, @WarrantyMonths, @ExpirationDate, @Status, @DepartmentID, @IsUnmarked, @DontTotalize);";
            this.EstimateID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE estimate SET"
                                    + " BusinessID = @BusinessID,"
                                    + " CustomerID = @CustomerID,"
                                    + " ContactID = @ContactID,"
                                    + " Date = @Date,"
                                    + " Description = @Description,"
                                    + " Discount = @Discount,"
                                    + " TotalBeforeTax = @TotalBeforeTax,"
                                    + " CurrencyID = @CurrencyID,"
                                    + " PaymentID = @PaymentID,"
                                    + " DeliveryDelay = @DeliveryDelay,"
                                    + " WarrantyMonths = @WarrantyMonths,"
                                    + " ExpirationDate = @ExpirationDate,"
                                    + " Status = @Status,"
                                    + " DepartmentID = @DepartmentID,"
                                    + " IsUnmarked = @IsUnmarked,"
                                    + " DontTotalize = @DontTotalize"
                                    + " WHERE EstimateID = @EstimateID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    [Serializable]
    public class EstimateItem : DbEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override int PrimaryKeyID { get { return ItemID; } }

        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            set
            {
                _ItemID = value;
                OnPropertyChanged("ItemID");
            }
        }
        public int EstimateID
        {
            get
            {
                return _EstimateID;
            }
            set
            {
                _EstimateID = value;
                OnPropertyChanged("EstimateID");
            }
        }
        public int ItemNumber
        {
            get
            {
                return _ItemNumber;
            }
            set
            {
                _ItemNumber = value;
                OnPropertyChanged("ItemNumber");
            }
        }
        public int? ProductID
        {
            get
            {
                return _ProductID;
            }
            set
            {
                _ProductID = value;
                OnPropertyChanged("ProductID");
            }
        }
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
        }
        public int? DeliveryDelay
        {
            get
            {
                return _DeliveryDelay;
            }
            set
            {
                _DeliveryDelay = value;
                OnPropertyChanged("DeliveryDelay");
            }
        }
        public decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public decimal TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }
        public int VatID
        {
            get
            {
                return _VatID;
            }
            set
            {
                _VatID = value;
                OnPropertyChanged("VatID");
            }
        }
        public byte[] CustomImage
        {
            get
            {
                return _CustomImage;
            }
            set
            {
                _CustomImage = value;
                OnPropertyChanged("Image");
            }
        }

        // Campos de soporte (no pertenecen a la tabla "estimate_item").
        public decimal VatPercentage
        {
            get
            {
                return _VatPercentage;
            }
            set
            {
                _VatPercentage = value;
                OnPropertyChanged("VatPercentage");
            }
        }
        public byte[] ProductImage
        {
            get
            {
                return _ProductImage;
            }
            set
            {
                _ProductImage = value;
                OnPropertyChanged("Image");
            }
        }

        private int _ItemID;
        private int _EstimateID;
        private int _ItemNumber;
        private int? _ProductID;
        private string _Description;
        private int? _DeliveryDelay;
        private decimal _Quantity;
        private decimal _Amount;
        private decimal _TotalAmount;
        private int _VatID;
        private byte[] _CustomImage;
        private decimal _VatPercentage;
        private byte[] _ProductImage;

        public static List<EstimateItem> GetItemsByEstimateId(int EstimateID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " estimate_item.ItemID,"
                                    + " estimate_item.EstimateID,"
                                    + " estimate_item.ItemNumber,"
                                    + " estimate_item.ProductID,"
                                    + " estimate_item.Description,"
                                    + " estimate_item.DeliveryDelay,"
                                    + " estimate_item.Quantity,"
                                    + " estimate_item.Amount,"
                                    + " estimate_item.TotalAmount,"
                                    + " estimate_item.VatID,"
                                    + " estimate_item.CustomImage,"
                                    + " vat.VatPercentage,"
                                    + " product.ProductImage"
                                    + " FROM estimate_item"
                                    + " INNER JOIN vat ON vat.VatID = estimate_item.VatID"
                                    + " LEFT JOIN product ON product.ProductID = estimate_item.ProductID"
                                    + " WHERE estimate_item.EstimateID = @EstimateID"
                                    + " ORDER BY estimate_item.ItemNumber;";
            object parameters = new { EstimateID };
            return DbManager.DbQueryList<EstimateItem>(handler, sqlCommand, parameters);
        }
        public static List<EstimateItem> GetItemsByCustomerId(int CustomerID, DateTime StartDate, DateTime EndDate, int MaxItems, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " estimate_item.ItemID,"
                                    + " estimate_item.EstimateID,"
                                    + " estimate_item.ItemNumber,"
                                    + " estimate_item.ProductID,"
                                    + " estimate_item.Description,"
                                    + " estimate_item.DeliveryDelay,"
                                    + " estimate_item.Quantity,"
                                    + " estimate_item.Amount,"
                                    + " estimate_item.TotalAmount,"
                                    + " estimate_item.VatID,"
                                    + " estimate_item.CustomImage,"
                                    + " vat.VatPercentage,"
                                    + " product.ProductImage"
                                    + " FROM estimate_item"
                                    + " INNER JOIN estimate ON estimate.EstimateID = estimate_item.EstimateID"
                                    + " INNER JOIN vat ON vat.VatID = estimate_item.VatID"
                                    + " LEFT JOIN product ON product.ProductID = estimate_item.ProductID"
                                    + " WHERE estimate.CustomerID = @CustomerID"
                                    + " AND estimate.Date >= @StartDate"
                                    + " AND estimate.Date <= @EndDate"
                                    + " ORDER BY estimate.Date DESC"
                                    + " LIMIT @MaxItems;";
            object parameters = new { CustomerID, StartDate, EndDate, MaxItems };
            return DbManager.DbQueryList<EstimateItem>(handler, sqlCommand, parameters);
        }
        public static void DeleteItemsByEstimateId(int EstimateID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM estimate_item WHERE EstimateID = @EstimateID;";
            object parameters = new { EstimateID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO estimate_item (EstimateID, ItemNumber, ProductID, Description, DeliveryDelay,"
                                    + " Quantity, Amount, TotalAmount, VatID, CustomImage) VALUES (@EstimateID, @ItemNumber,"
                                    + " @ProductID, @Description, @DeliveryDelay, @Quantity, @Amount, @TotalAmount, @VatID,"
                                    + " @CustomImage);";
            this.ItemID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public EstimateItem CopyItem()
        {
            return new EstimateItem()
            {
                ProductID = _ProductID,
                Description = _Description,
                DeliveryDelay = _DeliveryDelay,
                Quantity = _Quantity,
                Amount = _Amount,
                TotalAmount = _TotalAmount,
                VatID = _VatID,
                CustomImage = _CustomImage,
                VatPercentage = _VatPercentage,
                ProductImage = _ProductImage
            };
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    public class Expense : DbEntity
    {
        public override int PrimaryKeyID { get { return ExpenseID; } }

        public int ExpenseID { get; set; }
        public DateTime Date { get; set; }
        public int CategoryID { get; set; }
        public int SubcategoryID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyID { get; set; }
        public string InvoiceNumber { get; set; }

        // Campos de soporte (no pertenecen a la tabla "expense")
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string CurrencySymbol { get; set; }

        public static List<Expense> GetExpenses(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " expense.ExpenseID,"
                              + " expense.Date,"
                              + " expense.CategoryID,"
                              + " expense.SubcategoryID,"
                              + " expense.Description,"
                              + " expense.Amount,"
                              + " expense.CurrencyID,"
                              + " expense.InvoiceNumber,"
                              + " item_category.CategoryName,"
                              + " item_subcategory.SubcategoryName,"
                              + " currency.CurrencySymbol"
                              + " FROM expense"
                              + " INNER JOIN item_category ON item_category.CategoryID = expense.CategoryID"
                              + " INNER JOIN item_subcategory ON item_subcategory.SubcategoryID = expense.SubcategoryID"
                              + " INNER JOIN currency ON currency.CurrencyID = expense.CurrencyID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<Expense>(handler, sqlCommand);
        }
        public static Expense GetExpenseById(int ExpenseID, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " expense.ExpenseID,"
                              + " expense.Date,"
                              + " expense.CategoryID,"
                              + " expense.SubcategoryID,"
                              + " expense.Description,"
                              + " expense.Amount,"
                              + " expense.CurrencyID,"
                              + " expense.InvoiceNumber,"
                              + " item_category.CategoryName,"
                              + " item_subcategory.SubcategoryName,"
                              + " currency.CurrencySymbol"
                              + " FROM expense"
                              + " INNER JOIN item_category ON item_category.CategoryID = expense.CategoryID"
                              + " INNER JOIN item_subcategory ON item_subcategory.SubcategoryID = expense.SubcategoryID"
                              + " INNER JOIN currency ON currency.CurrencyID = expense.CurrencyID"
                              + " WHERE expense.ExpenseID = @ExpenseID;";
            object parameters = new { ExpenseID };
            return DbManager.DbQuerySingle<Expense>(handler, sqlCommand, parameters);
        }
        public static void DeleteExpenseById(int ExpenseID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM expense WHERE ExpenseID = @ExpenseID;";
            object parameters = new { ExpenseID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO expense (Date, CategoryID, SubcategoryID, Description, Amount, CurrencyID, InvoiceNumber)"
                                    + " VALUES (@Date, @CategoryID, @SubcategoryID, @Description, @Amount, @CurrencyID, @InvoiceNumber);";
            this.ExpenseID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE expense SET Date = @Date, CategoryID = @CategoryID, SubcategoryID = @SubcategoryID, Description = @Description,"
                                    + " Amount = @Amount, CurrencyID = @CurrencyID, InvoiceNumber = @InvoiceNumber WHERE ExpenseID = @ExpenseID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class Input : DbEntity
    {
        public override int PrimaryKeyID { get { return InputID; } }

        public int InputID { get; set; }
        public int CategoryID { get; set; }
        public int SubcategoryID { get; set; }
        public string Description { get; set; }

        // Campos de soporte (no pertenecen a la tabla "input").
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }

        public static List<Input> GetInputs(string viewSettingsString, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " input.InputID,"
                              + " input.CategoryID,"
                              + " input.SubcategoryID,"
                              + " input.Description,"
                              + " item_category.CategoryName,"
                              + " item_subcategory.SubcategoryName"
                              + " FROM input"
                              + " INNER JOIN item_category ON item_category.CategoryID = input.CategoryID"
                              + " INNER JOIN item_subcategory ON item_subcategory.SubcategoryID = input.SubcategoryID"
                              + viewSettingsString
                              + $" LIMIT {DbLayerSettings.DefaultRecordLimit};";
            return DbManager.DbQueryList<Input>(handler, sqlCommand);
        }
        public static List<Input> GetInputsByDescription(string Description, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " input.InputID,"
                                    + " input.CategoryID,"
                                    + " input.SubcategoryID,"
                                    + " input.Description,"
                                    + " item_category.CategoryName,"
                                    + " item_subcategory.SubcategoryName"
                                    + " FROM input"
                                    + " INNER JOIN item_category ON item_category.CategoryID = input.CategoryID"
                                    + " INNER JOIN item_subcategory ON item_subcategory.SubcategoryID = input.SubcategoryID"
                                    + " WHERE input.Description LIKE @DescriptionPattern"
                                    + " ORDER BY item_category.CategoryName ASC, item_subcategory.SubcategoryName ASC;";
            object parameters = new { DescriptionPattern = $"%{Description}%" };
            return DbManager.DbQueryList<Input>(handler, sqlCommand, parameters);
        }
        public static List<int> GetInputsIds(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT InputID FROM input ORDER BY InputID ASC;";
            var result = DbManager.DbQueryList<dynamic>(handler, sqlCommand);
            return result.Select(x => (int)x.InputID).ToList();
        }
        public static Input GetInputById(int InputID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " input.InputID,"
                                    + " input.CategoryID,"
                                    + " input.SubcategoryID,"
                                    + " input.Description,"
                                    + " item_category.CategoryName,"
                                    + " item_subcategory.SubcategoryName"
                                    + " FROM input"
                                    + " INNER JOIN item_category ON item_category.CategoryID = input.CategoryID"
                                    + " INNER JOIN item_subcategory ON item_subcategory.SubcategoryID = input.SubcategoryID"
                                    + " WHERE input.InputID = @InputID;";
            object parameters = new { InputID };
            return DbManager.DbQuerySingle<Input>(handler, sqlCommand, parameters);
        }
        public static void DeleteInputById(int InputID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM input WHERE InputID = @InputID;";
            object parameters = new { InputID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO input (CategoryID, SubcategoryID, Description) VALUES (@CategoryID, @SubcategoryID, @Description);";
            this.InputID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE input SET CategoryID = @CategoryID, SubcategoryID = @SubcategoryID, Description = @Description"
                                    + " WHERE InputID = @InputID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class IssuedNote : DbEntity
    {
        public override int PrimaryKeyID { get { return NoteID; } }

        public int NoteID { get; set; }
        public int BusinessID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public bool IsDebit { get; set; }
        public string NoteType { get; set; }
        public string NoteNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int CurrencyID { get; set; }
        public string Reason { get; set; }

        // Campos de soporte (no pertenecen a la tabla "issued_note")
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }
        public string CurrencySymbol { get; set; }
        public string IsDebitText { get; set; }

        public static List<IssuedNote> GetIssuedNotes(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " issued_note.NoteID,"
                              + " issued_note.BusinessID,"
                              + " issued_note.CustomerID,"
                              + " issued_note.Date,"
                              + " issued_note.IsDebit,"
                              + " issued_note.NoteType,"
                              + " issued_note.NoteNumber,"
                              + " issued_note.TotalAmount,"
                              + " issued_note.CurrencyID,"
                              + " issued_note.Reason,"
                              + " IF(issued_note.IsDebit,'Debito','Credito') AS IsDebitText,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencySymbol"
                              + " FROM issued_note"
                              + " INNER JOIN business ON business.BusinessID = issued_note.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = issued_note.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = issued_note.CurrencyID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<IssuedNote>(handler, sqlCommand);
        }
        public static List<IssuedNote> GetIssuedNotesByCustomerID(int CustomerID, int CurrencyID, DateTime startDate, DateTime endDate, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " issued_note.NoteID,"
                              + " issued_note.BusinessID,"
                              + " issued_note.CustomerID,"
                              + " issued_note.Date,"
                              + " issued_note.IsDebit,"
                              + " issued_note.NoteType,"
                              + " issued_note.NoteNumber,"
                              + " issued_note.TotalAmount,"
                              + " issued_note.CurrencyID,"
                              + " issued_note.Reason,"
                              + " IF(issued_note.IsDebit,'Debito','Credito') AS IsDebitText,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencySymbol"
                              + " FROM issued_note"
                              + " INNER JOIN business ON business.BusinessID = issued_note.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = issued_note.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = issued_note.CurrencyID"
                              + " WHERE issued_note.CustomerID = @CustomerID"
                              + " AND issued_note.CurrencyID = @CurrencyID"
                              + " AND issued_note.Date >= @startDate"
                              + " AND issued_note.Date <= @endDate"
                              + " ORDER BY issued_note.Date ASC;";
            object parameters = new { CustomerID, CurrencyID, startDate, endDate };
            return DbManager.DbQueryList<IssuedNote>(handler, sqlCommand, parameters);
        }
        public static IssuedNote GetNoteById(int NoteID, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " issued_note.NoteID,"
                              + " issued_note.BusinessID,"
                              + " issued_note.CustomerID,"
                              + " issued_note.Date,"
                              + " issued_note.IsDebit,"
                              + " issued_note.NoteType,"
                              + " issued_note.NoteNumber,"
                              + " issued_note.TotalAmount,"
                              + " issued_note.CurrencyID,"
                              + " issued_note.Reason,"
                              + " IF(issued_note.IsDebit,'Debito','Credito') AS IsDebitText,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencySymbol"
                              + " FROM issued_note"
                              + " INNER JOIN business ON business.BusinessID = issued_note.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = issued_note.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = issued_note.CurrencyID"
                              + " WHERE issued_note.NoteID = @NoteID;";
            object parameters = new { NoteID };
            return DbManager.DbQuerySingle<IssuedNote>(handler, sqlCommand, parameters);
        }
        public static decimal GetTotalDebitByCustomerId(int CustomerID, int CurrencyID, DateTime endDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " SUM(issued_note.TotalAmount)"
                                    + " FROM issued_note"
                                    + " WHERE issued_note.IsDebit = TRUE"
                                    + " AND issued_note.CustomerID = @CustomerID"
                                    + " AND issued_note.CurrencyID = @CurrencyID"
                                    + " AND issued_note.Date <= @endDate;";
            object parameters = new { CustomerID, CurrencyID, endDate };
            return DbManager.DbExecuteScalar<decimal>(handler, sqlCommand, parameters);
        }
        public static decimal GetTotalCreditByCustomerId(int CustomerID, int CurrencyID, DateTime endDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " SUM(issued_note.TotalAmount)"
                                    + " FROM issued_note"
                                    + " WHERE issued_note.IsDebit = FALSE"
                                    + " AND issued_note.CustomerID = @CustomerID"
                                    + " AND issued_note.CurrencyID = @CurrencyID"
                                    + " AND issued_note.Date <= @endDate;";
            object parameters = new { CustomerID, CurrencyID, endDate };
            return DbManager.DbExecuteScalar<decimal>(handler, sqlCommand, parameters);
        }
        public static bool CheckNoteNumberDuplicates(string NoteNumber, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT EXISTS(SELECT 1 FROM issued_note WHERE NoteNumber = @NoteNumber);";
            object parameters = new { NoteNumber };
            return DbManager.DbExecuteScalar<bool>(handler, sqlCommand, parameters);
        }
        public static void DeleteNoteById(int NoteID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM issued_note WHERE NoteID = @NoteID;";
            object parameters = new { NoteID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO issued_note (BusinessID, CustomerID, Date, IsDebit, NoteType, NoteNumber, TotalAmount, CurrencyID, Reason)"
                                    + " VALUES (@BusinessID, @CustomerID, @Date, @IsDebit, @NoteType, @NoteNumber, @TotalAmount, @CurrencyID, @Reason);";
            this.NoteID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE issued_note SET"
                                    + " BusinessID = @BusinessID,"
                                    + " CustomerID = @CustomerID,"
                                    + " Date = @Date,"
                                    + " IsDebit = @IsDebit,"
                                    + " NoteType = @NoteType,"
                                    + " NoteNumber = @NoteNumber,"
                                    + " TotalAmount = @TotalAmount,"
                                    + " CurrencyID = @CurrencyID,"
                                    + " Reason = @Reason"
                                    + " WHERE NoteID = @NoteID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class ItemCategory : DbEntity
    {
        public override int PrimaryKeyID { get { return CategoryID; } }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public static List<ItemCategory> GetCategories(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT CategoryID, CategoryName FROM item_category ORDER BY CategoryName ASC;";
            return DbManager.DbQueryList<ItemCategory>(handler, sqlCommand);
        }
    }
    public class ItemSubcategory : DbEntity
    {
        public override int PrimaryKeyID { get { return SubcategoryID; } }

        public int SubcategoryID { get; set; }
        public int CategoryID { get; set; }
        public string SubcategoryName { get; set; }

        public static List<ItemSubcategory> GetSubcategories(int CategoryID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT SubcategoryID, CategoryID, SubcategoryName FROM item_subcategory WHERE CategoryID = @CategoryID ORDER BY SubcategoryName ASC;";
            object parameters = new { CategoryID };
            return DbManager.DbQueryList<ItemSubcategory>(handler, sqlCommand, parameters);
        }
    }
    public class MailServer : DbEntity
    {
        public override int PrimaryKeyID { get { return MailServerID; } }

        public int MailServerID { get; set; }
        public int BusinessID { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpServerPort { get; set; }
        public bool SmtpServerEnableSsl { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }

        public static MailServer GetMailServerByBusinessId(int BusinessID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " mail_server.MailServerID,"
                                    + " mail_server.BusinessID,"
                                    + " mail_server.SmtpServer,"
                                    + " mail_server.SmtpServerPort,"
                                    + " mail_server.SmtpServerEnableSsl,"
                                    + " mail_server.EmailAddress,"
                                    + " mail_server.EmailPassword"
                                    + " FROM mail_server"
                                    + " WHERE mail_server.BusinessID = @BusinessID;";
            object parameters = new { BusinessID };
            return DbManager.DbQuerySingle<MailServer>(handler, sqlCommand, parameters);
        }

        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE mail_server SET SmtpServer = @SmtpServer, SmtpServerPort = @SmtpServerPort, SmtpServerEnableSsl = @SmtpServerEnableSsl,"
                                    + " EmailAddress = @EmailAddress, EmailPassword = @EmailPassword WHERE BusinessID = @BusinessID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class MailSetting : DbEntity
    {
        public override int PrimaryKeyID { get { return MailSettingID; } }

        public int MailSettingID { get; set; }
        public int UserID { get; set; }
        public int BusinessID { get; set; }
        public string CopyToEmailAddress { get; set; }
        public string EstimateEmailSubject { get; set; }
        public string EstimateEmailBody { get; set; }
        public string ReminderEmailSubject { get; set; }
        public string ReminderEmailBody { get; set; }
        public string PurchaseOrderEmailSubject { get; set; }
        public string PurchaseOrderEmailBody { get; set; }
        public byte[] SignatureLogo { get; set; }


        public void Insert()
        {
            using (var connection = new MySqlConnection("tu_cadena_de_conexion"))
            {
                connection.Open();
                string query = "INSERT INTO mail_setting (UserID, BusinessID, CopyToEmailAddress, EstimateEmailSubject, " +
                    "EstimateEmailBody, ReminderEmailSubject, ReminderEmailBody, PurchaseOrderEmailSubject, PurchaseOrderEmailBody) " +
                    "VALUES (@UserID, @BusinessID, @CopyToEmailAddress, @EstimateEmailSubject, @EstimateEmailBody, " +
                    "@ReminderEmailSubject, @ReminderEmailBody, @PurchaseOrderEmailSubject, @PurchaseOrderEmailBody)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@BusinessID", BusinessID);
                    command.Parameters.AddWithValue("@CopyToEmailAddress", CopyToEmailAddress);
                    command.Parameters.AddWithValue("@EstimateEmailSubject", EstimateEmailSubject);
                    command.Parameters.AddWithValue("@EstimateEmailBody", EstimateEmailBody);
                    command.Parameters.AddWithValue("@ReminderEmailSubject", ReminderEmailSubject);
                    command.Parameters.AddWithValue("@ReminderEmailBody", ReminderEmailBody);
                    command.Parameters.AddWithValue("@PurchaseOrderEmailSubject", PurchaseOrderEmailSubject);
                    command.Parameters.AddWithValue("@PurchaseOrderEmailBody", PurchaseOrderEmailBody);

                    command.ExecuteNonQuery();
                }
            }
        }


        public static MailSetting GetMailSettingByUserIdAndBusinessId(int UserID, int BusinessID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " mail_setting.MailSettingID,"
                                    + " mail_setting.UserID,"
                                    + " mail_setting.BusinessID,"
                                    + " mail_setting.CopyToEmailAddress,"
                                    + " mail_setting.EstimateEmailSubject,"
                                    + " mail_setting.EstimateEmailBody,"
                                    + " mail_setting.ReminderEmailSubject,"
                                    + " mail_setting.ReminderEmailBody,"
                                    + " mail_setting.PurchaseOrderEmailSubject,"
                                    + " mail_setting.PurchaseOrderEmailBody,"
                                    + " mail_setting.SignatureLogo"
                                    + " FROM mail_setting"
                                    + " WHERE mail_setting.UserID = @UserID AND mail_setting.BusinessID = @BusinessID;";
            object parameteres = new { UserID, BusinessID };
            return DbManager.DbQuerySingle<MailSetting>(handler, sqlCommand, parameteres);
        }

        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE mail_setting SET CopyToEmailAddress = @CopyToEmailAddress,"
                                    + " EstimateEmailSubject = @EstimateEmailSubject, EstimateEmailBody = @EstimateEmailBody,"
                                    + " ReminderEmailSubject = @ReminderEmailSubject, ReminderEmailBody = @ReminderEmailBody,"
                                    + " PurchaseOrderEmailSubject = @PurchaseOrderEmailSubject, PurchaseOrderEmailBody = @PurchaseOrderEmailBody,"
                                    + " SignatureLogo = @SignatureLogo WHERE UserID = @UserID AND BusinessID = @BusinessID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class MotorType : DbEntity
    {
        public override int PrimaryKeyID { get { return MotorTypeID; } }

        public int MotorTypeID { get; set; }
        public string MotorTypeName { get; set; }

        public static List<MotorType> GetMotorTypes(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT MotorTypeID, MotorTypeName FROM motor_type ORDER BY MotorTypeID ASC;";
            return DbManager.DbQueryList<MotorType>(handler, sqlCommand);
        }
    }
    public class Payment : DbEntity
    {
        public override int PrimaryKeyID { get { return PaymentID; } }

        public int PaymentID { get; set; }
        public string PaymentName { get; set; }

        public static List<Payment> GetPayments(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " payment.PaymentID,"
                                    + " payment.PaymentName"
                                    + " FROM payment"
                                    + " ORDER BY payment.PaymentID ASC;";
            return DbManager.DbQueryList<Payment>(handler, sqlCommand);
        }
    }

    public class Account : DbEntity
    {
        public override int PrimaryKeyID { get { return AccountID; } }

        public int AccountID { get; set; }
        public string AccountName { get; set; }

        public static List<Account> GetAccounts(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " account.AccountID,"
                                    + " account.AccountName"
                                    + " FROM account"
                                    + " ORDER BY account.AccountID ASC;";
            return DbManager.DbQueryList<Account>(handler, sqlCommand);
        }
    }

    public class PayOrder : DbEntity
    {
        public override int PrimaryKeyID { get { return PayOrderID; } }

        public int PayOrderID { get; set; }
        public int BusinessID { get; set; }
        public DateTime Date { get; set; }
        public int ProviderID { get; set; }

        // Campos de soporte (no pertenecen a la tabla "pay_order").
        public string BusinessName { get; set; }
        public string ProviderName { get; set; }

        public static List<PayOrder> GetPayOrders(string viewSettingsString, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " pay_order.PayOrderID,"
                              + " pay_order.BusinessID,"
                              + " pay_order.Date,"
                              + " pay_order.ProviderID,"
                              + " business.BusinessName,"
                              + " provider.ProviderName"
                              + " FROM pay_order"
                              + " INNER JOIN business ON business.BusinessID = pay_order.BusinessID"
                              + " INNER JOIN provider ON provider.ProviderID = pay_order.ProviderID"
                              + viewSettingsString
                              + $" LIMIT {DbLayerSettings.DefaultRecordLimit};";
            return DbManager.DbQueryList<PayOrder>(handler, sqlCommand);
        }
        public static PayOrder GetPayOrderById(int PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " pay_order.PayOrderID,"
                                    + " pay_order.BusinessID,"
                                    + " pay_order.Date,"
                                    + " pay_order.ProviderID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName"
                                    + " FROM pay_order"
                                    + " INNER JOIN business ON business.BusinessID = pay_order.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = pay_order.ProviderID"
                                    + " WHERE pay_order.PayOrderID = @PayOrderID;";
            object parameters = new { PayOrderID };
            return DbManager.DbQuerySingle<PayOrder>(handler, sqlCommand, parameters);
        }
        public static void DeletePayOrderById(int PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM pay_order WHERE PayOrderID = @PayOrderID;";
            object parameters = new { PayOrderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO pay_order (BusinessID, Date, ProviderID) VALUES (@BusinessID, @Date, @ProviderID);";
            this.PayOrderID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE pay_order SET BusinessID = @BusinessID, Date = @Date, ProviderID = @ProviderID WHERE PayOrderID = @PayOrderID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class PayOrderPayment : DbEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override int PrimaryKeyID { get { return PayOrderPaymentID; } }

        public int PayOrderPaymentID
        {
            get
            {
                return _PayOrderPaymentID;
            }
            set
            {
                _PayOrderPaymentID = value;
                OnPropertyChanged("PayOrderPaymentID");
            }
        }
        public int PayOrderID
        {
            get
            {
                return _PayOrderID;
            }
            set
            {
                _PayOrderID = value;
                OnPropertyChanged("PayOrderID");
            }
        }
        public int PaymentID
        {
            get
            {
                return _PaymentID;
            }
            set
            {
                _PaymentID = value;
                OnPropertyChanged("PaymentID");
            }
        }
        public decimal TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }
        public int CurrencyID
        {
            get
            {
                return _CurrencyID;
            }
            set
            {
                _CurrencyID = value;
                OnPropertyChanged("CurrencyID");
            }
        }
        public string AdditionalInformation
        {
            get
            {
                return _AdditionalInformation;
            }
            set
            {
                OnPropertyChanged("AdditionalInformation");
                _AdditionalInformation = value;
            }
        }

        // Campos de soporte (no pertenecen a la tabla "pay_order_payment").
        public string CurrencySymbol
        {
            get
            {
                return _CurrencySymbol;
            }
            set
            {
                _CurrencySymbol = value;
                OnPropertyChanged("CurrencySymbol");
            }
        }
        public string PaymentName
        {
            get
            {
                return _PaymentName;
            }
            set
            {
                _PaymentName = value;
                OnPropertyChanged("PaymentName");
            }
        }
        public string ProviderName
        {
            get
            {
                return _ProviderName;
            }
            set
            {
                _ProviderName = value;
                OnPropertyChanged("ProviderName");
            }
        }
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
                OnPropertyChanged("Date");
            }
        }
        public string BusinessName
        {
            get
            {
                return _BusinessName;
            }
            set
            {
                _BusinessName = value;
                OnPropertyChanged("BusinessName");
            }
        }

        private int _PayOrderPaymentID;
        private int _PayOrderID;
        private int _PaymentID;
        private decimal _TotalAmount;
        private int _CurrencyID;
        private string _AdditionalInformation;
        private string _CurrencySymbol;
        private string _PaymentName;
        private string _ProviderName;
        private DateTime _Date;
        private string _BusinessName;

        public static List<PayOrderPayment> GetPayments(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " pay_order_payment.PayOrderPaymentID,"
                              + " pay_order_payment.PayOrderID,"
                              + " pay_order_payment.PaymentID,"
                              + " pay_order_payment.TotalAmount,"
                              + " pay_order_payment.CurrencyID,"
                              + " pay_order_payment.AdditionalInformation,"
                              + " currency.CurrencySymbol,"
                              + " payment.PaymentName,"
                              + " provider.ProviderName,"
                              + " pay_order.Date,"
                              + " business.BusinessName"
                              + " FROM pay_order_payment"
                              + " INNER JOIN pay_order ON pay_order.PayOrderID = pay_order_payment.PayOrderID"
                              + " INNER JOIN currency ON currency.CurrencyID = pay_order_payment.CurrencyID"
                              + " INNER JOIN payment ON payment.PaymentID = pay_order_payment.PaymentID"
                              + " INNER JOIN provider ON provider.ProviderID = pay_order.ProviderID"
                              + " INNER JOIN business ON business.BusinessID = pay_order.BusinessID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<PayOrderPayment>(handler, sqlCommand);
        }
        public static List<PayOrderPayment> GetPaymentsByProviderId(int ProviderID, int CurrencyID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " pay_order_payment.PayOrderPaymentID,"
                                    + " pay_order_payment.PayOrderID,"
                                    + " pay_order_payment.PaymentID,"
                                    + " pay_order_payment.TotalAmount,"
                                    + " pay_order_payment.CurrencyID,"
                                    + " pay_order_payment.AdditionalInformation,"
                                    + " currency.CurrencySymbol,"
                                    + " payment.PaymentName,"
                                    + " provider.ProviderName,"
                                    + " pay_order.Date,"
                                    + " business.BusinessName"
                                    + " FROM pay_order_payment"
                                    + " INNER JOIN pay_order ON pay_order.PayOrderID = pay_order_payment.PayOrderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = pay_order_payment.CurrencyID"
                                    + " INNER JOIN payment ON payment.PaymentID = pay_order_payment.PaymentID"
                                    + " INNER JOIN provider ON provider.ProviderID = pay_order.ProviderID"
                                    + " INNER JOIN business ON business.BusinessID = pay_order.BusinessID"
                                    + " WHERE pay_order.ProviderID = @ProviderID AND pay_order_payment.CurrencyID = @CurrencyID"
                                    + " ORDER BY pay_order.Date DESC;";
            object parameters = new { ProviderID, CurrencyID };
            return DbManager.DbQueryList<PayOrderPayment>(handler, sqlCommand, parameters);
        }
        public static List<PayOrderPayment> GetPaymentsByPayOrderId(int PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " pay_order_payment.PayOrderPaymentID,"
                                    + " pay_order_payment.PayOrderID,"
                                    + " pay_order_payment.PaymentID,"
                                    + " pay_order_payment.TotalAmount,"
                                    + " pay_order_payment.CurrencyID,"
                                    + " pay_order_payment.AdditionalInformation,"
                                    + " currency.CurrencySymbol,"
                                    + " payment.PaymentName,"
                                    + " provider.ProviderName,"
                                    + " pay_order.Date,"
                                    + " business.BusinessName"
                                    + " FROM pay_order_payment"
                                    + " INNER JOIN pay_order ON pay_order.PayOrderID = pay_order_payment.PayOrderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = pay_order_payment.CurrencyID"
                                    + " INNER JOIN payment ON payment.PaymentID = pay_order_payment.PaymentID"
                                    + " INNER JOIN provider ON provider.ProviderID = pay_order.ProviderID"
                                    + " INNER JOIN business ON business.BusinessID = pay_order.BusinessID"
                                    + " WHERE pay_order.PayOrderID = @PayOrderID"
                                    + " ORDER BY pay_order.Date DESC;";
            object parameters = new { PayOrderID };
            return DbManager.DbQueryList<PayOrderPayment>(handler, sqlCommand, parameters);
        }
        public static void DeleteByPayOrderId(int PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM pay_order_payment WHERE PayOrderID = @PayOrderID;";
            object parameters = new { PayOrderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO pay_order_payment (PayOrderID, PaymentID, TotalAmount, CurrencyID, AdditionalInformation)"
                                    + " VALUES (@PayOrderID, @PaymentID, @TotalAmount, @CurrencyID, @AdditionalInformation);";
            this.PayOrderPaymentID = DbManager.DbInsert(handler, sqlCommand, this);
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    public class Product : DbEntity
    {
        public override int PrimaryKeyID { get { return ProductID; } }

        public int ProductID { get; set; }
        public string PartCode { get; set; }
        public bool IsSeal { get; set; }
        public int? SealTypeID { get; set; }
        public string Description { get; set; }
        public decimal Stock { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrencyID { get; set; }
        public byte[] ProductImage { get; set; }

        // Campos de soporte (no pertenecen a la tabla "product")
        public string TypeDescription { get; set; }
        public string CurrencySymbol { get; set; }

        public static List<Product> GetProducts(string viewSettingsString, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " product.ProductID,"
                              + " product.PartCode,"
                              + " product.IsSeal,"
                              + " product.SealTypeID,"
                              + " product.Description,"
                              + " product.Stock,"
                              + " product.UnitPrice,"
                              + " product.CurrencyID,"
                              + " product.ProductImage,"
                              + " seal_type.TypeDescription,"
                              + " currency.CurrencySymbol"
                              + " FROM product"
                              + " INNER JOIN currency ON currency.CurrencyID = product.CurrencyID"
                              + " LEFT JOIN seal_type ON seal_type.SealTypeID = product.SealTypeID"
                              + viewSettingsString
                              + $" LIMIT {DbLayerSettings.DefaultRecordLimit};";
            return DbManager.DbQueryList<Product>(handler, sqlCommand);
        }
        public static Product GetProductById(int ProductID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " product.ProductID,"
                                    + " product.PartCode,"
                                    + " product.IsSeal,"
                                    + " product.SealTypeID,"
                                    + " product.Description,"
                                    + " product.Stock,"
                                    + " product.UnitPrice,"
                                    + " product.CurrencyID,"
                                    + " product.ProductImage,"
                                    + " seal_type.TypeDescription,"
                                    + " currency.CurrencySymbol"
                                    + " FROM product"
                                    + " INNER JOIN currency ON currency.CurrencyID = product.CurrencyID"
                                    + " LEFT JOIN seal_type ON seal_type.SealTypeID = product.SealTypeID"
                                    + " WHERE product.ProductID = @ProductID;";
            object parameters = new { ProductID };
            return DbManager.DbQuerySingle<Product>(handler, sqlCommand, parameters);
        }
        public static Product GetProductByPartCode(string PartCode, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " product.ProductID,"
                                    + " product.PartCode,"
                                    + " product.IsSeal,"
                                    + " product.SealTypeID,"
                                    + " product.Description,"
                                    + " product.Stock,"
                                    + " product.UnitPrice,"
                                    + " product.CurrencyID,"
                                    + " product.ProductImage,"
                                    + " seal_type.TypeDescription,"
                                    + " currency.CurrencySymbol"
                                    + " FROM product"
                                    + " INNER JOIN currency ON currency.CurrencyID = product.CurrencyID"
                                    + " LEFT JOIN seal_type ON seal_type.SealTypeID = product.SealTypeID"
                                    + " WHERE product.PartCode = @PartCode;";
            object parameters = new { PartCode };
            return DbManager.DbQuerySingle<Product>(handler, sqlCommand, parameters);
        }
        public static List<string> GetPartCodes(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT product.PartCode FROM product ORDER BY product.PartCode ASC;";
            var result = DbManager.DbQueryList<dynamic>(handler, sqlCommand);
            return result.Select(x => (string)x.PartCode).ToList();
        }
        public static List<Customer> GetBuyersFromProduct(int ProductID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT DISTINCT"
                              + " customer.CustomerID,"
                              + " customer.CustomerName"
                              + " FROM customer"
                              + " INNER JOIN sale ON sale.CustomerID = customer.CustomerID"
                              + " INNER JOIN sale_item ON sale_item.SaleID = sale.SaleID"
                              + " WHERE sale_item.ProductID = @ProductID"
                              + " ORDER BY customer.CustomerName ASC;";
            object parameters = new { ProductID };
            return DbManager.DbQueryList<Customer>(handler, sqlCommand, parameters);
        }
        public static List<Sale> GetSalesFromProduct(int ProductID, int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT DISTINCT"
                              + " sale.SaleID,"
                              + " sale.Date,"
                              + " business.BusinessName"
                              + " FROM sale"
                              + " INNER JOIN business ON business.BusinessID = sale.BusinessID"
                              + " INNER JOIN sale_item ON sale_item.SaleID = sale.SaleID"
                              + " WHERE sale_item.ProductID = @ProductID AND sale.CustomerID = @CustomerID"
                              + " ORDER BY sale.Date DESC;";
            object parameters = new { ProductID, CustomerID };
            return DbManager.DbQueryList<Sale>(handler, sqlCommand, parameters);
        }
        public static void DeleteProductById(int ProductID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM product WHERE ProductID = @ProductID;";
            object parameters = new { ProductID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO product (PartCode, IsSeal, SealTypeID, Description, Stock, UnitPrice, CurrencyID, ProductImage)"
                                    + " VALUES (@PartCode, @IsSeal, @SealTypeID, @Description, @Stock, @UnitPrice, @CurrencyID, @ProductImage);";
            this.ProductID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE product SET"
                                    + " PartCode = @PartCode,"
                                    + " IsSeal = @IsSeal,"
                                    + " SealTypeID = @SealTypeID,"
                                    + " Description = @Description,"
                                    + " Stock = @Stock,"
                                    + " UnitPrice = @UnitPrice,"
                                    + " CurrencyID = @CurrencyID,"
                                    + " ProductImage = @ProductImage"
                                    + " WHERE ProductID = @ProductID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class ProgressUpdate : DbEntity
    {
        public override int PrimaryKeyID { get { return ProgressUpdateID; } }

        public int ProgressUpdateID { get; set; }
        public int RepairOrderID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public int UpdateTypeID { get; set; }
        public string Notes { get; set; }

        // Campos de soporte (no pertenecen a la tabla "progress_update")
        public string UserName { get; set; }
        public string UpdateTypeName { get; set; }

        public static List<ProgressUpdate> GetUpdatesByRepairOrderId(int RepairOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " progress_update.ProgressUpdateID,"
                                    + " progress_update.RepairOrderID,"
                                    + " progress_update.UserID,"
                                    + " progress_update.Date,"
                                    + " progress_update.UpdateTypeID,"
                                    + " progress_update.Notes,"
                                    + " user.UserName,"
                                    + " update_type.UpdateTypeName"
                                    + " FROM progress_update"
                                    + " INNER JOIN user ON user.UserID = progress_update.UserID"
                                    + " INNER JOIN update_type ON update_type.UpdateTypeID = progress_update.UpdateTypeID"
                                    + " WHERE progress_update.RepairOrderID = @RepairOrderID"
                                    + " ORDER BY progress_update.Date ASC;";
            object parameters = new { RepairOrderID };
            return DbManager.DbQueryList<ProgressUpdate>(handler, sqlCommand, parameters);
        }
        public static ProgressUpdate GetUpdateById(int ProgressUpdateID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " progress_update.ProgressUpdateID,"
                                    + " progress_update.RepairOrderID,"
                                    + " progress_update.UserID,"
                                    + " progress_update.Date,"
                                    + " progress_update.UpdateTypeID,"
                                    + " progress_update.Notes,"
                                    + " user.UserName,"
                                    + " update_type.UpdateTypeName"
                                    + " FROM progress_update"
                                    + " INNER JOIN user ON user.UserID = progress_update.UserID"
                                    + " INNER JOIN update_type ON update_type.UpdateTypeID = progress_update.UpdateTypeID"
                                    + " WHERE progress_update.ProgressUpdateID = @ProgressUpdateID;";
            object parameters = new { ProgressUpdateID };
            return DbManager.DbQuerySingle<ProgressUpdate>(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO progress_update (RepairOrderID, UserID, Date, UpdateTypeID, Notes)"
                                    + " VALUES (@RepairOrderID, @UserID, @Date, @UpdateTypeID, @Notes);";
            this.ProgressUpdateID = DbManager.DbInsert(handler, sqlCommand, this);
        }
    }
    public class Provider : DbEntity
    {
        public override int PrimaryKeyID { get { return ProviderID; } }

        public int ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string IdentityNumber { get; set; }
        public bool IsCUIT { get; set; }
        public string TaxGroup { get; set; }
        public int PaymentTerm { get; set; }
        public int BusinessID { get; set; }
        public int? CountryID { get; set; }
        public string Country { get; set; }
        public int? DistrictID { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime RegistryDate { get; set; }

        // Campos de soporte (no pertenecen a la tabla "provider")
        public string BusinessName { get; set; }

        public static List<Provider> GetProviders(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " provider.ProviderID,"
                              + " provider.ProviderName,"
                              + " provider.IdentityNumber,"
                              + " provider.IsCUIT,"
                              + " provider.TaxGroup,"
                              + " provider.PaymentTerm,"
                              + " provider.BusinessID,"
                              + " provider.CountryID,"
                              + " provider.Country,"
                              + " provider.DistrictID,"
                              + " provider.District,"
                              + " provider.City,"
                              + " provider.Address,"
                              + " provider.RegistryDate,"
                              + " business.BusinessName"
                              + " FROM provider"
                              + " INNER JOIN business ON business.BusinessID = provider.BusinessID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<Provider>(handler, sqlCommand);
        }
        public static List<Provider> GetProvidersLight(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " provider.ProviderID,"
                                    + " provider.ProviderName,"
                                    + " provider.BusinessID"
                                    + " FROM provider"
                                    + " ORDER BY provider.ProviderName ASC;";
            return DbManager.DbQueryList<Provider>(handler, sqlCommand);
        }
        public static Provider GetProviderById(int ProviderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " provider.ProviderID,"
                                    + " provider.ProviderName,"
                                    + " provider.IdentityNumber,"
                                    + " provider.IsCUIT,"
                                    + " provider.TaxGroup,"
                                    + " provider.PaymentTerm,"
                                    + " provider.BusinessID,"
                                    + " provider.CountryID,"
                                    + " provider.Country,"
                                    + " provider.DistrictID,"
                                    + " provider.District,"
                                    + " provider.City,"
                                    + " provider.Address,"
                                    + " provider.RegistryDate,"
                                    + " business.BusinessName"
                                    + " FROM provider"
                                    + " INNER JOIN business ON business.BusinessID = provider.BusinessID"
                                    + " WHERE provider.ProviderID = @ProviderID;";
            object parameters = new { ProviderID };
            return DbManager.DbQuerySingle<Provider>(handler, sqlCommand, parameters);
        }
        public static void DeleteProviderById(int ProviderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM provider WHERE ProviderID = @ProviderID;";
            object parameters = new { ProviderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO provider (ProviderName, IdentityNumber, IsCUIT, TaxGroup, PaymentTerm, BusinessID, CountryID,"
                                    + " Country, DistrictID, District, City, Address, RegistryDate) VALUES (@ProviderName, @IdentityNumber, @IsCUIT,"
                                    + " @TaxGroup, @PaymentTerm, @BusinessID, @CountryID, @Country, @DistrictID, @District, @City, @Address, @RegistryDate);";
            this.ProviderID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE provider SET ProviderName = @ProviderName, IdentityNumber = @IdentityNumber, IsCUIT = @IsCUIT,"
                                    + " TaxGroup = @TaxGroup, PaymentTerm = @PaymentTerm, BusinessID = @BusinessID, CountryID = @CountryID,"
                                    + " Country = @Country, DistrictID = @DistrictID, District = @District, City = @City, Address = @Address,"
                                    + " RegistryDate = @RegistryDate WHERE ProviderID = @ProviderID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class ProviderContact : DbEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override int PrimaryKeyID { get { return ContactID; } }

        public int ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; OnPropertyChanged("ContactID"); }
        }
        public int ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; OnPropertyChanged("ProviderID"); }
        }
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; OnPropertyChanged("ContactName"); }
        }
        public string Greeting
        {
            get { return _Greeting; }
            set { _Greeting = value; OnPropertyChanged("Greeting"); }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; OnPropertyChanged("Phone"); }
        }
        public string SecondaryPhone
        {
            get { return _SecondaryPhone; }
            set { _SecondaryPhone = value; OnPropertyChanged("SecondaryPhone"); }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged("Email"); }
        }

        private int _ContactID;
        private int _ProviderID;
        private string _ContactName;
        private string _Greeting;
        private string _Phone;
        private string _SecondaryPhone;
        private string _Email;

        public static List<ProviderContact> GetContactsByProviderId(int ProviderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " provider_contact.ContactID,"
                                    + " provider_contact.ProviderID,"
                                    + " provider_contact.ContactName,"
                                    + " provider_contact.Greeting,"
                                    + " provider_contact.Phone,"
                                    + " provider_contact.SecondaryPhone,"
                                    + " provider_contact.Email"
                                    + " FROM provider_contact"
                                    + " WHERE provider_contact.ProviderID = @ProviderID"
                                    + " ORDER BY provider_contact.ContactName ASC;";
            object parameters = new { ProviderID };
            return DbManager.DbQueryList<ProviderContact>(handler, sqlCommand, parameters);
        }
        public static ProviderContact GetContactById(int ContactID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " provider_contact.ContactID,"
                                    + " provider_contact.ProviderID,"
                                    + " provider_contact.ContactName,"
                                    + " provider_contact.Greeting,"
                                    + " provider_contact.Phone,"
                                    + " provider_contact.SecondaryPhone,"
                                    + " provider_contact.Email"
                                    + " FROM provider_contact"
                                    + " WHERE provider_contact.ContactID = @ContactID;";
            object parameters = new { ContactID };
            return DbManager.DbQuerySingle<ProviderContact>(handler, sqlCommand, parameters);
        }
        public static void DeleteContactById(int ContactID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM provider_contact WHERE ContactID = @ContactID;";
            object parameters = new { ContactID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO provider_contact"
                                    + " (ProviderID, ContactName, Greeting, Phone, SecondaryPhone, Email)"
                                    + " VALUES (@ProviderID, @ContactName, @Greeting, @Phone, @SecondaryPhone, @Email)";
            this.ContactID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE provider_contact SET"
                                    + " ProviderID = @ProviderID,"
                                    + " ContactName = @ContactName,"
                                    + " Greeting = @Greeting,"
                                    + " Phone = @Phone,"
                                    + " SecondaryPhone = @SecondaryPhone,"
                                    + " Email = @Email"
                                    + " WHERE ContactID = @ContactID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    public class PurchaseInvoice : DbEntity
    {
        public override int PrimaryKeyID { get { return PurchaseInvoiceID; } }

        public int PurchaseInvoiceID { get; set; }
        public int BusinessID { get; set; }
        public int ProviderID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int CurrencyID { get; set; }
        public string Status { get; set; }
        public int? PayOrderID { get; set; }

        // Campos de soporte (no pertenecen a la tabla "purchase_invoice").
        public string BusinessName { get; set; }
        public string ProviderName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        public static List<PurchaseInvoice> GetInvoices(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " purchase_invoice.PurchaseInvoiceID,"
                              + " purchase_invoice.BusinessID,"
                              + " purchase_invoice.ProviderID,"
                              + " purchase_invoice.InvoiceDate,"
                              + " purchase_invoice.InvoiceType,"
                              + " purchase_invoice.InvoiceNumber,"
                              + " purchase_invoice.TotalAmount,"
                              + " purchase_invoice.CurrencyID,"
                              + " purchase_invoice.Status,"
                              + " purchase_invoice.PayOrderID,"
                              + " business.BusinessName,"
                              + " provider.ProviderName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol"
                              + " FROM purchase_invoice"
                              + " INNER JOIN business ON business.BusinessID = purchase_invoice.BusinessID"
                              + " INNER JOIN provider ON provider.ProviderID = purchase_invoice.ProviderID"
                              + " INNER JOIN currency ON currency.CurrencyID = purchase_invoice.CurrencyID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<PurchaseInvoice>(handler, sqlCommand);
        }
        public static List<PurchaseInvoice> GetInvoicesByProviderId(int ProviderID, int CurrencyID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_invoice.PurchaseInvoiceID,"
                                    + " purchase_invoice.BusinessID,"
                                    + " purchase_invoice.ProviderID,"
                                    + " purchase_invoice.InvoiceDate,"
                                    + " purchase_invoice.InvoiceType,"
                                    + " purchase_invoice.InvoiceNumber,"
                                    + " purchase_invoice.TotalAmount,"
                                    + " purchase_invoice.CurrencyID,"
                                    + " purchase_invoice.Status,"
                                    + " purchase_invoice.PayOrderID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol"
                                    + " FROM purchase_invoice"
                                    + " INNER JOIN business ON business.BusinessID = purchase_invoice.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_invoice.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_invoice.CurrencyID"
                                    + " WHERE purchase_invoice.ProviderID = @ProviderID AND purchase_invoice.CurrencyID = @CurrencyID"
                                    + " ORDER BY purchase_invoice.InvoiceDate DESC;";
            object parameters = new { ProviderID, CurrencyID };
            return DbManager.DbQueryList<PurchaseInvoice>(handler, sqlCommand, parameters);
        }
        public static List<PurchaseInvoice> GetInvoicesByPayOrderId(int PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_invoice.PurchaseInvoiceID,"
                                    + " purchase_invoice.BusinessID,"
                                    + " purchase_invoice.ProviderID,"
                                    + " purchase_invoice.InvoiceDate,"
                                    + " purchase_invoice.InvoiceType,"
                                    + " purchase_invoice.InvoiceNumber,"
                                    + " purchase_invoice.TotalAmount,"
                                    + " purchase_invoice.CurrencyID,"
                                    + " purchase_invoice.Status,"
                                    + " purchase_invoice.PayOrderID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol"
                                    + " FROM purchase_invoice"
                                    + " INNER JOIN business ON business.BusinessID = purchase_invoice.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_invoice.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_invoice.CurrencyID"
                                    + " WHERE purchase_invoice.PayOrderID = @PayOrderID"
                                    + " ORDER BY purchase_invoice.InvoiceDate DESC;";
            object parameters = new { PayOrderID };
            return DbManager.DbQueryList<PurchaseInvoice>(handler, sqlCommand, parameters);
        }
        public static List<PurchaseInvoice> GetInvoicesByProviderIdAndPayOrderId(int ProviderID, int? PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_invoice.PurchaseInvoiceID,"
                                    + " purchase_invoice.BusinessID,"
                                    + " purchase_invoice.ProviderID,"
                                    + " purchase_invoice.InvoiceDate,"
                                    + " purchase_invoice.InvoiceType,"
                                    + " purchase_invoice.InvoiceNumber,"
                                    + " purchase_invoice.TotalAmount,"
                                    + " purchase_invoice.CurrencyID,"
                                    + " purchase_invoice.Status,"
                                    + " purchase_invoice.PayOrderID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol"
                                    + " FROM purchase_invoice"
                                    + " INNER JOIN business ON business.BusinessID = purchase_invoice.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_invoice.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_invoice.CurrencyID"
                                    + " WHERE purchase_invoice.ProviderID = @ProviderID"
                                    + " AND (purchase_invoice.PayOrderID = @PayOrderID OR purchase_invoice.PayOrderID IS NULL)"
                                    + " ORDER BY purchase_invoice.InvoiceDate DESC;";
            object parameters = new { ProviderID, PayOrderID };
            return DbManager.DbQueryList<PurchaseInvoice>(handler, sqlCommand, parameters);
        }
        public static PurchaseInvoice GetInvoiceById(int PurchaseInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_invoice.PurchaseInvoiceID,"
                                    + " purchase_invoice.BusinessID,"
                                    + " purchase_invoice.ProviderID,"
                                    + " purchase_invoice.InvoiceDate,"
                                    + " purchase_invoice.InvoiceType,"
                                    + " purchase_invoice.InvoiceNumber,"
                                    + " purchase_invoice.TotalAmount,"
                                    + " purchase_invoice.CurrencyID,"
                                    + " purchase_invoice.Status,"
                                    + " purchase_invoice.PayOrderID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol"
                                    + " FROM purchase_invoice"
                                    + " INNER JOIN business ON business.BusinessID = purchase_invoice.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_invoice.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_invoice.CurrencyID"
                                    + " WHERE purchase_invoice.PurchaseInvoiceID = @PurchaseInvoiceID;";
            object parameters = new { PurchaseInvoiceID };
            return DbManager.DbQuerySingle<PurchaseInvoice>(handler, sqlCommand, parameters);
        }
        public static bool CheckInvoiceNumberDuplicates(string InvoiceNumber, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT EXISTS(SELECT 1 FROM purchase_invoice WHERE InvoiceNumber = @InvoiceNumber);";
            object parameters = new { InvoiceNumber };
            return DbManager.DbExecuteScalar<bool>(handler, sqlCommand, parameters);
        }
        public static void LinkPayOrder(int PurchaseInvoiceID, int PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_invoice SET PayOrderID = @PayOrderID WHERE PurchaseInvoiceID = @PurchaseInvoiceID;";
            object parameters = new { PurchaseInvoiceID, PayOrderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UnlinkPayOrder(int PayOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_invoice SET PayOrderID = NULL WHERE PayOrderID = @PayOrderID;";
            object parameters = new { PayOrderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateStatus(int PurchaseInvoiceID, string Status, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_invoice SET Status = @Status WHERE PurchaseInvoiceID = @PurchaseInvoiceID;";
            object parameters = new { PurchaseInvoiceID, Status };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateStatusByPayOrderId(int PayOrderID, string Status, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_invoice SET Status = @Status WHERE PayOrderID = @PayOrderID;";
            object parameters = new { PayOrderID, Status };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void DeleteInvoiceById(int PurchaseInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM purchase_invoice WHERE PurchaseInvoiceID = @PurchaseInvoiceID;";
            object parameters = new { PurchaseInvoiceID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO purchase_invoice (BusinessID, ProviderID, InvoiceDate, InvoiceType, InvoiceNumber, TotalAmount, CurrencyID, Status, PayOrderID)"
                                    + " VALUES (@BusinessID, @ProviderID, @InvoiceDate, @InvoiceType, @InvoiceNumber, @TotalAmount, @CurrencyID, @Status, @PayOrderID);";
            this.PurchaseInvoiceID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_invoice SET BusinessID = @BusinessID, ProviderID = @ProviderID, InvoiceDate = @InvoiceDate, InvoiceType = @InvoiceType,"
                                    + " InvoiceNumber = @InvoiceNumber, TotalAmount = @TotalAmount, CurrencyID = @CurrencyID, Status = @Status, PayOrderID = @PayOrderID"
                                    + " WHERE PurchaseInvoiceID = @PurchaseInvoiceID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class PurchaseOrder : DbEntity
    {
        public override int PrimaryKeyID { get { return PurchaseOrderID; } }

        public int PurchaseOrderID { get; set; }
        public int BusinessID { get; set; }
        public int ProviderID { get; set; }
        public int? ContactID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public int CurrencyID { get; set; }
        public int? PurchaseInvoiceID { get; set; }

        // Campos de soporte (no pertenecen a la tabla "purchase_order")
        public string BusinessName { get; set; }
        public string ProviderName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string InvoiceNumber { get; set; }

        public static List<PurchaseOrder> GetPurchaseOrders(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " purchase_order.PurchaseOrderID,"
                              + " purchase_order.BusinessID,"
                              + " purchase_order.ProviderID,"
                              + " purchase_order.ContactID,"
                              + " purchase_order.Date,"
                              + " purchase_order.Description,"
                              + " purchase_order.TotalBeforeTax,"
                              + " purchase_order.CurrencyID,"
                              + " purchase_order.PurchaseInvoiceID,"
                              + " business.BusinessName,"
                              + " provider.ProviderName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol,"
                              + " purchase_invoice.InvoiceNumber"
                              + " FROM purchase_order"
                              + " INNER JOIN business ON business.BusinessID = purchase_order.BusinessID"
                              + " INNER JOIN provider ON provider.ProviderID = purchase_order.ProviderID"
                              + " INNER JOIN currency ON currency.CurrencyID = purchase_order.CurrencyID"
                              + " LEFT JOIN purchase_invoice ON purchase_invoice.PurchaseInvoiceID = purchase_order.PurchaseInvoiceID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<PurchaseOrder>(handler, sqlCommand);
        }
        public static List<PurchaseOrder> GetPurchaseOrdersByPurchaseInvoiceId(int PurchaseInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_order.PurchaseOrderID,"
                                    + " purchase_order.BusinessID,"
                                    + " purchase_order.ProviderID,"
                                    + " purchase_order.ContactID,"
                                    + " purchase_order.Date,"
                                    + " purchase_order.Description,"
                                    + " purchase_order.TotalBeforeTax,"
                                    + " purchase_order.CurrencyID,"
                                    + " purchase_order.PurchaseInvoiceID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol,"
                                    + " purchase_invoice.InvoiceNumber"
                                    + " FROM purchase_order"
                                    + " INNER JOIN business ON business.BusinessID = purchase_order.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_order.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_order.CurrencyID"
                                    + " LEFT JOIN purchase_invoice ON purchase_invoice.PurchaseInvoiceID = purchase_order.PurchaseInvoiceID"
                                    + " WHERE purchase_order.PurchaseInvoiceID = @PurchaseInvoiceID;";
            object parameters = new { PurchaseInvoiceID };
            return DbManager.DbQueryList<PurchaseOrder>(handler, sqlCommand, parameters);
        }
        public static List<PurchaseOrder> GetPurchaseOrdersByProviderIdAndPurchaseInvoiceId(int ProviderID, int? PurchaseInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_order.PurchaseOrderID,"
                                    + " purchase_order.BusinessID,"
                                    + " purchase_order.ProviderID,"
                                    + " purchase_order.ContactID,"
                                    + " purchase_order.Date,"
                                    + " purchase_order.Description,"
                                    + " purchase_order.TotalBeforeTax,"
                                    + " purchase_order.CurrencyID,"
                                    + " purchase_order.PurchaseInvoiceID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol,"
                                    + " purchase_invoice.InvoiceNumber"
                                    + " FROM purchase_order"
                                    + " INNER JOIN business ON business.BusinessID = purchase_order.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_order.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_order.CurrencyID"
                                    + " LEFT JOIN purchase_invoice ON purchase_invoice.PurchaseInvoiceID = purchase_order.PurchaseInvoiceID"
                                    + " WHERE purchase_order.ProviderID = @ProviderID AND (purchase_order.PurchaseInvoiceID = @PurchaseInvoiceID OR purchase_order.PurchaseInvoiceID IS NULL);";
            object parameters = new { ProviderID, PurchaseInvoiceID };
            return DbManager.DbQueryList<PurchaseOrder>(handler, sqlCommand, parameters);
        }
        public static PurchaseOrder GetPurchaseOrderById(int PurchaseOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_order.PurchaseOrderID,"
                                    + " purchase_order.BusinessID,"
                                    + " purchase_order.ProviderID,"
                                    + " purchase_order.ContactID,"
                                    + " purchase_order.Date,"
                                    + " purchase_order.Description,"
                                    + " purchase_order.TotalBeforeTax,"
                                    + " purchase_order.CurrencyID,"
                                    + " purchase_order.PurchaseInvoiceID,"
                                    + " business.BusinessName,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencyName,"
                                    + " currency.CurrencySymbol,"
                                    + " purchase_invoice.InvoiceNumber"
                                    + " FROM purchase_order"
                                    + " INNER JOIN business ON business.BusinessID = purchase_order.BusinessID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_order.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_order.CurrencyID"
                                    + " LEFT JOIN purchase_invoice ON purchase_invoice.PurchaseInvoiceID = purchase_order.PurchaseInvoiceID"
                                    + " WHERE purchase_order.PurchaseOrderID = @PurchaseOrderID;";
            object parameters = new { PurchaseOrderID };
            return DbManager.DbQuerySingle<PurchaseOrder>(handler, sqlCommand, parameters);
        }
        public static void LinkPurchaseInvoice(int PurchaseOrderID, int PurchaseInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_order SET PurchaseInvoiceID = @PurchaseInvoiceID WHERE PurchaseOrderID = @PurchaseOrderID;";
            object parameters = new { PurchaseOrderID, PurchaseInvoiceID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UnlinkPurchaseInvoice(int PurchaseInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_order SET PurchaseInvoiceID = NULL WHERE PurchaseInvoiceID = @PurchaseInvoiceID;";
            object parameters = new { PurchaseInvoiceID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void DeletePurchaseOrderById(int PurchaseOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM purchase_order WHERE PurchaseOrderID = @PurchaseOrderID;";
            object parameters = new { PurchaseOrderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO purchase_order (BusinessID, ProviderID, ContactID, Date, Description, TotalBeforeTax, CurrencyID, PurchaseInvoiceID)"
                                    + " VALUES (@BusinessID, @ProviderID, @ContactID, @Date, @Description, @TotalBeforeTax, @CurrencyID, @PurchaseInvoiceID);";
            this.PurchaseOrderID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE purchase_order SET"
                                    + " BusinessID = @BusinessID,"
                                    + " ProviderID = @ProviderID,"
                                    + " ContactID = @ContactID,"
                                    + " Date = @Date,"
                                    + " Description = @Description,"
                                    + " TotalBeforeTax = @TotalBeforeTax,"
                                    + " CurrencyID = @CurrencyID,"
                                    + " PurchaseInvoiceID = @PurchaseInvoiceID"
                                    + " WHERE PurchaseOrderID = @PurchaseOrderID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    [Serializable]
    public class PurchaseOrderItem : DbEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override int PrimaryKeyID { get { return ItemID; } }

        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            set
            {
                _ItemID = value;
                OnPropertyChanged("ItemID");
            }
        }
        public int PurchaseOrderID
        {
            get
            {
                return _PurchaseOrderID;
            }
            set
            {
                _PurchaseOrderID = value;
                OnPropertyChanged("PurchaseOrderID");
            }
        }
        public int? InputID
        {
            get
            {
                return _InputID;
            }
            set
            {
                _InputID = value;
                OnPropertyChanged("InputID");
            }
        }
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
        }
        public decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public decimal TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }
        public int VatID
        {
            get
            {
                return _VatID;
            }
            set
            {
                _VatID = value;
                OnPropertyChanged("VatID");
            }
        }

        // Campos de soporte (no pertenecen a la tabla "purchase_order_item").
        public decimal VatPercentage
        {
            get
            {
                return _VatPercentage;
            }
            set
            {
                _VatPercentage = value;
                OnPropertyChanged("VatPercentage");
            }
        }
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
                OnPropertyChanged("Date");
            }
        }
        public string ProviderName
        {
            get
            {
                return _ProviderName;
            }
            set
            {
                _ProviderName = value;
                OnPropertyChanged("ProviderName");
            }
        }
        public string CurrencySymbol
        {
            get
            {
                return _CurrencySymbol;
            }
            set
            {
                _CurrencySymbol = value;
                OnPropertyChanged("CurrencySymbol");
            }
        }

        private int _ItemID;
        private int _PurchaseOrderID;
        private int? _InputID;
        private string _Description;
        private decimal _Quantity;
        private decimal _Amount;
        private decimal _TotalAmount;
        private int _VatID;
        private decimal _VatPercentage;
        private DateTime _Date;
        private string _ProviderName;
        private string _CurrencySymbol;

        public static List<PurchaseOrderItem> GetItemsByPurchaseOrderId(int PurchaseOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_order_item.ItemID,"
                                    + " purchase_order_item.PurchaseOrderID,"
                                    + " purchase_order_item.InputID,"
                                    + " purchase_order_item.Description,"
                                    + " purchase_order_item.Quantity,"
                                    + " purchase_order_item.Amount,"
                                    + " purchase_order_item.TotalAmount,"
                                    + " purchase_order_item.VatID,"
                                    + " vat.VatPercentage"
                                    + " FROM purchase_order_item"
                                    + " INNER JOIN vat ON vat.VatID = purchase_order_item.VatID"
                                    + " WHERE purchase_order_item.PurchaseOrderID = @PurchaseOrderID;";
            object parameters = new { PurchaseOrderID };
            return DbManager.DbQueryList<PurchaseOrderItem>(handler, sqlCommand, parameters);
        }
        public static List<PurchaseOrderItem> GetItemsByInputId(int InputID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " purchase_order_item.ItemID,"
                                    + " purchase_order_item.PurchaseOrderID,"
                                    + " purchase_order_item.InputID,"
                                    + " purchase_order_item.Description,"
                                    + " purchase_order_item.Quantity,"
                                    + " purchase_order_item.Amount,"
                                    + " purchase_order_item.TotalAmount,"
                                    + " purchase_order_item.VatID,"
                                    + " vat.VatPercentage,"
                                    + " purchase_order.Date,"
                                    + " provider.ProviderName,"
                                    + " currency.CurrencySymbol"
                                    + " FROM purchase_order_item"
                                    + " INNER JOIN vat ON vat.VatID = purchase_order_item.VatID"
                                    + " INNER JOIN purchase_order ON purchase_order.PurchaseOrderID = purchase_order_item.PurchaseOrderID"
                                    + " INNER JOIN provider ON provider.ProviderID = purchase_order.ProviderID"
                                    + " INNER JOIN currency ON currency.CurrencyID = purchase_order.CurrencyID"
                                    + " WHERE purchase_order_item.InputID = @InputID"
                                    + " ORDER BY purchase_order.Date DESC, purchase_order_item.Amount ASC;";
            object parameters = new { InputID };
            return DbManager.DbQueryList<PurchaseOrderItem>(handler, sqlCommand, parameters);
        }
        public static void DeleteItemsByPurchaseOrderId(int PurchaseOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM purchase_order_item WHERE PurchaseOrderID = @PurchaseOrderID;";
            object parameters = new { PurchaseOrderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO purchase_order_item (PurchaseOrderID, InputID, Description, Quantity, Amount, TotalAmount, VatID)"
                                    + " VALUES (@PurchaseOrderID, @InputID, @Description, @Quantity, @Amount, @TotalAmount, @VatID);";
            this.ItemID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public PurchaseOrderItem CopyItem()
        {
            return new PurchaseOrderItem()
            {
                InputID = _InputID,
                Description = _Description,
                Quantity = _Quantity,
                Amount = _Amount,
                TotalAmount = _TotalAmount,
                VatID = _VatID,
                VatPercentage = _VatPercentage
            };
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    public class RepairOrder : DbEntity
    {
        public override int PrimaryKeyID { get { return RepairOrderID; } }

        public int RepairOrderID { get; set; }
        public string RefNumber { get; set; }
        public DateTime Date { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryNoteNumberCustomer { get; set; }
        public string DeliveryNoteNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public int? EstimateID { get; set; }
        public int? MotorTypeID { get; set; }
        public string MotorTypeName { get; set; }
        public string PumpBrand { get; set; }
        public string PumpModel { get; set; }
        public bool IsSinglePhase { get; set; }
        public bool AppliesForWinding { get; set; }
        public bool RequiresWinding { get; set; }
        public string EnginePower { get; set; }
        public string Bearings { get; set; }
        public bool RequiresSandblast { get; set; }
        public string Locks { get; set; }
        public string PaintColor { get; set; }
        public bool SealsApply { get; set; }
        public bool RequiresNewSeals { get; set; }
        public string Seals { get; set; }
        public int? SealsDeliveredToID { get; set; }
        public string SealsDeliveredToName { get; set; }
        public bool RequiresNewCapacitor { get; set; }
        public int? CapacitorVoltage { get; set; }
        public int? CapacitorCapacity { get; set; }
        public bool RequiresShaftCladding { get; set; }
        public bool RequiresShaftManufacturing { get; set; }
        public int? CableLenght { get; set; }
        public string CableSize { get; set; }
        public bool RequiresOrings { get; set; }
        public bool RequiresJoints { get; set; }
        public bool RequiresDielectricOil { get; set; }
        public bool RequiresNewImpeller { get; set; }
        public bool RequiresPins { get; set; }
        public bool RequiresPackings { get; set; }
        public bool RequiresGrease { get; set; }
        public string MissingParts { get; set; }
        public string Notes { get; set; }
        public string StorageLocation { get; set; }
        public string Status { get; set; }
        public int PriorityID { get; set; }
        public bool Approved { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool Completed { get; set; }
        public int Stage { get; set; }
        public byte[] DevicePicture { get; set; }

        // Campos de soporte (no pertenecen a la tabla "repair_order")
        public string PriorityName { get; set; }

        public static List<RepairOrder> GetRepairOrders(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                            + " priority.PriorityName,"
                            + " IF(customer.CustomerName IS NOT NULL, customer.CustomerName, repair_order.CustomerName) AS CustomerName,"
                            + " repair_order.CustomerID,"
                            + " repair_order.Date,"
                            + " repair_order.DeliveryNoteNumber,"
                            + " repair_order.EstimateID,"
                            + " repair_order.InvoiceNumber,"
                            + " repair_order.PumpBrand,"
                            + " repair_order.PumpModel,"
                            + " repair_order.RepairOrderID,"
                            + " repair_order.Status"
                            + " FROM repair_order"
                            + " LEFT JOIN customer ON customer.CustomerID = repair_order.CustomerID"
                            + " INNER JOIN priority ON priority.PriorityID = repair_order.PriorityID"
                            + viewSettingsString
                            + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<RepairOrder>(handler, sqlCommand);
        }
        public static List<RepairOrder> GetRepairOrdersByCustomerId(int CustomerID, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                            + " repair_order.CustomerID,"
                            + " repair_order.Date,"
                            + " repair_order.RepairOrderID"
                            + " FROM repair_order"
                            + " WHERE repair_order.CustomerID = @CustomerID"
                            + " ORDER BY repair_order.Date DESC;";
            object parameters = new { CustomerID };
            return DbManager.DbQueryList<RepairOrder>(handler, sqlCommand, parameters);
        }
        public static RepairOrder GetRepairOrderById(int RepairOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " repair_order.RepairOrderID,"
                                    + " repair_order.RefNumber,"
                                    + " repair_order.Date,"
                                    + " repair_order.CustomerID,"
                                    + " IF (repair_order.CustomerID IS NOT NULL, customer.CustomerName, repair_order.CustomerName) AS CustomerName,"
                                    + " repair_order.PhoneNumber,"
                                    + " repair_order.DeliveryNoteNumberCustomer,"
                                    + " repair_order.DeliveryNoteNumber,"
                                    + " repair_order.InvoiceNumber,"
                                    + " repair_order.EstimateID,"
                                    + " repair_order.MotorTypeID,"
                                    + " IF(repair_order.MotorTypeID IS NOT NULL, motor_type.MotorTypeName, repair_order.MotorTypeName) AS MotorTypeName,"
                                    + " repair_order.PumpBrand,"
                                    + " repair_order.PumpModel,"
                                    + " repair_order.IsSinglePhase,"
                                    + " repair_order.AppliesForWinding,"
                                    + " repair_order.RequiresWinding,"
                                    + " repair_order.EnginePower,"
                                    + " repair_order.Bearings,"
                                    + " repair_order.RequiresSandblast,"
                                    + " repair_order.Locks,"
                                    + " repair_order.PaintColor,"
                                    + " repair_order.SealsApply,"
                                    + " repair_order.RequiresNewSeals,"
                                    + " repair_order.Seals,"
                                    + " repair_order.SealsDeliveredToID,"
                                    + " repair_order.SealsDeliveredToName,"
                                    + " repair_order.RequiresNewCapacitor,"
                                    + " repair_order.CapacitorVoltage,"
                                    + " repair_order.CapacitorCapacity,"
                                    + " repair_order.RequiresShaftCladding,"
                                    + " repair_order.RequiresShaftManufacturing,"
                                    + " repair_order.CableLenght,"
                                    + " repair_order.CableSize,"
                                    + " repair_order.RequiresOrings,"
                                    + " repair_order.RequiresJoints,"
                                    + " repair_order.RequiresDielectricOil,"
                                    + " repair_order.RequiresNewImpeller,"
                                    + " repair_order.RequiresPins,"
                                    + " repair_order.RequiresPackings,"
                                    + " repair_order.RequiresGrease,"
                                    + " repair_order.MissingParts,"
                                    + " repair_order.Notes,"
                                    + " repair_order.StorageLocation,"
                                    + " repair_order.Status,"
                                    + " repair_order.PriorityID,"
                                    + " repair_order.Approved,"
                                    + " repair_order.ApprovalDate,"
                                    + " repair_order.Completed,"
                                    + " repair_order.Stage,"
                                    + " repair_order.DevicePicture,"
                                    + " priority.PriorityName"
                                    + " FROM repair_order"
                                    + " LEFT JOIN customer ON customer.CustomerID = repair_order.CustomerID"
                                    + " LEFT JOIN motor_type ON motor_type.MotorTypeID = repair_order.MotorTypeID"
                                    + " INNER JOIN priority ON priority.PriorityID = repair_order.PriorityID"
                                    + " WHERE repair_order.RepairOrderID = @RepairOrderID;";
            object parameters = new { RepairOrderID };
            return DbManager.DbQuerySingle<RepairOrder>(handler, sqlCommand, parameters);
        }
        public static int GetRepairOrdersCount(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT COUNT(1) FROM repair_order;";
            return DbManager.DbExecuteScalar<int>(handler, sqlCommand, null);
        }
        public static void UpdatePriority(int RepairOrderID, int PriorityID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE repair_order SET PriorityID = @PriorityID WHERE RepairOrderID = @RepairOrderID;";
            object parameters = new { RepairOrderID, PriorityID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateInvoiceNumberBySaleId(int SaleID, string InvoiceNumber, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE (repair_order INNER JOIN sale ON repair_order.EstimateID = sale.EstimateID)"
                                    + " SET repair_order.InvoiceNumber = @InvoiceNumber"
                                    + " WHERE sale.SaleID = @SaleID;";
            object parameters = new { SaleID, InvoiceNumber };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateDeliveryNoteNumberBySaleId(int SaleID, string DeliveryNoteNumber, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE (repair_order INNER JOIN sale ON repair_order.EstimateID = sale.EstimateID)"
                                    + " SET repair_order.DeliveryNoteNumber = @DeliveryNoteNumber"
                                    + " WHERE sale.SaleID = @SaleID;";
            object parameters = new { SaleID, DeliveryNoteNumber };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateStatusById(int RepairOrderID, string Status, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE repair_order SET Status = @Status WHERE RepairOrderID = @RepairOrderID;";
            object parameters = new { RepairOrderID, Status };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateStageById(int RepairOrderID, int Stage, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE repair_order SET Stage = @Stage WHERE RepairOrderID = @RepairOrderID;";
            object parameters = new { RepairOrderID, Stage };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void SetAsCompleted(int RepairOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE repair_order SET Completed = @Completed WHERE RepairOrderID = @RepairOrderID;";
            object parameters = new { RepairOrderID, Completed = true };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void SetAsApproved(int RepairOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE repair_order SET Approved = @Approved, ApprovalDate = @ApprovalDate WHERE RepairOrderID = @RepairOrderID;";
            object parameters = new { RepairOrderID, Approved = true, ApprovalDate = DateTime.Today };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters, 174, RepairOrderID);
        }
        public static void DeleteRepairOrderById(int RepairOrderID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM repair_order WHERE RepairOrderID = @RepairOrderID;";
            object parameters = new { RepairOrderID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters, 173, RepairOrderID);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO repair_order (RefNumber, Date, CustomerID, CustomerName, PhoneNumber, DeliveryNoteNumberCustomer, DeliveryNoteNumber,"
                                    + " InvoiceNumber, EstimateID, MotorTypeID, MotorTypeName, PumpBrand, PumpModel, IsSinglePhase, AppliesForWinding, RequiresWinding,"
                                    + " EnginePower, Bearings, RequiresSandblast, Locks, PaintColor, SealsApply, RequiresNewSeals, Seals, SealsDeliveredToID,"
                                    + " SealsDeliveredToName, RequiresNewCapacitor, CapacitorVoltage, CapacitorCapacity, RequiresShaftCladding, RequiresShaftManufacturing,"
                                    + " CableLenght, CableSize, RequiresOrings, RequiresJoints, RequiresDielectricOil, RequiresNewImpeller, RequiresPins, RequiresPackings,"
                                    + " RequiresGrease, MissingParts, Notes, StorageLocation, Status, PriorityID, Approved, ApprovalDate, Completed, Stage, DevicePicture)"
                                    + " VALUES (@RefNumber, @Date, @CustomerID, @CustomerName, @PhoneNumber, @DeliveryNoteNumberCustomer, @DeliveryNoteNumber,"
                                    + " @InvoiceNumber, @EstimateID, @MotorTypeID, @MotorTypeName, @PumpBrand, @PumpModel, @IsSinglePhase, @AppliesForWinding, @RequiresWinding,"
                                    + " @EnginePower, @Bearings, @RequiresSandblast, @Locks, @PaintColor, @SealsApply, @RequiresNewSeals, @Seals, @SealsDeliveredToID,"
                                    + " @SealsDeliveredToName, @RequiresNewCapacitor, @CapacitorVoltage, @CapacitorCapacity, @RequiresShaftCladding, @RequiresShaftManufacturing,"
                                    + " @CableLenght, @CableSize, @RequiresOrings, @RequiresJoints, @RequiresDielectricOil, @RequiresNewImpeller, @RequiresPins, @RequiresPackings,"
                                    + " @RequiresGrease, @MissingParts, @Notes, @StorageLocation, @Status, @PriorityID, @Approved, @ApprovalDate, @Completed, @Stage, @DevicePicture);";
            this.RepairOrderID = DbManager.DbInsert(handler, sqlCommand, this, 171);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE repair_order SET"
                                    + " RefNumber = @RefNumber,"
                                    + " Date = @Date,"
                                    + " CustomerID = @CustomerID,"
                                    + " CustomerName = @CustomerName,"
                                    + " PhoneNumber = @PhoneNumber,"
                                    + " DeliveryNoteNumberCustomer = @DeliveryNoteNumberCustomer,"
                                    + " DeliveryNoteNumber = @DeliveryNoteNumber,"
                                    + " InvoiceNumber = @InvoiceNumber,"
                                    + " EstimateID = @EstimateID,"
                                    + " MotorTypeID = @MotorTypeID,"
                                    + " MotorTypeName = @MotorTypeName,"
                                    + " PumpBrand = @PumpBrand,"
                                    + " PumpModel = @PumpModel,"
                                    + " IsSinglePhase = @IsSinglePhase,"
                                    + " AppliesForWinding = @AppliesForWinding,"
                                    + " RequiresWinding = @RequiresWinding,"
                                    + " EnginePower = @EnginePower,"
                                    + " Bearings = @Bearings,"
                                    + " RequiresSandblast = @RequiresSandblast,"
                                    + " Locks = @Locks,"
                                    + " PaintColor = @PaintColor,"
                                    + " SealsApply = @SealsApply,"
                                    + " RequiresNewSeals = @RequiresNewSeals,"
                                    + " Seals = @Seals,"
                                    + " SealsDeliveredToID = @SealsDeliveredToID,"
                                    + " SealsDeliveredToName = @SealsDeliveredToName,"
                                    + " RequiresNewCapacitor = @RequiresNewCapacitor,"
                                    + " CapacitorVoltage = @CapacitorVoltage,"
                                    + " CapacitorCapacity = @CapacitorCapacity,"
                                    + " RequiresShaftCladding = @RequiresShaftCladding,"
                                    + " RequiresShaftManufacturing = @RequiresShaftManufacturing,"
                                    + " CableLenght = @CableLenght,"
                                    + " CableSize = @CableSize,"
                                    + " RequiresOrings = @RequiresOrings,"
                                    + " RequiresJoints = @RequiresJoints,"
                                    + " RequiresDielectricOil = @RequiresDielectricOil,"
                                    + " RequiresNewImpeller = @RequiresNewImpeller,"
                                    + " RequiresPins = @RequiresPins,"
                                    + " RequiresPackings = @RequiresPackings,"
                                    + " RequiresGrease = @RequiresGrease,"
                                    + " MissingParts = @MissingParts,"
                                    + " Notes = @Notes,"
                                    + " StorageLocation = @StorageLocation,"
                                    + " Status = @Status,"
                                    + " PriorityID = @PriorityID,"
                                    + " Approved = @Approved,"
                                    + " ApprovalDate = @ApprovalDate,"
                                    + " Completed = @Completed,"
                                    + " Stage = @Stage,"
                                    + " DevicePicture = @DevicePicture"
                                    + " WHERE RepairOrderID = @RepairOrderID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this, 172, RepairOrderID);
        }
    }
    public class Sale : DbEntity
    {
        public override int PrimaryKeyID { get { return SaleID; } }

        public int SaleID { get; set; }
        public int BusinessID { get; set; }
        public int CustomerID { get; set; }
        public int? EstimateID { get; set; }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public int CurrencyID { get; set; }
        public int PaymentID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ShippingID { get; set; }
        public bool Shipped { get; set; }
        public DateTime? ShippingDate { get; set; }
        public int? ShippingCarrierID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Notes { get; set; }
        public int DepartmentID { get; set; }
        public decimal? TotalCost { get; set; }
        public bool IsUnmarked { get; set; }
        public bool HasInvoices { get; set; }
        public bool HasPayments { get; set; }

        // Campos de soporte (no pertenecen a la tabla "sale")
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }
        public string CurrencySymbol { get; set; }
        public string PaymentName { get; set; }
        public string ShippingName { get; set; }
        public string DepartmentName { get; set; }
        public string IsUnmarkedText { get; set; }
        public string HasInvoicesText { get; set; }
        public string HasPaymentsText { get; set; }

        public static List<Sale> GetSales(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " sale.SaleID,"
                              + " sale.BusinessID,"
                              + " sale.CustomerID,"
                              + " sale.EstimateID,"
                              + " sale.Date,"
                              + " sale.Discount,"
                              + " sale.TotalBeforeTax,"
                              + " sale.CurrencyID,"
                              + " sale.PaymentID,"
                              + " sale.DeliveryDate,"
                              + " sale.ShippingID,"
                              + " sale.Shipped,"
                              + " sale.ShippingDate,"
                              + " sale.ShippingCarrierID,"
                              + " sale.PurchaseOrderNumber,"
                              + " sale.Notes,"
                              + " sale.DepartmentID,"
                              + " sale.TotalCost,"
                              + " sale.IsUnmarked,"
                              + " IF(sale.IsUnmarked,'N','B') AS IsUnmarkedText,"
                              + " sale.HasInvoices,"
                              + " IF(sale.HasInvoices,'Si','No') AS HasInvoicesText,"
                              + " sale.HasPayments,"
                              + " IF(sale.HasPayments,'Si','No') AS HasPaymentsText,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencySymbol,"
                              + " payment.PaymentName,"
                              + " shipping.ShippingName,"
                              + " department.DepartmentName"
                              + " FROM sale"
                              + " INNER JOIN business ON business.BusinessID = sale.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = sale.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = sale.CurrencyID"
                              + " INNER JOIN payment ON payment.PaymentID = sale.PaymentID"
                              + " INNER JOIN shipping ON shipping.ShippingID = sale.ShippingID"
                              + " INNER JOIN department ON department.DepartmentID = sale.DepartmentID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<Sale>(handler, sqlCommand);
        }
        public static List<Sale> GetSalesByCustomerId(int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale.SaleID,"
                              + " sale.BusinessID,"
                              + " sale.CustomerID,"
                              + " sale.EstimateID,"
                              + " sale.Date,"
                              + " sale.Discount,"
                              + " sale.TotalBeforeTax,"
                              + " sale.CurrencyID,"
                              + " sale.PaymentID,"
                              + " sale.DeliveryDate,"
                              + " sale.ShippingID,"
                              + " sale.Shipped,"
                              + " sale.ShippingDate,"
                              + " sale.ShippingCarrierID,"
                              + " sale.PurchaseOrderNumber,"
                              + " sale.Notes,"
                              + " sale.DepartmentID,"
                              + " sale.TotalCost,"
                              + " sale.IsUnmarked,"
                              + " sale.HasInvoices,"
                              + " sale.HasPayments,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencySymbol,"
                              + " payment.PaymentName,"
                              + " shipping.ShippingName,"
                              + " department.DepartmentName"
                              + " FROM sale"
                              + " INNER JOIN business ON business.BusinessID = sale.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = sale.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = sale.CurrencyID"
                              + " INNER JOIN payment ON payment.PaymentID = sale.PaymentID"
                              + " INNER JOIN shipping ON shipping.ShippingID = sale.ShippingID"
                              + " INNER JOIN department ON department.DepartmentID = sale.DepartmentID"
                              + " WHERE sale.CustomerID = @CustomerID"
                              + " ORDER BY sale.SaleID DESC;";
            object parameters = new { CustomerID };
            return DbManager.DbQueryList<Sale>(handler, sqlCommand, parameters);
        }
        public static List<Sale> GetSalesByEstimateId(int EstimateID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale.SaleID,"
                              + " sale.TotalBeforeTax,"
                              + " currency.CurrencySymbol"
                              + " FROM sale"
                              + " INNER JOIN currency ON currency.CurrencyID = sale.CurrencyID"
                              + " INNER JOIN estimate ON estimate.EstimateID = sale.EstimateID"
                              + " WHERE estimate.EstimateID = @EstimateID"
                              + " ORDER BY sale.SaleID DESC;";
            object parameters = new { EstimateID };
            return DbManager.DbQueryList<Sale>(handler, sqlCommand, parameters);
        }
        public static List<Sale> GetSalesBySaleInvoiceId(int SaleInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale.SaleID,"
                              + " sale.TotalBeforeTax,"
                              + " currency.CurrencySymbol"
                              + " FROM sale"
                              + " INNER JOIN currency ON currency.CurrencyID = sale.CurrencyID"
                              + " INNER JOIN sale_invoice_link ON sale_invoice_link.SaleID = sale.SaleID"
                              + " WHERE sale_invoice_link.SaleInvoiceID = @SaleInvoiceID"
                              + " ORDER BY sale.SaleID DESC;";
            object parameters = new { SaleInvoiceID };
            return DbManager.DbQueryList<Sale>(handler, sqlCommand, parameters);
        }
        public static List<Sale> GetSalesByUnmarkedPaymentId(int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale.SaleID,"
                              + " sale.TotalBeforeTax,"
                              + " currency.CurrencySymbol"
                              + " FROM sale"
                              + " INNER JOIN currency ON currency.CurrencyID = sale.CurrencyID"
                              + " INNER JOIN sale_payment_link ON sale_payment_link.SaleID = sale.SaleID"
                              + " WHERE sale_payment_link.CustomerPaymentID = @CustomerPaymentID"
                              + " ORDER BY sale.SaleID DESC;";
            object parameters = new { CustomerPaymentID };
            return DbManager.DbQueryList<Sale>(handler, sqlCommand, parameters);
        }
        public static List<Sale> GetSalesByDeliveryDate(DateTime Date, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " sale.SaleID,"
                                    + " sale.CustomerID,"
                                    + " sale.Date,"
                                    + " sale.DeliveryDate,"
                                    + " sale.Shipped,"
                                    + " customer.CustomerName,"
                                    + " shipping.ShippingName"
                                    + " FROM sale"
                                    + " INNER JOIN customer ON customer.CustomerID = sale.CustomerID"
                                    + " INNER JOIN shipping ON shipping.ShippingID = sale.ShippingID"
                                    + " WHERE sale.DeliveryDate = @Date"
                                    + " ORDER BY sale.Shipped ASC, sale.Date ASC;";
            object parameters = new { Date };
            return DbManager.DbQueryList<Sale>(handler, sqlCommand, parameters);
        }
        public static List<DateTime> GetUnshippedSalesDates(DateTime StartDate, DateTime EndDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT DISTINCT DeliveryDate FROM sale WHERE (DeliveryDate BETWEEN @StartDate AND @EndDate) AND Shipped = 0;";
            object parameters = new { StartDate, EndDate };
            var result = DbManager.DbQueryList<dynamic>(handler, sqlCommand, parameters);
            return result.Select(x => ((DateTime)x.DeliveryDate).Date).ToList();
        }
        public static Sale GetSaleById(int SaleID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale.SaleID,"
                              + " sale.BusinessID,"
                              + " sale.CustomerID,"
                              + " sale.EstimateID,"
                              + " sale.Date,"
                              + " sale.Discount,"
                              + " sale.TotalBeforeTax,"
                              + " sale.CurrencyID,"
                              + " sale.PaymentID,"
                              + " sale.DeliveryDate,"
                              + " sale.ShippingID,"
                              + " sale.Shipped,"
                              + " sale.ShippingDate,"
                              + " sale.ShippingCarrierID,"
                              + " sale.PurchaseOrderNumber,"
                              + " sale.Notes,"
                              + " sale.DepartmentID,"
                              + " sale.TotalCost,"
                              + " sale.IsUnmarked,"
                              + " sale.HasInvoices,"
                              + " sale.HasPayments,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencySymbol,"
                              + " payment.PaymentName,"
                              + " shipping.ShippingName,"
                              + " department.DepartmentName"
                              + " FROM sale"
                              + " INNER JOIN business ON business.BusinessID = sale.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = sale.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = sale.CurrencyID"
                              + " INNER JOIN payment ON payment.PaymentID = sale.PaymentID"
                              + " INNER JOIN shipping ON shipping.ShippingID = sale.ShippingID"
                              + " INNER JOIN department ON department.DepartmentID = sale.DepartmentID"
                              + " WHERE sale.SaleID = @SaleID;";
            object parameters = new { SaleID };
            return DbManager.DbQuerySingle<Sale>(handler, sqlCommand, parameters);
        }
        public static void LinkInvoice(int SaleID, int SaleInvoiceID, DbTransactionHandler handler = null)
        {
            // Comando para asociar factura con venta.
            const string sqlCommand1 = "INSERT INTO sale_invoice_link (SaleID, SaleInvoiceID) VALUES (@SaleID, @SaleInvoiceID);";
            // Comando para actualizar estado de la venta.
            const string sqlCommand2 = "UPDATE sale SET HasInvoices = 1 WHERE SaleID = @SaleID;";
            object parameters = new { SaleID, SaleInvoiceID };
            // Ejecución de ambos comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
            }
        }
        public static void UnlinkInvoiceFromAll(int SaleInvoiceID, DbTransactionHandler handler = null)
        {
            // Comando para actualizar el estado de las ventas que solo tienen esta factura.
            const string sqlCommand1 = "UPDATE (sale INNER JOIN sale_invoice_link ON sale.SaleID = sale_invoice_link.SaleID)"
                                     + " SET sale.HasInvoices = 0 WHERE sale_invoice_link.SaleInvoiceID = @SaleInvoiceID"
                                     + " AND sale.SaleID IN (SELECT sale_invoice_link.SaleID FROM sale_invoice_link GROUP BY sale_invoice_link.SaleID HAVING COUNT(1) = 1);";
            // Comando para desvincular la factura de todas las ventas.
            const string sqlCommand2 = "DELETE FROM sale_invoice_link WHERE SaleInvoiceID = @SaleInvoiceID;";
            object parameters = new { SaleInvoiceID };
            // Ejecución de ambos comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
            }
        }
        public static void LinkUnmarkedPayment(int SaleID, int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            // Comando para asociar cobro con venta.
            const string sqlCommand1 = "INSERT INTO sale_payment_link (SaleID, CustomerPaymentID) VALUES (@SaleID, @CustomerPaymentID);";
            // Comando para actualizar estado de la venta.
            const string sqlCommand2 = "UPDATE sale SET HasPayments = 1 WHERE SaleID = @SaleID;";
            object parameters = new { SaleID, CustomerPaymentID };
            // Ejecución de todos los comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
            }
        }
        public static void UnlinkUnmarkedPaymentFromAll(int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            // Comando para actualizar el estado de las ventas que solo tienen este cobro.
            const string sqlCommand1 = "UPDATE (sale INNER JOIN sale_payment_link ON sale.SaleID = sale_payment_link.SaleID)"
                                     + " SET sale.HasPayments = 0 WHERE sale_payment_link.CustomerPaymentID = @CustomerPaymentID"
                                     + " AND sale.SaleID IN (SELECT sale_payment_link.SaleID FROM sale_payment_link GROUP BY sale_payment_link.SaleID HAVING COUNT(1) = 1);";
            // Comando para desvincular el cobro de todas las ventas.
            const string sqlCommand2 = "DELETE FROM sale_payment_link WHERE CustomerPaymentID = @CustomerPaymentID;";
            object parameters = new { CustomerPaymentID };
            // Ejecución de ambos comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
            }
        }
        public static void RescheduleOutdatedSales(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE sale SET DeliveryDate = @today WHERE Shipped = 0 AND DeliveryDate < @today;";
            object parameters = new { today = DateTime.Today };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateShippingInformation(int SaleID, bool Shipped, DateTime? ShippingDate, int? ShippingCarrierID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE sale SET"
                                    + " Shipped = @Shipped,"
                                    + " ShippingDate = @ShippingDate,"
                                    + " ShippingCarrierID = @ShippingCarrierID"
                                    + " WHERE SaleID = @SaleID;";
            object parameters = new { SaleID, Shipped, ShippingDate, ShippingCarrierID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void DeleteSaleById(int SaleID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM sale WHERE SaleID = @SaleID;";
            object parameters = new { SaleID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO sale (BusinessID, CustomerID, EstimateID, Date, Discount, TotalBeforeTax, CurrencyID, PaymentID,"
                              + " DeliveryDate, ShippingID, Shipped, ShippingDate, ShippingCarrierID, PurchaseOrderNumber, Notes, DepartmentID, TotalCost, IsUnmarked, HasInvoices, HasPayments)"
                              + " VALUES (@BusinessID, @CustomerID, @EstimateID, @Date, @Discount, @TotalBeforeTax, @CurrencyID, @PaymentID,"
                              + " @DeliveryDate, @ShippingID, @Shipped, @ShippingDate, @ShippingCarrierID, @PurchaseOrderNumber, @Notes, @DepartmentID, @TotalCost, @IsUnmarked, @HasInvoices, @HasPayments)";
            this.SaleID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE sale SET"
                              + " BusinessID = @BusinessID,"
                              + " CustomerID = @CustomerID,"
                              + " EstimateID = @EstimateID,"
                              + " Date = @Date,"
                              + " Discount = @Discount,"
                              + " TotalBeforeTax = @TotalBeforeTax,"
                              + " CurrencyID = @CurrencyID,"
                              + " PaymentID = @PaymentID,"
                              + " DeliveryDate = @DeliveryDate,"
                              + " ShippingID = @ShippingID,"
                              + " Shipped = @Shipped,"
                              + " ShippingDate = @ShippingDate,"
                              + " ShippingCarrierID = @ShippingCarrierID,"
                              + " PurchaseOrderNumber = @PurchaseOrderNumber,"
                              + " Notes = @Notes,"
                              + " DepartmentID = @DepartmentID,"
                              + " TotalCost = @TotalCost,"
                              + " IsUnmarked = @IsUnmarked,"
                              + " HasInvoices = @HasInvoices,"
                              + " HasPayments = @HasPayments"
                              + " WHERE SaleID = @SaleID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    [Serializable]
    public class SaleItem : DbEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override int PrimaryKeyID { get { return ItemID; } }

        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            set
            {
                _ItemID = value;
                OnPropertyChanged("ItemID");
            }
        }
        public int SaleID
        {
            get
            {
                return _SaleID;
            }
            set
            {
                _SaleID = value;
                OnPropertyChanged("SaleID");
            }
        }
        public int ItemNumber
        {
            get
            {
                return _ItemNumber;
            }
            set
            {
                _ItemNumber = value;
                OnPropertyChanged("ItemNumber");
            }
        }
        public int? ProductID
        {
            get
            {
                return _ProductID;
            }
            set
            {
                _ProductID = value;
                OnPropertyChanged("ProductID");
            }
        }
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
        }
        public int? DeliveryDelay
        {
            get
            {
                return _DeliveryDelay;
            }
            set
            {
                _DeliveryDelay = value;
                OnPropertyChanged("DeliveryDelay");
            }
        }
        public decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public decimal TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
            set
            {
                _TotalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }
        public int VatID
        {
            get
            {
                return _VatID;
            }
            set
            {
                _VatID = value;
                OnPropertyChanged("VatID");
            }
        }
        public byte[] CustomImage
        {
            get
            {
                return _CustomImage;
            }
            set
            {
                _CustomImage = value;
                OnPropertyChanged("Image");
            }
        }
        public int? DeliveryNoteID
        {
            get
            {
                return _DeliveryNoteID;
            }
            set
            {
                _DeliveryNoteID = value;
                OnPropertyChanged("DeliveryNoteID");
            }
        }
        public int? InvoiceID
        {
            get
            {
                return _InvoiceID;
            }
            set
            {
                _InvoiceID = value;
                OnPropertyChanged("InvoiceID");
            }
        }
        public decimal? Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                _Cost = value;
                OnPropertyChanged("Cost");
            }
        }

        // Campos de soporte (no pertenecen a la tabla "sale_item").
        public decimal VatPercentage
        {
            get
            {
                return _VatPercentage;
            }
            set
            {
                _VatPercentage = value;
                OnPropertyChanged("VatPercentage");
            }
        }
        public byte[] ProductImage
        {
            get
            {
                return _ProductImage;
            }
            set
            {
                _ProductImage = value;
                OnPropertyChanged("Image");
            }
        }

        private int _ItemID;
        private int _SaleID;
        private int _ItemNumber;
        private int? _ProductID;
        private string _Description;
        private int? _DeliveryDelay;
        private decimal _Quantity;
        private decimal _Amount;
        private decimal _TotalAmount;
        private int _VatID;
        private byte[] _CustomImage;
        private decimal _VatPercentage;
        private byte[] _ProductImage;
        private int? _DeliveryNoteID;
        private int? _InvoiceID;
        private decimal? _Cost;

        public static List<SaleItem> GetItemsBySaleId(int SaleID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " sale_item.ItemID,"
                                    + " sale_item.SaleID,"
                                    + " sale_item.ItemNumber,"
                                    + " sale_item.ProductID,"
                                    + " sale_item.Description,"
                                    + " sale_item.DeliveryDelay,"
                                    + " sale_item.Quantity,"
                                    + " sale_item.Amount,"
                                    + " sale_item.TotalAmount,"
                                    + " sale_item.VatID,"
                                    + " sale_item.CustomImage,"
                                    + " sale_item.DeliveryNoteID,"
                                    + " sale_item.InvoiceID,"
                                    + " sale_item.Cost,"
                                    + " vat.VatPercentage,"
                                    + " product.ProductImage"
                                    + " FROM sale_item"
                                    + " INNER JOIN vat ON vat.VatID = sale_item.VatID"
                                    + " LEFT JOIN product ON product.ProductID = sale_item.ProductID"
                                    + " WHERE sale_item.SaleID = @SaleID"
                                    + " ORDER BY sale_item.ItemNumber;";
            object parameters = new { SaleID };
            return DbManager.DbQueryList<SaleItem>(handler, sqlCommand, parameters);
        }
        public static List<SaleItem> GetItemsByDeliveryNoteId(int DeliveryNoteID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " sale_item.ItemID,"
                                    + " sale_item.SaleID,"
                                    + " sale_item.ItemNumber,"
                                    + " sale_item.ProductID,"
                                    + " sale_item.Description,"
                                    + " sale_item.DeliveryDelay,"
                                    + " sale_item.Quantity,"
                                    + " sale_item.Amount,"
                                    + " sale_item.TotalAmount,"
                                    + " sale_item.VatID,"
                                    + " sale_item.CustomImage,"
                                    + " sale_item.DeliveryNoteID,"
                                    + " sale_item.InvoiceID,"
                                    + " sale_item.Cost,"
                                    + " vat.VatPercentage,"
                                    + " product.ProductImage"
                                    + " FROM sale_item"
                                    + " INNER JOIN vat ON vat.VatID = sale_item.VatID"
                                    + " LEFT JOIN product ON product.ProductID = sale_item.ProductID"
                                    + " WHERE sale_item.DeliveryNoteID = @DeliveryNoteID"
                                    + " ORDER BY sale_item.ItemNumber;";
            object parameters = new { DeliveryNoteID };
            return DbManager.DbQueryList<SaleItem>(handler, sqlCommand, parameters);
        }
        public static List<SaleItem> GetItemsFromEstimate(int EstimateID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " estimate_item.ItemID,"
                                    + " estimate_item.ItemNumber,"
                                    + " estimate_item.ProductID,"
                                    + " estimate_item.Description,"
                                    + " estimate_item.DeliveryDelay,"
                                    + " estimate_item.Quantity,"
                                    + " estimate_item.Amount,"
                                    + " estimate_item.TotalAmount,"
                                    + " estimate_item.VatID,"
                                    + " estimate_item.CustomImage,"
                                    + " vat.VatPercentage,"
                                    + " product.ProductImage"
                                    + " FROM estimate_item"
                                    + " INNER JOIN vat ON vat.VatID = estimate_item.VatID"
                                    + " LEFT JOIN product ON product.ProductID = estimate_item.ProductID"
                                    + " WHERE estimate_item.EstimateID = @EstimateID"
                                    + " ORDER BY estimate_item.ItemNumber;";
            object parameters = new { EstimateID };
            return DbManager.DbQueryList<SaleItem>(handler, sqlCommand, parameters);
        }
        public static void LinkItemToDeliveryNote(int DeliveryNoteID, int ItemID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE sale_item SET DeliveryNoteID = @DeliveryNoteID WHERE ItemID = @ItemID;";
            object parameters = new { DeliveryNoteID, ItemID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UnlinkAllItemsFromDeliveryNote(int DeliveryNoteID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE sale_item SET DeliveryNoteID = NULL WHERE DeliveryNoteID = @DeliveryNoteID;";
            object parameters = new { DeliveryNoteID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void DeleteItemsBySaleId(int SaleID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM sale_item WHERE SaleID = @SaleID;";
            object parameters = new { SaleID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO sale_item (SaleID, ItemNumber, ProductID, Description, DeliveryDelay,"
                                    + " Quantity, Amount, TotalAmount, VatID, CustomImage, DeliveryNoteID, InvoiceID, Cost)"
                                    + " VALUES (@SaleID, @ItemNumber, @ProductID, @Description, @DeliveryDelay, @Quantity,"
                                    + " @Amount, @TotalAmount, @VatID, @CustomImage, @DeliveryNoteID, @InvoiceID, @Cost);";
            this.ItemID = DbManager.DbInsert(handler, sqlCommand, this);
        }

        public SaleItem CopyItem()
        {
            return new SaleItem()
            {
                ProductID = _ProductID,
                Description = _Description,
                DeliveryDelay = _DeliveryDelay,
                Quantity = _Quantity,
                Amount = _Amount,
                TotalAmount = _TotalAmount,
                VatID = _VatID,
                CustomImage = _CustomImage,
                Cost = _Cost,
                VatPercentage = _VatPercentage,
                ProductImage = _ProductImage
            };
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    public class SaleInvoice : DbEntity
    {
        public override int PrimaryKeyID { get { return SaleInvoiceID; } }

        public int SaleInvoiceID { get; set; }
        public int BusinessID { get; set; }
        public int CustomerID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int CurrencyID { get; set; }
        public string Status { get; set; }

        // Campos de soporte (no pertenecen a la tabla "sale_invoice")
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        public static List<SaleInvoice> GetInvoices(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " sale_invoice.SaleInvoiceID,"
                              + " sale_invoice.BusinessID,"
                              + " sale_invoice.CustomerID,"
                              + " sale_invoice.InvoiceDate,"
                              + " sale_invoice.InvoiceType,"
                              + " sale_invoice.InvoiceNumber,"
                              + " sale_invoice.TotalAmount,"
                              + " sale_invoice.CurrencyID,"
                              + " sale_invoice.Status,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol"
                              + " FROM sale_invoice"
                              + " INNER JOIN business ON business.BusinessID = sale_invoice.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = sale_invoice.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = sale_invoice.CurrencyID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<SaleInvoice>(handler, sqlCommand);
        }
        public static List<SaleInvoice> GetInvoicesByCustomerId(int CustomerID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale_invoice.SaleInvoiceID,"
                              + " sale_invoice.BusinessID,"
                              + " sale_invoice.CustomerID,"
                              + " sale_invoice.InvoiceDate,"
                              + " sale_invoice.InvoiceType,"
                              + " sale_invoice.InvoiceNumber,"
                              + " sale_invoice.TotalAmount,"
                              + " sale_invoice.CurrencyID,"
                              + " sale_invoice.Status,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol"
                              + " FROM sale_invoice"
                              + " INNER JOIN business ON business.BusinessID = sale_invoice.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = sale_invoice.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = sale_invoice.CurrencyID"
                              + " WHERE sale_invoice.CustomerID = @CustomerID"
                              + " ORDER BY sale_invoice.InvoiceDate DESC;";
            object parameters = new { CustomerID };
            return DbManager.DbQueryList<SaleInvoice>(handler, sqlCommand, parameters);
        }
        public static List<SaleInvoice> GetInvoicesByCustomerId(int CustomerID, int CurrencyID, DateTime startDate, DateTime endDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale_invoice.SaleInvoiceID,"
                              + " sale_invoice.BusinessID,"
                              + " sale_invoice.CustomerID,"
                              + " sale_invoice.InvoiceDate,"
                              + " sale_invoice.InvoiceType,"
                              + " sale_invoice.InvoiceNumber,"
                              + " sale_invoice.TotalAmount,"
                              + " sale_invoice.CurrencyID,"
                              + " sale_invoice.Status,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol"
                              + " FROM sale_invoice"
                              + " INNER JOIN business ON business.BusinessID = sale_invoice.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = sale_invoice.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = sale_invoice.CurrencyID"
                              + " WHERE sale_invoice.CustomerID = @CustomerID"
                              + " AND sale_invoice.CurrencyID = @CurrencyID"
                              + " AND sale_invoice.InvoiceDate >= @startDate"
                              + " AND sale_invoice.InvoiceDate <= @endDate"
                              + " ORDER BY sale_invoice.InvoiceDate ASC;";
            object parameters = new { CustomerID, CurrencyID, startDate, endDate };
            return DbManager.DbQueryList<SaleInvoice>(handler, sqlCommand, parameters);
        }
        public static List<SaleInvoice> GetInvoicesByPaymentId(int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale_invoice.SaleInvoiceID,"
                              + " sale_invoice.InvoiceNumber,"
                              + " sale_invoice.TotalAmount,"
                              + " currency.CurrencySymbol"
                              + " FROM sale_invoice"
                              + " INNER JOIN currency ON currency.CurrencyID = sale_invoice.CurrencyID"
                              + " INNER JOIN customer_payment_link ON customer_payment_link.SaleInvoiceID = sale_invoice.SaleInvoiceID"
                              + " WHERE customer_payment_link.CustomerPaymentID = @CustomerPaymentID"
                              + " ORDER BY sale_invoice.InvoiceDate DESC;";
            object parameters = new { CustomerPaymentID };
            return DbManager.DbQueryList<SaleInvoice>(handler, sqlCommand, parameters);
        }
        public static List<SaleInvoice> GetInvoicesBySaleId(int SaleID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT "
                                    + " sale_invoice.SaleInvoiceID,"
                                    + " sale_invoice.InvoiceNumber,"
                                    + " sale_invoice.InvoiceDate,"
                                    + " sale_invoice.TotalAmount,"
                                    + " currency.CurrencySymbol"
                                    + " FROM sale_invoice"
                                    + " INNER JOIN currency ON currency.CurrencyID = sale_invoice.CurrencyID"
                                    + " INNER JOIN sale_invoice_link ON sale_invoice_link.SaleInvoiceID = sale_invoice.SaleInvoiceID"
                                    + " WHERE sale_invoice_link.SaleID = @SaleID"
                                    + " ORDER BY sale_invoice.InvoiceDate DESC;";
            object parameters = new { SaleID };
            return DbManager.DbQueryList<SaleInvoice>(handler, sqlCommand, parameters);
        }
        public static SaleInvoice GetInvoiceById(int SaleInvoiceID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " sale_invoice.SaleInvoiceID,"
                              + " sale_invoice.BusinessID,"
                              + " sale_invoice.CustomerID,"
                              + " sale_invoice.InvoiceDate,"
                              + " sale_invoice.InvoiceType,"
                              + " sale_invoice.InvoiceNumber,"
                              + " sale_invoice.TotalAmount,"
                              + " sale_invoice.CurrencyID,"
                              + " sale_invoice.Status,"
                              + " business.BusinessName,"
                              + " customer.CustomerName,"
                              + " currency.CurrencyName,"
                              + " currency.CurrencySymbol"
                              + " FROM sale_invoice"
                              + " INNER JOIN business ON business.BusinessID = sale_invoice.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = sale_invoice.CustomerID"
                              + " INNER JOIN currency ON currency.CurrencyID = sale_invoice.CurrencyID"
                              + " WHERE sale_invoice.SaleInvoiceID = @SaleInvoiceID;";
            object parameters = new { SaleInvoiceID };
            return DbManager.DbQuerySingle<SaleInvoice>(handler, sqlCommand, parameters);
        }
        public static decimal GetTotalByCustomerId(int CustomerID, int CurrencyID, DateTime endDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " SUM(sale_invoice.TotalAmount)"
                                    + " FROM sale_invoice"
                                    + " WHERE sale_invoice.CustomerID = @CustomerID"
                                    + " AND sale_invoice.CurrencyID = @CurrencyID"
                                    + " AND sale_invoice.InvoiceDate <= @endDate;";
            object parameters = new { CustomerID, CurrencyID, endDate };
            return DbManager.DbExecuteScalar<decimal>(handler, sqlCommand, parameters);
        }
        public static bool CheckInvoiceNumberDuplicates(string InvoiceNumber, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT EXISTS(SELECT 1 FROM sale_invoice WHERE InvoiceNumber = @InvoiceNumber);";
            object parameters = new { InvoiceNumber };
            return DbManager.DbExecuteScalar<bool>(handler, sqlCommand, parameters);
        }
        public static void LinkPayment(int SaleInvoiceID, int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            // Comando para asociar cobro con factura.
            const string sqlCommand1 = "INSERT INTO customer_payment_link (SaleInvoiceID, CustomerPaymentID) VALUES (@SaleInvoiceID, @CustomerPaymentID);";
            // Comando para actualizar estado de la factura.
            const string sqlCommand2 = "UPDATE sale_invoice SET Status = 'Pago' WHERE SaleInvoiceID = @SaleInvoiceID;";
            // Comando para actualizar estado la(s) venta(s) (por medio de factura).
            const string sqlCommand3 = "UPDATE (sale INNER JOIN sale_invoice_link ON sale.SaleID = sale_invoice_link.SaleID) SET sale.HasPayments = 1 WHERE sale_invoice_link.SaleInvoiceID = @SaleInvoiceID;";
            object parameters = new { SaleInvoiceID, CustomerPaymentID };
            // Ejecución de todos los comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand3, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand3, parameters);
            }
        }
        public static void UnlinkPaymentFromAll(int CustomerPaymentID, DbTransactionHandler handler = null)
        {
            // Comando para actualizar el estado de las facturas que solo tienen este cobro.
            const string sqlCommand1 = "UPDATE (sale_invoice INNER JOIN customer_payment_link ON sale_invoice.SaleInvoiceID = customer_payment_link.SaleInvoiceID)"
                                     + " SET sale_invoice.Status = 'Pendiente' WHERE customer_payment_link.CustomerPaymentID = @CustomerPaymentID"
                                     + " AND sale_invoice.SaleInvoiceID IN (SELECT customer_payment_link.SaleInvoiceID FROM customer_payment_link GROUP BY customer_payment_link.SaleInvoiceID HAVING COUNT(1) = 1);";
            // Comando para actualizar el estado de las ventas que solo tienen este cobro (por medio de factura).
            const string sqlCommand2 = "UPDATE (sale INNER JOIN sale_invoice_link ON sale.SaleID = sale_invoice_link.SaleID INNER JOIN customer_payment_link ON sale_invoice_link.SaleInvoiceID = customer_payment_link.SaleInvoiceID)"
                                     + " SET sale.HasPayments = 0 WHERE customer_payment_link.CustomerPaymentID = @CustomerPaymentID"
                                     + " AND sale.SaleID IN (SELECT sale_invoice_link.SaleID FROM (sale_invoice_link INNER JOIN customer_payment_link ON sale_invoice_link.SaleInvoiceID = customer_payment_link.SaleInvoiceID) GROUP BY sale_invoice_link.SaleID, sale_invoice_link.SaleInvoiceID HAVING COUNT(1) = 1);";
            // Comando para desvincular el cobro de todas las facturas.
            const string sqlCommand3 = "DELETE FROM customer_payment_link WHERE CustomerPaymentID = @CustomerPaymentID;";
            object parameters = new { CustomerPaymentID };
            // Ejecución de todos los comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand3, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand3, parameters);
            }
        }
        public static void DeleteInvoiceById(int SaleInvoiceID, DbTransactionHandler handler = null)
        {
            // Comando para actualizar el estado de las ventas que solo tienen esta factura.
            const string sqlCommand1 = "UPDATE (sale INNER JOIN sale_invoice_link ON sale.SaleID = sale_invoice_link.SaleID)"
                                     + " SET sale.HasInvoices = 0 WHERE sale_invoice_link.SaleInvoiceID = @SaleInvoiceID"
                                     + " AND sale.SaleID IN (SELECT sale_invoice_link.SaleID FROM sale_invoice_link GROUP BY sale_invoice_link.SaleID HAVING COUNT(1) = 1);";
            // Comando para eliminar la factura.
            const string sqlCommand2 = "DELETE FROM sale_invoice WHERE SaleInvoiceID = @SaleInvoiceID;";
            object parameters = new { SaleInvoiceID };
            // Ejecución de ambos comandos en una transacción.
            if (handler == null)
            {
                using (handler = new DbTransactionHandler())
                {
                    DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                    DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
                    handler.CommitTransaction();
                }
            }
            else
            {
                DbManager.DbExecuteNonQuery(handler, sqlCommand1, parameters);
                DbManager.DbExecuteNonQuery(handler, sqlCommand2, parameters);
            }
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO sale_invoice (BusinessID, CustomerID, InvoiceDate, InvoiceType, InvoiceNumber, TotalAmount, CurrencyID, Status)"
                              + " VALUES (@BusinessID, @CustomerID, @InvoiceDate, @InvoiceType, @InvoiceNumber, @TotalAmount, @CurrencyID, @Status);";
            this.SaleInvoiceID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE sale_invoice SET"
                              + " BusinessID = @BusinessID,"
                              + " CustomerID = @CustomerID,"
                              + " InvoiceDate = @InvoiceDate,"
                              + " InvoiceType = @InvoiceType,"
                              + " InvoiceNumber = @InvoiceNumber,"
                              + " TotalAmount = @TotalAmount,"
                              + " CurrencyID = @CurrencyID,"
                              + " Status = @Status"
                              + " WHERE SaleInvoiceID = @SaleInvoiceID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class ScheduledTask : DbEntity
    {
        public override int PrimaryKeyID { get { return TaskID; } }

        public int TaskID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool Completed { get; set; }

        public static List<ScheduledTask> GetOutdatedTasks(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " task.TaskID,"
                                    + " task.Date,"
                                    + " task.Description,"
                                    + " task.Priority,"
                                    + " task.Completed"
                                    + " FROM task"
                                    + " WHERE task.Completed = 0 AND task.Date < @today;";
            object parameters = new { today = DateTime.Today };
            return DbManager.DbQueryList<ScheduledTask>(handler, sqlCommand, parameters);
        }
        public static List<ScheduledTask> GetTasksByDate(DateTime Date, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " task.TaskID,"
                                    + " task.Date,"
                                    + " task.Description,"
                                    + " task.Priority,"
                                    + " task.Completed"
                                    + " FROM task"
                                    + " WHERE task.Date = @Date"
                                    + " ORDER BY task.Completed ASC, task.Priority ASC;";
            object parameters = new { Date };
            return DbManager.DbQueryList<ScheduledTask>(handler, sqlCommand, parameters);
        }
        public static List<DateTime> GetPendingTasksDates(DateTime StartDate, DateTime EndDate, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT DISTINCT Date FROM task WHERE (Date BETWEEN @StartDate AND @EndDate) AND Completed = 0;";
            object parameters = new { StartDate, EndDate };
            var result = DbManager.DbQueryList<dynamic>(handler, sqlCommand, parameters);
            return result.Select(x => ((DateTime)x.Date).Date).ToList();
        }
        public static ScheduledTask GetTaskById(int TaskID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " task.TaskID,"
                                    + " task.Date,"
                                    + " task.Description,"
                                    + " task.Priority,"
                                    + " task.Completed"
                                    + " FROM task"
                                    + " WHERE task.TaskID = @TaskID;";
            object parameters = new { TaskID };
            return DbManager.DbQuerySingle<ScheduledTask>(handler, sqlCommand, parameters);
        }
        public static void SetTaskCompleted(int TaskID, bool Completed, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE task SET Completed = @Completed WHERE TaskID = @TaskID;";
            object parameters = new { TaskID, Completed };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void DeleteTaskById(int TaskID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM task WHERE TaskID = @TaskID;";
            object parameters = new { TaskID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO task (Date, Description, Priority, Completed) VALUES (@Date, @Description, @Priority, @Completed);";
            this.TaskID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE task SET"
                                    + " Date = @Date,"
                                    + " Description = @Description,"
                                    + " Priority = @Priority,"
                                    + " Completed = @Completed"
                                    + " WHERE TaskID = @TaskID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }
    public class SealType : DbEntity
    {
        public override int PrimaryKeyID { get { return SealTypeID; } }

        public int SealTypeID { get; set; }
        public string TypeDescription { get; set; }

        public static List<SealType> GetSealTypes(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT seal_type.SealTypeID, seal_type.TypeDescription FROM seal_type ORDER BY seal_type.SealTypeID ASC;";
            return DbManager.DbQueryList<SealType>(handler, sqlCommand);
        }
    }
    public class Shipping : DbEntity
    {
        public override int PrimaryKeyID { get { return ShippingID; } }

        public int ShippingID { get; set; }
        public string ShippingName { get; set; }

        public static List<Shipping> GetShippings(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                              + " shipping.ShippingID,"
                              + " shipping.ShippingName"
                              + " FROM shipping"
                              + " ORDER BY shipping.ShippingID ASC;";
            return DbManager.DbQueryList<Shipping>(handler, sqlCommand);
        }
    }
    public class ShippingCarrier : DbEntity
    {
        public override int PrimaryKeyID { get { return ShippingCarrierID; } }

        public int ShippingCarrierID { get; set; }
        public string CarrierName { get; set; }

        public static List<ShippingCarrier> GetCarriers(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT ShippingCarrierID, CarrierName FROM shipping_carrier ORDER BY ShippingCarrierID ASC;";
            return DbManager.DbQueryList<ShippingCarrier>(handler, sqlCommand);
        }
    }
    public class UpdateType : DbEntity
    {
        public override int PrimaryKeyID { get { return UpdateTypeID; } }

        public int UpdateTypeID { get; set; }
        public string UpdateTypeName { get; set; }
        public int RequiredAccessLevel { get; set; }
        public int Stage { get; set; }
        public bool AllowDuplicates { get; set; }

        public static List<UpdateType> GetAllowedUpdateTypes(int AccessLevel, int RepairOrderID, int Stage, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " update_type.UpdateTypeID,"
                                    + " update_type.UpdateTypeName"
                                    + " FROM update_type"
                                    + " WHERE update_type.RequiredAccessLevel <= @AccessLevel"
                                    + " AND update_type.Stage = @Stage"
                                    + " AND update_type.UpdateTypeID NOT IN"
                                    + " (SELECT update_type.UpdateTypeID"
                                    + " FROM (update_type INNER JOIN progress_update ON progress_update.UpdateTypeID = update_type.UpdateTypeID)"
                                    + " WHERE progress_update.RepairOrderID = @RepairOrderID"
                                    + " AND update_type.AllowDuplicates = 0);";
            object parameters = new { AccessLevel, RepairOrderID, Stage };
            return DbManager.DbQueryList<UpdateType>(handler, sqlCommand, parameters);
        }
    }
    public class User : DbEntity
    {
        public override int PrimaryKeyID { get { return UserID; } }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AccessLevel { get; set; }
        public string ChatColor { get; set; }
        public int DepartmentID { get; set; }
        public string Notifications { get; set; }

        // Campos de soporte (no pertenecen a la tabla "user").
        public string DepartmentName { get; set; }

        public static List<User> GetUsersByAccessLevel(int AccessLevel, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " user.UserID,"
                                    + " user.UserName,"
                                    + " user.Password,"
                                    + " user.AccessLevel,"
                                    + " user.ChatColor,"
                                    + " user.DepartmentID,"
                                    + " user.Notifications,"
                                    + " department.DepartmentName"
                                    + " FROM user"
                                    + " INNER JOIN department ON department.DepartmentID = user.DepartmentID"
                                    + " WHERE user.AccessLevel >= @AccessLevel"
                                    + " ORDER BY user.UserName ASC;";
            object parameters = new { AccessLevel };
            return DbManager.DbQueryList<User>(handler, sqlCommand, parameters);
        }
        public static List<User> GetUsersBelowAccessLevel(int AccessLevel, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " user.UserID,"
                                    + " user.UserName,"
                                    + " user.Password,"
                                    + " user.AccessLevel,"
                                    + " user.ChatColor,"
                                    + " user.DepartmentID,"
                                    + " user.Notifications,"
                                    + " department.DepartmentName"
                                    + " FROM user"
                                    + " INNER JOIN department ON department.DepartmentID = user.DepartmentID"
                                    + " WHERE user.AccessLevel <= @AccessLevel AND user.AccessLevel > 0"
                                    + " ORDER BY user.UserName ASC;";
            object parameters = new { AccessLevel };
            return DbManager.DbQueryList<User>(handler, sqlCommand, parameters);
        }
        public static List<User> GetUsersByDepartmentId(int DepartmentID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " user.UserID,"
                                    + " user.UserName,"
                                    + " user.Password,"
                                    + " user.AccessLevel,"
                                    + " user.ChatColor,"
                                    + " user.DepartmentID,"
                                    + " user.Notifications,"
                                    + " department.DepartmentName"
                                    + " FROM user"
                                    + " INNER JOIN department ON department.DepartmentID = user.DepartmentID"
                                    + " WHERE user.DepartmentID = @DepartmentID"
                                    + " ORDER BY user.UserName ASC;";
            object parameters = new { DepartmentID };
            return DbManager.DbQueryList<User>(handler, sqlCommand, parameters);
        }
        public static User GetPotentialUserByUserName(string UserName, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " user.UserID,"
                                    + " user.UserName,"
                                    + " user.Password,"
                                    + " user.AccessLevel,"
                                    + " user.ChatColor,"
                                    + " user.DepartmentID,"
                                    + " user.Notifications,"
                                    + " department.DepartmentName"
                                    + " FROM user"
                                    + " INNER JOIN department ON department.DepartmentID = user.DepartmentID"
                                    + " WHERE user.UserName = @UserName;";
            object parameters = new { UserName };
            return DbManager.DbQuerySingleOrDefault<User>(handler, sqlCommand, parameters);
        }
        public static void ChangePassword(int UserID, string NewPassword, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE user SET Password = @NewPassword WHERE UserID = @UserID;";
            object parameters = new { UserID, NewPassword };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
        public static void UpdateNotifications(int UserID, string Notifications, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE user SET Notifications = @Notifications WHERE UserID = @UserID;";
            object parameters = new { UserID, Notifications };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }
    }
    public class Vat : DbEntity
    {
        public override int PrimaryKeyID { get { return VatID; } }

        public int VatID { get; set; }
        public string Description { get; set; }
        public decimal VatPercentage { get; set; }
        public int AfipCode { get; set; }

        public static List<Vat> GetVats(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT vat.VatID, vat.Description, vat.VatPercentage, vat.AfipCode FROM vat ORDER BY vat.VatID ASC;";
            return DbManager.DbQueryList<Vat>(handler, sqlCommand);
        }
    }
    public class Event : DbEntity
    {
        public override int PrimaryKeyID { get { return EventID; } }

        public int EventID { get; set; }
        public int EventCode { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }

        public static List<Event> GetEvents(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT event.EventID, event.EventCode, event.Description FROM event ORDER BY event.EventCode ASC;";
            return DbManager.DbQueryList<Event>(handler, sqlCommand);
        }
    }
    public class Record : DbEntity
    {
        public override int PrimaryKeyID { get { return RecordID; } }

        public int RecordID { get; set; }
        public DateTime Time { get; set; }
        public int UserID { get; set; }
        public int EventCode { get; set; }
        public int? RefID { get; set; }

        // Campos de soporte (no pertenecen a la tabla "record").
        public string UserName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Time:dd/MM/yy HH:mm} - {Description} ({UserName})"; 
        }

        public static List<Record> GetRecordsByDate(DateTime date, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " record.RecordID,"
                                    + " record.Time,"
                                    + " record.UserID,"
                                    + " record.EventCode,"
                                    + " record.RefID,"
                                    + " user.UserName,"
                                    + " event.Description"
                                    + " FROM record"
                                    + " INNER JOIN user ON user.UserID = record.UserID"
                                    + " INNER JOIN event ON event.EventCode = record.EventCode"
                                    + " WHERE DATE(record.Time) = @Date"
                                    + " ORDER BY record.Time DESC;";
            object parameters = new { date.Date };
            return DbManager.DbQueryList<Record>(handler, sqlCommand, parameters);
        }
    }
    public class TechReport : DbEntity
    {
        public override int PrimaryKeyID { get { return TechReportID; } }

        public int TechReportID { get; set; }
        public int BusinessID { get; set; }
        public int CustomerID { get; set; }
        public int? ContactID { get; set; }
        public DateTime Date { get; set; }
        public string ReportBody { get; set; }

        // Campos de soporte (no pertenecen a la tabla "tech_report")
        public string BusinessName { get; set; }
        public string CustomerName { get; set; }

        public static List<TechReport> GetReports(string viewSettingsString, int recordLimit = DbLayerSettings.DefaultRecordLimit, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " tech_report.TechReportID,"
                              + " tech_report.BusinessID,"
                              + " tech_report.CustomerID,"
                              + " tech_report.ContactID,"
                              + " tech_report.Date,"
                              + " business.BusinessName,"
                              + " customer.CustomerName"
                              + " FROM tech_report"
                              + " INNER JOIN business ON business.BusinessID = tech_report.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = tech_report.CustomerID"
                              + viewSettingsString
                              + $" LIMIT {recordLimit};";
            return DbManager.DbQueryList<TechReport>(handler, sqlCommand);
        }
        public static TechReport GetReportById(int TechReportID, DbTransactionHandler handler = null)
        {
            string sqlCommand = "SELECT"
                              + " tech_report.TechReportID,"
                              + " tech_report.BusinessID,"
                              + " tech_report.CustomerID,"
                              + " tech_report.ContactID,"
                              + " tech_report.Date,"
                              + " tech_report.ReportBody,"
                              + " business.BusinessName,"
                              + " customer.CustomerName"
                              + " FROM tech_report"
                              + " INNER JOIN business ON business.BusinessID = tech_report.BusinessID"
                              + " INNER JOIN customer ON customer.CustomerID = tech_report.CustomerID"
                              + " WHERE tech_report.TechReportID = @TechReportID;";
            object parameters = new { TechReportID };
            return DbManager.DbQuerySingle<TechReport>(handler, sqlCommand, parameters);
        }
        public static void DeleteReportById(int TechReportID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "DELETE FROM tech_report WHERE TechReportID = @TechReportID;";
            object parameters = new { TechReportID };
            DbManager.DbExecuteNonQuery(handler, sqlCommand, parameters);
        }

        public void Insert(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "INSERT INTO tech_report (BusinessID, CustomerID, ContactID, Date, ReportBody)"
                                    + " VALUES (@BusinessID, @CustomerID, @ContactID, @Date, @ReportBody);";
            this.TechReportID = DbManager.DbInsert(handler, sqlCommand, this);
        }
        public void Update(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "UPDATE tech_report SET"
                                    + " BusinessID = @BusinessID,"
                                    + " CustomerID = @CustomerID,"
                                    + " ContactID = @ContactID,"
                                    + " Date = @Date,"
                                    + " ReportBody = @ReportBody"
                                    + " WHERE TechReportID = @TechReportID;";
            DbManager.DbExecuteNonQuery(handler, sqlCommand, this);
        }
    }

    public class City : DbEntity
    {
        public override int PrimaryKeyID { get { return CityID; } }

        public int CityID { get; set; }
        public string CityName { get; set; }
        public int PostalCode { get; set; }
        public int DistrictID { get; set; }
        public int CountryID { get; set; }

        public static List<City> GetCities(int CountryID, int DistrictID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " city.CityID,"
                                    + " city.CityName,"
                                    + " city.PostalCode,"
                                    + " city.DistrictID,"
                                    + " city.CountryID"
                                    + " FROM geo_db.city"
                                    + " WHERE city.CountryID = @CountryID AND city.DistrictID = @DistrictID"
                                    + " ORDER BY city.CityName ASC;";
            object parameters = new { CountryID, DistrictID };
            return DbManager.DbQueryList<City>(handler, sqlCommand, parameters);
        }
    }
    public class Country : DbEntity
    {
        public override int PrimaryKeyID { get { return CountryID; } }

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public static List<Country> GetCountries(DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " country.CountryID,"
                                    + " country.CountryName"
                                    + " FROM geo_db.country"
                                    + " ORDER BY country.CountryName ASC;";
            return DbManager.DbQueryList<Country>(handler, sqlCommand);
        }
    }
    public class District : DbEntity
    {
        public override int PrimaryKeyID { get { return DistrictID; } }

        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int CountryID { get; set; }

        public static List<District> GetDistrictsByCountryID(int CountryID, DbTransactionHandler handler = null)
        {
            const string sqlCommand = "SELECT"
                                    + " district.DistrictID,"
                                    + " district.DistrictName,"
                                    + " district.CountryID"
                                    + " FROM geo_db.district"
                                    + " WHERE district.CountryID = @CountryID"
                                    + " ORDER BY district.DistrictName ASC;";
            object parameters = new { CountryID };
            return DbManager.DbQueryList<District>(handler, sqlCommand, parameters);
        }
    }
}