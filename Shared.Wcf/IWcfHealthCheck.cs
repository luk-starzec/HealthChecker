using System.ServiceModel;

namespace Shared.Wcf
{
    [ServiceContract]
    public interface IWcfHealthCheck
    {
        [OperationContract]
        bool HealthCheck();
    }
}
