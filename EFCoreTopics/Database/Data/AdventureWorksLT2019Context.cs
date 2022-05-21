using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EFCoreTopics.Database.Models;

namespace EFCoreTopics.Database.Data
{
    public partial class AdventureWorksLContext : DbContext
    {
        public AdventureWorksLContext()
        {
        }

        public AdventureWorksLContext(DbContextOptions<AdventureWorksLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<BuildVersion> BuildVersions { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductDescription> ProductDescriptions { get; set; } = null!;
        public virtual DbSet<ProductModel> ProductModels { get; set; } = null!;
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; } = null!;
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; } = null!;
        public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; } = null!;
        public virtual DbSet<VGetAllCategory> VGetAllCategories { get; set; } = null!;
        public virtual DbSet<VProductAndDescription> VProductAndDescriptions { get; set; } = null!;
        public virtual DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=AdventureWorksLT2019;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "SalesLT");

                entity.HasComment("Street address information for customers.");

                entity.HasIndex(e => e.Rowguid, "AK_Address_rowguid")
                    .IsUnique();

                entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion }, "IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion");

                entity.HasIndex(e => e.StateProvince, "IX_Address_StateProvince");

                entity.Property(e => e.AddressId)
                    .HasColumnName("AddressID")
                    .HasComment("Primary key for Address records.");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(60)
                    .HasComment("First street address line.");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(60)
                    .HasComment("Second street address line.");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .HasComment("Name of the city.");

                entity.Property(e => e.CountryRegion).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(15)
                    .HasComment("Postal code for the street address.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.StateProvince)
                    .HasMaxLength(50)
                    .HasComment("Name of state or province.");

                #region Computed Column

                entity.Property(e => e.SearchTerm).HasComputedColumnSql("[City]+','+[PostalCode]");

                #endregion

            });

            modelBuilder.Entity<BuildVersion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BuildVersion");

                entity.HasComment("Current version number of the AdventureWorksLT 2012 sample database. ");

                entity.Property(e => e.DatabaseVersion)
                    .HasMaxLength(25)
                    .HasColumnName("Database Version")
                    .HasComment("Version number of the database in 9.yy.mm.dd.00 format.");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.SystemInformationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SystemInformationID")
                    .HasComment("Primary key for BuildVersion records.");

                entity.Property(e => e.VersionDate)
                    .HasColumnType("datetime")
                    .HasComment("Date and time the record was last updated.");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "SalesLT");

                entity.HasComment("Customer information.");

                entity.HasIndex(e => e.Rowguid, "AK_Customer_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.EmailAddress, "IX_Customer_EmailAddress");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasComment("Primary key for Customer records.");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(128)
                    .HasComment("The customer's organization.");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .HasComment("E-mail address for the person.");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasComment("First name of the person.");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasComment("Last name of the person.");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .HasComment("Middle name or middle initial of the person.");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.NameStyle).HasComment("0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Password for the e-mail account.");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Random value concatenated with the password string before the password is hashed.");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasComment("Phone number associated with the person.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.SalesPerson)
                    .HasMaxLength(256)
                    .HasComment("The customer's sales person, an employee of AdventureWorks Cycles.");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(10)
                    .HasComment("Surname suffix. For example, Sr. or Jr.");

                entity.Property(e => e.Title)
                    .HasMaxLength(8)
                    .HasComment("A courtesy title. For example, Mr. or Ms.");
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.AddressId })
                    .HasName("PK_CustomerAddress_CustomerID_AddressID");

                entity.ToTable("CustomerAddress", "SalesLT");

                entity.HasComment("Cross-reference table mapping customers to their address(es).");

                entity.HasIndex(e => e.Rowguid, "AK_CustomerAddress_rowguid")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasComment("Primary key. Foreign key to Customer.CustomerID.");

                entity.Property(e => e.AddressId)
                    .HasColumnName("AddressID")
                    .HasComment("Primary key. Foreign key to Address.AddressID.");

                entity.Property(e => e.AddressType)
                    .HasMaxLength(50)
                    .HasComment("The kind of Address. One of: Archive, Billing, Home, Main Office, Primary, Shipping");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog");

                entity.HasComment("Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct. Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct.");

                entity.Property(e => e.ErrorLogId)
                    .HasColumnName("ErrorLogID")
                    .HasComment("Primary key for ErrorLog records.");

                entity.Property(e => e.ErrorLine).HasComment("The line number at which the error occurred.");

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(4000)
                    .HasComment("The message text of the error that occurred.");

                entity.Property(e => e.ErrorNumber).HasComment("The error number of the error that occurred.");

                entity.Property(e => e.ErrorProcedure)
                    .HasMaxLength(126)
                    .HasComment("The name of the stored procedure or trigger where the error occurred.");

                entity.Property(e => e.ErrorSeverity).HasComment("The severity of the error that occurred.");

                entity.Property(e => e.ErrorState).HasComment("The state number of the error that occurred.");

                entity.Property(e => e.ErrorTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The date and time at which the error occurred.");

                entity.Property(e => e.UserName)
                    .HasMaxLength(128)
                    .HasComment("The user who executed the batch in which the error occurred.");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "SalesLT");

                entity.HasComment("Products sold or used in the manfacturing of sold products.");

                entity.HasIndex(e => e.Name, "AK_Product_Name")
                    .IsUnique();

                entity.HasIndex(e => e.ProductNumber, "AK_Product_ProductNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid, "AK_Product_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("Primary key for Product records.");

                entity.Property(e => e.Color)
                    .HasMaxLength(15)
                    .HasComment("Product color.");

                entity.Property(e => e.DiscontinuedDate)
                    .HasColumnType("datetime")
                    .HasComment("Date the product was discontinued.");

                entity.Property(e => e.ListPrice)
                    .HasColumnType("money")
                    .HasComment("Selling price.");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("Name of the product.");

                entity.Property(e => e.ProductCategoryId)
                    .HasColumnName("ProductCategoryID")
                    .HasComment("Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. ");

                entity.Property(e => e.ProductModelId)
                    .HasColumnName("ProductModelID")
                    .HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.");

                entity.Property(e => e.ProductNumber)
                    .HasMaxLength(25)
                    .HasComment("Unique product identification number.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.SellEndDate)
                    .HasColumnType("datetime")
                    .HasComment("Date the product was no longer available for sale.");

                entity.Property(e => e.SellStartDate)
                    .HasColumnType("datetime")
                    .HasComment("Date the product was available for sale.");

                entity.Property(e => e.Size)
                    .HasMaxLength(5)
                    .HasComment("Product size.");

                entity.Property(e => e.StandardCost)
                    .HasColumnType("money")
                    .HasComment("Standard cost of the product.");

                entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");

                entity.Property(e => e.ThumbnailPhotoFileName)
                    .HasMaxLength(50)
                    .HasComment("Small image file name.");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(8, 2)")
                    .HasComment("Product weight.");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductModelId);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "SalesLT");

                entity.HasComment("High-level product categorization.");

                entity.HasIndex(e => e.Name, "AK_ProductCategory_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid, "AK_ProductCategory_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductCategoryId)
                    .HasColumnName("ProductCategoryID")
                    .HasComment("Primary key for ProductCategory records.");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("Category description.");

                entity.Property(e => e.ParentProductCategoryId)
                    .HasColumnName("ParentProductCategoryID")
                    .HasComment("Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.HasOne(d => d.ParentProductCategory)
                    .WithMany(p => p.InverseParentProductCategory)
                    .HasForeignKey(d => d.ParentProductCategoryId)
                    .HasConstraintName("FK_ProductCategory_ProductCategory_ParentProductCategoryID_ProductCategoryID");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription", "SalesLT");

                entity.HasComment("Product descriptions in several languages.");

                entity.HasIndex(e => e.Rowguid, "AK_ProductDescription_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductDescriptionId)
                    .HasColumnName("ProductDescriptionID")
                    .HasComment("Primary key for ProductDescription records.");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasComment("Description of the product.");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("ProductModel", "SalesLT");

                entity.HasIndex(e => e.Name, "AK_ProductModel_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid, "AK_ProductModel_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.CatalogDescription, "PXML_ProductModel_CatalogDescription");

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

                entity.Property(e => e.CatalogDescription).HasColumnType("xml");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ProductModelProductDescription>(entity =>
            {
                entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture })
                    .HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

                entity.ToTable("ProductModelProductDescription", "SalesLT");

                entity.HasComment("Cross-reference table mapping product descriptions and the language the description is written in.");

                entity.HasIndex(e => e.Rowguid, "AK_ProductModelProductDescription_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductModelId)
                    .HasColumnName("ProductModelID")
                    .HasComment("Primary key. Foreign key to ProductModel.ProductModelID.");

                entity.Property(e => e.ProductDescriptionId)
                    .HasColumnName("ProductDescriptionID")
                    .HasComment("Primary key. Foreign key to ProductDescription.ProductDescriptionID.");

                entity.Property(e => e.Culture)
                    .HasMaxLength(6)
                    .IsFixedLength()
                    .HasComment("The culture for which the description is written");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ProductDescription)
                    .WithMany(p => p.ProductModelProductDescriptions)
                    .HasForeignKey(d => d.ProductDescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.ProductModelProductDescriptions)
                    .HasForeignKey(d => d.ProductModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId })
                    .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

                entity.ToTable("SalesOrderDetail", "SalesLT");

                entity.HasComment("Individual products associated with a specific sales order. See SalesOrderHeader.");

                entity.HasIndex(e => e.Rowguid, "AK_SalesOrderDetail_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.ProductId, "IX_SalesOrderDetail_ProductID");

                entity.Property(e => e.SalesOrderId)
                    .HasColumnName("SalesOrderID")
                    .HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

                entity.Property(e => e.SalesOrderDetailId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SalesOrderDetailID")
                    .HasComment("Primary key. One incremental unique number per product sold.");

                entity.Property(e => e.LineTotal)
                    .HasColumnType("numeric(38, 6)")
                    .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", false)
                    .HasComment("Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.OrderQty).HasComment("Quantity ordered per product.");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("Product sold to customer. Foreign key to Product.ProductID.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasComment("Selling price of a single product.");

                entity.Property(e => e.UnitPriceDiscount)
                    .HasColumnType("money")
                    .HasComment("Discount amount.");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.SalesOrderDetails)
                    .HasForeignKey(d => d.SalesOrderId);
            });

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {
                entity.HasKey(e => e.SalesOrderId)
                    .HasName("PK_SalesOrderHeader_SalesOrderID");

                entity.ToTable("SalesOrderHeader", "SalesLT");

                entity.HasComment("General sales order information.");

                entity.HasIndex(e => e.SalesOrderNumber, "AK_SalesOrderHeader_SalesOrderNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid, "AK_SalesOrderHeader_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerId, "IX_SalesOrderHeader_CustomerID");

                entity.Property(e => e.SalesOrderId)
                    .HasColumnName("SalesOrderID")
                    .HasComment("Primary key.");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(15)
                    .HasComment("Financial accounting number reference.");

                entity.Property(e => e.BillToAddressId)
                    .HasColumnName("BillToAddressID")
                    .HasComment("The ID of the location to send invoices.  Foreign key to the Address table.");

                entity.Property(e => e.Comment).HasComment("Sales representative comments.");

                entity.Property(e => e.CreditCardApprovalCode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Approval code provided by the credit card company.");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasComment("Customer identification number. Foreign key to Customer.CustomerID.");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasComment("Date the order is due to the customer.");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Shipping cost.");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.OnlineOrderFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Dates the sales order was created.");

                entity.Property(e => e.PurchaseOrderNumber)
                    .HasMaxLength(25)
                    .HasComment("Customer purchase order number reference. ");

                entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.Property(e => e.SalesOrderNumber)
                    .HasMaxLength(25)
                    .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
                    .HasComment("Unique sales order identification number.");

                entity.Property(e => e.ShipDate)
                    .HasColumnType("datetime")
                    .HasComment("Date the order was shipped to the customer.");

                entity.Property(e => e.ShipMethod)
                    .HasMaxLength(50)
                    .HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");

                entity.Property(e => e.ShipToAddressId)
                    .HasColumnName("ShipToAddressID")
                    .HasComment("The ID of the location to send goods.  Foreign key to the Address table.");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");

                entity.Property(e => e.TaxAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Tax amount.");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false)
                    .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.");

                entity.HasOne(d => d.BillToAddress)
                    .WithMany(p => p.SalesOrderHeaderBillToAddresses)
                    .HasForeignKey(d => d.BillToAddressId)
                    .HasConstraintName("FK_SalesOrderHeader_Address_BillTo_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrderHeaders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ShipToAddress)
                    .WithMany(p => p.SalesOrderHeaderShipToAddresses)
                    .HasForeignKey(d => d.ShipToAddressId)
                    .HasConstraintName("FK_SalesOrderHeader_Address_ShipTo_AddressID");
            });

            modelBuilder.Entity<VGetAllCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vGetAllCategories", "SalesLT");

                entity.Property(e => e.ParentProductCategoryName).HasMaxLength(50);

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<VProductAndDescription>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vProductAndDescription", "SalesLT");

                entity.HasComment("Product names and descriptions. Product descriptions are provided in multiple languages.");

                entity.Property(e => e.Culture)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductModel).HasMaxLength(50);
            });

            modelBuilder.Entity<VProductModelCatalogDescription>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vProductModelCatalogDescription", "SalesLT");

                entity.HasComment("Displays the content from each element in the xml column CatalogDescription for each product in the Sales.ProductModel table that has catalog data.");

                entity.Property(e => e.Color).HasMaxLength(256);

                entity.Property(e => e.Copyright).HasMaxLength(30);

                entity.Property(e => e.Crankset).HasMaxLength(256);

                entity.Property(e => e.MaintenanceDescription).HasMaxLength(256);

                entity.Property(e => e.Material).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NoOfYears).HasMaxLength(256);

                entity.Property(e => e.Pedal).HasMaxLength(256);

                entity.Property(e => e.PictureAngle).HasMaxLength(256);

                entity.Property(e => e.PictureSize).HasMaxLength(256);

                entity.Property(e => e.ProductLine).HasMaxLength(256);

                entity.Property(e => e.ProductModelId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ProductModelID");

                entity.Property(e => e.ProductPhotoId)
                    .HasMaxLength(256)
                    .HasColumnName("ProductPhotoID");

                entity.Property(e => e.ProductUrl)
                    .HasMaxLength(256)
                    .HasColumnName("ProductURL");

                entity.Property(e => e.RiderExperience).HasMaxLength(1024);

                entity.Property(e => e.Rowguid).HasColumnName("rowguid");

                entity.Property(e => e.Saddle).HasMaxLength(256);

                entity.Property(e => e.Style).HasMaxLength(256);

                entity.Property(e => e.WarrantyDescription).HasMaxLength(256);

                entity.Property(e => e.WarrantyPeriod).HasMaxLength(256);

                entity.Property(e => e.Wheel).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #region RawSqlQueries

        public async Task<List<Address>> GetAddressNonInterpolatedAsync(int id)
            =>
                await this.Addresses.FromSqlRaw("Select * From [SalesLT].[Address] where [AddressID]>{0}", id)
                    .ToListAsync();

        public async Task<List<Address>> GetAddressInterpolatedAsync(int id) => await this.Addresses
            .FromSqlInterpolated($"Select * From [SalesLT].[Address] where [AddressID]>{id}").ToListAsync();

        #endregion

    }
}
