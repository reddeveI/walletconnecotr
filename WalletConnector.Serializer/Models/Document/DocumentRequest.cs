using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static WalletConnector.Serializer.OpenwayModel;

namespace WalletConnector.Serializer.Models.Document
{
    [XmlRoot(ElementName = "UFXMsg")]
    public class DocumentRequest : OpenwayModel
    {
        public override string MsgType => "Doc";
    }

    public static class DocumentBuilder
    {
        public static DocumentRequest CreateDefaultDocument() =>
            CommonRequestBuilder.Create<DocumentRequest>(MsgType.Document);

        public static DocumentRequest AddTransactionType(this DocumentRequest data)
        {
            data.MsgData.Doc.TransType = new TransType
            {
                TransCode = new TransCode
                {
                    MsgCode = "account_transfer",
                    TransTypeExtension = "W2W"
                }
            };
            data.MsgData.Doc.DocRefSet = new DocRefSet
            {
                Parm = new Parm
                {
                    ParmCode = "SRN",
                    Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32)
                }
            };
            return data;
        }

        public static DocumentRequest AddTransactionInfo(this DocumentRequest data, string requestor, string destination)
        {
            data.MsgData.Doc.Description = $"Перевод со счёта {requestor} на счёт {destination}";
            data.MsgData.Doc.Requestor = new ContractOwner
            {
                CbsNumber = requestor
            };
            data.MsgData.Doc.Destination = new ContractOwner
            {
                CbsNumber = destination
            };
            return data;
        }

        public static DocumentRequest AddTransactionAmount(this DocumentRequest data, string currency, string amount)
        {
            data.MsgData.Doc.Transaction = new Transaction
            {
                Currency = currency,
                Amount = amount
            };
            return data;
        }

    }
}
