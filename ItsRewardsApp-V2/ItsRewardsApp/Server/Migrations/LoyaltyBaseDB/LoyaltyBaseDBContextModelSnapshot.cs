﻿// <auto-generated />
using System;
using ItsRewardsApp.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ItsRewardsApp.Server.Migrations.LoyaltyBaseDB
{
    [DbContext(typeof(LoyaltyBaseDBContext))]
    partial class LoyaltyBaseDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ItsRewardsApp.Shared.Models.StoreTbl", b =>
                {
                    b.Property<int>("StoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreID"), 1L, 1);

                    b.Property<string>("AcctgSprdsheets")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Active")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("AllowableVariance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ApplicationTypeId")
                        .HasColumnType("int");

                    b.Property<string>("AreaCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookWorksAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("CCProcessingPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CellPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CheckCompetitors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClosedOnSunday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CokeCustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ComCashLastInvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ComCashLastInvoiceNo")
                        .HasColumnType("int");

                    b.Property<short?>("ComCashLocationNo")
                        .HasColumnType("smallint");

                    b.Property<string>("ComPort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComputerPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComputerUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ConfigDownloadedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ConfigDownloadedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ConfigDownloadedDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConfigDownloadedNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ConfigDownloadedScheduledBy")
                        .HasColumnType("int");

                    b.Property<int?>("ConnectivityBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ConnectivityDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ConnectivityDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConnectivityNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ConnectivityScheduledBy")
                        .HasColumnType("int");

                    b.Property<string>("ContactEmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConversionEMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerTrainingBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CustomerTrainingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CustomerTrainingDateDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerTrainingDateNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerTrainingDateScheduledBy")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerTrainingScheduledBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CustomerTrainingScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CustomerTrainingScheduledDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerTrainingScheduledNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerTrainingScheduledScheduledBy")
                        .HasColumnType("int");

                    b.Property<string>("DataServer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DatabaseID")
                        .HasColumnType("int");

                    b.Property<int?>("DatabaseSetupBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DatabaseSetupDAteScheduled")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatabaseSetupDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DatabaseSetupNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DatabaseSetupScheduledBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateEntered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("DefaultEDIServer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DefaultEDIVendorID")
                        .HasColumnType("int");

                    b.Property<string>("DefaultRemoteServer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeleteRow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayFritosForm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EDIMailBoxSetupBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EDIMailBoxSetupDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EDIMailBoxSetupDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("EDIMailBoxSetupNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EDIMailBoxSetupScheduledBy")
                        .HasColumnType("int");

                    b.Property<string>("EMail1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMail2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMail3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMailForCommunications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EPB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EPB_IRI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeID_ModifiedBy")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId_EnteredBy")
                        .HasColumnType("int");

                    b.Property<string>("EnhancedHandheld")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EquipmentShippedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EquipmentShippedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EquipmentShippedDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("EquipmentShippedNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EquipmentShippedScheduledBy")
                        .HasColumnType("int");

                    b.Property<string>("ExternalIPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FintechCustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstLoyaltyCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstMagneticStripNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("FuelEPAAllowableVariance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("GetConfig")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetEJFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetInvoices")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetLogFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetNewPOSDCVer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetNewVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetPLUTOTFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetPLUs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetSales")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetSalesOnce")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetSales_Base")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GetSales_Original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GilbarcoCustomer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HackneyCustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HandheldAnswer")
                        .HasColumnType("int");

                    b.Property<DateTime?>("HandheldLastGenerate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HandheldLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HandheldSeed")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAddressInternal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAddressUpdated")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPPort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InProcess")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InProcessNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceInput")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvoicesCheckedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InvoicesCheckedByDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoicesCheckedByNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvoicesCheckedByScheduledBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InvoicesCheckedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsCommander")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ItemChangedAndSentBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ItemChangedAndSentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ItemChangedAndSentDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("ItemChangedAndSentNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ItemChangedAndSentScheduledBy")
                        .HasColumnType("int");

                    b.Property<int?>("LEID")
                        .HasColumnType("int");

                    b.Property<string>("LMIAccessCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LMIEMAil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LMIPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastLoyaltyCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastMagneticStripNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastPOFileSent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastPriceChange")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastSalesDownload")
                        .HasColumnType("datetime2");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocalServerID")
                        .HasColumnType("int");

                    b.Property<string>("LocalSynch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogInNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("LotteryCommission")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Loyalty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoyaltyEMailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoyaltyHouseAccounts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxItemsToUpload")
                        .HasColumnType("int");

                    b.Property<string>("McConnellCustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MealTaxString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MinutesToWaitPer250Records")
                        .HasColumnType("int");

                    b.Property<string>("MovingStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NaxmlVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetworkPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetworkUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nielsen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nielsen2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NielsenSlotNumber")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoveltyCustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NucleusVersionID")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfShifts")
                        .HasColumnType("int");

                    b.Property<string>("OriginalDefaultEDIServer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalGetSales")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalSendData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutboundConnection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OutputID")
                        .HasColumnType("int");

                    b.Property<string>("PLUNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PLURows")
                        .HasColumnType("int");

                    b.Property<string>("POBillToAddress1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POBillToAddress2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POBillToCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POBillToName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POBillToState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POBillToZip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POShipToAddress1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POShipToAddress2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POShipToCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POShipToName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POShipToState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POShipToZip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartialRollout")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportTaxBasis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportXMLVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhysicalEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Prefix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriceBookID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PriceCheckCycleStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceCheckLookedUpSKUCount")
                        .HasColumnType("int");

                    b.Property<int>("PriceCheckType")
                        .HasColumnType("int");

                    b.Property<string>("ProcessOldSalesFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessSalesFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReStartSE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReStartSELastDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReactivateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReadOnlyPromotions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegisterIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RegisterModelID")
                        .HasColumnType("int");

                    b.Property<string>("RegisterPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegisterUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportServer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RequestedPeriodNumber")
                        .HasColumnType("int");

                    b.Property<int?>("RequestedPeriodNumberForIRI")
                        .HasColumnType("int");

                    b.Property<int?>("RequestedPeriodNumberToCollect")
                        .HasColumnType("int");

                    b.Property<string>("ResetModifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestartSynchEngine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouterIPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouterModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouterPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouterUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SMSEMailAddressID")
                        .HasColumnType("int");

                    b.Property<int?>("SalesImportedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SalesImportedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SalesImportedDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("SalesImportedNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SalesImportedScheduledBy")
                        .HasColumnType("int");

                    b.Property<string>("SalesNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SalesTaxString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sam4ParentStoreID")
                        .HasColumnType("int");

                    b.Property<string>("SamsungMachineID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short?>("SapphireNumberOfShifts")
                        .HasColumnType("smallint");

                    b.Property<string>("SendCheckDigitToPOS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SendCompressedUPCToPOS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SendData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SendData_Original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SendSalesFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShowTips1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SiteReadyForTrainingBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SiteReadyForTrainingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SiteReadyForTrainingDateDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("SiteReadyForTrainingDateNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SiteReadyForTrainingDateScheduledBy")
                        .HasColumnType("int");

                    b.Property<string>("SoftwareSet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoftwareSetID")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StationNo")
                        .HasColumnType("int");

                    b.Property<string>("StoreBlurbForCoupons")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StoreTypeID")
                        .HasColumnType("int");

                    b.Property<string>("StoreWebSiteAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Suffix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TankReadingControlField")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrulyActiveIETest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateFuelInventoryCost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateInventory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateOnHandOnInvoiceReceipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateRetailPricesOnInvoiceReceipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateVendorCostOnInvoiceReceipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseBlackPipe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseLinkedItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsePassportFuelDiscounts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseSSL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseSapphire14DigitSKUs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseSapphireExpandedDeal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseSapphireNAXML")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseShiftSales")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseStorePrices")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VelocityReportsImportedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VelocityReportsImportedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("VelocityReportsImportedDateScheduled")
                        .HasColumnType("datetime2");

                    b.Property<string>("VelocityReportsImportedNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VelocityReportsImportedScheduledBy")
                        .HasColumnType("int");

                    b.Property<string>("VendorAcctNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorCustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VendorID")
                        .HasColumnType("int");

                    b.Property<string>("VendorLocationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WarehouseEmployeeID")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("WhenChanged")
                        .HasColumnType("datetime2");

                    b.Property<string>("WhoChanged")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("osversion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("qbClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("qbCompany")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("qbSalesTaxAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sendpromotion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreID");

                    b.ToTable("tblstore");
                });

            modelBuilder.Entity("ItsRewardsApp.Shared.Models.TobaccoRebate", b =>
                {
                    b.Property<int>("RebateTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RebateTypeID"), 1L, 1);

                    b.Property<string>("Active")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaAllOtherManCig")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AltriaBatchType")
                        .HasColumnType("int");

                    b.Property<string>("AltriaChainNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaPMUSA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaSubmission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaUSSTC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltriaUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Altria_Client_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Altria_Client_Secret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChainName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cigar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cigs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DataBaseID")
                        .HasColumnType("int");

                    b.Property<string>("DayClosed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EVape")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsAltriaApproved")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsAltriaChain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsRJRApproved")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastAltriaEMailDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastAltriaFileSent")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastLateSalesEMailDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastRJREMailDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastRJRFileSent")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OutletSequence")
                        .HasColumnType("int");

                    b.Property<string>("PromotionUpdateScript")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RCN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRPortalPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRPortalUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRStoreAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRSubmission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RJRUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RegisterID")
                        .HasColumnType("int");

                    b.Property<string>("SendEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Smokeless")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreID")
                        .HasColumnType("int");

                    b.Property<string>("Tier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("coupons")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("iriemail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loyalty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("marlboromultipack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("msaemail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("promotiontext")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("rjrsubmitiondate")
                        .HasColumnType("datetime2");

                    b.HasKey("RebateTypeID");

                    b.ToTable("tblTobaccoRebateProgramType");
                });
#pragma warning restore 612, 618
        }
    }
}
