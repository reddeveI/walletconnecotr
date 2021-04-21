using System.Collections.Generic;
using System.Xml.Serialization;
using static WalletConnector.Serializer.OpenwayModel;

namespace WalletConnector.Serializer.Models.Information
{
    [XmlRoot(ElementName = "UFXMsg")]
    public class InformationRequest : OpenwayModel
    {
        public string Test { get; set; }

        public override string MsgType => "Information";

    }

    public static class InformationBuilder
    {
        public static InformationRequest CreateDefaultInformation() =>
            CommonRequestBuilder.Create<InformationRequest>(MsgType.Information);

        public static InformationRequest AddResultDetails(this InformationRequest data)
        {
            data.MsgData.Information.ResultDtls = new ResultDtls
            {
                Parm = new List<Parm>()
                {
                    new() { ParmCode = "Status", Value = "Y" },
                    new() { ParmCode = "Client", Value = "Y" },
                    new() { ParmCode = "Balance", Value = "WALLET" },
                    new() { ParmCode = "Product", Value = "Y" },
                    new() { ParmCode = "ContractClassifier", Value = "Y" },
                    new() { ParmCode = "ExtraRs", Value = "USAGE_REMAIN;BALANCE_SECTIONS;" }
                },
            };
            data.MsgData.Information.ResultDtls.Filter = new Filter
            {
                Type = "ContractList",
                Code = "IssuingContracts"
            };
            return data;
        }

        public static InformationRequest AddPhoneNumber(this InformationRequest data, string phone)
        {
            data.MsgData.Information.ObjectFor = new ObjectFor
            {
                ClientIdt = new ClientIdt
                {
                    ClientInfo = new ClientInfo
                    {
                        ClientNumber = phone
                    }
                }
            };
            return data;
        }
    }
}
