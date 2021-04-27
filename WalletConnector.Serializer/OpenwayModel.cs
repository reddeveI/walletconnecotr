using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WalletConnector.Serializer
{
    public class OpenwayModel
    {
        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }

        [XmlAttribute(AttributeName = "msg_type")]
        public virtual string MsgType { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "direction")]
        public string Direction { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

        [XmlElement("MsgId")]
        public Guid MsgId { get; set; }

        [XmlElement(ElementName = "Source")]
        public SourceAttribute Source { get; set; }

        public class SourceAttribute
        {
            [XmlAttribute(AttributeName = "app")]
            public string App { get; set; }
        }

        [XmlElement("MsgData")]
        public MsgDataType MsgData { get; set; }

        public class MsgDataType
        {
            [XmlElement("Application")]
            public ApplicationType Application { get; set; }

            [XmlElement("Information")]
            public InformationType Information { get; set; }

            [XmlElement("Doc")]
            public DocumentType Doc { get; set; }
        }

        public class ApplicationType
        {
            [XmlElement("RegNumber")]
            public Guid RegNumber { get; set; }

            [XmlElement("Institution")]
            public string Institution { get; set; }

            [XmlElement("InstitutionIDType")]
            public string InstitutionIdType { get; set; }

            [XmlElement("OrderDprt")]
            public string OrderDepartment { get; set; }

            [XmlElement("ObjectType")]
            public string ObjectType { get; set; }

            [XmlElement("ActionType")]
            public string ActionType { get; set; }

            [XmlElement("ResultDtls")]
            public ResultDtls ResultDtls { get; set; }

            [XmlElement("ProductCategory")]
            public string ProductCategory { get; set; }

            [XmlElement("ObjectFor")]
            public ObjectFor ObjectFor { get; set; }

            [XmlElement("Data")]
            public Data Data { get; set; }

            [XmlElement("SubApplList")]
            public SubApplList SubApplList { get; set; }

            [XmlElement("Status")]
            public Status Status { get; set; }
        }

        public class ResultDtls
        {
            [XmlElement("Parm")]
            public List<Parm> Parm { get; set; }

            [XmlElement("Filter")]
            public Filter Filter { get; set; }
        }

        public class ObjectFor
        {
            [XmlElement("ClientIDT")]
            public ClientIdt ClientIdt { get; set; }

            [XmlElement("ContractIDT")]
            public ContractIdt ContractIdt { get; set; }
        }

        public class ClientIdt
        {
            public ClientInfo ClientInfo { get; set; }
        }

        public class AdditionalClient
        {
            public ClientInfo ClientInfo { get; set; }

            [XmlElement("BaseAddress")]
            public BaseAddress BaseAddress { get; set; }

            [XmlElement("DateOpen")]
            public string DateOpen { get; set; }
        }

        public class Parm
        {
            [XmlElement("ParmCode")]
            public string ParmCode { get; set; }

            [XmlElement("Value")]
            public string Value { get; set; }

        }

        public class Data
        {
            [XmlElement("Client")]
            public Client Client { get; set; }

            [XmlElement("Contract")]
            public AddContract Contract { get; set; }

            [XmlElement("CustomerClassifiers")]
            public CustomerClassifiers CustomerClassifiers { get; set; }

            [XmlElement("SetStatus")]
            public SetStatus SetStatus { get; set; }
        }

        public class SetStatus
        {
            [XmlElement("StatusCode")]
            public string StatusCode { get; set; }

            [XmlElement("StatusComment")]
            public string StatusComment { get; set; }
        }

        public class CustomerClassifiers
        {
            [XmlElement("Classifier")]
            public Classifier Classifier { get; set; }
        }

        public class Classifier
        {
            [XmlElement("Code")]
            public string Code { get; set; }

            [XmlElement("Value")]
            public string Value { get; set; }
        }

        public class SubApplList
        {
            [XmlElement("Application")]
            public SubApplication SubApplication { get; set; }
        }

        public class Status
        {
            [XmlElement("RespClass")]
            public string RespClass { get; set; }

            [XmlElement("RespCode")]
            public string RespCode { get; set; }

            [XmlElement("RespText")]
            public string RespText { get; set; }

            [XmlElement("PostingStatus")]
            public string PostingStatus { get; set; }
        }

        public class Client
        {
            [XmlElement("ClientType")]
            public string ClientType { get; set; }

            [XmlElement("ClientInfo")]
            public ClientInfoFull ClientInfo { get; set; }

            [XmlElement("BaseAddress")]
            public BaseAddress BaseAddress { get; set; }
        }

        public class BaseAddress
        {
            [XmlElement("City")]
            public string City { get; set; }

            [XmlElement("AddressLine1")]
            public string AddressLine1 { get; set; }
        }

        public class AddContract
        {
            [XmlElement("AddContractInfo")]
            public AddContractInfo AddContractInfo { get; set; }
        }

        public class AddContractInfo
        {
            [XmlElement("ExtraRs")]
            public string ExtraRs { get; set; }

            [XmlElement("AddInfo02")]
            public string AddInfo02 { get; set; }
        }

        public class ClientInfo
        {
            [XmlElement("ClientNumber")]
            public string ClientNumber { get; set; }

            [XmlElement("RegNumberType")]
            public string RegNumberType { get; set; }

            [XmlElement("RegNumber")]
            public string RegNumber { get; set; }

            [XmlElement("RegNumberDetails")]
            public string RegNumberDetails { get; set; }

            [XmlElement("ShortName")]
            public string ShortName { get; set; }

            [XmlElement("TaxpayerIdentifier")]
            public string TaxpayerIdentifier { get; set; }

            [XmlElement("FirstName")]
            public string FirstName { get; set; }

            [XmlElement("LastName")]
            public string LastName { get; set; }

            [XmlElement("MiddleName")]
            public string MiddleName { get; set; }

            [XmlElement("SecurityName")]
            public string SecurityName { get; set; }

            [XmlElement("BirthDate")]
            public string BirthDate { get; set; }

            [XmlElement("Gender")]
            public string Gender { get; set; }
        }

        public class ClientInfoFull : ClientInfo
        {

        }

        public class SubApplication
        {
            [XmlElement("RegNumber")]
            public Guid SubRegNumber { get; set; }

            [XmlElement("ObjectType")]
            public string ObjectType { get; set; }

            [XmlElement("ActionType")]
            public string SubActionType { get; set; }

            [XmlElement("ObjectFor")]
            public ObjectFor ObjectFor { get; set; }

            [XmlElement("Data")]
            public SubData SubData { get; set; }

            [XmlElement("DataRs")]
            public DataRs DataRs { get; set; }
        }

        public class SubData
        {
            [XmlElement("Contract")]
            public Contract Contract { get; set; }

            [XmlElement("CustomerClassifiers")]
            public CustomerClassifiers CustomerClassifiers { get; set; }
        }

        public class DataRs
        {
            [XmlElement("ContractRs")]
            public List<ContractRs> ContractRs { get; set; }
        }

        public class ContractRs
        {
            [XmlElement("Contract")]
            public Contract RsContract { get; set; }

            [XmlElement("Info")]
            public RsInfo RsInfo { get; set; }
        }

        public class Contract
        {
            [XmlElement("ContractIDT")]
            public ContractIdt ContractIdt { get; set; }

            [XmlElement("ContractName")]
            public string ContractName { get; set; }

            [XmlElement("Product")]
            public Product Product { get; set; }

            [XmlElement("AddContractInfo")]
            public AddContractInfo AddContractInfo { get; set; }

            [XmlElement("Currency")]
            public string Currency { get; set; }
        }

        public class RsInfo
        {
            [XmlElement("Balances")]
            public Balances Balances { get; set; }

            [XmlElement("Classifiers")]
            public Classifiers Classifiers { get; set; }

            [XmlElement("Status")]
            public ContractStatus Status { get; set; }
        }

        public class Balances
        {
            [XmlElement("Balance")]
            public List<Balance> Balance { get; set; }
        }

        public class Balance
        {
            [XmlElement("Name")]
            public string Name { get; set; }

            [XmlElement("Type")]
            public string Type { get; set; }

            [XmlElement("Amount")]
            public string Amount { get; set; }

            [XmlElement("Currency")]
            public string Currency { get; set; }

        }

        public class Classifiers
        {
            [XmlElement("Classifier")]
            public List<Classifier> Classifier { get; set; }
        }

        public class ContractStatus
        {
            [XmlElement("StatusClass")]
            public string StatusClass { get; set; }

            [XmlElement("StatusCode")]
            public string StatusCode { get; set; }

            [XmlElement("StatusDetails")]
            public string StatusDetails { get; set; }
        }

        public class ContractIdt
        {
            [XmlElement("CBSNumber")]
            public string CbsNumber { get; set; }

            [XmlElement("ContractNumber")]
            public string ContractNumber { get; set; }

            public AdditionalClient Client { get; set; }
        }

        public class Product
        {
            [XmlElement("ProductCode1")]
            public string ProductCode { get; set; }
        }

        public class InformationType
        {
            public InformationType() { }

            public InformationType(string actionType)
            {
                ActionType = actionType;
            }

            [XmlElement("DataRs")]
            public DataRs DataRs { get; set; }

            [XmlElement("RegNumber")]
            public Guid RegNumber { get; set; }

            [XmlElement("ObjectType")]
            public string ObjectType { get; set; }

            [XmlElement("ActionType")]
            public string ActionType { get; set; }

            [XmlElement("ResultDtls")]
            public ResultDtls ResultDtls { get; set; }

            [XmlElement("ObjectFor")]
            public ObjectFor ObjectFor { get; set; }

            [XmlElement("Status")]
            public Status Status { get; set; }
        }

        public class Filter
        {
            [XmlElement("Type")]
            public string Type { get; set; }

            [XmlElement("Code")]
            public string Code { get; set; }
        }

        public class DocumentType
        {
            [XmlElement("TransType")]
            public TransType TransType { get; set; }

            [XmlElement("DocRefSet")]
            public DocRefSet DocRefSet { get; set; }

            [XmlElement("Description")]
            public string Description { get; set; }

            [XmlElement("Requestor")]
            public ContractOwner Requestor { get; set; }

            [XmlElement("Source")]
            public ContractOwner Source { get; set; }

            [XmlElement("Destination")]
            public ContractOwner Destination { get; set; }

            [XmlElement("Transaction")]
            public Transaction Transaction { get; set; }

            [XmlElement("Status")]
            public Status Status { get; set; }
        }

        public class TransType
        {
            [XmlElement("TransCode")]
            public TransCode TransCode { get; set; }
        }

        public class TransCode
        {
            [XmlElement("MsgCode")]
            public string MsgCode { get; set; }

            [XmlElement("ServiceCode")]
            public string ServiceCode { get; set; }

            [XmlElement("TransTypeExtension")]
            public string TransTypeExtension { get; set; }
        }

        public class DocRefSet
        {
            [XmlElement("Parm")]
            public Parm Parm { get; set; }
        }

        public class ContractOwner
        {
            [XmlElement("ContractNumber")]
            public string ContractNumber { get; set; }

            [XmlElement("CBSNumber")]
            public string CbsNumber { get; set; }
        }

        public class Transaction
        {
            [XmlElement("Currency")]
            public string Currency { get; set; }

            [XmlElement("Amount")]
            public string Amount { get; set; }

            [XmlElement("Extra")]
            public Extra Extra { get; set; }
        }

        public class Extra
        {
            [XmlElement("Type")]
            public string Type { get; set; }

            [XmlElement("Details")]
            public string Details { get; set; }
        }

    }
}
