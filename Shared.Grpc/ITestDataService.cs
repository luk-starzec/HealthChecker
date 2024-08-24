using MagicOnion;

namespace Shared.Grpc
{
    public interface ITestDataService : IService<ITestDataService>
    {
        UnaryResult<string> GetData();
    }
}
