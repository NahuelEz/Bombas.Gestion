using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using ClosedXML.Excel;


namespace Clover.Gestion
{


    public partial class Main : Form
    {
        public EndPointManager ChatServerEndPoint;
        public Dictionary<int, List<ChatMessage>> MessageHistory = new Dictionary<int, List<ChatMessage>>();

        private ViewSettings viewSettingsCustomers;
        private ViewSettings viewSettingsEstimates;
        private ViewSettings viewSettingsSales;
        private ViewSettings viewSettingsSaleInvoices;
        private ViewSettings viewSettingsIssuedNotes;
        private ViewSettings viewSettingsCustomerPayments;
        private ViewSettings viewSettingsProducts;
        private ViewSettings viewSettingsProviders;
        private ViewSettings viewSettingsPurchaseOrders;
        private ViewSettings viewSettingsInputs;
        private ViewSettings viewSettingsExpenses;
        private ViewSettings viewSettingsPurchaseInvoices;
        private ViewSettings viewSettingsPayOrders;
        private ViewSettings viewSettingsRepairOrders;
        private ViewSettings viewSettingsSellosOrders;
        private ViewSettings viewSettingsTechReports;

        private CloverCalendar taskCalendar = new CloverCalendar();
        private DateTime? lastMessageDate = null;

        private List<Record> TodayRecords = new List<Record>();
        private List<int> KnownRecords = new List<int>();



