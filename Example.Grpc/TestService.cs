using MagicOnion;
using MagicOnion.Server;
using Shared.Grpc;

namespace Example.Grpc;

public class TestService : ServiceBase<ITestService>, ITestService
{
    public UnaryResult<string> GetData()
    {
        return UnaryResult.FromResult($"Test data generated at {DateTime.Now}");
    }
}
