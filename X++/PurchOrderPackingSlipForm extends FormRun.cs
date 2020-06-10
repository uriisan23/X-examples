
     [Form]
public class PurchOrderPackingSlipForm extends FormRun
{
    /// <summary>
    ///
    /// </summary>
    public void init()
    {
        PurchOrderPackingSlipTmp tmpTable;

        super();

        if(element.args() && element.args().parmObject())
        {
            tmpTable = this.fillTmpTable();

            PurchOrderPackingSlipTableTmp.setTmpData(tmpTable);
        }
    }

    private PurchOrderPackingSlipTmp fillTmpTable()
    {
        PurchTable purchTable;
        PurchLine purchLine;
        VendPackingSlipTrans vendPackingSlipTrans;
        VendInvoiceJour vendInvoiceJour;

        InventTable inventTable;
        EcoResProduct ecoResProduct;

        PurchOrderPackingSlipTmp purchPackingSlipTmp;
        
        QueryRun queryRun = element.args().parmObject();

        //RecordInsertList recordInsertList = new RecordInsertList(tableNum(PurchOrderPackingSlipTmp));
        
        //ttsbegin;

        purchPackingSlipTmp.clear();

        while(queryRun.next())
        {
            purchTable = queryRun.get(tableNum(PurchTable));
            purchLine = queryRun.get(tableNum(PurchLine));
            vendPackingSlipTrans = queryRun.get(tableNum(VendPackingSlipTrans));
            vendInvoiceJour = VendInvoiceJour::findFromPurchId(purchTable.PurchId);
            inventTable = InventTable::find(purchLine.ItemId);
            ecoResProduct = inventTable.Product();

            purchPackingSlipTmp.PurchId = purchTable.PurchId;
            purchPackingSlipTmp.PackingSlipId = vendPackingSlipTrans.PackingSlipId;
            purchPackingSlipTmp.InvoiceId = vendInvoiceJour.InvoiceId;
            purchPackingSlipTmp.PurchStatus = purchTable.PurchStatus;
            purchPackingSlipTmp.VendAccount = purchTable.OrderAccount;
            purchPackingSlipTmp.CreationDate = DateTimeUtil::date(purchTable.CreatedDateTime);
            purchPackingSlipTmp.LineNum = purchLine.LineNumber;
            purchPackingSlipTmp.ItemId = purchLine.ItemId;
            purchPackingSlipTmp.Description = EcoResProductTranslation::findByProductLanguage(ecoResProduct.RecId, xUserInfo::find().language).Description;
            purchPackingSlipTmp.Qty = purchLine.PurchQty;
            purchPackingSlipTmp.CurrencyCode = purchTable.CurrencyCode;
            purchPackingSlipTmp.ExchRate = vendInvoiceJour.ExchRate;
            purchPackingSlipTmp.PurchPrice = purchLine.PurchPrice;
            purchPackingSlipTmp.Amount = (purchLine.PurchQty * purchLine.PurchPrice) - purchLine.totalDiscountAmount();
            purchPackingSlipTmp.Name = purchLine.Name;
            purchPackingSlipTmp.DebitMainAccount = "";
            purchPackingSlipTmp.CreditMainAccount = "";
            purchPackingSlipTmp.PedimentNumber = "";
            purchPackingSlipTmp.insert();
            //recordInsertList.add(purchPackingSlipTmp);
        }

        //recordInsertList.insertDatabase();

        //ttscommit;

        return purchPackingSlipTmp;
    }

}