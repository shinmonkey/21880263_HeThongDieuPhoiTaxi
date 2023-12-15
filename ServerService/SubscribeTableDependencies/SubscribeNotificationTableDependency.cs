using ServerService.Hubs;
using ServerService.Models;
using TableDependency.SqlClient;

namespace ServerService.SubscribeTableDependencies
{
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Notification>? tableDependency;
        NotificationHub notificationHub;

        public SubscribeNotificationTableDependency(NotificationHub notificationHub)
        {
            this.notificationHub = notificationHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Notification>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Notification)} SqlTableDependency error: {e.Error.Message}");
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            if(e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var notification = e.Entity;

                if (notification.MessageType == "DatXe")
                {
                    //await notificationHub.SendNotificationToAll(notification.Message);
                }
            }
        }
    }
}
