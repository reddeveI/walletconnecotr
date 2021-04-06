using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using static WalletConnector.Infrastructure.WalletService.Openway.Models.Information.InformationRequest;
using static WalletConnector.Infrastructure.WalletService.Openway.OpenwayModel;

namespace WalletConnector.Infrastructure.WalletService.Openway.Models.Information
{
    [XmlRoot(ElementName = "UFXMsg")]
    public class InformationRequest : OpenwayModel
    {
        public InformationRequest()
        {

        }

        public InformationRequest(string scheme, string msgType, string direction, string version)
        {
            Scheme = scheme;
            MsgType = msgType;
            Direction = direction;
            Version = version;
        }
    }

    public static class InformationBuilder
    {
        public static InformationRequest CreateInformationRequest()
        {
            return new InformationRequest(
                scheme: "WAY4Appl", 
                msgType: "Information", 
                direction: "Rq", 
                version: "2.0");
        }

        public static InformationRequest GenerateMessageId(this InformationRequest data)
        {
            data.MsgId = Guid.NewGuid();
            return data;
        }

        public static InformationRequest AddSourceAttribute(this InformationRequest data)
        {
            data.Source = new SourceAttribute
            {
                App = "RSMFRONT"
            };
            return data;
        }

        public static InformationRequest GenerateRegNumber(this InformationRequest data)
        {
            data.MsgData = new MsgDataType
            {
                Information = new InformationType
                {
                    RegNumber = Guid.NewGuid()
                }
            };
            return data;
        }

        public static InformationRequest AddObjectType(this InformationRequest data)
        {
            data.MsgData.Information.ObjectType = "Contract";
            return data;
        }

        public static InformationRequest AddActionType(this InformationRequest data)
        {
            data.MsgData.Information.ActionType = "Inquiry";
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
            return data;
        }

        public static InformationRequest AddFilters(this InformationRequest data)
        {
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

    #region
    //public static class MessageDataBuilder
    //{
    //    public static MsgData CreateMessageData() 
    //        => new MsgData { Information = new InformationType(actionType: "Inquiry") };
        
    //    public static MsgData GenerateRegNumber(this MsgData data)
    //    {
    //        data.Information.RegNumber = Guid.NewGuid();
    //        return data;
    //    }

    //    public static MsgData AddResultDetails(this MsgData data)
    //    {
    //        data.Information.ResultDtls = new ResultDtls
    //        {
    //            Parm = new List<Parm>()
    //            {
    //                new Parm { ParmCode = "Status", Value = "Y" },
    //                new Parm { ParmCode = "Client", Value = "Y" },
    //                new Parm { ParmCode = "Balance", Value = "WALLET" },
    //                new Parm { ParmCode = "Product", Value = "Y" },
    //                new Parm { ParmCode = "ContractClassifier", Value = "Y" },
    //                new Parm { ParmCode = "ExtraRs", Value = "USAGE_REMAIN;BALANCE_SECTIONS;" }
    //            },
    //        };
    //        return data;
    //    }

    //    public static MsgData AddFilters(this MsgData data)
    //    {
    //        data.Information.ResultDtls.Filter = new Filter
    //        {
    //            Type = "ContractList",
    //            Code = "IssuingContracts"
    //        };
    //        return data;
    //    }
    //}
    #endregion
}
