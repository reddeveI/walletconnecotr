using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static WalletConnector.Serializer.OpenwayModel;

namespace WalletConnector.Serializer.Models.Application
{
    [XmlRoot(ElementName = "UFXMsg")]
    public class ApplicationRequest : OpenwayModel
    {
    }

    public static class ApplicationBuilder
    {
        public static ApplicationRequest CreateDefaultApplication()
        {
            var data = new ApplicationRequest();
            data.Scheme = "WAY4Appl";
            data.MsgType = "Application";
            data.Direction = "Rq";
            data.Version = "2.0";
            data.MsgId = Guid.NewGuid();
            data.Source = new SourceAttribute { App = "RSMFRONT" };
            data.MsgData = new MsgDataType
            {
                Application = new ApplicationType
                {
                    RegNumber = Guid.NewGuid(),
                    Institution = "9999",
                    InstitutionIdType = "Branch",
                    OrderDepartment = "9901",
                    ObjectType = "Client",
                    ActionType = "AddOrUpdate",
                    ProductCategory = "Issuing"
                }
            };

            return data;
        }

        public static ApplicationRequest AddResultDetails(this ApplicationRequest data)
        {
            data.MsgData.Application.ResultDtls = new ResultDtls
            {
                Parm = new List<Parm>()
                {
                    new() { ParmCode = "AcceptRq", Value = "Y" },
                    new() { ParmCode = "Response", Value = "Y" },
                },
            };
            return data;
        }

        public static ApplicationRequest AddPhoneNumber(this ApplicationRequest data, string phone)
        {
            data.MsgData.Application.ObjectFor = new ObjectFor
            {
                ClientIdt = new ClientIdt
                {
                    ClientInfo = new ClientInfo
                    {
                        ClientNumber = phone,
                        ShortName = phone
                    }
                }
            };
            return data;
        }

        public static ApplicationRequest AddClientData(this ApplicationRequest data, string phone)
        {
            data.MsgData.Application.Data = new Data
            {
                Client = new Client
                {
                    ClientType = "PR",
                    ClientInfo = new ClientInfoFull
                    {
                        ClientNumber = phone,
                        RegNumber = Guid.NewGuid().ToString("N").Substring(0, 16).ToUpperInvariant(),
                        ShortName = phone
                    }
                }
            };
            return data;
        }

        public static ApplicationRequest AddSubApplication(this ApplicationRequest data, string phone)
        {
            data.MsgData.Application.SubApplList = new SubApplList
            {
                SubApplication = new SubApplication
                {
                    SubRegNumber = Guid.NewGuid(),
                    ObjectType = "Contract",
                    SubActionType = "Add",
                    SubData = new SubData
                    {
                        Contract = new Contract
                        {
                            ContractIdt = new ContractIdt
                            {
                                CbsNumber = phone
                            },
                            ContractName = "RGC" + Guid.NewGuid().ToString("N").Substring(0, 16).ToUpperInvariant(),
                            Product = new Product
                            {
                                ProductCode = "999-PCW"
                            }
                        }
                    }
                }
            };
            return data;
        }
    }
}
