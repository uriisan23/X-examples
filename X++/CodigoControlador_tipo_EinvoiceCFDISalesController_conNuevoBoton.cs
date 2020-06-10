

 public class EInvoiceController_PackingList extends EInvoiceControllerBase_MX
{
    CustInvoiceJour custInvoiceJour;
    EInvoiceJour_MX   eInvoiceJour_MX;

    /// <summary>
    /// Initializes the list of journals to be printed, from the arguments.
    /// </summary>
    protected void initJournalList()
    {
        if (this.parmArgs().dataset() == tableNum(WMSShipment))
        {
            WMSShipment wmsShipment = args.record();
            journalList = FormLetter::createJournalListCopy(ShipCarrierShipmentInvoice::custInvoiceJour(wmsShipment.ShipmentId));
        }
        else
        {
            super();
        }
    }

    /// <summary>
    /// Gets the document type for the print management.
    /// </summary>
    /// <returns>A <c>PrintMgmtDocumentType</c> option.</returns>
    protected PrintMgmtDocumentType getPrintMgmtDocumentType()
    {
        return PrintMgmtDocumentType::SalesOrderInvoice;
    }

    /// <summary>
    /// Gets the current invoice to be printed.
    /// </summary>
    /// <returns>A record of the transaction to be printed.</returns>
    protected Common getInvoiceJour()
    {
        return custInvoiceJour;
    }

    /// <summary>
    /// Loads the first invoice to be printed in invoiceJour.
    /// </summary>
    /// <returns>True if there are one invoice and it was load.</returns>
    protected boolean first()
    {
        return journalList.first(custInvoiceJour);
    }

    /// <summary>
    /// Loads the next invoice to be printed in invoiceJour.
    /// </summary>
    /// <returns>True if there are one more invoice and it was load.</returns>
    protected boolean next()
    {
        return journalList.next(custInvoiceJour);
    }

    /// <summary>
    /// Returns the language id of the invoice transaction that is being printed.
    /// </summary>
    /// <returns>the language id.</returns>
    protected LanguageId getInvoiceLanguageId()
    {
        return custInvoiceJour.LanguageId; 
    }

    /// <summary>
    /// Defines if the invoice to be printed is CFDI or CFD.
    /// </summary>
    /// <returns>True if the invoice is a CFDI, false if is CFD.</returns>
    protected boolean isCFDI()
    {
        return true;
    }

    protected Common getPrintMgmtReferencedTable()
    {
        Common  printMgmtReferencedTable;

        // Determine where to start looking for Print Mgmt settings
        if (SalesParameters::find().DeleteHeading == NoYes::Yes)
        {
            // The original SalesTable record no longer exists, so skip that Print Mgmt level
            // and start with the customer instead.
            printMgmtReferencedTable = custInvoiceJour.custTable_InvoiceAccount();

            if (printMgmtReferencedTable == null)
            {
                printMgmtReferencedTable = custInvoiceJour.custTable_OrderAccount();
            }
        }
        else
        {                                   
            printMgmtReferencedTable = custInvoiceJour.salesTable();
        }

        return printMgmtReferencedTable;
    }

    protected void preRunModifyContract()
    {
        EInvoiceCFDIReportContract_MX contract;
        contract = this.parmReportContract().parmRdpContract() as EInvoiceCFDIReportContract_MX;
        contract.parmRecordId(this.getEInvoiceJour().RecId);
      
        this.parmReportName("BPC_EinvoiceCFDIReport_MX.BPC_PackingListReport");
        

    }

    protected EInvoiceJour_MX getEInvoiceJour()
    {
        return EInvoiceJour_MX::findByRef(this.getInvoiceJour().TableId, this.getInvoiceJour().RecId);
    }

    protected void outputReport()
    {
        EInvoiceJourBaseMap_MX eInvoiceJourMapping;
       SRSPrintDestinationSettings printerSettings = formLetterReport.getCurrentPrintSetting().parmPrintJobSettings();

         eInvoiceJour_MX = this.getEInvoiceJour();
        eInvoiceJourMapping = EInvoiceJourBaseMap_MX::construct(eInvoiceJour_MX);

        if (sendMailCalled)
        {
            printerSettings.parmPrintToArchive(true);
            printerSettings.printMediumType(SRSPrintMediumType::Archive);
        }

        formLetterReport.getCurrentPrintSetting().parmReportFormatName(this.parmReportName());

        super();

        this.sendPDFEmail(eInvoiceJourMapping, printerSettings);
    }

    /// <summary>
    /// Displays the <c>EInvoiceReport_MX</c> SRS Report.
    /// </summary>
    /// <param name="_args">Args object.</param>


    public static void main(Args _args)
    {
        EInvoiceController_PackingList controller = new EInvoiceController_PackingList();

        
        controller.initialize(_args);
        controller.getInvoiceJour();
        controller.startOperation();

        

    }

}




