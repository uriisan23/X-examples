 
[FormDataSourceEventHandler(formDataSourceStr(VendBankAccounts, VendBankAccount), FormDataSourceEventType::ValidatingWrite)]
    public static void VendBankAccount_OnValidatingWrite(FormDataSource sender, FormDataSourceEventArgs e)
    {
        VendBankAccount   vendbankAccount = sender.cursor();
        VendTable  vendTable;

        int InternationalEnabled = vendBankAccount.EnumBankPayment;

        if (InternationalEnabled == 2 )
        {
           
            sender.object(fieldNum(VendBankAccount, CurrencyCode)).mandatory(true);
            sender.object(fieldNum(VendBankAccount, SWIFTNo)).mandatory(true);
            sender.object(fieldNum(VendBankAccount, BankIBAN)).mandatory(true);
            sender.object(fieldNum(VendBankAccount, Location)).mandatory(true);
           
        }
        else
        {
            sender.object(fieldNum(VendBankAccount, CurrencyCode)).mandatory(false);
            sender.object(fieldNum(VendBankAccount, SWIFTNo)).mandatory(false);
            sender.object(fieldNum(VendBankAccount, BankIBAN)).mandatory(false);
            sender.object(fieldNum(VendBankAccount, Location)).mandatory(false);

        }
    }

          /// <summary>
          ///
          /// </summary>
          /// <param name="args"></param>
          [PostHandlerFor(tableStr(VendBankAccount), tableMethodStr(VendBankAccount, validateWrite))]
          public static void VendBankAccount_Post_validateWrite(XppPrePostArgs args)
          {
                    
              VendBankAccount   vendbankAccount = args.getThis() as VendBankAccount;
              VendTable  vendTable;



          int typePayment =  vendbankAccount.EnumBankPayment;
          int  lenAcc=   strLen(vendbankaccount.AccountNum);
          boolean setvaluesAccnum = isInteger(vendbankaccount.AccountNum);
 
          switch(typePayment)
           {
             case EnumBankPayment::NationalType:
                  if(setvaluesAccnum)
                  {
                      if(lenAcc > 18 )
                        {
                          throw error ("@BPC:FUN03ErrorLen18");
                        }
                  }
                  else
                  {
                      throw error("@BPC:FUN03ErrorOfNum");
                  }

                                  
              break;
              case EnumBankPayment::CIEType:
                   if(lenAcc > 7 )
                   {
                       throw error("@BPC:FUN03ErrorLen7");
                   }
              break;
              case EnumBankPayment::InternationalType:

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
    [DataEventHandler(tableStr(VendBankAccount), DataEventType::Inserted)]
    public static void VendBankAccount_onInserted(Common sender, DataEventArgs e)
    {
        VendBankAccount   vendbankAccount = sender.cursor();
        VendTable  vendTable;

        select firstonly forupdate  vendTable where  vendTable.AccountNum == vendbankaccount.VendAccount &&  vendTable.BankAccount == vendbankaccount.AccountID;


             int typePayment =  vendbankAccount.EnumBankPayment;

        switch(typePayment)
        {
              case  EnumBankPayment::NationalType:
                ttsbegin;
                vendTable.EnumBankPayment=0;
                vendTable.doUpdate();//   update();
                ttscommit;  
              break;
              case  EnumBankPayment::CIEType:
                ttsbegin;
                vendTable.EnumBankPayment=1;
                vendTable.doUpdate();//   update();
                ttscommit;
              case EnumBankPayment::InternationalType:
                vendTable.EnumBankPayment=2;
                vendTable.doUpdate();//   update();
                ttscommit;
               break;
           default:
               break;
        }


    }

}