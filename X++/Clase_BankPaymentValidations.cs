
class BankPaymentValidations
{
 
   

    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    /// BPC USL 25/04/2019 BPC_FUN03_BanksVendor  CUZTOMITATION
    /// MAKES VALIDATIONS ON THE BANK ACCOUNT FIELD FOR THE LENGTH DEPENDS ON THE TYPE BANK PAYMENT FIELD SELECTION
    [PostHandlerFor(tableStr(VendTable), tableMethodStr(VendTable, validateWrite))]
    public static void VendTable_Post_validateWrite(XppPrePostArgs args)
    {
      
     
        VendTable  vendTable = args.getThis() as VendTable;
        VendBankAccount vendbankaccount;
        //VendBankAccount vendbankaccount;
       
     

      
        //VendTable  vendTable ;   int  lenAcc=   strLen(vendTable.BankAccount);
       
        select firstonly vendTable where vendTable.BankAccount == vendbankaccount.AccountID && vendTable.BankAccount == vendbankaccount.AccountNum ;
       
        int typePayment =  vendTable.EnumBankPayment;
        int  lenAcc=   strLen(vendbankaccount.AccountNum);
        boolean setvaluesAccnum = isInteger(vendbankaccount.AccountNum);
      
       // if(lenAcc == 0 ) { info("@BPC:FUN03ErrorLen0");}

     

       

        switch(typePayment)
        {
        
            case EnumBankPayment::NationalType:
               if(setvaluesAccnum)
                {
                    if(lenAcc > 18 )
                    {
                        warning("@BPC:FUN03ErrorLen18");
                    }
                }
                else
                {
                    warning("@BPC:FUN03ErrorOfNum");
                }
       
            break;
            case EnumBankPayment::CIEType:
               if(lenAcc > 7 )
                {
                    warning("@BPC:FUN03ErrorLen7");
                }
            break;
            case EnumBankPayment::InternationalType:

         /*       select firstonly forupdate  vendbankaccount where  vendbankaccount.VendAccount == vendTable.AccountNum;
                ttsbegin;
                vendbankaccount.IsInternationalAcc=1;
                vendbankaccount.doUpdate();//   update();
                ttscommit;  */
            break;
            default:
            break;
        }
        
}

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// BPC USL 25/04/2019 BPC_FUN03_BanksVendor  CUZTOMITATION
    /// MAKES MANDATORY THE BANK ACCOUNT FIELD TO ADD A NEW CUSTOMER 
    //[FormDataSourceEventHandler(formDataSourceStr(VendTable, VendTable), FormDataSourceEventType::ValidatingWrite)]
    //public static void VendTable_OnValidatingWrite(FormDataSource sender, FormDataSourceEventArgs e)
    //{
    //    sender.object(fieldNum(VendTable,BankAccount)).mandatory(true);
    //}

}