        public Main()
        {

            Logger.SetOutputFilename(Path.Combine(Application.StartupPath, "Clover.Gestion.log"));

            // ViewSettings Customers
            viewSettingsCustomers = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("customer.CustomerID", "ID Cliente", DataFieldTypes.Integer),
                new DataField("customer.CustomerName", "Nombre", DataFieldTypes.String),
                new DataField("customer.IdentityNumber", "Documento", DataFieldTypes.String),
                new DataField("customer.TaxGroup", "Condición IVA", DataFieldTypes.String),
                new DataField("payment.PaymentName", "Medio de pago", DataFieldTypes.String),
                new DataField("customer.PaymentTerm", "Plazo de pago", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Asignado a", DataFieldTypes.String),
                new DataField("customer.District", "Distrito", DataFieldTypes.String),
                new DataField("customer.City", "Ciudad", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsCustomers.AddSortLevel("customer.CustomerName", SortDirection.ASC);

            // ViewSettings Estimates
            viewSettingsEstimates = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("estimate.EstimateID", "ID Presupuesto", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("customer.CustomerName", "Cliente", DataFieldTypes.String),
                new DataField("estimate.Date", "Fecha creación", DataFieldTypes.DateTime),
                new DataField("estimate.TotalBeforeTax", "Total sin IVA", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String),
                new DataField("payment.PaymentName", "Medio de pago", DataFieldTypes.String),
                new DataField("estimate.ExpirationDate", "Vencimiento", DataFieldTypes.DateTime),
                new DataField("estimate.Status", "Estado", DataFieldTypes.String),
                new DataField("IsUnmarkedText", "Condición", DataFieldTypes.String, true)
            });
            viewSettingsEstimates.AddSortLevel("estimate.Status", SortDirection.ASC);
            viewSettingsEstimates.AddSortLevel("estimate.Date", SortDirection.DESC);
            viewSettingsEstimates.AddSortLevel("business.BusinessName", SortDirection.ASC);

            // ViewSettings Sales
            viewSettingsSales = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("sale.SaleID", "ID Venta",DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("customer.CustomerName", "Cliente", DataFieldTypes.String),
                new DataField("sale.Date", "Fecha venta", DataFieldTypes.DateTime),
                new DataField("sale.TotalBeforeTax", "Total sin IVA", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String),
                new DataField("payment.PaymentName", "Medio de pago", DataFieldTypes.String),
                new DataField("IsUnmarkedText", "Condición", DataFieldTypes.String, true),
                new DataField("HasInvoicesText", "Facturado", DataFieldTypes.String, true),
                new DataField("HasPaymentsText", "Cobrado", DataFieldTypes.String, true)
            });
            viewSettingsSales.AddSortLevel("sale.Date", SortDirection.DESC);
            viewSettingsSales.AddSortLevel("business.BusinessName", SortDirection.ASC);
            viewSettingsSales.AddSortLevel("customer.CustomerName", SortDirection.ASC);

            // ViewSettings SaleInvoices
            viewSettingsSaleInvoices = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("sale_invoice.SaleInvoiceID", "ID Factura", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("sale_invoice.InvoiceDate", "Fecha factura", DataFieldTypes.DateTime),
                new DataField("customer.CustomerName", "Cliente", DataFieldTypes.String),
                new DataField("sale_invoice.InvoiceType", "Tipo factura", DataFieldTypes.String),
                new DataField("sale_invoice.InvoiceNumber", "N° de factura", DataFieldTypes.String),
                new DataField("sale_invoice.TotalAmount", "Importe", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String),
                new DataField("sale_invoice.Status", "Estado", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsSaleInvoices.AddSortLevel("sale_invoice.InvoiceDate", SortDirection.DESC);
            viewSettingsSaleInvoices.AddSortLevel("business.BusinessName", SortDirection.ASC);
            viewSettingsSaleInvoices.AddSortLevel("customer.CustomerName", SortDirection.ASC);

            // ViewSettings IssuedNotes
            viewSettingsIssuedNotes = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("issued_note.NoteID", "ID Nota", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("issued_note.Date", "Fecha nota", DataFieldTypes.DateTime),
                new DataField("customer.CustomerName", "Cliente", DataFieldTypes.String),
                new DataField("IsDebitText", "Debito/Credito", DataFieldTypes.String, true),
                new DataField("issued_note.NoteType", "Tipo nota", DataFieldTypes.String),
                new DataField("issued_note.NoteNumber", "N° de nota", DataFieldTypes.String),
                new DataField("issued_note.TotalAmount", "Importe", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsIssuedNotes.AddSortLevel("issued_note.Date", SortDirection.DESC);

            // ViewSettings CustomerPayments
            viewSettingsCustomerPayments = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("customer_payment.CustomerPaymentID", "ID Cobro", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("customer.CustomerName", "Cliente", DataFieldTypes.String),
                new DataField("customer_payment.Date", "Fecha", DataFieldTypes.DateTime),
                new DataField("account.AccountName", "Cuenta", DataFieldTypes.String),
                new DataField("customer_payment.TotalAmount", "Importe", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String),
                new DataField("IsUnmarkedText", "Condición", DataFieldTypes.String, true),
                new DataField("customer_payment.FechaChequeDiferido", "Fecha Cheque Diferido", DataFieldTypes.DateTime),
            });
            // Orden predeterminado
            viewSettingsCustomerPayments.AddSortLevel("customer_payment.Date", SortDirection.DESC);
            viewSettingsCustomerPayments.AddSortLevel("business.BusinessName", SortDirection.ASC);

            // ViewSettings Products
            viewSettingsProducts = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("product.ProductID", "ID Producto", DataFieldTypes.Integer),
                new DataField("product.PartCode", "Código de parte", DataFieldTypes.String),
                new DataField("seal_type.TypeDescription", "Tipo de sello", DataFieldTypes.String),
                new DataField("product.Stock", "Stock", DataFieldTypes.Integer),
                new DataField("product.UnitPrice", "P. unitario sin IVA", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol","Moneda",DataFieldTypes.String)
            });
            viewSettingsProducts.AddSortLevel("product.PartCode", SortDirection.ASC);

            // ViewSettings Providers
            viewSettingsProviders = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("provider.ProviderID", "ID Proveedor", DataFieldTypes.Integer),
                new DataField("provider.ProviderName", "Nombre", DataFieldTypes.String),
                new DataField("provider.IdentityNumber", "Documento", DataFieldTypes.String),
                new DataField("provider.TaxGroup", "Condición IVA", DataFieldTypes.String),
                new DataField("business.BusinessName", "Asignado a", DataFieldTypes.String),
                new DataField("provider.District", "Distrito", DataFieldTypes.String),
                new DataField("provider.City", "Ciudad", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsProviders.AddSortLevel("provider.ProviderName", SortDirection.ASC);

            // ViewSettings PurchaseOrders
            viewSettingsPurchaseOrders = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("purchase_order.PurchaseOrderID", "ID Orden", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("purchase_order.Date", "Fecha", DataFieldTypes.DateTime),
                new DataField("provider.ProviderName", "Proveedor", DataFieldTypes.String),
                new DataField("purchase_order.Description", "Descripción", DataFieldTypes.String),
                new DataField("purchase_order.TotalBeforeTax", "Total sin IVA", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String),
                new DataField("purchase_invoice.InvoiceNumber", "N° de factura", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsPurchaseOrders.AddSortLevel("purchase_order.Date", SortDirection.DESC);
            viewSettingsPurchaseOrders.AddSortLevel("business.BusinessName", SortDirection.ASC);
            viewSettingsPurchaseOrders.AddSortLevel("provider.ProviderName", SortDirection.ASC);

            // ViewSettings Inputs
            viewSettingsInputs = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("input.InputID", "ID Insumo", DataFieldTypes.Integer),
                new DataField("item_category.CategoryName", "Categoría", DataFieldTypes.String),
                new DataField("item_subcategory.SubcategoryName", "Subcategoría", DataFieldTypes.String),
                new DataField("input.Description", "Descripción", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsInputs.AddSortLevel("item_category.CategoryName", SortDirection.ASC);
            viewSettingsInputs.AddSortLevel("item_subcategory.SubcategoryName", SortDirection.ASC);

            // ViewSettings Expenses
            viewSettingsExpenses = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("expense.ExpenseID", "ID Gasto", DataFieldTypes.Integer),
                new DataField("expense.Date", "Fecha", DataFieldTypes.DateTime),
                new DataField("item_category.CategoryName", "Categoría", DataFieldTypes.String),
                new DataField("item_subcategory.SubcategoryName", "Subcategoría", DataFieldTypes.String),
                new DataField("expense.Amount", "Importe", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String),
                new DataField("expense.InvoiceNumber", "N° de factura", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsExpenses.AddSortLevel("expense.Date", SortDirection.DESC);
            viewSettingsExpenses.AddSortLevel("item_category.CategoryName", SortDirection.ASC);
            viewSettingsExpenses.AddSortLevel("item_subcategory.SubcategoryName", SortDirection.ASC);

            // ViewSettings PurchaseInvoices
            viewSettingsPurchaseInvoices = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("purchase_invoice.PurchaseInvoiceID", "ID Factura", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("purchase_invoice.InvoiceDate", "Fecha factura", DataFieldTypes.DateTime),
                new DataField("provider.ProviderName", "Proveedor", DataFieldTypes.String),
                new DataField("purchase_invoice.InvoiceType", "Tipo factura", DataFieldTypes.String),
                new DataField("purchase_invoice.InvoiceNumber", "N° de factura", DataFieldTypes.String),
                new DataField("purchase_invoice.TotalAmount", "Importe", DataFieldTypes.Integer),
                new DataField("currency.CurrencySymbol", "Moneda", DataFieldTypes.String),
                new DataField("purchase_invoice.Status", "Estado", DataFieldTypes.String),
                new DataField("purchase_invoice.PayOrderID", "ID Orden de pago", DataFieldTypes.Integer)
            });
            // Orden predeterminado
            viewSettingsPurchaseInvoices.AddSortLevel("purchase_invoice.InvoiceDate", SortDirection.DESC);
            viewSettingsPurchaseInvoices.AddSortLevel("business.BusinessName", SortDirection.ASC);
            viewSettingsPurchaseInvoices.AddSortLevel("provider.ProviderName", SortDirection.ASC);

            // ViewSettings PayOrders
            viewSettingsPayOrders = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("pay_order.Date", "Fecha", DataFieldTypes.DateTime),
                new DataField("pay_order.PayOrderID", "ID Orden pago", DataFieldTypes.Integer),
                new DataField("provider.ProviderName", "Proveedor", DataFieldTypes.String)
            });
            // Orden predeterminado
            viewSettingsPayOrders.AddSortLevel("pay_order.Date", SortDirection.DESC);
            viewSettingsPayOrders.AddSortLevel("business.BusinessName", SortDirection.ASC);
            viewSettingsPayOrders.AddSortLevel("provider.ProviderName", SortDirection.ASC);

            // ViewSettings RepairOrders
            viewSettingsRepairOrders = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("repair_order.RepairOrderID", "ID Orden", DataFieldTypes.Integer),
                new DataField("repair_order.EstimateID", "ID Presupuesto", DataFieldTypes.Integer),
                new DataField("repair_order.Date", "Fecha", DataFieldTypes.DateTime),
                new DataField("CustomerName", "Cliente", DataFieldTypes.String, true),
                new DataField("repair_order.PumpBrand", "Marca", DataFieldTypes.String),
                new DataField("repair_order.PumpModel", "Modelo", DataFieldTypes.String),
                new DataField("repair_order.Status", "Estado", DataFieldTypes.String),
                new DataField("repair_order.DeliveryNoteNumber", "N° de remito", DataFieldTypes.String),
                new DataField("repair_order.InvoiceNumber", "N° de factura", DataFieldTypes.String),
                new DataField("repair_order.PriorityName", "Prioridad", DataFieldTypes.String),
                new DataField("repair_order.Customer", "Cliente", DataFieldTypes.String),
            });
            // Orden predeterminado
            viewSettingsRepairOrders.AddSortLevel("repair_order.Status", SortDirection.ASC);
            viewSettingsRepairOrders.AddSortLevel("repair_order.Date", SortDirection.DESC);


            // ViewSettings SellosOrders
            viewSettingsSellosOrders = new ViewSettings(10, 5, new DataField[]
            {
            new DataField("sellosorders.OrdenID", "ID Orden", DataFieldTypes.Integer),
            new DataField("sellosorders.RefNumber", "N° de Referencia", DataFieldTypes.String),
            new DataField("sellosorders.OrderDate", "Fecha de Orden", DataFieldTypes.DateTime),
            new DataField("sellosorders.Customer", "Cliente", DataFieldTypes.String, true),
            new DataField("sellosorders.PhoneNumber", "Teléfono", DataFieldTypes.String),
            new DataField("sellosorders.DeliveryNoteNumberCustomer", "N° Remito Cliente", DataFieldTypes.String),
            new DataField("sellosorders.DeliveryNoteNumber", "N° de Remito", DataFieldTypes.String),
            new DataField("sellosorders.InvoiceNumber", "N° de Factura", DataFieldTypes.String),
            new DataField("sellosorders.MotorType", "Tipo de Motor", DataFieldTypes.String),
            new DataField("sellosorders.PumpBrand", "Marca de Bomba", DataFieldTypes.String),
            new DataField("sellosorders.Seal", "Sello", DataFieldTypes.String),
            new DataField("sellosorders.SealDeliveredTo", "Sello Entregado a", DataFieldTypes.String),
            new DataField("sellosorders.ComboBox1", "ComboBox1", DataFieldTypes.String),
            new DataField("sellosorders.PistaRotativa", "Pista Rotativa", DataFieldTypes.String),
            new DataField("sellosorders.Material", "Material", DataFieldTypes.String),
            new DataField("sellosorders.Pista", "Pista", DataFieldTypes.String),
            new DataField("sellosorders.PistaMaterial", "Material de Pista", DataFieldTypes.String),
            new DataField("sellosorders.Elastomero", "Elastómero", DataFieldTypes.String),
            new DataField("sellosorders.DiamAlambre", "Diámetro de Alambre", DataFieldTypes.String),
            new DataField("sellosorders.Vueltas", "Vueltas", DataFieldTypes.String),
            new DataField("sellosorders.Peso", "Peso", DataFieldTypes.String),
            new DataField("sellosorders.Conex", "Conex", DataFieldTypes.String),
            new DataField("sellosorders.RPMs", "RPMs", DataFieldTypes.String),
            new DataField("sellosorders.RanuraAlt", "Altura de Ranura", DataFieldTypes.String),
            new DataField("sellosorders.HP", "HP", DataFieldTypes.String),
            new DataField("sellosorders.Notes", "Notas", DataFieldTypes.String)
            });

            // Orden predeterminado
            viewSettingsSellosOrders.AddSortLevel("sellosorders.OrderDate", SortDirection.DESC);
            viewSettingsSellosOrders.AddSortLevel("sellosorders.Customer", SortDirection.ASC);




            // ViewSettings TechReports
            viewSettingsTechReports = new ViewSettings(5, 3, new DataField[]
            {
                new DataField("tech_report.TechReportID", "ID Informe", DataFieldTypes.Integer),
                new DataField("business.BusinessName", "Empresa", DataFieldTypes.String),
                new DataField("customer.CustomerName", "Cliente", DataFieldTypes.String),
                new DataField("tech_report.Date", "Fecha", DataFieldTypes.DateTime)
            });
            // Orden predeterminado
            viewSettingsTechReports.AddSortLevel("tech_report.Date", SortDirection.DESC);

            InitializeComponent();
            DbLayerSettings.SetConnectionString("192.168.100.77", 3306, "sellosmec_db_v2", "root", "Vial2911");
            CargarSellosOrders();

            // Configurar propiedades AutoGenerateColumns para los DataGridView necesarios
            dgvCustomers.AutoGenerateColumns = false;
            dgvEstimates.AutoGenerateColumns = false;
            dgvSales.AutoGenerateColumns = false;
            dgvSaleInvoices.AutoGenerateColumns = false;
            dgvIssuedNotes.AutoGenerateColumns = false;
            dgvCustomerPayments.AutoGenerateColumns = false;
            dgvProducts.AutoGenerateColumns = false;
            dgvProviders.AutoGenerateColumns = false;
            dgvPurchaseOrders.AutoGenerateColumns = false;
            dgvInputs.AutoGenerateColumns = false;
            dgvExpenses.AutoGenerateColumns = false;
            dgvPurchaseInvoices.AutoGenerateColumns = false;
            dgvPayOrders.AutoGenerateColumns = false;
            dgvRepairOrders.AutoGenerateColumns = false;
            dgvTechReports.AutoGenerateColumns = false;
            this.txtChatInput.KeyDown += new KeyEventHandler(txtChatInput_KeyDown);


            // Configurar el evento para taskCalendar
            taskCalendar.NewFrame += taskCalendar_NewFrame;



        }


        private void SetupTabsBasedOnUserID()
        {
            int userId = AppEnvironment.CurrentUser.UserID;

            // Limpiar todas las pestañas visibles
            tclMainContainer.TabPages.Clear();

            // Configurar pestañas principales según el UserID
            switch (userId)
            {
                case 4: // Departamento de Finanzas
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);
                    tclMainContainer.TabPages.Add(Saldos);
                    tclMainContainer.TabPages.Add(tabAccountancy);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabSales, tabSaleInvoices, tabIssuedNotes, tabCustomerPayments });
                    break;

                case 40: // Departamento de Cobranzas
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);
                    tclMainContainer.TabPages.Add(Saldos);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabSales, tabSaleInvoices, tabIssuedNotes, tabCustomerPayments });
                    break;





                case 3: // Departamento de sello mec
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabProducts, tabRepairOrders });
                    ConfigurePurchasesContainerTabs(new List<TabPage> { tabProviders, tabPurchaseOrders });
                    break;


                case 23:
                case 24: // Departamento de sello mec
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabProducts, tabRepairOrders });
                    ConfigurePurchasesContainerTabs(new List<TabPage> { tabProviders, tabPurchaseOrders });
                    break;


                case 61: // Departamento de facturacion
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabSales, tabIssuedNotes, tabSaleInvoices });
                    break;



                case 36: // Departamento de Compras
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabRepairOrders, tabTechReports });
                    ConfigurePurchasesContainerTabs(new List<TabPage> { tabProviders, tabPurchaseOrders });
                    break;


                case 22:
                case 33: // Departamento de rep de bombas
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabRepairOrders, tabTechReports});
                    break;

                case 32:
                case 67:
                case 21:
                case 19: // Departamento de Compras
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers, tabEstimates, tabProducts});
                    ConfigurePurchasesContainerTabs(new List<TabPage> { tabProviders, tabPurchaseOrders });
                    break;



                case 5: // Departamento Pañol
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);

                    // Configurar sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(new List<TabPage> { tabCustomers });
                    ConfigurePurchasesContainerTabs(new List<TabPage> { tabProviders, tabPurchaseOrders, tabInputs });
                    break;



                case 99: // Departamento Administrativo (Acceso Completo)
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabAccountancy);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);
                    tclMainContainer.TabPages.Add(Saldos);
                    tclMainContainer.TabPages.Add(tabPage4);

                    // Mostrar todas las sub-pestañas dentro de tclSalesContainer
                    ConfigureSalesContainerTabs(tclSalesContainer.TabPages.Cast<TabPage>().ToList());
                    break;




                default: // Usuarios no configurados tienen acceso completo
                         // Agregar todas las pestañas principales
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabAccountancy);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);
                    tclMainContainer.TabPages.Add(Saldos);

                    // Configurar sub-pestañas: Todas disponibles
                    ConfigureSalesContainerTabs(tclSalesContainer.TabPages.Cast<TabPage>().ToList());
                    ConfigurePurchasesContainerTabs(tclPurchasesContainer.TabPages.Cast<TabPage>().ToList());
                    break;



                case 1: // ADMINISTRADOR
                    tclMainContainer.TabPages.Add(tabSalesManagement);
                    tclMainContainer.TabPages.Add(tabPurchasesManagement);
                    tclMainContainer.TabPages.Add(tabAccountancy);
                    tclMainContainer.TabPages.Add(tabCalendar);
                    tclMainContainer.TabPages.Add(tabPage3);
                    tclMainContainer.TabPages.Add(tabChat);
                    tclMainContainer.TabPages.Add(tabPage1);
                    tclMainContainer.TabPages.Add(Saldos);
                    tclMainContainer.TabPages.Add(tabPage4);

                    // Configurar sub-pestañas: Todas disponibles
                    ConfigureSalesContainerTabs(tclSalesContainer.TabPages.Cast<TabPage>().ToList());
                    ConfigurePurchasesContainerTabs(tclPurchasesContainer.TabPages.Cast<TabPage>().ToList());
                    break;
            }
        }




        private void ConfigurePurchasesContainerTabs(List<TabPage> tabsToShow)
        {
            // Iterar sobre todas las sub-pestañas de tclPurchasesContainer
            foreach (TabPage subTab in tclPurchasesContainer.TabPages.Cast<TabPage>().ToList())
            {
                // Quitar las pestañas no permitidas
                if (!tabsToShow.Contains(subTab))
                {
                    tclPurchasesContainer.TabPages.Remove(subTab);
                }
            }

            // Agregar las pestañas permitidas que no estén ya presentes
            foreach (TabPage subTab in tabsToShow)
            {
                if (!tclPurchasesContainer.TabPages.Contains(subTab))
                {
                    tclPurchasesContainer.TabPages.Add(subTab);
                }
            }
        }


        private void ConfigureSalesContainerTabs(List<TabPage> tabsToShow)
        {
            // Quitar todas las pestañas no permitidas
            foreach (TabPage subTab in tclSalesContainer.TabPages.Cast<TabPage>().ToList())
            {
                if (!tabsToShow.Contains(subTab))
                {
                    tclSalesContainer.TabPages.Remove(subTab); // Eliminar sub-pestaña no permitida
                }
            }

            // Agregar todas las pestañas permitidas que no estén ya presentes
            foreach (TabPage subTab in tabsToShow)
            {
                if (!tclSalesContainer.TabPages.Contains(subTab))
                {
                    tclSalesContainer.TabPages.Add(subTab); // Agregar sub-pestaña permitida
                }
            }
        }


        private void btnAbrirCRM_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de entrada de contraseña
            string inputPassword = PromptForPassword();

            // Validar la contraseña ingresada
            if (IsValidPassword(inputPassword))
            {
                // Contraseña válida: abrir el formulario CRM_MAIN
                CRM_MAIN crmForm = new CRM_MAIN();
                crmForm.Show();
            }
            else
            {
                // Contraseña inválida: mostrar un mensaje de error
                MessageBox.Show("Contraseña incorrecta. No tienes acceso al CRM.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Muestra un cuadro de diálogo para que el usuario ingrese la contraseña.
        /// </summary>
        /// <returns>La contraseña ingresada por el usuario.</returns>
        private string PromptForPassword()
        {
            string password = null;

            using (Form passwordForm = new Form())
            {
                passwordForm.Text = "Autenticación requerida";
                passwordForm.Size = new Size(300, 150);
                passwordForm.StartPosition = FormStartPosition.CenterParent;

                Label lblPassword = new Label() { Text = "Ingrese la contraseña:", Left = 10, Top = 20, Width = 200 };
                TextBox txtPassword = new TextBox() { Left = 10, Top = 50, Width = 260, PasswordChar = '*' };
                Button btnOK = new Button() { Text = "Aceptar", Left = 60, Top = 80, Width = 80 };
                Button btnCancel = new Button() { Text = "Cancelar", Left = 160, Top = 80, Width = 80 };

                btnOK.DialogResult = DialogResult.OK;
                btnCancel.DialogResult = DialogResult.Cancel;

                passwordForm.Controls.Add(lblPassword);
                passwordForm.Controls.Add(txtPassword);
                passwordForm.Controls.Add(btnOK);
                passwordForm.Controls.Add(btnCancel);

                passwordForm.AcceptButton = btnOK;
                passwordForm.CancelButton = btnCancel;

                if (passwordForm.ShowDialog() == DialogResult.OK)
                {
                    password = txtPassword.Text;
                }
            }

            return password;
        }

        /// <summary>
        /// Valida si la contraseña ingresada es correcta.
        /// </summary>
        /// <param name="inputPassword">La contraseña ingresada.</param>
        /// <returns>True si la contraseña es válida, de lo contrario false.</returns>
        private bool IsValidPassword(string inputPassword)
        {
            // Establecer la contraseña correcta (puedes cambiarla o cargarla desde la configuración)
            const string correctPassword = "Admin123"; // Cambia esto por la contraseña deseada

            return inputPassword == correctPassword;
        }

        private void CargarSellosOrders()
        {
            // Obtener los datos de sellosorders
            DataTable sellosOrders = GetSellosOrders();

            // Asignar los datos al DataGridView
            dgvSellosrepair.DataSource = sellosOrders;
        }




        private DataTable GetSellosOrders()
        {
            DataTable sellosOrders = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();

                    // Consulta SQL corregida
                    string query = @"
                SELECT 
                    `sellosorders`.`OrdenID`,
                    `sellosorders`.`RefNumber`,
                    `sellosorders`.`OrderDate`,
                    `sellosorders`.`Customer`,
                    `sellosorders`.`PhoneNumber`,
                    `sellosorders`.`DeliveryNoteNumberCustomer`,
                    `sellosorders`.`DeliveryNoteNumber`,
                    `sellosorders`.`InvoiceNumber`,
                    `sellosorders`.`MotorType`,
                    `sellosorders`.`PumpBrand`,
                    `sellosorders`.`Seal`,
                    `sellosorders`.`SealDeliveredTo`,
                    `sellosorders`.`ComboBox1`,
                    `sellosorders`.`PistaRotativa`,
                    `sellosorders`.`Material`,
                    `sellosorders`.`Pista`,
                    `sellosorders`.`PistaMaterial`,
                    `sellosorders`.`Elastomero`,
                    `sellosorders`.`DiamAlambre`,
                    `sellosorders`.`Vueltas`,
                    `sellosorders`.`Peso`,
                    `sellosorders`.`Conex`,
                    `sellosorders`.`RPMs`,
                    `sellosorders`.`RanuraAlt`,
                    `sellosorders`.`HP`,
                    `sellosorders`.`Notes`
                FROM `sellosmec_db_v2`.`sellosorders`";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(sellosOrders);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de SellosOrders: " + ex.Message);
            }

            return sellosOrders;
        }




        private async void Main_Shown(object sender, EventArgs e)
        {
            try
            {
                using (var form = new Login())
                {
                    form.ShowDialog();
                    if (!form.LogInAuthorized)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }
            catch (Exception unknownException)
            {
                // Waypoint MA101
                MessageBox.Show("Error desconocido."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + unknownException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA101. Message: " + unknownException.Message);
                Application.Exit();
                return;
            }


            // Seleccionar la pestaña de inicio según la configuración
            tclMainContainer.SelectedIndex = AppEnvironment.CurrentSettings.StartTabIndex;
            switch (AppEnvironment.CurrentSettings.StartTabIndex)
            {
                case 0:
                    {
                        tclSalesContainer.SelectedIndex = AppEnvironment.CurrentSettings.StartSubtabIndex;
                        tclSalesContainer.TabPages[AppEnvironment.CurrentSettings.StartSubtabIndex].Select();
                        break;
                    }
                case 1:
                    {
                        tclPurchasesContainer.SelectedIndex = AppEnvironment.CurrentSettings.StartSubtabIndex;
                        tclPurchasesContainer.TabPages[AppEnvironment.CurrentSettings.StartSubtabIndex].Select();
                        break;
                    }
                case 2:
                    {
                        tclAccountancyContainer.SelectedIndex = AppEnvironment.CurrentSettings.StartSubtabIndex;
                        tclAccountancyContainer.TabPages[AppEnvironment.CurrentSettings.StartSubtabIndex].Select();
                        break;
                    }
                case 3:
                    {
                        tclMainContainer.TabPages[3].Select();
                        break;
                    }
            }
            tclMainContainer.Visible = true;

            // Cargar usuarios para el chat
            var chatUsers = new List<User>();
            try
            {
                chatUsers = await Task.Run(() => User.GetUsersByAccessLevel(4));
            }
            catch (Exception dbException)
            {
                // Waypoint CH101
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CH101 (Flag: MySQL). Message: " + dbException.Message);
                Application.Exit();
                return;
            }
            foreach (var user in chatUsers)
            {
                lvwClients.Items.Add(new ListViewItem() { Text = user.UserName, ForeColor = Color.DarkGray, Tag = user });
                // Carga historial de conversación con cada usuario registrado.
                try
                {
                    var messages = await Task.Run(() => ChatMessage.GetMessagesBetweenTwoUsers(AppEnvironment.CurrentUser.UserID, user.UserID, DateTime.Today.AddDays(-15)));
                    MessageHistory.Add(user.UserID, messages);
                }
                catch (Exception dbException)
                {
                    // Waypoint CH102
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint CH102 (Flag: MySQL). Message: " + dbException.Message);
                    continue;
                }
            }

            // Cargar historial de conversación pública
            var publicMessages = new List<ChatMessage>();
            try
            {
                publicMessages = await Task.Run(() => ChatMessage.GetPublicMessages(AppEnvironment.CurrentUser.UserID, DateTime.Today.AddDays(-15)));
            }
            catch (Exception dbException)
            {
                // Waypoint CH103
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CH103 (Flag: MySQL). Message: " + dbException.Message);
            }

            // Imprimir mensajes en ventana de chat
            foreach (var messageGroup in publicMessages.GroupBy(m => m.Timestamp.Date).OrderBy(g => g.Key))
            {
                rtbChatMessages.DeselectAll();
                rtbChatMessages.AppendText(Environment.NewLine);
                rtbChatMessages.SelectionAlignment = HorizontalAlignment.Center;
                rtbChatMessages.SelectionColor = Color.Black;
                rtbChatMessages.AppendText(messageGroup.Key.ToString("dddd, d 'de' MMMM"));
                foreach (var message in messageGroup.OrderBy(m => m.Timestamp))
                {
                    rtbChatMessages.DeselectAll();
                    rtbChatMessages.AppendText(Environment.NewLine);
                    rtbChatMessages.SelectionAlignment = HorizontalAlignment.Left;
                    if (message.SenderID == AppEnvironment.CurrentUser.UserID)
                    {
                        rtbChatMessages.SelectionColor = Color.Black;
                        rtbChatMessages.AppendText("Tu");
                    }
                    else
                    {
                        rtbChatMessages.SelectionColor = Color.FromName(message.ChatColor);
                        rtbChatMessages.AppendText(message.UserName);
                    }
                    rtbChatMessages.SelectionColor = Color.Black;
                    rtbChatMessages.AppendText(" : ");
                    rtbChatMessages.AppendText(message.Body);
                }
                lastMessageDate = messageGroup.Key;
            }
            rtbChatMessages.ScrollToCaret();

            // Conectar con el servidor de chat
            try
            {
                ChatServerEndPoint = await EndPointManager.ConnectAsync(new IPEndPoint(IPAddress.Parse(AppEnvironment.CurrentSettings.ChatServerIP), AppEnvironment.CurrentSettings.ChatServerPort), HandleEnvelope);
                // Envía handshake.
                await ChatServerEndPoint.SendEnvelopeAsync(new Envelope(AppEnvironment.CurrentUser, null, EnvelopeType.Handshake, null, false));
                MonitorConnection();
            }
            catch (Exception exception)
            {
                // Waypoint CH104
                lblConnectionError.Visible = true;
                btnSendMessage.Enabled = false;
                ChatServerEndPoint?.Shutdown();
                tmrRetryConnection.Start();
                Logger.AppendLog("Exception at Waypoint CH104. Message: " + exception.Message);
            }

            SetupTabsBasedOnUserID();
        }







        private DataTable GetSellosOrdersData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    `sellosorders`.`OrdenID`,
                    `sellosorders`.`RefNumber`,
                    `sellosorders`.`OrderDate`,
                    `sellosorders`.`Customer`,
                    `sellosorders`.`PhoneNumber`,
                    `sellosorders`.`DeliveryNoteNumberCustomer`,
                    `sellosorders`.`DeliveryNoteNumber`,
                    `sellosorders`.`InvoiceNumber`,
                    `sellosorders`.`MotorType`,
                    `sellosorders`.`PumpBrand`,
                    `sellosorders`.`Seal`,
                    `sellosorders`.`SealDeliveredTo`,
                    `sellosorders`.`ComboBox1`,
                    `sellosorders`.`PistaRotativa`,
                    `sellosorders`.`Material`,
                    `sellosorders`.`Pista`,
                    `sellosorders`.`PistaMaterial`,
                    `sellosorders`.`Elastomero`,
                    `sellosorders`.`DiamAlambre`,
                    `sellosorders`.`Vueltas`,
                    `sellosorders`.`Peso`,
                    `sellosorders`.`Conex`,
                    `sellosorders`.`RPMs`,
                    `sellosorders`.`RanuraAlt`,
                    `sellosorders`.`HP`,
                    `sellosorders`.`Notes`
                FROM `sellosmec_db_v2`.`sellosorders`";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de sellosorders: " + ex.Message);
            }

            return dataTable;
        }



        // Método para controlar el acceso basado en el nivel de usuario
        private void ControlAccessBasedOnLevel()
        {
            // Obtenemos el nivel de acceso del usuario
            int accessLevel = AppEnvironment.CurrentUser.AccessLevel;

            // Si el usuario no tiene nivel de acceso 5, ocultamos la pestaña tabPage4
            if (accessLevel < 5)
            {
                // Ocultar tabPage4
                tclMainContainer.TabPages.Remove(tabPage4);

                // Ocultar los botones btnOpenUserSelection y btnCreateGroupChat_Click
                btnOpenUserSelection.Visible = false;
                btnCreateGroupChat_Click.Visible = false;
            }
        }



        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChatServerEndPoint?.Shutdown();
        }


        private async void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                // Filtrado con Ctrl + B
                if (e.KeyCode == Keys.B) // Quick search with Ctrl + B
                {
                    switch (tclMainContainer.SelectedIndex)
                    {
                        case 0: // Ventas
                            await FiltrarVentas(); // Realiza el filtrado para ventas
                            break;
                        case 1: // Compras
                            await FiltrarCompras(); // Realiza el filtrado para compras
                            break;
                        default:
                            MessageBox.Show("No se puede filtrar en esta pestaña.");
                            break;
                    }
                }

                // Restaurar vista con Ctrl + R
                if (e.KeyCode == Keys.R) // Restore view settings with Ctrl + R
                {
                    switch (tclMainContainer.SelectedIndex)
                    {
                        case 0: // Ventas
                            await RestaurarVentas(); // Restaura la vista para ventas
                            break;
                        case 1: // Compras
                            await RestaurarCompras(); // Restaura la vista para compras
                            break;
                        default:
                            MessageBox.Show("No se puede restaurar la vista en esta pestaña.");
                            break;
                    }
                }
            }

            // Enviar mensaje con Enter en pestaña específica
            if (e.KeyCode == Keys.Enter)
            {
                if (tclMainContainer.SelectedIndex == 4)
                {
                    btnSendMessage.PerformClick();
                    e.SuppressKeyPress = true;
                }
            }
        }


        private async Task FiltrarVentas()
        {
            var activeTab = tclSalesContainer.SelectedTab; // Obtiene la pestaña activa

            if (activeTab == tabCustomers)
            {
                await EjecutarBusqueda("customer.CustomerName", viewSettingsCustomers, lblCustomersFilterWarning, UpdateCustomersAsync);
            }
            else if (activeTab == tabEstimates)
            {
                await EjecutarBusqueda("customer.CustomerName", viewSettingsEstimates, lblEstimatesFilterWarning, UpdateEstimatesAsync);
            }
            else if (activeTab == tabSales)
            {
                await EjecutarBusqueda("customer.CustomerName", viewSettingsSales, lblSalesFilterWarning, UpdateSalesAsync);
            }
            else if (activeTab == tabSaleInvoices)
            {
                await EjecutarBusqueda("customer.CustomerName", viewSettingsSaleInvoices, lblSaleInvoicesFilterWarning, UpdateSaleInvoicesAsync);
            }
            else if (activeTab == tabIssuedNotes)
            {
                await EjecutarBusqueda("customer.CustomerName", viewSettingsIssuedNotes, lblIssuedNotesFilterWarning, UpdateIssuedNotesAsync);
            }
            else if (activeTab == tabCustomerPayments)
            {
                await EjecutarBusqueda("customer.CustomerName", viewSettingsCustomerPayments, lblCustomerPaymentsFilterWarning, UpdateCustomerPaymentsAsync);
            }
            else if (activeTab == tabProducts)
            {
                await EjecutarBusqueda("product.PartCode", viewSettingsProducts, lblProductsFilterWarning, UpdateProductsAsync);
            }
            else if (activeTab == tabRepairOrders)
            {
                await EjecutarBusqueda("CustomerName", viewSettingsRepairOrders, lblRepairOrdersFilterWarning, UpdateRepairOrdersAsync);
            }
            else if (activeTab == tabTechReports)
            {
                await EjecutarBusqueda("customer.CustomerName", viewSettingsTechReports, lblTechReportsFilterWarning, UpdateTechReportsAsync);
            }
            else
            {
                MessageBox.Show("No se puede filtrar en esta pestaña.");
            }
        }


        private async Task FiltrarCompras()
        {
            switch (tclPurchasesContainer.SelectedIndex)
            {
                case 0: // Proveedores
                    await EjecutarBusqueda("provider.ProviderName", viewSettingsProviders, lblProvidersFilterWarning, UpdateProvidersAsync);
                    break;
                case 1: // Órdenes de compra
                    await EjecutarBusqueda("provider.ProviderName", viewSettingsPurchaseOrders, lblPurchaseOrdersFilterWarning, UpdatePurchaseOrdersAsync);
                    break;
                case 2: // Facturas de compra
                    await EjecutarBusqueda("provider.ProviderName", viewSettingsPurchaseInvoices, lblPurchaseInvoicesFilterWarning, UpdatePurchaseInvoicesAsync);
                    break;
                case 3: // Órdenes de pago
                    await EjecutarBusqueda("provider.ProviderName", viewSettingsPayOrders, lblPayOrdersFilterWarning, UpdatePayOrdersAsync);
                    break;
                default:
                    MessageBox.Show("No se puede filtrar en esta pestaña.");
                    break;
            }
        }

        private async Task EjecutarBusqueda(string campo, ViewSettings viewSettings, Label lblWarning, Func<Task> updateFunc)
        {
            try
            {
                using (var form = new SHA_QuickSearch("nombre"))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Verifica si el campo existe en la configuración de la vista
                        var field = viewSettings.Fields.FirstOrDefault(f => f.Name == campo);
                        if (field == null)
                        {
                            MessageBox.Show($"El campo '{campo}' no se encuentra en la configuración de vista.",
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Aplicar filtro
                        viewSettings.Filters.Clear();
                        viewSettings.Filters.Add(new Filter(field, 5, form.SearchInput));
                        lblWarning.Visible = true;

                        // Actualizar vista
                        await updateFunc();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog($"Error en EjecutarBusqueda: {ex.Message} - StackTrace: {ex.StackTrace}");
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.R))
            {
                switch (tclMainContainer.SelectedIndex)
                {
                    case 0: // Ventas
                        RestaurarVentas(); // Llamada a método asincrónico sin await
                        break;
                    case 1: // Compras
                        RestaurarCompras(); // Llamada a método asincrónico sin await
                        break;
                    default:
                        MessageBox.Show("No se puede restaurar la vista en esta pestaña.");
                        break;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async Task RestaurarVentas()
        {
            switch (tclSalesContainer.SelectedIndex)
            {
                case 0: // Clientes
                    await RestaurarVista(viewSettingsCustomers, lblCustomersFilterWarning, UpdateCustomersAsync);
                    break;
                case 1: // Presupuestos
                    await RestaurarVista(viewSettingsEstimates, lblEstimatesFilterWarning, UpdateEstimatesAsync);
                    break;
                case 2: // Ventas
                    await RestaurarVista(viewSettingsSales, lblSalesFilterWarning, UpdateSalesAsync);
                    break;
                case 3: // Facturas de venta
                    await RestaurarVista(viewSettingsSaleInvoices, lblSaleInvoicesFilterWarning, UpdateSaleInvoicesAsync);
                    break;
                case 4: // Notas emitidas
                    await RestaurarVista(viewSettingsIssuedNotes, lblIssuedNotesFilterWarning, UpdateIssuedNotesAsync);
                    break;
                case 5: // Pagos de clientes
                    await RestaurarVista(viewSettingsCustomerPayments, lblCustomerPaymentsFilterWarning, UpdateCustomerPaymentsAsync);
                    break;
                case 6: // Productos
                    await RestaurarVista(viewSettingsProducts, lblProductsFilterWarning, UpdateProductsAsync);
                    break;
                case 7: // Órdenes de reparación
                    await RestaurarVista(viewSettingsRepairOrders, lblRepairOrdersFilterWarning, UpdateRepairOrdersAsync);
                    break;
                case 8: // Informes técnicos
                    await RestaurarVista(viewSettingsTechReports, lblTechReportsFilterWarning, UpdateTechReportsAsync);
                    break;
                default:
                    MessageBox.Show("No se puede restaurar en esta pestaña.");
                    break;
            }
        }

        private async Task RestaurarCompras()
        {
            switch (tclPurchasesContainer.SelectedIndex)
            {
                case 0: // Proveedores
                    await RestaurarVista(viewSettingsProviders, lblProvidersFilterWarning, UpdateProvidersAsync);
                    break;
                case 1: // Órdenes de compra
                    await RestaurarVista(viewSettingsPurchaseOrders, lblPurchaseOrdersFilterWarning, UpdatePurchaseOrdersAsync);
                    break;
                case 2: // Facturas de compra
                    await RestaurarVista(viewSettingsPurchaseInvoices, lblPurchaseInvoicesFilterWarning, UpdatePurchaseInvoicesAsync);
                    break;
                case 3: // Órdenes de pago
                    await RestaurarVista(viewSettingsPayOrders, lblPayOrdersFilterWarning, UpdatePayOrdersAsync);
                    break;
                default:
                    MessageBox.Show("No se puede restaurar en esta pestaña.");
                    break;
            }
        }

        private async Task RestaurarVista(ViewSettings viewSettings, Label lblWarning, Func<Task> updateFunc)
        {
            viewSettings.Filters.Clear();
            lblWarning.Visible = false;
            await updateFunc();
        }




        private void tabSalesManagement_Enter(object sender, EventArgs e)
        {
            tclSalesContainer.SelectedTab.Select();
        }
        private void tabPurchasesManagement_Enter(object sender, EventArgs e)
        {
            tclPurchasesContainer.SelectedTab.Select();
        }
        private void tabAccountancy_Enter(object sender, EventArgs e)
        {
            tclAccountancyContainer.SelectedTab.Select();
        }
        private async void tmrAutoUpdate_Tick(object sender, EventArgs e)
        {
            switch (tclMainContainer.SelectedIndex)
            {
                case 0:
                    {
                        switch (tclSalesContainer.SelectedIndex)
                        {
                            case 0:
                                await UpdateCustomersAsync();
                                break;
                            case 1:
                                await UpdateEstimatesAsync();
                                break;
                            case 2:
                                await UpdateSalesAsync();
                                break;
                            case 3:
                                await UpdateSaleInvoicesAsync();
                                break;
                            case 4:
                                await UpdateIssuedNotesAsync();
                                break;
                            case 5:
                                await UpdateCustomerPaymentsAsync();
                                break;
                            case 6:
                                await UpdateProductsAsync();
                                break;
                            case 7:
                                await UpdateRepairOrdersAsync();
                                break;
                            case 8:
                                await UpdateTechReportsAsync();
                                break;
                        }
                        break;
                    }
                case 1:
                    {
                        switch (tclPurchasesContainer.SelectedIndex)
                        {
                            case 0:
                                await UpdateProvidersAsync();
                                break;
                            case 1:
                                await UpdatePurchaseOrdersAsync();
                                break;
                            case 2:
                                await UpdatePurchaseInvoicesAsync();
                                break;
                            case 3:
                                await UpdatePayOrdersAsync();
                                break;
                            case 4:
                                await UpdateInputsAsync();
                                break;
                        }
                        break;
                    }
                case 2:
                    {
                        switch (tclAccountancyContainer.SelectedIndex)
                        {
                            case 0:
                                await UpdateExpensesAsync();
                                break;
                        }
                        break;
                    }
                case 3:
                    {
                        await UpdateTasksAsync(false);
                        break;
                    }
            }
            await UpdateNotifications();
        }

        private async Task UpdateNotifications()
        {
            tmrAutoUpdate.Stop();
            // Obtengo todos los registros del día.
            List<Record> records;
            try
            {
                records = await Task.Run(() => Record.GetRecordsByDate(DateTime.Now.Date));
            }
            catch (Exception dbException)
            {
                // Waypoint MA107
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA107 (Flag: MySQL). Message: " + dbException.Message);
                tmrAutoUpdate.Start();
                return;
            }
            // Defino eventos que le interesan al usuario.
            var subscribedEvents = new List<int>();
            if (!string.IsNullOrWhiteSpace(AppEnvironment.CurrentUser.Notifications))
            {
                subscribedEvents = AppEnvironment.CurrentUser.Notifications.Split('-').Select(code => Convert.ToInt32(code)).ToList();
            }
            // Selecciono los que le importan al usuario.
            TodayRecords = records.Where(r => subscribedEvents.Contains(r.EventCode)).ToList();
            // Si el centro de notificaciones está abierto, actualizo lista.
            var form = (NotificationCenter)Application.OpenForms["NotificationCenter"];
            form?.UpdateGrid(TodayRecords);
            // Los eventos generados por el mismo usuario no hace falta notificarlos.
            var ownButUnknown = TodayRecords
                .Where(r => r.UserID == AppEnvironment.CurrentUser.UserID)
                .Select(r => r.RecordID)
                .Except(KnownRecords);
            KnownRecords.AddRange(ownButUnknown);
            // Verifico si hubo nuevos eventos.
            var unknown = TodayRecords
                .Select(r => r.RecordID)
                .Except(KnownRecords);
            // Veo si hay que notificar.
            if (unknown.Count() > 0)
            {
                if (form != null)
                {
                    form.WindowState = FormWindowState.Normal;
                    form.BringToFront();
                    form.AddHighlightedRecords(unknown);
                }
                else
                {
                    form = new NotificationCenter();
                    form.Show(this);
                    form.UpdateGrid(TodayRecords);
                    form.AddHighlightedRecords(unknown);
                }
                trayIcon.ShowBalloonTip(5000, "Clover Gestión", "Hay notificaciones nuevas.", ToolTipIcon.Info);
                KnownRecords.AddRange(unknown);
            }
            tmrAutoUpdate.Start();
        }
        public async void OpenRecordElement(Record record)
        {
            if (!record.RefID.HasValue)
            {
                MessageBox.Show("El registro no tiene un elemento asociado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (record.EventCode)
            {
                case 171: // Orden de reparación creada.
                case 172: // Orden de reparación modificada.
                case 174: // Orden de reparación aprobada.
                    {
                        // Desactiva actualización automática.
                        tmrAutoUpdate.Stop();
                        tabSalesManagement.Enter -= tabSalesManagement_Enter;
                        tabRepairOrders.Enter -= tabRepairOrders_Enter;
                        // Selecciona secciones correspondientes.
                        tclMainContainer.SelectTab("tabSalesManagement");
                        tclSalesContainer.SelectTab("tabRepairOrders");
                        // Borra filtros y actualiza lista.
                        viewSettingsRepairOrders.Filters.Clear();
                        lblRepairOrdersFilterWarning.Visible = false;
                        await UpdateRepairOrdersAsync();
                        // Selecciona elemento en lista si está presente.
                        var matches = dgvRepairOrders.Rows.Cast<DataGridViewRow>().Where(x => ((RepairOrder)x.DataBoundItem).RepairOrderID == record.RefID.Value);
                        if (matches.Count() != 0)
                        {
                            int index = matches.Single().Index;
                            dgvRepairOrders.CurrentCell = dgvRepairOrders.Rows[index].Cells[0];
                        }
                        // Reactiva actualización automática.
                        tmrAutoUpdate.Start();
                        tabSalesManagement.Enter += tabSalesManagement_Enter;
                        tabRepairOrders.Enter += tabRepairOrders_Enter;
                        // Abre el elemento.
                        using (var form = new RO_RepairOrder(record.RefID.Value))
                        {
                            form.ShowDialog();
                        }
                        await UpdateRepairOrdersAsync();
                        break;
                    }
            }
        }

        private void btnAboutMe_Click(object sender, EventArgs e)
        {
            try
            {
                using (AboutBox aboutBox = new AboutBox())
                {
                    aboutBox.ShowDialog();
                }
            }
            catch (Exception unknownException)
            {
                // Waypoint MA102
                MessageBox.Show("Error desconocido."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + unknownException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA102. Message: " + unknownException.Message);
            }
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                using (ST_Settings settingUI = new ST_Settings())
                {
                    settingUI.ShowDialog();
                }
            }
            catch (Exception unknownException)
            {
                // Waypoint MA103
                MessageBox.Show("Error desconocido."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + unknownException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA103. Message: " + unknownException.Message);
            }
        }
        private void btnAdminTools_Click(object sender, EventArgs e)
        {
            using (var form = new AT_Tools())
            {
                form.ShowDialog();
            }
        }
        private void btnNotificationCenter_Click(object sender, EventArgs e)
        {
            var form = (NotificationCenter)Application.OpenForms["NotificationCenter"];
            if (form != null)
            {
                form.WindowState = FormWindowState.Normal;
                form.BringToFront();
            }
            else
            {
                form = new NotificationCenter();
                form.Show(this);
                form.UpdateGrid(TodayRecords);
            }
        }

        #region Customers Tab
        private async void btnRefreshCustomers_Click(object sender, EventArgs e)
        {
            await UpdateCustomersAsync();
        }
        private async void btnCustomersViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsCustomers))
            {
                form.ShowDialog();
                lblCustomersFilterWarning.Visible = (viewSettingsCustomers.Filters.Count != 0);
            }
            await UpdateCustomersAsync();
        }
        private async void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (var form = new CU_Customer())
            {
                form.ShowDialog();
            }
            await UpdateCustomersAsync();
        }

        private async void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvCustomers, e.ColumnIndex, viewSettingsCustomers);
                await UpdateCustomersAsync();
            }
        }

        private async void cmsItemOpenCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
            using (var form = new CU_Customer(selectedCustomer.CustomerID))
            {
                form.ShowDialog();
            }
            await UpdateCustomersAsync();
        }
        private void cmsItemShowCustomerBalance_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
            using (var form = new CU_CustomerBalance(selectedCustomer.CustomerID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemShowCustomerHistory_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
            using (var form = new CU_CustomerHistory(selectedCustomer.CustomerID, selectedCustomer.CustomerName))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemMakeEstimateFromCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
            using (var form = new ES_Estimate(selectedCustomer.CustomerID, ESParameterType.PreselectedCustomerID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación del siguiente cliente:\n\n{selectedCustomer.CustomerID:D8} - {selectedCustomer.CustomerName}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => Customer.DeleteCustomerById(selectedCustomer.CustomerID));
                MessageBox.Show("Cliente eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint CU101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint CU101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateCustomersAsync();
        }

        private async void tabCustomers_Enter(object sender, EventArgs e)
        {
            await UpdateCustomersAsync();
        }

        private async Task UpdateCustomersAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshCustomers.Text = "Actualizando...";
            btnRefreshCustomers.Enabled = false;
            btnCustomersViewSettings.Enabled = false;
            try
            {
                var customers = await Task.Run(() => Customer.GetCustomers(viewSettingsCustomers.ToString()));
                SetGridDataSource(dgvCustomers, customers, viewSettingsCustomers);
            }
            catch (Exception dbException)
            {
                // Waypoint CU102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU102 (Flag: MySQL). Message: " + dbException.Message);
                dgvCustomers.DataSource = null;
            }
            finally
            {
                btnRefreshCustomers.Text = "Actualizar";
                btnRefreshCustomers.Enabled = true;
                btnCustomersViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region Estimates Tab
        private async void btnRefreshEstimates_Click(object sender, EventArgs e)
        {
            await UpdateEstimatesAsync();
        }
        private async void btnEstimatesViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsEstimates))
            {
                form.ShowDialog();
                lblEstimatesFilterWarning.Visible = (viewSettingsEstimates.Filters.Count != 0);
            }
            await UpdateEstimatesAsync();
        }
        private async void btnAddEstimate_Click(object sender, EventArgs e)
        {
            using (var form = new ES_Estimate())
            {
                form.ShowDialog();
            }
            await UpdateEstimatesAsync();
        }
        private async void btnExportEstimates_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<Estimate>>(() => Estimate.GetEstimates(viewSettingsEstimates.ToString(), 1000)), dgvEstimates);
        }

        private async void dgvEstimates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvEstimates, e.ColumnIndex, viewSettingsEstimates);
                await UpdateEstimatesAsync();
            }
        }

        private async void cmsItemOpenEstimates_Click(object sender, EventArgs e)
        {
            if (dgvEstimates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedEstimate = (Estimate)dgvEstimates.SelectedRows[0].DataBoundItem;
            using (var form = new ES_Estimate(selectedEstimate.EstimateID, ESParameterType.EstimateID))
            {
                form.ShowDialog();
            }
            await UpdateEstimatesAsync();
        }
        private async void cmsMakeSaleFromEstimate_Click(object sender, EventArgs e)
        {
            if (dgvEstimates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedEstimate = (Estimate)dgvEstimates.SelectedRows[0].DataBoundItem;
            using (var form = new SA_Sale(selectedEstimate.EstimateID, SAParameterType.PreselectedEstimateID))
            {
                form.ShowDialog();
            }
            await UpdateEstimatesAsync();
        }
        private async void cmsItemSetEstimateAsRejected_Click(object sender, EventArgs e)
        {
            if (dgvEstimates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedEstimate = (Estimate)dgvEstimates.SelectedRows[0].DataBoundItem;
            if (selectedEstimate.Status == "Rechazado")
            {
                MessageBox.Show("El estado actual del presupuesto es RECHAZADO.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (selectedEstimate.Status == "Vendido")
            {
                MessageBox.Show("No es posible realizar la operación: el presupuesto tiene una venta asociada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                await Task.Run(() => Estimate.UpdateStatus(selectedEstimate.EstimateID, "Rechazado"));
            }
            catch (Exception dbException)
            {
                // Waypoint ES101
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES101 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            await UpdateEstimatesAsync();
        }
        private async void cmsItemSetEstimateAsActive_Click(object sender, EventArgs e)
        {
            if (dgvEstimates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedEstimate = (Estimate)dgvEstimates.SelectedRows[0].DataBoundItem;
            if (selectedEstimate.Status == "Activo")
            {
                MessageBox.Show("El estado actual del presupuesto es ACTIVO.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (selectedEstimate.Status == "Vendido")
            {
                MessageBox.Show("No es posible realizar la operación: el presupuesto tiene una venta asociada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                await Task.Run(() => Estimate.UpdateStatus(selectedEstimate.EstimateID, "Activo"));
            }
            catch (Exception dbException)
            {
                // Waypoint ES102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES102 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            await UpdateEstimatesAsync();
        }
        private async void cmsItemGoToSaleFromEstimate_Click(object sender, EventArgs e)
        {
            if (dgvEstimates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedEstimate = (Estimate)dgvEstimates.SelectedRows[0].DataBoundItem;
            List<Sale> linkedSales;
            try
            {
                linkedSales = await Task.Run(() => Sale.GetSalesByEstimateId(selectedEstimate.EstimateID));
            }
            catch (Exception dbException)
            {
                // Waypoint ES106
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES106 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            if (linkedSales.Count == 0)
            {
                MessageBox.Show("El presupuesto seleccionado no tiene ventas asociadas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (linkedSales.Count == 1)
            {
                using (var form = new SA_Sale(linkedSales.Single().SaleID, SAParameterType.SaleID))
                {
                    form.ShowDialog();
                }
            }
            else
            {
                int? selectedSaleID = null;
                using (var form = new ES_LinkedSaleSelector(linkedSales))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        selectedSaleID = form.SelectedSaleID;
                    }
                    else
                    {
                        return;
                    }
                }
                using (var form = new SA_Sale(selectedSaleID.Value, SAParameterType.SaleID))
                {
                    form.ShowDialog();
                }
            }
        }
        private void cmsItemGoToCustomerFromEstimate_Click(object sender, EventArgs e)
        {
            if (dgvEstimates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedEstimate = (Estimate)dgvEstimates.SelectedRows[0].DataBoundItem;
            using (var form = new CU_Customer(selectedEstimate.CustomerID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemDeleteEstimate_Click(object sender, EventArgs e)
        {
            if (dgvEstimates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedEstimate = (Estimate)dgvEstimates.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación del siguiente presupuesto:\n\nID Presupuesto: {selectedEstimate.EstimateID:D8}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => Estimate.DeleteEstimateById(selectedEstimate.EstimateID));
                MessageBox.Show("Presupuesto eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint ES104
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint ES104 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateEstimatesAsync();
        }

        private async void tabEstimates_Enter(object sender, EventArgs e)
        {
            await UpdateEstimatesAsync();
        }

        private async Task UpdateEstimatesAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshEstimates.Text = "Actualizando...";
            btnRefreshEstimates.Enabled = false;
            btnEstimatesViewSettings.Enabled = false;
            try
            {
                var estimates = await Task.Run(() => Estimate.GetEstimates(viewSettingsEstimates.ToString()));
                SetGridDataSource(dgvEstimates, estimates, viewSettingsEstimates);
                PaintEstimates();
            }
            catch (Exception dbException)
            {
                // Waypoint ES105
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES105 (Flag: MySQL). Message: " + dbException.Message);
                dgvEstimates.DataSource = null;
            }
            finally
            {
                btnRefreshEstimates.Text = "Actualizar";
                btnRefreshEstimates.Enabled = true;
                btnEstimatesViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        private void PaintEstimates()
        {
            foreach (DataGridViewRow row in dgvEstimates.Rows)
            {
                var estimate = (Estimate)row.DataBoundItem;
                switch (estimate.Status)
                {
                    case "Rechazado":
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(230, 184, 183);
                            break;
                        }
                    case "Vendido":
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(184, 204, 228);
                            break;
                        }
                    case "Activo":
                        {
                            if (estimate.IsUnmarked)
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 237, 153);
                            }
                            else if (estimate.TotalBeforeTax == 0)
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(248, 203, 173);
                            }
                            break;
                        }
                }
            }
        }
        #endregion
        #region Sales Tab
        private async void btnRefreshSales_Click(object sender, EventArgs e)
        {
            await UpdateSalesAsync();
        }
        private async void btnSalesViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsSales))
            {
                form.ShowDialog();
                lblSalesFilterWarning.Visible = (viewSettingsSales.Filters.Count != 0);
            }
            await UpdateSalesAsync();
        }
        private async void btnExportSales_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<Sale>>(() => Sale.GetSales(viewSettingsSales.ToString(), 1000)), dgvSales);
        }
        private async void btnAddSale_Click(object sender, EventArgs e)
        {
            using (var form = new SA_Sale())
            {
                form.ShowDialog();
            }
            await UpdateSalesAsync();
        }

        private async void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvSales, e.ColumnIndex, viewSettingsSales);
                await UpdateSalesAsync();
            }
        }

        private async void cmsItemOpenSale_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            using (var form = new SA_Sale(selectedSale.SaleID, SAParameterType.SaleID))
            {
                form.ShowDialog();
            }
            await UpdateSalesAsync();
        }
        private async void cmsItemMakeInvoice_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            if (selectedSale.IsUnmarked)
            {
                return;
            }
            using (var form = new SI_SaleInvoice(selectedSale.SaleID, SIParameterType.PreselectedSaleID))
            {
                form.ShowDialog();
            }
            await UpdateSalesAsync();
        }
        private void cmsItemMakeDeliveryNote_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            if (selectedSale.IsUnmarked)
            {
                return;
            }
            using (var form = new SA_DeliveryNote(selectedSale.SaleID, DNParameterType.SaleID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemMakeXNote_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            if (!selectedSale.IsUnmarked)
            {
                return;
            }
            using (var form = new SA_ControlTicket(selectedSale.SaleID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemMakeUnmarkedPayment_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            if (!selectedSale.IsUnmarked)
            {
                return;
            }
            using (var form = new CP_CustomerPayment(selectedSale.SaleID, CPParameterType.PreselectedSaleID))
            {
                form.ShowDialog();
            }
            await UpdateSalesAsync();
        }
        private void cmsItemGoToEstimateFromSale_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            if (!selectedSale.EstimateID.HasValue)
            {
                MessageBox.Show("La venta seleccionada no tiene un presupuesto asociado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var form = new ES_Estimate(selectedSale.EstimateID.Value, ESParameterType.EstimateID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemGoToCustomerFromSale_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            using (var form = new CU_Customer(selectedSale.CustomerID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemDeleteSale_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            string textMessage = $"Por favor, confirme la eliminación de la siguiente venta:\n\nID Venta: {selectedSale.SaleID:D8}\n\nSe eliminarán también todos los remitos asociados.";
            var dialog = MessageBox.Show(textMessage, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        // ATENCIÓN: No se debe alterar el orden de estas operaciones.
                        Estimate.UpdateStatusSingleLinkBySaleId(selectedSale.SaleID, "Activo", handler);
                        Sale.DeleteSaleById(selectedSale.SaleID, handler);
                        handler.CommitTransaction();
                    }
                });
                MessageBox.Show("Venta eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint SA101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint SA101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateSalesAsync();
        }
        private void cmsSaleOptions_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            cmsItemMakeInvoice.Visible = !selectedSale.IsUnmarked;
            cmsItemMakeDeliveryNote.Visible = !selectedSale.IsUnmarked;
            cmsItemMakeXNote.Visible = selectedSale.IsUnmarked;
            cmsItemMakeUnmarkedPayment.Visible = selectedSale.IsUnmarked;
        }

        private async void tabSales_Enter(object sender, EventArgs e)
        {
            await UpdateSalesAsync();
        }

        private async Task UpdateSalesAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshSales.Text = "Actualizando...";
            btnRefreshSales.Enabled = false;
            btnSalesViewSettings.Enabled = false;
            try
            {
                var sales = await Task.Run(() => Sale.GetSales(viewSettingsSales.ToString()));
                SetGridDataSource(dgvSales, sales, viewSettingsSales);
                PaintSales();
            }
            catch (Exception dbException)
            {
                // Waypoint SA102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA102 (Flag: MySQL). Message: " + dbException.Message);
                dgvSales.DataSource = null;
            }
            finally
            {
                btnRefreshSales.Text = "Actualizar";
                btnRefreshSales.Enabled = true;
                btnSalesViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        private void PaintSales()
        {
            foreach (DataGridViewRow row in dgvSales.Rows)
            {
                var sale = (Sale)row.DataBoundItem;
                if (sale.HasPayments)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(226, 239, 218); // Verde.
                }
                else if (sale.HasInvoices)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(184, 204, 228); // Azul.
                }
                else if (sale.IsUnmarked)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 237, 153); // Amarillo.
                }
            }
        }
        #endregion
        #region SaleInvoices Tab
        private async void btnRefreshSaleInvoices_Click(object sender, EventArgs e)
        {
            await UpdateSaleInvoicesAsync();
        }
        private async void btnSaleInvoicesViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsSaleInvoices))
            {
                form.ShowDialog();
                lblSaleInvoicesFilterWarning.Visible = (viewSettingsSaleInvoices.Filters.Count != 0);
            }
            await UpdateSaleInvoicesAsync();
        }
        private async void btnExportSaleInvoices_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<SaleInvoice>>(() => SaleInvoice.GetInvoices(viewSettingsSaleInvoices.ToString(), 1000)), dgvSaleInvoices);
        }
        private async void btnAddSaleInvoice_Click(object sender, EventArgs e)
        {
            using (var form = new SI_SaleInvoice())
            {
                form.ShowDialog();
            }
            await UpdateSaleInvoicesAsync();
        }

        private async void dgvSaleInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvSaleInvoices, e.ColumnIndex, viewSettingsSaleInvoices);
                await UpdateSaleInvoicesAsync();
            }
        }

        private async void cmsItemOpenSaleInvoice_Click(object sender, EventArgs e)
        {
            if (dgvSaleInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSaleInvoice = (SaleInvoice)dgvSaleInvoices.SelectedRows[0].DataBoundItem;
            using (var form = new SI_SaleInvoice(selectedSaleInvoice.SaleInvoiceID, SIParameterType.SaleInvoiceID))
            {
                form.ShowDialog();
            }
            await UpdateSaleInvoicesAsync();
        }
        private async void cmsItemDeleteSaleInvoice_Click(object sender, EventArgs e)
        {
            if (dgvSaleInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSaleInvoice = (SaleInvoice)dgvSaleInvoices.SelectedRows[0].DataBoundItem;
            string textMessage = $"Por favor, confirme la eliminación de la siguiente factura:\n\nID Factura: {selectedSaleInvoice.SaleInvoiceID:D8}";
            var dialog = MessageBox.Show(textMessage, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => SaleInvoice.DeleteInvoiceById(selectedSaleInvoice.SaleInvoiceID));
                MessageBox.Show("Factura eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint SI101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint SI101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateSaleInvoicesAsync();
        }

        private async void tabSaleInvoices_Enter(object sender, EventArgs e)
        {
            await UpdateSaleInvoicesAsync();
        }

        private async Task UpdateSaleInvoicesAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshSaleInvoices.Text = "Actualizando...";
            btnRefreshSaleInvoices.Enabled = false;
            btnSaleInvoicesViewSettings.Enabled = false;
            try
            {
                var saleInvoices = await Task.Run(() => SaleInvoice.GetInvoices(viewSettingsSaleInvoices.ToString()));
                SetGridDataSource(dgvSaleInvoices, saleInvoices, viewSettingsSaleInvoices);
            }
            catch (Exception dbException)
            {
                // Waypoint SI102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SI102 (Flag: MySQL). Message: " + dbException.Message);
                dgvSaleInvoices.DataSource = null;
            }
            finally
            {
                btnRefreshSaleInvoices.Text = "Actualizar";
                btnRefreshSaleInvoices.Enabled = true;
                btnSaleInvoicesViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region IssuedNotes Tab
        private async void btnRefreshIssuedNotes_Click(object sender, EventArgs e)
        {
            await UpdateIssuedNotesAsync();
        }
        private async void btnIssuedNotesViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsIssuedNotes))
            {
                form.ShowDialog();
                lblIssuedNotesFilterWarning.Visible = (viewSettingsIssuedNotes.Filters.Count != 0);
            }
            await UpdateIssuedNotesAsync();
        }
        private async void btnAddIssuedNote_Click(object sender, EventArgs e)
        {
            using (var form = new ISN_IssuedNote())
            {
                form.ShowDialog();
            }
            await UpdateIssuedNotesAsync();
        }

        private async void dgvIssuedNotes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvIssuedNotes, e.ColumnIndex, viewSettingsIssuedNotes);
                await UpdateIssuedNotesAsync();
            }
        }

        private async void cmsItemOpenIssuedNote_Click(object sender, EventArgs e)
        {
            if (dgvIssuedNotes.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedNote = (IssuedNote)dgvIssuedNotes.SelectedRows[0].DataBoundItem;
            using (var form = new ISN_IssuedNote(selectedNote.NoteID))
            {
                form.ShowDialog();
            }
            await UpdateIssuedNotesAsync();
        }
        private async void cmsItemDeleteIssuedNote_Click(object sender, EventArgs e)
        {
            if (dgvIssuedNotes.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedNote = (IssuedNote)dgvIssuedNotes.SelectedRows[0].DataBoundItem;
            string textMessage = $"Por favor, confirme la eliminación de la siguiente nota:\n\nID Nota: {selectedNote.NoteID:D8}";
            var dialog = MessageBox.Show(textMessage, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => IssuedNote.DeleteNoteById(selectedNote.NoteID));
                MessageBox.Show("Nota eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint ISN101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint ISN101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateIssuedNotesAsync();
        }

        private async void tabIssuedNotes_Enter(object sender, EventArgs e)
        {
            await UpdateIssuedNotesAsync();
        }

        private async Task UpdateIssuedNotesAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshIssuedNotes.Text = "Actualizando...";
            btnRefreshIssuedNotes.Enabled = false;
            btnIssuedNotesViewSettings.Enabled = false;
            try
            {
                var issuedNotes = await Task.Run(() => IssuedNote.GetIssuedNotes(viewSettingsIssuedNotes.ToString()));
                SetGridDataSource(dgvIssuedNotes, issuedNotes, viewSettingsIssuedNotes);
            }
            catch (Exception dbException)
            {
                // Waypoint ISN102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ISN102 (Flag: MySQL). Message: " + dbException.Message);
                dgvIssuedNotes.DataSource = null;
            }
            finally
            {
                btnRefreshIssuedNotes.Text = "Actualizar";
                btnRefreshIssuedNotes.Enabled = true;
                btnIssuedNotesViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region CustomerPayments Tab
        private async void btnRefreshCustomerPayments_Click(object sender, EventArgs e)
        {
            await UpdateCustomerPaymentsAsync();
        }
        private async void btnCustomerPaymentsViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsCustomerPayments))
            {
                form.ShowDialog();
                lblCustomerPaymentsFilterWarning.Visible = (viewSettingsCustomerPayments.Filters.Count != 0);
            }
            await UpdateCustomerPaymentsAsync();
        }
        private async void btnExportCustomerPayments_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<CustomerPayment>>(() => CustomerPayment.GetPayments(viewSettingsCustomerPayments.ToString(), 1000)), dgvCustomerPayments);
        }
        private async void btnAddCustomerPayment_Click(object sender, EventArgs e)
        {
            using (var form = new CP_CustomerPayment())
            {
                form.ShowDialog();
            }
            await UpdateCustomerPaymentsAsync();
        }

        private async void dgvCustomerPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvCustomerPayments, e.ColumnIndex, viewSettingsCustomerPayments);
                await UpdateCustomerPaymentsAsync();
            }
        }

        private async void cmsItemOpenCustomerPayment_Click(object sender, EventArgs e)
        {
            if (dgvCustomerPayments.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedCustomerPayment = (CustomerPayment)dgvCustomerPayments.SelectedRows[0].DataBoundItem;
            using (var form = new CP_CustomerPayment(selectedCustomerPayment.CustomerPaymentID, CPParameterType.CustomerPaymentID))
            {
                form.ShowDialog();
            }
            await UpdateCustomerPaymentsAsync();
        }
        private async void cmsItemDeleteCustomerPayment_Click(object sender, EventArgs e)
        {
            if (dgvCustomerPayments.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedCustomerPayment = (CustomerPayment)dgvCustomerPayments.SelectedRows[0].DataBoundItem;
            string textMessage = $"Por favor, confirme la eliminación del siguiente pago:\n\nID Pago: {selectedCustomerPayment.CustomerPaymentID:D8}";
            var dialog = MessageBox.Show(textMessage, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => CustomerPayment.DeletePaymentById(selectedCustomerPayment.CustomerPaymentID));
                MessageBox.Show("Cobro eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint CP101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint CP101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateCustomerPaymentsAsync();
        }

        private async void tabCustomerPayments_Enter(object sender, EventArgs e)
        {
            await UpdateCustomerPaymentsAsync();
        }

        private async Task UpdateCustomerPaymentsAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshCustomerPayments.Text = "Actualizando...";
            btnRefreshCustomerPayments.Enabled = false;
            btnCustomerPaymentsViewSettings.Enabled = false;
            try
            {
                var customerPayments = await Task.Run(() => CustomerPayment.GetPayments(viewSettingsCustomerPayments.ToString()));
                SetGridDataSource(dgvCustomerPayments, customerPayments, viewSettingsCustomerPayments);
            }
            catch (Exception dbException)
            {
                // Waypoint CP102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CP102 (Flag: MySQL). Message: " + dbException.Message);
                dgvCustomerPayments.DataSource = null;
            }
            finally
            {
                btnRefreshCustomerPayments.Text = "Actualizar";
                btnRefreshCustomerPayments.Enabled = true;
                btnCustomerPaymentsViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region Products Tab
        private async void btnRefreshProducts_Click(object sender, EventArgs e)
        {
            await UpdateProductsAsync();
        }
        private async void btnProductViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsProducts))
            {
                form.ShowDialog();
                lblProductsFilterWarning.Visible = (viewSettingsProducts.Filters.Count != 0);
            }
            await UpdateProductsAsync();
        }
        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var form = new PR_Product())
            {
                form.ShowDialog();
            }
            await UpdateProductsAsync();
        }

        private async void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvProducts, e.ColumnIndex, viewSettingsProducts);
                await UpdateProductsAsync();
            }
        }

        private async void cmsItemOpenProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;
            using (var form = new PR_Product(selectedProduct.ProductID))
            {
                form.ShowDialog();
            }
            await UpdateProductsAsync();
        }
        private void cmsItemShowProductHistory_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;
            using (var form = new PR_ProductHistory(selectedProduct.ProductID, selectedProduct.PartCode))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación del siguiente producto:\n\n{selectedProduct.ProductID:D8} - {selectedProduct.PartCode}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => Product.DeleteProductById(selectedProduct.ProductID));
                MessageBox.Show("Producto eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint PR101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint PR101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateProductsAsync();
        }

        private async void tabProducts_Enter(object sender, EventArgs e)
        {
            await UpdateProductsAsync();
        }

        private async Task UpdateProductsAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshProducts.Text = "Actualizando...";
            btnRefreshProducts.Enabled = false;
            btnProductViewSettings.Enabled = false;
            try
            {
                var products = await Task.Run(() => Product.GetProducts(viewSettingsProducts.ToString()));
                SetGridDataSource(dgvProducts, products, viewSettingsProducts);
            }
            catch (Exception dbException)
            {
                // Waypoint PR102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PR102 (Flag: MySQL). Message: " + dbException.Message);
                dgvProducts.DataSource = null;
            }
            finally
            {
                btnRefreshProducts.Text = "Actualizar";
                btnRefreshProducts.Enabled = true;
                btnProductViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region RepairOrders Tab
        private async void btnRefreshRepairOrders_Click(object sender, EventArgs e)
        {
            await UpdateRepairOrdersAsync();
        }
        private async void btnRepairOrdersViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsRepairOrders))
            {
                form.ShowDialog();
                lblRepairOrdersFilterWarning.Visible = (viewSettingsRepairOrders.Filters.Count != 0);
            }
            await UpdateRepairOrdersAsync();
        }
        private async void btnAddRepairOrder_Click(object sender, EventArgs e)
        {
            using (var form = new RO_RepairOrder())
            {
                form.ShowDialog();
            }
            await UpdateRepairOrdersAsync();
        }
        private async void btnExportRepairOrders_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<RepairOrder>>(() => RepairOrder.GetRepairOrders(viewSettingsRepairOrders.ToString(), 1000)), dgvRepairOrders);
        }

        private async void dgvRepairOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvRepairOrders, e.ColumnIndex, viewSettingsRepairOrders);
                await UpdateRepairOrdersAsync();
            }
        }

        private async void cmsItemOpenRepairOrder_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            using (var form = new RO_RepairOrder(selectedRepairOrder.RepairOrderID))
            {
                form.ShowDialog();
            }
            await UpdateRepairOrdersAsync();
        }
        private async void cmsItemMakeEstimateFromRepairOrder_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            if (!selectedRepairOrder.CustomerID.HasValue)
            {
                MessageBox.Show("No se puede realizar la operación: el cliente no está registrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedRepairOrder.EstimateID.HasValue)
            {
                string messageText = "La orden de reparación ya tiene un presupuesto asociado.\n\nPor favor, confirme la operación.";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialog != DialogResult.OK)
                {
                    return;
                }
            }
            using (var form = new ES_Estimate(selectedRepairOrder.RepairOrderID, ESParameterType.RepairOrderID))
            {
                form.ShowDialog();
            }
            await UpdateRepairOrdersAsync();
        }
        private async void cmsItemLinkEstimateToRepairOrder_Click(object sender, EventArgs e)
        {
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            if (!selectedRepairOrder.CustomerID.HasValue)
            {
                MessageBox.Show("No se puede realizar la operación: el cliente no está registrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedRepairOrder.EstimateID.HasValue)
            {
                string messageText = "La orden de reparación ya tiene un presupuesto asociado.\n\nPor favor, confirme la operación.";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialog != DialogResult.OK)
                {
                    return;
                }
            }
            using (var form = new RO_LinkEstimate(selectedRepairOrder.RepairOrderID, selectedRepairOrder.CustomerID.Value))
            {
                form.ShowDialog();
            }
            await UpdateRepairOrdersAsync();
        }
        private void cmsItemGoToCustomerFromRepairOrder_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            if (!selectedRepairOrder.CustomerID.HasValue)
            {
                MessageBox.Show("La orden de reparación no tiene cliente asociado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var form = new CU_Customer(selectedRepairOrder.CustomerID.Value))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemGoToEstimateFromRepairOrder_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            if (!selectedRepairOrder.EstimateID.HasValue)
            {
                MessageBox.Show("La orden de reparación no tiene presupuesto asociado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var form = new ES_Estimate(selectedRepairOrder.EstimateID.Value, ESParameterType.EstimateID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemUpdateRepairOrder_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            using (var form = new RO_OrderUpdate(selectedRepairOrder.RepairOrderID))
            {
                form.ShowDialog();
            }
            await UpdateRepairOrdersAsync();
        }
        private async void cmsItemSetRepairOrderPriorityAsNormal_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            try
            {
                await Task.Run(() => RepairOrder.UpdatePriority(selectedRepairOrder.RepairOrderID, 3));
                MessageBox.Show("Prioridad definida.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint RO101
                MessageBox.Show("(RO101) Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateRepairOrdersAsync();
        }
        private async void cmsItemSetRepairOrderPriorityAsHigh_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            try
            {
                await Task.Run(() => RepairOrder.UpdatePriority(selectedRepairOrder.RepairOrderID, 2));
                MessageBox.Show("Prioridad definida.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint RO102
                MessageBox.Show("(RO102) Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO102 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateRepairOrdersAsync();
        }
        private async void cmsItemSetRepairOrderPriorityAsVeryHigh_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            try
            {
                await Task.Run(() => RepairOrder.UpdatePriority(selectedRepairOrder.RepairOrderID, 1));
                MessageBox.Show("Prioridad definida.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint RO103
                MessageBox.Show("(RO103) Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO103 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateRepairOrdersAsync();
        }
        private async void cmsItemDeleteRepairOrder_Click(object sender, EventArgs e)
        {
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación de la siguiente orden de reparación:\n\nID Orden: {selectedRepairOrder.RepairOrderID:D8}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => RepairOrder.DeleteRepairOrderById(selectedRepairOrder.RepairOrderID));
                MessageBox.Show("Orden de reparación eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint RO104
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint RO104 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateRepairOrdersAsync();
        }

        private async void tabRepairOrders_Enter(object sender, EventArgs e)
        {
            await UpdateRepairOrdersAsync();
        }

        private async Task UpdateRepairOrdersAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshRepairOrders.Text = "Actualizando...";
            btnRefreshRepairOrders.Enabled = false;
            btnRepairOrdersViewSettings.Enabled = false;
            try
            {
                var orders = await Task.Run(() => RepairOrder.GetRepairOrders(viewSettingsRepairOrders.ToString()));
                SetGridDataSource(dgvRepairOrders, orders, viewSettingsRepairOrders);

                // Restaurar colores basados en clientes no registrados
                PaintRepairOrders();
            }
            catch (Exception dbException)
            {
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at UpdateRepairOrdersAsync. Message: " + dbException.Message);
                dgvRepairOrders.DataSource = null;
            }
            finally
            {
                btnRefreshRepairOrders.Text = "Actualizar";
                btnRefreshRepairOrders.Enabled = true;
                btnRepairOrdersViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }

        private void PaintRepairOrders()
        {
            foreach (DataGridViewRow row in dgvRepairOrders.Rows)
            {
                if (!((RepairOrder)row.DataBoundItem).CustomerID.HasValue)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(248, 203, 173); // Rojo suave
                }
            }
        }


        private void ConfigurarViewSettingsRepairOrders()
        {
            viewSettingsRepairOrders = new ViewSettings(5, 3, new DataField[]
            {
        new DataField("repair_order.RepairOrderID", "ID Orden", DataFieldTypes.Integer),
        new DataField("repair_order.CustomerName", "Cliente", DataFieldTypes.String),
        new DataField("repair_order.PumpBrand", "Marca de Bomba", DataFieldTypes.String),
        new DataField("repair_order.PumpModel", "Modelo de Bomba", DataFieldTypes.String),
        new DataField("repair_order.Status", "Estado", DataFieldTypes.String),
        new DataField("repair_order.PriorityID", "Prioridad", DataFieldTypes.Integer),
                // Agregar otros campos según necesidad
            });
            viewSettingsRepairOrders.AddSortLevel("repair_order.Status", SortDirection.ASC);
            viewSettingsRepairOrders.AddSortLevel("repair_order.CustomerName", SortDirection.ASC);
        }
        #endregion
        #region TechReports Tab
        private async void btnRefreshTechReports_Click(object sender, EventArgs e)
        {
            await UpdateTechReportsAsync();
        }
        private async void btnTechReportsViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsTechReports))
            {
                form.ShowDialog();
                lblTechReportsFilterWarning.Visible = (viewSettingsTechReports.Filters.Count != 0);
            }
            await UpdateTechReportsAsync();
        }
        private async void btnAddTechReport_Click(object sender, EventArgs e)
        {
            using (var form = new TR_TechReport())
            {
                form.ShowDialog();
            }
            await UpdateTechReportsAsync();
        }

        private async void dgvTechReports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvTechReports, e.ColumnIndex, viewSettingsTechReports);
                await UpdateTechReportsAsync();
            }
        }

        private async void cmsItemOpenTechReport_Click(object sender, EventArgs e)
        {
            if (dgvTechReports.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedReport = (TechReport)dgvTechReports.SelectedRows[0].DataBoundItem;
            using (var form = new TR_TechReport(selectedReport.TechReportID))
            {
                form.ShowDialog();
            }
            await UpdateTechReportsAsync();
        }
        private void cmsItemGoToCustomerFromTechReport_Click(object sender, EventArgs e)
        {
            if (dgvTechReports.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedReport = (TechReport)dgvTechReports.SelectedRows[0].DataBoundItem;
            using (var form = new CU_Customer(selectedReport.CustomerID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemDeleteTechReport_Click(object sender, EventArgs e)
        {
            if (dgvTechReports.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedReport = (TechReport)dgvTechReports.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación del siguiente informe:\n\n{selectedReport.TechReportID:D8} - {selectedReport.CustomerName}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => TechReport.DeleteReportById(selectedReport.TechReportID));
                MessageBox.Show("Informe eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint TR101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint TR101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateTechReportsAsync();
        }

        private async void tabTechReports_Enter(object sender, EventArgs e)
        {
            await UpdateTechReportsAsync();
        }

        private async Task UpdateTechReportsAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshTechReports.Text = "Actualizando...";
            btnRefreshTechReports.Enabled = false;
            btnTechReportsViewSettings.Enabled = false;
            try
            {
                var reports = await Task.Run(() => TechReport.GetReports(viewSettingsTechReports.ToString()));
                SetGridDataSource(dgvTechReports, reports, viewSettingsTechReports);
            }
            catch (Exception dbException)
            {
                // Waypoint TR102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TR102 (Flag: MySQL). Message: " + dbException.Message);
                dgvTechReports.DataSource = null;
            }
            finally
            {
                btnRefreshTechReports.Text = "Actualizar";
                btnRefreshTechReports.Enabled = true;
                btnTechReportsViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion

        #region Providers Tab
        private async void btnRefreshProviders_Click(object sender, EventArgs e)
        {
            await UpdateProvidersAsync();
        }
        private async void btnProvidersViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsProviders))
            {
                form.ShowDialog();
                lblProvidersFilterWarning.Visible = (viewSettingsProviders.Filters.Count != 0);
            }
            await UpdateProvidersAsync();
        }
        private async void btnAddProvider_Click(object sender, EventArgs e)
        {
            using (var form = new PV_Provider())
            {
                form.ShowDialog();
            }
            await UpdateProvidersAsync();
        }
        private async void btnExportProviders_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<Provider>>(() => Provider.GetProviders(viewSettingsProviders.ToString(), 1000)), dgvProviders);
        }

        private async void dgvProviders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvProviders, e.ColumnIndex, viewSettingsProviders);
                await UpdateProvidersAsync();
            }
        }

        private async void cmsItemOpenProvider_Click(object sender, EventArgs e)
        {
            if (dgvProviders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProvider = (Provider)dgvProviders.SelectedRows[0].DataBoundItem;
            using (var form = new PV_Provider(selectedProvider.ProviderID))
            {
                form.ShowDialog();
            }
            await UpdateProvidersAsync();
        }
        private void cmsItemOpenProviderCurrentAccount_Click(object sender, EventArgs e)
        {
            if (dgvProviders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProvider = (Provider)dgvProviders.SelectedRows[0].DataBoundItem;
            using (var form = new PV_CurrentAccount(selectedProvider.ProviderID, selectedProvider.ProviderName))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemMakePurchaseOrderFromProvider_Click(object sender, EventArgs e)
        {
            if (dgvProviders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProvider = (Provider)dgvProviders.SelectedRows[0].DataBoundItem;
            using (var form = new PO_PurchaseOrder(selectedProvider.ProviderID, POParameterType.PreselectedProviderID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemDeleteProvider_Click(object sender, EventArgs e)
        {
            if (dgvProviders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProvider = (Provider)dgvProviders.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación del siguiente proveedor:\n\n{selectedProvider.ProviderID:D8} - {selectedProvider.ProviderName}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => Provider.DeleteProviderById(selectedProvider.ProviderID));
                MessageBox.Show("Proveedor eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint PV101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint PV101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateProvidersAsync();
        }

        private async void tabProviders_Enter(object sender, EventArgs e)
        {
            await UpdateProvidersAsync();
        }

        private async Task UpdateProvidersAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshProviders.Text = "Actualizando...";
            btnRefreshProviders.Enabled = false;
            btnProvidersViewSettings.Enabled = false;
            try
            {
                var providers = await Task.Run(() => Provider.GetProviders(viewSettingsProviders.ToString()));
                SetGridDataSource(dgvProviders, providers, viewSettingsProviders);
            }
            catch (Exception dbException)
            {
                // Waypoint PV102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PV102 (Flag: MySQL). Message: " + dbException.Message);
                dgvProviders.DataSource = null;
            }
            finally
            {
                btnRefreshProviders.Text = "Actualizar";
                btnRefreshProviders.Enabled = true;
                btnProvidersViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region PurchaseOrders Tab
        private async void btnRefreshPurchaseOrders_Click(object sender, EventArgs e)
        {
            await UpdatePurchaseOrdersAsync();
        }
        private async void btnPurchaseOrdersViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsPurchaseOrders))
            {
                form.ShowDialog();
                lblPurchaseOrdersFilterWarning.Visible = (viewSettingsPurchaseOrders.Filters.Count != 0);
            }
            await UpdatePurchaseOrdersAsync();
        }
        private async void btnAddPurchaseOrder_Click(object sender, EventArgs e)
        {
            using (var form = new PO_PurchaseOrder())
            {
                form.ShowDialog();
            }
            await UpdatePurchaseOrdersAsync();
        }
        private async void btnExportPurchaseOrders_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<PurchaseOrder>>(() => PurchaseOrder.GetPurchaseOrders(viewSettingsPurchaseOrders.ToString(), 1000)), dgvPurchaseOrders);
        }

        private async void dgvPurchaseOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvPurchaseOrders, e.ColumnIndex, viewSettingsPurchaseOrders);
                await UpdatePurchaseOrdersAsync();
            }
        }

        private async void cmsItemOpenPurchaseOrder_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPurchaseOrder = (PurchaseOrder)dgvPurchaseOrders.SelectedRows[0].DataBoundItem;
            using (var form = new PO_PurchaseOrder(selectedPurchaseOrder.PurchaseOrderID, POParameterType.PurchaseOrderID))
            {
                form.ShowDialog();
            }
            await UpdatePurchaseOrdersAsync();
        }
        private async void cmsItemDeletePurchaseOrder_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPurchaseOrder = (PurchaseOrder)dgvPurchaseOrders.SelectedRows[0].DataBoundItem;
            if (selectedPurchaseOrder.PurchaseInvoiceID.HasValue)
            {
                MessageBox.Show("No es posible realizar la operación: la orden de compra tiene una factura asociada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string messageText = $"Por favor, confirme la eliminación de la siguiente orden de compra:\n\nID Orden de compra: {selectedPurchaseOrder.PurchaseOrderID:D8}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => PurchaseOrder.DeletePurchaseOrderById(selectedPurchaseOrder.PurchaseOrderID));
                MessageBox.Show("Orden de compra eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint PO101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint PO101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdatePurchaseOrdersAsync();
        }

        private async void tabPurchaseOrders_Enter(object sender, EventArgs e)
        {
            await UpdatePurchaseOrdersAsync();
        }

        private async Task UpdatePurchaseOrdersAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshPurchaseOrders.Text = "Actualizando...";
            btnRefreshPurchaseOrders.Enabled = false;
            btnPurchaseOrdersViewSettings.Enabled = false;
            try
            {
                var orders = await Task.Run(() => PurchaseOrder.GetPurchaseOrders(viewSettingsPurchaseOrders.ToString()));
                SetGridDataSource(dgvPurchaseOrders, orders, viewSettingsPurchaseOrders);
            }
            catch (Exception dbException)
            {
                // Waypoint PO102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO102 (Flag: MySQL). Message: " + dbException.Message);
                dgvPurchaseOrders.DataSource = null;
            }
            finally
            {
                btnRefreshPurchaseOrders.Text = "Actualizar";
                btnRefreshPurchaseOrders.Enabled = true;
                btnPurchaseOrdersViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region PurchaseInvoices Tab
        private async void btnRefreshPurchaseInvoices_Click(object sender, EventArgs e)
        {
            await UpdatePurchaseInvoicesAsync();
        }
        private async void btnPurchaseInvoicesViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsPurchaseInvoices))
            {
                form.ShowDialog();
                lblPurchaseInvoicesFilterWarning.Visible = (viewSettingsPurchaseInvoices.Filters.Count != 0);
            }
            await UpdatePurchaseInvoicesAsync();
        }
        private async void btnAddPurchaseInvoice_Click(object sender, EventArgs e)
        {
            using (var form = new PI_PurchaseInvoice())
            {
                form.ShowDialog();
            }
            await UpdatePurchaseInvoicesAsync();
        }
        private async void btnExportPurchaseInvoices_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<PurchaseInvoice>>(() => PurchaseInvoice.GetInvoices(viewSettingsPurchaseInvoices.ToString(), 1000)), dgvPurchaseInvoices);
        }

        private async void dgvPurchaseInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvPurchaseInvoices, e.ColumnIndex, viewSettingsPurchaseInvoices);
                await UpdatePurchaseInvoicesAsync();
            }
        }

        private async void cmsItemOpenPurchaseInvoice_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPurchaseInvoice = (PurchaseInvoice)dgvPurchaseInvoices.SelectedRows[0].DataBoundItem;
            using (var form = new PI_PurchaseInvoice(selectedPurchaseInvoice.PurchaseInvoiceID))
            {
                form.ShowDialog();
            }
            await UpdatePurchaseInvoicesAsync();
        }
        private async void cmsItemDeletePurchaseInvoice_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPurchaseInvoice = (PurchaseInvoice)dgvPurchaseInvoices.SelectedRows[0].DataBoundItem;
            if (selectedPurchaseInvoice.PayOrderID.HasValue)
            {
                MessageBox.Show("No es posible realizar la operación: la factura tiene órdenes de pago asociadas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string messageText = $"Por favor, confirme la eliminación de la siguiente factura:\n\nID Factura: {selectedPurchaseInvoice.PurchaseInvoiceID:D8}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => PurchaseInvoice.DeleteInvoiceById(selectedPurchaseInvoice.PurchaseInvoiceID));
                MessageBox.Show("Factura de proveedor eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint PI101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint PI101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdatePurchaseInvoicesAsync();
        }

        private async void tabPurchaseInvoices_Enter(object sender, EventArgs e)
        {
            await UpdatePurchaseInvoicesAsync();
        }

        private async Task UpdatePurchaseInvoicesAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshPurchaseInvoices.Text = "Actualizando...";
            btnRefreshPurchaseInvoices.Enabled = false;
            btnPurchaseInvoicesViewSettings.Enabled = false;
            try
            {
                var invoices = await Task.Run(() => PurchaseInvoice.GetInvoices(viewSettingsPurchaseInvoices.ToString()));
                SetGridDataSource(dgvPurchaseInvoices, invoices, viewSettingsPurchaseInvoices);
            }
            catch (Exception dbException)
            {
                // Waypoint PI102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PI102 (Flag: MySQL). Message: " + dbException.Message);
                dgvPurchaseInvoices.DataSource = null;
            }
            finally
            {
                btnRefreshPurchaseInvoices.Text = "Actualizar";
                btnRefreshPurchaseInvoices.Enabled = true;
                btnPurchaseInvoicesViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region PayOrders Tab
        private async void btnRefreshPayOrders_Click(object sender, EventArgs e)
        {
            await UpdatePayOrdersAsync();
        }
        private async void btnPayOrdersViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsPayOrders))
            {
                form.ShowDialog();
                lblPayOrdersFilterWarning.Visible = (viewSettingsPayOrders.Filters.Count != 0);
            }
            await UpdatePayOrdersAsync();
        }
        private async void btnAddPayOrder_Click(object sender, EventArgs e)
        {
            using (var form = new PP_PayOrder())
            {
                form.ShowDialog();
            }
            await UpdatePayOrdersAsync();
        }
        private async void btnExportPayOrders_Click(object sender, EventArgs e)
        {
            // Pregunta al usuario ruta de destino y formato.
            string filePath;
            int filterIndex;
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.AddExtension = true;
                saveDialog.FileName = "informe.xlsx";
                saveDialog.Filter = "Libro de Excel|*.xlsx|Documento PDF|*.pdf";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveDialog.FileName;
                    filterIndex = saveDialog.FilterIndex;
                }
                else
                {
                    return;
                }
            }
            // Obtiene información de base de datos.
            List<PayOrderPayment> payments;
            try
            {
                payments = await Task.Run(() => PayOrderPayment.GetPayments(viewSettingsPayOrders.ToString(), 1000));
            }
            catch (Exception dbException)
            {
                // Waypoint PP101
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP101 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            // Exporta información.
            try
            {
                switch (filterIndex)
                {
                    case 1:
                    default: // Formato Excel.
                        {
                            await Task.Run(() => ExcelGeneration.ExportExcelPayOrdersReport(filePath, payments));
                            break;
                        }
                    case 2: // Formato PDF.
                        {
                            await Task.Run(() => PdfGeneration.ExportPdfPayOrdersReport(payments, filePath));
                            break;
                        }
                }
            }
            catch (Exception exportException)
            {
                // Waypoint PP102
                MessageBox.Show("Error exportar la información."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP102. Message: " + exportException.Message);
                return;
            }
            // Abre el archivo.
            try
            {
                using (var fileOpenProcess = new Process())
                {
                    fileOpenProcess.StartInfo.FileName = filePath;
                    fileOpenProcess.Start();
                }
            }
            catch (Exception fileOpenException)
            {
                // Waypoint PP103
                MessageBox.Show("Error al abrir archivo."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + fileOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP103. Message: " + fileOpenException.Message);
            }
        }

        private async void dgvPayOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvPayOrders, e.ColumnIndex, viewSettingsPayOrders);
                await UpdatePayOrdersAsync();
            }
        }

        private async void cmsItemOpenPayOrder_Click(object sender, EventArgs e)
        {
            if (dgvPayOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPayOrder = (PayOrder)dgvPayOrders.SelectedRows[0].DataBoundItem;
            using (var form = new PP_PayOrder(selectedPayOrder.PayOrderID))
            {
                form.ShowDialog();
            }
            await UpdatePayOrdersAsync();
        }
        private async void cmsItemDeletePayOrder_Click(object sender, EventArgs e)
        {
            if (dgvPayOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPayOrder = (PayOrder)dgvPayOrders.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación de la siguiente orden de pago:\n\nID Orden: {selectedPayOrder.PayOrderID:D8}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        // ATENCIÓN: No se debe alterar el orden de estas operaciones.
                        PurchaseInvoice.UpdateStatusByPayOrderId(selectedPayOrder.PayOrderID, "Pendiente", handler);
                        PayOrder.DeletePayOrderById(selectedPayOrder.PayOrderID, handler);
                        handler.CommitTransaction();
                    }
                });
                MessageBox.Show("Orden de pago eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint PP104
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint PP104 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdatePayOrdersAsync();
        }

        private async void tabPayOrders_Enter(object sender, EventArgs e)
        {
            await UpdatePayOrdersAsync();
        }

        private async Task UpdatePayOrdersAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshPayOrders.Text = "Actualizando...";
            btnRefreshPayOrders.Enabled = false;
            btnPayOrdersViewSettings.Enabled = false;
            try
            {
                var orders = await Task.Run(() => PayOrder.GetPayOrders(viewSettingsPayOrders.ToString()));
                SetGridDataSource(dgvPayOrders, orders, viewSettingsPayOrders);
            }
            catch (Exception dbException)
            {
                // Waypoint PP105
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP105 (Flag: MySQL). Message: " + dbException.Message);
                dgvPayOrders.DataSource = null;
            }
            finally
            {
                btnRefreshPayOrders.Text = "Actualizar";
                btnRefreshPayOrders.Enabled = true;
                btnPayOrdersViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion
        #region Inputs Tab
        private async void btnRefreshInputs_Click(object sender, EventArgs e)
        {
            await UpdateInputsAsync();
        }
        private async void btnInputsViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsInputs))
            {
                form.ShowDialog();
                lblInputsFilterWarning.Visible = (viewSettingsInputs.Filters.Count != 0);
            }
            await UpdateInputsAsync();
        }
        private async void btnAddInput_Click(object sender, EventArgs e)
        {
            using (var form = new IN_Input())
            {
                form.ShowDialog();
            }
            await UpdateInputsAsync();
        }

        private async void dgvInputs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvInputs, e.ColumnIndex, viewSettingsInputs);
                await UpdateInputsAsync();
            }
        }

        private async void cmsItemOpenInput_Click(object sender, EventArgs e)
        {
            if (dgvInputs.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedInput = (Input)dgvInputs.SelectedRows[0].DataBoundItem;
            using (var form = new IN_Input(selectedInput.InputID))
            {
                form.ShowDialog();
            }
            await UpdateInputsAsync();
        }
        private void cmsItemShowLatestEstimates_Click(object sender, EventArgs e)
        {
            if (dgvInputs.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedInput = (Input)dgvInputs.SelectedRows[0].DataBoundItem;
            using (var form = new IN_InputHistory(selectedInput.InputID))
            {
                form.ShowDialog();
            }
        }
        private async void cmsItemDeleteInput_Click(object sender, EventArgs e)
        {
            if (dgvInputs.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedInput = (Input)dgvInputs.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación del siguiente insumo:\n\nID Insumo: {selectedInput.InputID:D8}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => Input.DeleteInputById(selectedInput.InputID));
                MessageBox.Show("Insumo eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint IN101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint IN101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateInputsAsync();
        }

        private async void tabInputs_Enter(object sender, EventArgs e)
        {
            await UpdateInputsAsync();
        }

        private async Task UpdateInputsAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshInputs.Text = "Actualizando...";
            btnRefreshInputs.Enabled = false;
            btnInputsViewSettings.Enabled = false;
            try
            {
                var inputs = await Task.Run(() => Input.GetInputs(viewSettingsInputs.ToString()));
                SetGridDataSource(dgvInputs, inputs, viewSettingsInputs);
            }
            catch (Exception dbException)
            {
                // Waypoint IN102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint IN102 (Flag: MySQL). Message: " + dbException.Message);
                dgvInputs.DataSource = null;
            }
            finally
            {
                btnRefreshInputs.Text = "Actualizar";
                btnRefreshInputs.Enabled = true;
                btnInputsViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion

        #region Expenses Tab
        private async void btnRefreshExpenses_Click(object sender, EventArgs e)
        {
            await UpdateExpensesAsync();
        }
        private async void btnExpensesViewSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ViewSettings(viewSettingsExpenses))
            {
                form.ShowDialog();
                lblExpensesFilterWarning.Visible = (viewSettingsExpenses.Filters.Count != 0);
            }
            await UpdateExpensesAsync();
        }
        private async void btnAddExpense_Click(object sender, EventArgs e)
        {
            using (var form = new EX_Expense())
            {
                form.ShowDialog();
            }
            await UpdateExpensesAsync();
        }
        private async void btnExportExpenses_Click(object sender, EventArgs e)
        {
            await Export(new Func<List<Expense>>(() => Expense.GetExpenses(viewSettingsExpenses.ToString(), 1000)), dgvExpenses);
        }

        private async void dgvExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                HandleColumnSorting(dgvExpenses, e.ColumnIndex, viewSettingsExpenses);
                await UpdateExpensesAsync();
            }
        }

        private async void cmsItemOpenExpense_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedExpense = (Expense)dgvExpenses.SelectedRows[0].DataBoundItem;
            using (var form = new EX_Expense(selectedExpense.ExpenseID))
            {
                form.ShowDialog();
            }
            await UpdateExpensesAsync();
        }
        private async void cmsItemDeleteExpense_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedExpense = (Expense)dgvExpenses.SelectedRows[0].DataBoundItem;
            string messageText = $"Por favor, confirme la eliminación del siguiente gasto:\n\nID Gasto: {selectedExpense.ExpenseID:D8}";
            var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => Expense.DeleteExpenseById(selectedExpense.ExpenseID));
                MessageBox.Show("Gasto eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint EX101
                if (DbHelpers.CheckFKConstraintViolation(dbException))
                {
                    const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                             + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                    MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Logger.AppendLog("Exception at Waypoint EX101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateExpensesAsync();
        }

        private async void tabExpenses_Enter(object sender, EventArgs e)
        {
            await UpdateExpensesAsync();
        }

        private async Task UpdateExpensesAsync()
        {
            tmrAutoUpdate.Stop();
            btnRefreshExpenses.Text = "Actualizando...";
            btnRefreshExpenses.Enabled = false;
            btnExpensesViewSettings.Enabled = false;
            try
            {
                var expenses = await Task.Run(() => Expense.GetExpenses(viewSettingsExpenses.ToString()));
                SetGridDataSource(dgvExpenses, expenses, viewSettingsExpenses);
            }
            catch (Exception dbException)
            {
                // Waypoint EX102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint EX102 (Flag: MySQL). Message: " + dbException.Message);
                dgvExpenses.DataSource = null;
            }
            finally
            {
                btnRefreshExpenses.Text = "Actualizar";
                btnRefreshExpenses.Enabled = true;
                btnExpensesViewSettings.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion

        #region Tasks Tab
        private async void btnRefreshTasks_Click(object sender, EventArgs e)
        {
            await UpdateTasksAsync(false);
        }
        private async void btnReschedulePending_Click(object sender, EventArgs e)
        {
            string textMessage = "El sistema reprogramará todas las tareas y envíos pendientes a la fecha de hoy.\n\n¿Desea continuar?";
            var dialog = MessageBox.Show(textMessage, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        // Reprograma tareas fuera de fecha.
                        var outdatedTasks = ScheduledTask.GetOutdatedTasks(handler);
                        foreach (var task in outdatedTasks)
                        {
                            task.Description = $"(REPRO. {task.Date:dd/MM/yy}) {task.Description}";
                            task.Date = DateTime.Today;
                            task.Update(handler);
                        }
                        // Reprograma envíos fuera de fecha.
                        Sale.RescheduleOutdatedSales(handler);
                        handler.CommitTransaction();
                    }
                });
            }
            catch (Exception dbException)
            {
                // Waypoint TK101
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK101 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateTasksAsync(false);
        }
        private async void btnNextMonth_Click(object sender, EventArgs e)
        {
            taskCalendar.Time = taskCalendar.Time.AddMonths(1);
            await UpdateTasksAsync(true);
        }
        private async void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            taskCalendar.Time = taskCalendar.Time.AddMonths(-1);
            await UpdateTasksAsync(true);
        }
        private async void btnAddTask_Click(object sender, EventArgs e)
        {
            using (var form = new TK_Task())
            {
                form.ShowDialog();
            }
            await UpdateTasksAsync(false);
        }

        private async void tabCalendar_Enter(object sender, EventArgs e)
        {
            await UpdateTasksAsync(false);
        }

        private void pnlTaskCalendar_SizeChanged(object sender, EventArgs e)
        {
            if (pnlTaskCalendar.Size.Width > 0 && pnlTaskCalendar.Size.Height > 0)
            {
                taskCalendar.DrawCalendar(pnlTaskCalendar.Size);
            }
        }
        private async void pnlTaskCalendar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var timeFromLocation = taskCalendar.GetTimeFromLocation(e.Location);
            if (!timeFromLocation.HasValue)
            {
                return;
            }
            using (var form = new TK_CalendarDayView(timeFromLocation.Value))
            {
                form.ShowDialog();
            }
            await UpdateTasksAsync(false);
        }

        private void taskCalendar_NewFrame(object sender, Bitmap e)
        {
            if (pnlTaskCalendar.BackgroundImage != null)
            {
                pnlTaskCalendar.BackgroundImage.Dispose();
                pnlTaskCalendar.BackgroundImage = null;
            }
            pnlTaskCalendar.BackgroundImage = e;
        }

        private async Task UpdateTasksAsync(bool forceDrawing)
        {
            tmrAutoUpdate.Stop();
            btnRefreshTasks.Text = "Actualizando...";
            btnRefreshTasks.Enabled = false;
            try
            {
                // Obtiene información de base de datos.
                var tasksDates = await Task.Run(() => ScheduledTask.GetPendingTasksDates(taskCalendar.Time, taskCalendar.Time.AddMonths(1).AddDays(-1)));
                var shipmentsDates = await Task.Run(() => Sale.GetUnshippedSalesDates(taskCalendar.Time, taskCalendar.Time.AddMonths(1).AddDays(-1)));
                // Guarda los eventos actuales.
                var lastEvents = taskCalendar.CustomColoring.ToArray();
                // Agrega eventos al calendario.
                taskCalendar.CustomColoring.Clear();
                foreach (var date in tasksDates.Concat(shipmentsDates).Distinct().OrderBy(X => X))
                {
                    if (tasksDates.Contains(date) && shipmentsDates.Contains(date))
                    {
                        taskCalendar.CustomColoring.Add(date, CloverCalendar.ColoringOptions.Both);
                    }
                    else if (tasksDates.Contains(date))
                    {
                        taskCalendar.CustomColoring.Add(date, CloverCalendar.ColoringOptions.Event);
                    }
                    else
                    {
                        taskCalendar.CustomColoring.Add(date, CloverCalendar.ColoringOptions.Shipment);
                    }
                }
                // Re-dibuja el calendario si hay nuevos eventos o fue forzado.
                if (!lastEvents.SequenceEqual(taskCalendar.CustomColoring) || forceDrawing)
                {
                    taskCalendar.DrawCalendar(pnlTaskCalendar.Size);
                }
            }
            catch (Exception dbException)
            {
                taskCalendar.CustomColoring.Clear();
                taskCalendar.DrawCalendar(pnlTaskCalendar.Size);
                // Waypoint TK102
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK102 (Flag: MySQL). Message: " + dbException.Message);
            }
            finally
            {
                btnRefreshTasks.Text = "Actualizar";
                btnRefreshTasks.Enabled = true;
                tmrAutoUpdate.Start();
            }
        }
        #endregion

        #region Chat Tab


        private void txtChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Prevenir que se añada una nueva línea
                e.SuppressKeyPress = true;

                // Enviar el mensaje
                btnSendMessage.PerformClick();
            }
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("btnSendMessage_Click triggered.");

            if (string.IsNullOrWhiteSpace(txtChatInput?.Text))
            {
                return;
            }

            if (AppEnvironment.CurrentUser == null)
            {
                MessageBox.Show("Error: Usuario no está autenticado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var envelope = new Envelope(AppEnvironment.CurrentUser, null, EnvelopeType.BroadcastMessage, txtChatInput.Text, false);
            txtChatInput.Text = string.Empty;

            try
            {
                if (ChatServerEndPoint == null)
                {
                    MessageBox.Show("Error: Servidor de chat no está disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ChatServerEndPoint.SendEnvelope(envelope);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error al enviar mensaje."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CH105. Message: " + exception.Message);
                return;
            }

            if (envelope != null)
            {
                PrintMessage(envelope);
                StoreMessage(envelope);
            }
        }

        private void lvwClients_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwClients.SelectedItems.Count == 0)
            {
                return;
            }
            var user = (User)lvwClients.SelectedItems[0].Tag;
            if (user.UserID == AppEnvironment.CurrentUser.UserID)
            {
                return;
            }
            if (this.OwnedForms.Any(f => f is CH_PrivateChat && ((User)f.Tag).UserID == user.UserID))
            {
                var clientForm = this.OwnedForms.Single(f => f is CH_PrivateChat && ((User)f.Tag).UserID == user.UserID);
                clientForm.WindowState = FormWindowState.Normal;
                clientForm.BringToFront();
            }
            else
            {
                new CH_PrivateChat(user).Show(this);
            }
        }

        private async void tmrRetryConnection_Tick(object sender, EventArgs e)
        {
            tmrRetryConnection.Stop();
            // Connect to chat server
            try
            {
                ChatServerEndPoint = await EndPointManager.ConnectAsync(new IPEndPoint(IPAddress.Parse(AppEnvironment.CurrentSettings.ChatServerIP), AppEnvironment.CurrentSettings.ChatServerPort), HandleEnvelope);
                // Send handshake
                await ChatServerEndPoint.SendEnvelopeAsync(new Envelope(AppEnvironment.CurrentUser, null, EnvelopeType.Handshake, null, false));
                MonitorConnection();
                lblConnectionError.Visible = false;
                btnSendMessage.Enabled = true;
            }
            catch
            {
                // Waypoint CH106
                lblConnectionError.Visible = true;
                btnSendMessage.Enabled = false;
                ChatServerEndPoint?.Shutdown();
                tmrRetryConnection.Start();
            }
        }

        private async void MonitorConnection()
        {
            await ChatServerEndPoint.ReadTask;
            // Connection lost
            lblConnectionError.Visible = true;
            btnSendMessage.Enabled = false;
            ChatServerEndPoint?.Shutdown();
            tmrRetryConnection.Start();
        }
        private void HandleEnvelope(EndPointManager sender, Envelope receivedEnvelope)
        {
            switch (receivedEnvelope.EnvelopeType)
            {
                case EnvelopeType.BroadcastMessage:
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            // Show notification
                            if (!receivedEnvelope.IsQueuedEnvelope)
                            {
                                trayIcon.ShowBalloonTip(5000, $"Mensaje de {receivedEnvelope.Sender.UserName}", receivedEnvelope.Body, ToolTipIcon.Info);
                            }
                            // Print message on chat
                            PrintMessage(receivedEnvelope);
                            // Save message to db
                            StoreMessage(receivedEnvelope);
                        });
                        break;
                    }
                case EnvelopeType.Message:
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            if (this.OwnedForms.Any(f => f is CH_PrivateChat && ((User)f.Tag).UserID == receivedEnvelope.Sender.UserID))
                            {
                                var chatForm = (CH_PrivateChat)this.OwnedForms.Single(f => f is CH_PrivateChat && ((User)f.Tag).UserID == receivedEnvelope.Sender.UserID);
                                chatForm.WindowState = FormWindowState.Normal;
                                chatForm.BringToFront();
                                chatForm.HandleEnvelope(receivedEnvelope);
                            }
                            else
                            {
                                var chatForm = new CH_PrivateChat(receivedEnvelope.Sender);
                                chatForm.Show(this);
                                chatForm.HandleEnvelope(receivedEnvelope);
                            }
                            // Show notification
                            if (!receivedEnvelope.IsQueuedEnvelope)
                            {
                                trayIcon.ShowBalloonTip(5000, $"Mensaje privado de {receivedEnvelope.Sender.UserName}", receivedEnvelope.Body, ToolTipIcon.Info);
                            }
                        });
                        break;
                    }
                case EnvelopeType.ConnectedClientsList:
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            var connectedClients = receivedEnvelope.Body.Split(';');
                            foreach (ListViewItem item in lvwClients.Items)
                            {
                                if (connectedClients.Contains(item.Text))
                                {
                                    item.ForeColor = Color.Black;
                                }
                                else
                                {
                                    item.ForeColor = Color.DarkGray;
                                }
                            }
                        });
                        break;
                    }
                case EnvelopeType.ServerShuttingDown:
                    {
                        trayIcon.ShowBalloonTip(5000, "Clover Gestión", "El servidor de chat fue apagado por el administrador.", ToolTipIcon.Warning);
                        break;
                    }
            }
        }
        private void PrintMessage(Envelope envelope)
        {
            if (lastMessageDate == null || lastMessageDate != DateTime.Today)
            {
                rtbChatMessages.DeselectAll();
                rtbChatMessages.AppendText(Environment.NewLine);
                rtbChatMessages.SelectionAlignment = HorizontalAlignment.Center;
                rtbChatMessages.SelectionColor = Color.Black;
                rtbChatMessages.AppendText(DateTime.Today.ToString("dddd, d 'de' MMMM"));
                lastMessageDate = DateTime.Today;
            }
            rtbChatMessages.DeselectAll();
            rtbChatMessages.AppendText(Environment.NewLine);
            rtbChatMessages.SelectionAlignment = HorizontalAlignment.Left;
            if (envelope.Sender.UserID == AppEnvironment.CurrentUser.UserID)
            {
                rtbChatMessages.SelectionColor = Color.Black;
                rtbChatMessages.AppendText($"Tu : {envelope.Body}");
            }
            else
            {
                rtbChatMessages.SelectionColor = Color.FromName(envelope.Sender.ChatColor);
                rtbChatMessages.AppendText(envelope.Sender.UserName);
                rtbChatMessages.SelectionColor = Color.Black;
                rtbChatMessages.AppendText(" : ");
                rtbChatMessages.AppendText(envelope.Body);
            }
            rtbChatMessages.ScrollToCaret();
        }
        private async void StoreMessage(Envelope envelope)
        {
            var message = new ChatMessage()
            {
                OwnerID = AppEnvironment.CurrentUser.UserID,
                SenderID = envelope.Sender.UserID,
                Timestamp = envelope.Timestamp,
                Body = envelope.Body
            };
            try
            {
                await Task.Run(() => message.Insert());
            }
            catch (Exception dbException)
            {
                // Waypoint CH107
                MessageBox.Show("Error al guardar mensaje en historial."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CH107 (Flag: MySQL). Message: " + dbException.Message);
            }
        }
        #endregion

        // Funciones compartidas.
        private void SetGridDataSource<T>(DataGridView target, List<T> items, ViewSettings viewSettings)
        {
            // Guarda Id del elemento seleccionado.
            int selectedItemId = -1;
            if (target.SelectedRows.Count != 0)
            {
                selectedItemId = ((DbEntity)target.SelectedRows[0].DataBoundItem).PrimaryKeyID;
            }
            // Define origen de datos del grid.
            target.DataSource = items;
            // Muestra indicadores de ordenamiento en encabezado.
            foreach (var level in viewSettings.SortLevels)
            {
                string dataPropertyName = level.Field.Name.Contains('.') ? level.Field.Name.Split('.')[1] : level.Field.Name;
                var column = target.Columns.Cast<DataGridViewColumn>().Single(x => x.DataPropertyName == dataPropertyName);
                column.HeaderCell.SortGlyphDirection = (System.Windows.Forms.SortOrder)level.Direction; // Asegúrate de usar System.Windows.Forms.SortOrder
            }
            // Restaura Id seleccionado
            var matches = target.Rows.Cast<DataGridViewRow>().Where(x => ((DbEntity)x.DataBoundItem).PrimaryKeyID == selectedItemId);
            if (matches.Count() != 0)
            {
                int index = matches.Single().Index;
                target.CurrentCell = target.Rows[index].Cells[0];
            }
        }

        private void HandleColumnSorting(DataGridView target, int columnIndex, ViewSettings viewSettings)
        {
            var col = target.Columns[columnIndex];
            if (col.SortMode == DataGridViewColumnSortMode.NotSortable)
            {
                return;
            }
            string fieldName = viewSettings.Fields.Single(f => f.Name.Split('.').Last() == col.DataPropertyName).Name;
            if (col.HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Descending) // Asegúrate de usar System.Windows.Forms.SortOrder
            {
                viewSettings.RemoveSortLevel(fieldName);
                col.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
            }
            else if (viewSettings.TryAddSortLevel(fieldName, (SortDirection)((int)col.HeaderCell.SortGlyphDirection + 1)))
            {
                col.HeaderCell.SortGlyphDirection = (System.Windows.Forms.SortOrder)((int)col.HeaderCell.SortGlyphDirection + 1); // Asegúrate de usar System.Windows.Forms.SortOrder
            }
        }

        private void dgvDelta_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona fila cuando se hace click con el botón derecho.
            var target = (DataGridView)sender;
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = target.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    target.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private async Task Export<T>(Func<List<T>> pullDelegate, DataGridView dataGridView) where T : DbEntity
        {
            // Pregunta al usuario ruta de destino y formato.
            string filePath;
            int filterIndex;
            using (var saveDialog = new SaveFileDialog()
            {
                AddExtension = true,
                FileName = "informe.xlsx",
                Filter = "Libro de Excel|*.xlsx|Documento PDF|*.pdf"
            })
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveDialog.FileName;
                    filterIndex = saveDialog.FilterIndex;
                }
                else
                {
                    return;
                }
            }
            // Obtiene información de base de datos.
            List<T> entities;
            try
            {
                entities = await Task.Run(() => pullDelegate.Invoke());
            }
            catch (Exception dbException)
            {
                // Waypoint MA104
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA104 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            // Exporta información.
            try
            {
                var table = BuildDataTable(entities, dataGridView);
                switch (filterIndex)
                {
                    case 1:
                    default: // Formato Excel.
                        {
                            await Task.Run(() => ExcelGeneration.ExportExcelDataTable(filePath, table));
                            break;
                        }
                    case 2: // Formato PDF.
                        {
                            float[] columnWidths = GetColumnsWidthPercent(dataGridView);
                            await Task.Run(() => PdfGeneration.ExportPdfDataTable("Informe", columnWidths, table, filePath, true, true));
                            break;
                        }
                }
            }
            catch (Exception exportException)
            {
                // Waypoint MA105
                MessageBox.Show("Error exportar la información."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA105. Message: " + exportException.Message);
                return;
            }
            // Abre el archivo.
            try
            {
                using (var fileOpenProcess = new Process())
                {
                    fileOpenProcess.StartInfo.FileName = filePath;
                    fileOpenProcess.Start();
                }
            }
            catch (Exception fileOpenException)
            {
                // Waypoint MA106
                MessageBox.Show("Error al abrir archivo."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + fileOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA106. Message: " + fileOpenException.Message);
            }
        }
        private DataTable BuildDataTable<T>(List<T> entities, DataGridView dataGridView) where T : DbEntity
        {
            var dataTable = new DataTable();
            // Genera las columnas a partir del DataGridView.
            Type type = typeof(T);
            string[] dataPropertyNames = new string[dataGridView.ColumnCount];
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                var property = type.GetProperty(dataGridView.Columns[i].DataPropertyName);
                // Agrega columna a tabla.
                var column = new DataColumn
                {
                    ColumnName = $"column{i + 1}",
                    Caption = dataGridView.Columns[i].HeaderText,
                    DataType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType,
                    AllowDBNull = true
                };
                dataTable.Columns.Add(column);
                // Guarda nombre de la propiedad.
                dataPropertyNames[i] = dataGridView.Columns[i].DataPropertyName;
            }
            // Genera las filas a partir de los objetos.
            foreach (var entity in entities)
            {
                object[] values = new object[dataPropertyNames.Length];
                for (int i = 0; i < dataPropertyNames.Length; i++)
                {
                    values[i] = type.GetProperty(dataPropertyNames[i]).GetValue(entity);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        private float[] GetColumnsWidthPercent(DataGridView dataGridView)
        {
            float[] columnWidths = dataGridView.Columns.Cast<DataGridViewColumn>().Select(x => (float)x.Width).ToArray();
            float sum = columnWidths.Sum();
            return columnWidths.Select(x => x / sum).ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrarIngreso_Click(object sender, EventArgs e)
        {
            using (var formIngreso = new FormIngresoEgreso("Ingreso"))
            {
                if (formIngreso.ShowDialog() == DialogResult.OK)
                {
                    // Aquí guardas la información ingresada en la base de datos.
                    RegistrarTransaccion("Ingreso", formIngreso.Monto, formIngreso.Metodo);
                }
            }
        }

        private void btnRegistrarEgreso_Click(object sender, EventArgs e)
        {
            using (var formEgreso = new FormIngresoEgreso("Egreso"))
            {
                if (formEgreso.ShowDialog() == DialogResult.OK)
                {
                    // Aquí guardas la información ingresada en la base de datos.
                    RegistrarTransaccion("Egreso", formEgreso.Monto, formEgreso.Metodo);
                }
            }
        }

        private void btnVerDineroDisponible_Click(object sender, EventArgs e)
        {
            // Abrir el formulario para seleccionar la fecha
            using (var formFecha = new FormSeleccionarFecha())
            {
                if (formFecha.ShowDialog() == DialogResult.OK)
                {
                    // Obtener la fecha seleccionada del formulario
                    DateTime startDate = formFecha.FechaSeleccionada;

                    var saldos = new Dictionary<string, decimal>();

                    // Inicializar los métodos de pago con saldo 0
                    foreach (var metodo in Enum.GetNames(typeof(PaymentMethod)))
                    {
                        saldos[metodo] = 0m;
                    }

                    // Consulta SQL para obtener ingresos desde sale y egresos desde purchase_invoice filtrados por fecha de inicio
                    string query = @"
                SELECT 
                    p.PaymentName, 
                    SUM(CASE WHEN s.TotalBeforeTax >= 0 AND s.Date >= @startDate THEN s.TotalBeforeTax ELSE 0 END) AS TotalIngreso,
                    SUM(CASE WHEN s.TotalBeforeTax < 0 AND s.Date >= @startDate THEN s.TotalBeforeTax ELSE 0 END) AS TotalEgreso,
                    COALESCE((SELECT SUM(pi.TotalAmount) 
                              FROM `sellosmec_db_v2`.`purchase_invoice` pi 
                              WHERE pi.PayOrderID = s.PaymentID AND pi.InvoiceDate >= @startDate), 0) AS EgresosFactura
                FROM `sellosmec_db_v2`.`sale` s
                JOIN `sellosmec_db_v2`.`payment` p ON s.PaymentID = p.PaymentID
                WHERE p.PaymentName NOT IN ('Tarjeta de crédito', 'Tarjeta de débito', 'Transferencia', 'Cheque diferido', 'Cuenta bancaria')
                GROUP BY p.PaymentName, s.PaymentID";

                    using (var connection = new MySqlConnection(DbConfig.CadenaConexion))
                    {
                        using (var command = new MySqlCommand(query, connection))
                        {
                            // Añadir el parámetro de la fecha de inicio a la consulta
                            command.Parameters.AddWithValue("@startDate", startDate);

                            connection.Open();
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string metodo = reader["PaymentName"].ToString();
                                    decimal ingreso = Convert.ToDecimal(reader["TotalIngreso"]);
                                    decimal egreso = Convert.ToDecimal(reader["TotalEgreso"]);
                                    decimal egresoFactura = Convert.ToDecimal(reader["EgresosFactura"]);

                                    // El saldo total es la suma de ingresos menos egresos, menos egresos de facturas
                                    saldos[metodo] = ingreso + egreso - egresoFactura;
                                }
                            }
                        }
                    }

                    // Mostrar los saldos disponibles con el nombre del método de pago
                    StringBuilder sb = new StringBuilder();
                    foreach (var saldo in saldos)
                    {
                        sb.AppendLine($"{saldo.Key}: {saldo.Value:C}");
                    }

                    MessageBox.Show(sb.ToString(), "Saldos Disponibles");
                }
            }
        }







        private void RegistrarTransaccion(string tipoTransaccion, decimal monto, string metodo)
        {
            string query = "INSERT INTO transacciones (TipoTransaccion, Monto, Metodo) VALUES (@tipoTransaccion, @monto, @metodo)";

            using (var connection = new MySqlConnection(DbConfig.CadenaConexion))
            {
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tipoTransaccion", tipoTransaccion);
                    command.Parameters.AddWithValue("@monto", monto);
                    command.Parameters.AddWithValue("@metodo", metodo);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabRepairSellos_Click(object sender, EventArgs e)
        {

        }

        private void btnregistrarrepsellos_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario que deseas abrir
            SellosForm formRegistroOrdenSellos = new SellosForm();

            // Mostrar el formulario como una ventana modal
            formRegistroOrdenSellos.ShowDialog();

            // O si prefieres que no sea modal
            // formRegistroOrdenSellos.Show();
        }

        private void btnCreateGroupChat_Click_Click(object sender, EventArgs e)
        {
            FormCreateGroupChat formCreateGroupChat = new FormCreateGroupChat();
            formCreateGroupChat.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenChat_Click_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenGroupChats_Click_Click(object sender, EventArgs e)
        {
            FormGroupChats formGroupChats = new FormGroupChats();
            formGroupChats.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void Main_Load(object sender, EventArgs e)
        {
            dataGridViewAgenda.AutoGenerateColumns = true;
            ActualizarDataGridView();
        }

        private void btnOpenUserSelection_Click(object sender, EventArgs e)
        {
            UserSelectionForm userSelectionForm = new UserSelectionForm();
            userSelectionForm.ShowDialog(); // Mostrar el formulario como modal
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private DataTable GetSalesData(DateTime startDate, DateTime endDate)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();

                    string query = @"
            SELECT 
                `sale_item`.`ItemID`,
                `sale_item`.`SaleID`,
                `sale_item`.`ItemNumber`,
                `sale_item`.`ProductID`,
                `sale_item`.`Description`,
                `sale_item`.`DeliveryDelay`,
                `sale_item`.`Quantity`,
                `sale_item`.`Amount`,
                `sale_item`.`TotalAmount`,
                `sale_item`.`VatID`,
                `sale_item`.`DeliveryNoteID`,
                `sale_item`.`InvoiceID`,
                `sale_item`.`Cost`
            FROM `sellosmec_db_v2`.`sale_item`
            WHERE `sale_item`.`SaleID` IN (
                SELECT `sale`.`SaleID` 
                FROM `sellosmec_db_v2`.`sale` 
                WHERE `sale`.`Date` BETWEEN @startDate AND @endDate
            )";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        // Aumentar el tiempo de espera del comando a 180 segundos
                        cmd.CommandTimeout = 180;

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                // Traducir los nombres de las columnas a español
                RenameColumnsToSpanish(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de ventas: " + ex.Message);
            }

            return dataTable;
        }

        private DataTable GetAllSalesData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();

                    string query = @"
            SELECT 
                `sale_item`.`ItemID`,
                `sale_item`.`SaleID`,
                `sale_item`.`ItemNumber`,
                `sale_item`.`ProductID`,
                `sale_item`.`Description`,
                `sale_item`.`DeliveryDelay`,
                `sale_item`.`Quantity`,
                `sale_item`.`Amount`,
                `sale_item`.`TotalAmount`,
                `sale_item`.`VatID`,
                `sale_item`.`DeliveryNoteID`,
                `sale_item`.`InvoiceID`,
                `sale_item`.`Cost`
            FROM `sellosmec_db_v2`.`sale_item`";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Aumentar el tiempo de espera del comando a 180 segundos
                        cmd.CommandTimeout = 180;

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                // Traducir los nombres de las columnas a español
                RenameColumnsToSpanish(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de ventas: " + ex.Message);
            }

            return dataTable;
        }

        // Función para renombrar las columnas a español
        private void RenameColumnsToSpanish(DataTable dataTable)
        {
            if (dataTable.Columns.Contains("ItemID"))
                dataTable.Columns["ItemID"].ColumnName = "ID del Artículo";
            if (dataTable.Columns.Contains("SaleID"))
                dataTable.Columns["SaleID"].ColumnName = "ID de la Venta";
            if (dataTable.Columns.Contains("ItemNumber"))
                dataTable.Columns["ItemNumber"].ColumnName = "Número del Artículo";
            if (dataTable.Columns.Contains("ProductID"))
                dataTable.Columns["ProductID"].ColumnName = "ID del Producto";
            if (dataTable.Columns.Contains("Description"))
                dataTable.Columns["Description"].ColumnName = "Descripción";
            if (dataTable.Columns.Contains("DeliveryDelay"))
                dataTable.Columns["DeliveryDelay"].ColumnName = "Demora en la Entrega";
            if (dataTable.Columns.Contains("Quantity"))
                dataTable.Columns["Quantity"].ColumnName = "Cantidad";
            if (dataTable.Columns.Contains("Amount"))
                dataTable.Columns["Amount"].ColumnName = "Monto";
            if (dataTable.Columns.Contains("TotalAmount"))
                dataTable.Columns["TotalAmount"].ColumnName = "Monto Total";
            if (dataTable.Columns.Contains("VatID"))
                dataTable.Columns["VatID"].ColumnName = "ID del IVA";
            if (dataTable.Columns.Contains("DeliveryNoteID"))
                dataTable.Columns["DeliveryNoteID"].ColumnName = "ID de la Nota de Entrega";
            if (dataTable.Columns.Contains("InvoiceID"))
                dataTable.Columns["InvoiceID"].ColumnName = "ID de la Factura";
            if (dataTable.Columns.Contains("Cost"))
                dataTable.Columns["Cost"].ColumnName = "Costo";
        }





        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }




        public class Actividad
        {
            public DateTime Fecha { get; set; }
            public string NombreEmpleado { get; set; }
            public string ActividadTexto { get; set; }
            public string Prioridad { get; set; }
            public string Estado { get; set; }
        }

        private List<Actividad> listaActividades = new List<Actividad>();

        private void btnAgregarActividad_Click(object sender, EventArgs e)
        {
            // Crear una nueva actividad
            Actividad nuevaActividad = new Actividad
            {
                Fecha = dtpFecha.Value.Date,
                NombreEmpleado = txtNombre.Text,
                ActividadTexto = txtActividad.Text,
                Prioridad = cboPrioridad.SelectedItem.ToString(),
                Estado = cboEstado.SelectedItem.ToString()
            };

            // Agregar la actividad a la lista
            listaActividades.Add(nuevaActividad);

            // Actualizar el DataGridView
            ActualizarDataGridView();
        }

        private void ActualizarDataGridView()
        {
            dataGridViewAgenda.DataSource = null;
            dataGridViewAgenda.DataSource = listaActividades;
        }

        private void btnEliminarActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewAgenda.SelectedRows.Count > 0)
            {
                // Obtener la actividad seleccionada
                Actividad actividadSeleccionada = (Actividad)dataGridViewAgenda.SelectedRows[0].DataBoundItem;

                // Eliminar la actividad de la lista
                listaActividades.Remove(actividadSeleccionada);

                // Actualizar el DataGridView
                ActualizarDataGridView();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormModifyUserAccess formAsignarRangos = new FormModifyUserAccess();
            formAsignarRangos.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btncambiocontraseñaform_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario de cambio de contraseña
            FormChangePassword changePasswordForm = new FormChangePassword();

            // Mostrar el formulario como un cuadro de diálogo modal
            changePasswordForm.ShowDialog();

            // Si deseas abrir el formulario sin bloquear el formulario principal, usa:
            // changePasswordForm.Show();
        }


        private void btnabrircrearusuario_Click(object sender, EventArgs e)
        {
            FormCreateUser createUserForm = new FormCreateUser();
            createUserForm.ShowDialog();
        }


        private void btnOpenDeleteUserForm_Click(object sender, EventArgs e)
        {
            FormDeleteUser deleteUserForm = new FormDeleteUser();
            deleteUserForm.Show();
        }

        private void btnAbrirAsignarRangosm_Click(object sender, EventArgs e)
        {
            // Crea una instancia del formulario de asignación de rangos
            FormModifyUserAccess formAsignarRangos = new FormModifyUserAccess();

            // Muestra el formulario
            formAsignarRangos.ShowDialog(); // ShowDialog() lo muestra de manera modal
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShowPayments_Click(object sender, EventArgs e)
        {
            FormCustomerPayments form = new FormCustomerPayments();
            form.ShowDialog();
        }

        private void btnExportSales2_Click(object sender, EventArgs e)
        {
            // Obtener las fechas seleccionadas por el usuario
            DateTime startDate = datePickerStart.Value.Date;
            DateTime endDate = datePickerEnd.Value.Date;

            // Validar que la fecha de inicio no sea mayor que la fecha de fin
            if (startDate > endDate)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.");
                return;
            }

            // Obtener los datos de la base de datos con el filtro de fechas (sale_item basado en rango de fechas de sale)
            DataTable salesData = GetSalesData(startDate, endDate);

            if (salesData.Rows.Count > 0)
            {
                // Exportar los datos a Excel
                SaveToExcel(salesData);
            }
            else
            {
                MessageBox.Show("No hay datos de ventas para exportar en el rango de fechas seleccionado.");
            }
        }

        private void btnExportAllSales_Click(object sender, EventArgs e)
        {
            // Obtener todos los datos de la base de datos de la tabla sale_item
            DataTable salesData = GetAllSalesData();

            if (salesData.Rows.Count > 0)
            {
                // Exportar los datos a Excel
                SaveToExcel(salesData);
            }
            else
            {
                MessageBox.Show("No hay datos de ventas para exportar.");
            }
        }

        private void SaveToExcel(DataTable dataTable)
        {
            // Limpiar los saltos de línea en la columna de Descripción (suponiendo que es la columna 6)
            foreach (DataRow row in dataTable.Rows)
            {
                if (dataTable.Columns.Contains("Descripción")) // Ajusta el nombre según el nombre de tu columna
                {
                    row["Descripción"] = row["Descripción"].ToString().Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\r", " ");
                }
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable, "Ventas");

                // Ajustar el formato de las columnas al contenido, pero limitar la columna de descripción
                worksheet.Columns().AdjustToContents(); // Ajustar el ancho de las columnas
                worksheet.Column(6).Width = 50; // Ajustar la columna de descripción a un ancho fijo

                // Desactivar el ajuste de texto en la columna de Descripción (suponiendo que es la columna 6)
                worksheet.Column(6).Style.Alignment.WrapText = false;

                // Opcional: establecer el formato de las cabeceras
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true; // Negrita en las cabeceras
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Centrar el texto en las cabeceras
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray; // Color de fondo para las cabeceras

                // Aplicar bordes a todas las celdas
                worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                    saveFileDialog.Title = "Guardar Ventas como Excel";
                    saveFileDialog.FileName = "Ventas.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Datos exportados correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                        }
                    }
                }
            }
        }






        private void lblPurchaseOrdersFilterWarning_Click(object sender, EventArgs e)
        {

        }
    }

}
