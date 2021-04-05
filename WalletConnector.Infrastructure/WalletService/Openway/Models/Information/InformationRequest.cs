using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using static WalletConnector.Infrastructure.WalletService.Openway.OpenwayModel;

namespace WalletConnector.Infrastructure.WalletService.Openway.Models.Information
{
    [XmlRoot(ElementName = "UFXMsg")]
    public class InformationRequest : OpenwayModel
    {
        public InformationRequest()
        {

        }
        public InformationRequest(AccountInfoRequestDto operationRequest)
        {
            Scheme = "WAY4Appl";
            MsgType = "Information";
            Direction = "Rq";
            Version = "2.0";
            MsgId = Guid.NewGuid();
            Source = new SourceAttribute
            {
                App = "RSMFRONT"
            };
            MsgData = new MsgData
            {
                Information = new InformationType
                {
                    RegNumber = Guid.NewGuid(),
                    ActionType = "Inquiry",
                    ResultDtls = new ResultDtls
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
                        Filter = new Filter
                        {
                            Type = "ContractList",
                            Code = "IssuingContracts"
                        }
                    },
                    ObjectFor = new ObjectFor
                    {
                        ClientIdt = new ClientIdt
                        {
                            ClientInfo = new ClientInfo
                            {
                                ClientNumber = operationRequest.Phone,
                            }
                        }
                    },
                    ObjectType = "Contract",
                }
            };
        }

        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }

        [XmlAttribute(AttributeName = "msg_type")]
        public string MsgType { get; set; }

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
        public MsgData MsgData { get; set; }
    }
}
