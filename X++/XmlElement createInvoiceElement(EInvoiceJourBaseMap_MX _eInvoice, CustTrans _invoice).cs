 protected XmlElement createInvoiceElement(EInvoiceJourBaseMap_MX _eInvoice, CustTrans _invoice) 
    {
        XmlElement invoiceElement = this.createElement('DoctoRelacionado');
        invoiceElement.setAttribute('IdDocumento', _eInvoice.eInvoiceUUID());
        this.setAttributeIfNotNull(invoiceElement, 'Serie', _eInvoice.eInvoiceJour().InvoiceSeries);
        this.setAttributeIfNotNull(invoiceElement, 'Folio', _eInvoice.eInvoiceJour().InvoiceFolio);
        invoiceElement.setAttribute('MonedaDR', _eInvoice.currencyCode());

        this.createTipoCambioDRAttribute(invoiceElement, _eInvoice);
        invoiceElement.setAttribute('MetodoDePagoDR', _eInvoice.orderOfPayment());

        Integer counter;
        AmountCur runningTotal;
        CustSettlement custSettlement;

        _invoice = custTrans.recId nativo
        payment =  _invoice.OffsetRecId == custTrans.RecId

        select sum(SettleAmountCur), minof(CreatedDateTime) from custSettlement
            where custSettlement.TransRecId == _invoice.RecId (5637163331)
               && custSettlement.OffsetRecid == payment.RecId (5637163326)
               && custSettlement.CanBeReversed == NoYes::Yes;

        utcdatetime settlementCreatedDateTime = custSettlement.CreatedDateTime;
        AmountCur settleAmountCurrent = custSettlement.SettleAmountCur;

        while select sum(SettleAmountCur)
            from custSettlement
            group by TransRecId, OffsetRecid
            where custSettlement.TransRecId == _invoice.RecId
               && custSettlement.CanBeReversed
               && custSettlement.CreatedDateTime < settlementCreatedDateTime
        {
            counter++;
            runningTotal += custSettlement.SettleAmountCur;
        }

        AmountCur invoiceOpenBalancePrev = _invoice.AmountCur - runningTotal;




         protected str getXMLelement( str Chain, str separator,int position)
    {
        List _list = new List(Types::String);
        container    packedList;
        ListIterator iterator;
        str          cadena;
        str          XMLvalue;   

        cadena = Chain;
        _list = strSplit(cadena,separator);
        iterator = new ListIterator(_list);

        while(iterator.more())
        {
            packedList += iterator.value();
            iterator.next();
        
        }

        XMLvalue =  conPeek(packedList,position);
       
        return XMLvalue;
    }




