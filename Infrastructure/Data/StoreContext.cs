using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data
{
    public class StoreContext {
        private ConfigureConnection configureConnection;
        public StoreContext(IOptionsMonitor < ConfigureConnection > optionsMonitor) {
            configureConnection = optionsMonitor.CurrentValue;
        }
        public IDbConnection ConnectSkinet() => new SqlConnection(configureConnection.skinet);
    }
}