using HealthDashboard.WebApp.Interfaces;
using Shared.Wcf;
using System.ServiceModel;

namespace HealthDashboard.WebApp.Services;

public class WcfHealthService : IWcfHealthService
{
    private static Dictionary<string, ChannelFactory<IWcfHealthCheck>> _factories = [];

    public bool CheckHealth(string address)
    {
        try
        {
            var channel = CreateChannel(address);
            var result = channel.HealthCheck();
            return result;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public IWcfHealthCheck CreateChannel(string address)
    {
        var factory = GetChannelFactory(address);
        var channel = factory.CreateChannel();
        ((IClientChannel)channel).Faulted += new EventHandler(ChannelFaulted);
        return channel;
    }

    protected ChannelFactory<IWcfHealthCheck> GetChannelFactory(string address)
    {
        if (!_factories.TryGetValue(address, out var factory))
        {
            var baseAddress = new Uri(address);
            var binding = new NetTcpBinding(SecurityMode.None, false);
            var endpoint = new EndpointAddress(baseAddress);

            factory = new ChannelFactory<IWcfHealthCheck>(binding, endpoint);
            factory.Faulted += new EventHandler(FactoryFaulted);

            _factories.TryAdd(address, factory);
        }
        return factory;
    }

    private void ChannelFaulted(object sender, EventArgs e)
    {
        IClientChannel channel = (IClientChannel)sender;
        try
        {
            channel.Close();
        }
        catch
        {
            channel.Abort();
        }
    }

    private void FactoryFaulted(object sender, EventArgs e)
    {
        var factory = (ChannelFactory)sender;

        if (factory.State != CommunicationState.Closed)
        {
            try
            {
                factory.Close();
            }
            catch
            {
                factory.Abort();
            }
            finally
            {
                var address = factory.Endpoint.Address.Uri.ToString();

                if (_factories.ContainsKey(address))
                    _factories.Remove(address);
            }
        }
    }

    public void Dispose()
    {
        foreach (var address in _factories.Keys)
        {
            var factory = _factories[address];

            try
            {
                factory.Close();
            }
            catch
            {
                factory.Abort();
            }
        }
        _factories.Clear();
    }

}
