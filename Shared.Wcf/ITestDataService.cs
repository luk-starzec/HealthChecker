using System.ServiceModel;

namespace Shared.Wcf
{
    [ServiceContract]
    public interface ITestDataService
    {
        [OperationContract]
        string GetData();
    }
}
