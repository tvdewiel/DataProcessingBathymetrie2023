using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBManager
{
    public class MongoDBDataSetRepository
    {
        private string connectionString;
        private IMongoClient dbClient;
        private IMongoDatabase database;

        public MongoDBDataSetRepository(string connectionString)
        {
            this.connectionString = connectionString;
            dbClient = new MongoClient(connectionString);
            database = dbClient.GetDatabase("Belgica2023");
        }
        public void WriteDataSets(List<MongoDBDataSet> dataSets)
        {
            var collection = database.GetCollection<MongoDBDataSet>("datasets");
            collection.InsertMany(dataSets);
        }
        public List<MongoDBDataSet> FindDataSets(string campaignCode,DataSetType dataSetType,string dataSeries) 
        {
            var collection = database.GetCollection<MongoDBDataSet>("datasets");
            return collection.Find(x=>x.metaInfo.CampaignCode==campaignCode 
                && x.metaInfo.DataSetType==dataSetType
                && x.metaInfo.DataSeries==dataSeries).ToList();
        }
        public List<MongoDBDataSet> FilterDataSets(string campaignCode, DataSetType dataSetType, string dataSeries)
        {
            var collection = database.GetCollection<MongoDBDataSet>("datasets");
            var filter1=Builders<MongoDBDataSet>.Filter.Eq(x=>x.metaInfo.DataSetType,dataSetType);
            var filter2 = Builders<MongoDBDataSet>.Filter.Eq(x => x.metaInfo.CampaignCode, campaignCode);
            var filter3=Builders<MongoDBDataSet>.Filter.Eq(x=>x.metaInfo.DataSeries, dataSeries);
            return collection.Find(filter1&filter2&filter3).ToList();
        }
    }
}
