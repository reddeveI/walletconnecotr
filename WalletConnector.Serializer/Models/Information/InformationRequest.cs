using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static WalletConnector.Serializer.OpenwayModel;

namespace WalletConnector.Serializer.Models.Information
{
    [XmlRoot(ElementName = "UFXMsg")]
    public class InformationRequest : OpenwayModel
    {
        public string Test { get; set; }
    }

    public static class InformationBuilder
    {
        public static InformationRequest CreateDefaultInformation()
        {
            var data = new InformationRequest();
            data.Scheme = "WAY4Appl";
            data.MsgType = "Information";
            data.Direction = "Rq";
            data.Version = "2.0";
            data.MsgId = Guid.NewGuid();
            data.Source = new SourceAttribute { App = "RSMFRONT" };
            data.MsgData = new MsgDataType
            {
                Information = new InformationType
                {
                    RegNumber = Guid.NewGuid(),
                    ObjectType = "Contract",
                    ActionType = "Inquiry"
                }
            };

            return data;
        }

        public static InformationRequest AddResultDetails(this InformationRequest data)
        {
            data.MsgData.Information.ResultDtls = new ResultDtls
            {
                Parm = new List<Parm>()
                {
                    new Parm { ParmCode = "Status", Value = "Y" },
                    new Parm { ParmCode = "Client", Value = "Y" },
                    new Parm { ParmCode = "Balance", Value = "WALLET" },
                    new Parm { ParmCode = "Product", Value = "Y" },
                    new Parm { ParmCode = "ContractClassifier", Value = "Y" },
                    new Parm { ParmCode = "ExtraRs", Value = "USAGE_REMAIN;BALANCE_SECTIONS;" }
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
