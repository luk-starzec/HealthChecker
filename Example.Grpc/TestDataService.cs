using MagicOnion;
using MagicOnion.Server;
using Shared.Grpc;

namespace Example.Grpc;

public class TestDataService : ServiceBase<ITestDataService>, ITestDataService
{
    public UnaryResult<string> GetData()
    {
        return UnaryResult.FromResult($"Test data generated at {DateTime.Now}");
    }
}
