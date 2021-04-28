using System;
using WalletConnector.Serializer.Models.Application;
using WalletConnector.Serializer.Models.Document;
using WalletConnector.Serializer.Models.Information;

namespace WalletConnector.Serializer.Models
{
    public static class CommonRequestBuilder
    {
        public static T Create<T>(MsgType msgType) where T: OpenwayModel
        {
            OpenwayModel request;

            switch (msgType)
            {
                case MsgType.Application:
                    request = new ApplicationRequest();
                    request.MsgData = new OpenwayModel.MsgDataType()
                    {
                        Application = new OpenwayModel.ApplicationType
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
                    break;
                case MsgType.Information:
                    request = new InformationRequest();
                    request.MsgData = new OpenwayModel.MsgDataType()
                    {
                        Information = new OpenwayModel.InformationType
                        {
                            RegNumber = Guid.NewGuid(),
                            ObjectType = "Contract",
                            ActionType = "Inquiry"
                        }
                    };
                    break;
                case MsgType.Document:
                    request = new DocumentRequest();
                    request.MsgData = new OpenwayModel.MsgDataType()
                    {
                        Doc = new OpenwayModel.DocumentType
                        {

                        }
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(msgType), msgType, null);
            }
            
            request.Scheme = "WAY4Appl";
            request.Direction = "Rq";
            request.Version = "2.0";
            request.MsgId = Guid.NewGuid();
            request.Source = new OpenwayModel.SourceAttribute { App = "RSMFRONT" };

            return request as T;
        }
    }

    public enum MsgType
    {
        Application,
        Information,
        Document
    }
}