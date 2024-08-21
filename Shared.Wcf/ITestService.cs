using System.ServiceModel;

namespace Shared.Wcf
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string GetData();
    }
}
