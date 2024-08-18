using MagicOnion;

namespace Shared.Grpc
{
    public interface ITestService : IService<ITestService>
    {
        UnaryResult<string> GetData();
    }
}
