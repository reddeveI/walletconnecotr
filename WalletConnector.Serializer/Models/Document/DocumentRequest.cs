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

        public static DocumentRequest AddTransactionType(this DocumentRequest data, string messageCode, string type)
        {
            data.MsgData.Doc.TransType = new TransType
            {
                TransCode = new TransCode
                {
                    MsgCode = messageCode,
                    TransTypeExtension = type
                }
            };
            return data;
        }

        public static DocumentRequest AddTransactionCode(this DocumentRequest data, string messageCode, string serviceCode)
        {
            data.MsgData.Doc.TransType = new TransType
            {
                TransCode = new TransCode
                {
                    MsgCode = messageCode,
                    ServiceCode = serviceCode
                }
            };
            return data;
        }

        public static DocumentRequest AddDocumentType(this DocumentRequest data, string messageCode)
        {
            data.MsgData.Doc.TransType = new TransType
            {
                TransCode = new TransCode
                {
                    MsgCode = messageCode,
                }
            };
            return data;
        }

        public static DocumentRequest AddTransactionId(this DocumentRequest data, string externalId)
        {
            data.MsgData.Doc.DocRefSet = new DocRefSet
            {
                Parm = new Parm
                {
                    ParmCode = "SRN",
                    Value = externalId ?? Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32)
                }
            };
            return data;
        }

        public static DocumentRequest AddTransactionDescription(this DocumentRequest data, string description)
        {
            data.MsgData.Doc.Description = description;

            return data;
        }

        public static DocumentRequest AddTransactionRequestorInfo(this DocumentRequest data, string requestor)
        {
            data.MsgData.Doc.Requestor = new ContractOwner
            {
                CbsNumber = requestor
            };
            return data;
        }

        public static DocumentRequest AddTransactionDestinationInfo(this DocumentRequest data, string destination)
        {
            data.MsgData.Doc.Destination = new ContractOwner
            {
                CbsNumber = destination
            };
            return data;
        }

        public static DocumentRequest AddTransactionSourceInfo(this DocumentRequest data, string destination)
        {
            data.MsgData.Doc.Source = new ContractOwner
            {
                CbsNumber = destination
            };
            return data;
        }

        public static DocumentRequest AddTransactionAmount(this DocumentRequest data, string currency, decimal amount)
        {
            data.MsgData.Doc.Transaction = new Transaction
            {
                Currency = currency,
                Amount = amount
            };
            return data;
        }

        public static DocumentRequest AddExtraAmount(this DocumentRequest data, decimal commission)
        {
            data.MsgData.Doc.Transaction.Extra = new Extra
            {
                Type = "CustomData",
                Details = ComissionFormatter.FormatDetail(commission)
            };
            return data;
        }

    }

    public class ComissionFormatter
    {
        public static string FormatDetail(decimal comission)
        {
            var com = String.Format("{0:0.00}", decimal.Round(comission / 100, 2)).Replace(",", "").Replace(".", "");

            string result = "F0";

            //200

            var hex0 = ("0" + Convert.ToString(com.Length + 5, 16)).ToUpper();
            var hex1 = ("0" + Convert.ToString(com.Length + 1, 16)).ToUpper();

            result += hex0 + "C1" + hex1 + "T" + com;

            return result;
        }
    }
}
