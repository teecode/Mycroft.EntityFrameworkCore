namespace Mycroft.EntityFrameworkCore.Data.Repository.Utility
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TicketsCollectionName { get; set; }
        public string TerminalTransactionsCollectionName { get; set; }
        public string AgentTransactionsCollectionName { get; set; }
    }
}