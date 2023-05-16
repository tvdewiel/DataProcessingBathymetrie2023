using DataSetManager;
using MongoDBManager;

namespace MongoDBManagerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //var data = FileDataSetManager.ReadData(@"C:\data\belgica\belgica.txt");
            //var sets = FileDataSetManager.MakeDataSetsWithTestSet(data.data, new List<int> { 500,1000, 2000, 25000, 50000 });
            string connString = "mongodb://localhost:27017";
            string source = @"C:\data\belgica\belgica.txt";
            string campaign = "April 2023";
            string dataseries = "ModelTest - ID v 5";
            MongoDBDataSetRepository repo = new MongoDBDataSetRepository(connString);
            //List<MongoDBDataSet> mds = new List<MongoDBDataSet>();

            //DataSetMetaInfo metaInfo=new DataSetMetaInfo(sets[0].data.Count,source,campaign,dataseries,DataSetType.TestSet);
            //MongoDBDataSet testset = new MongoDBDataSet(sets[0], metaInfo);
            //mds.Add(testset);
            //for(int i=1;i<sets.Count;i++)
            //{
            //    metaInfo= new DataSetMetaInfo(sets[i].data.Count, source, campaign, dataseries, DataSetType.DataSet);
            //    mds.Add(new MongoDBDataSet(sets[i],metaInfo));
            //}
            //repo.WriteDataSets(mds);
            var res = repo.FilterDataSets(campaign, DataSetType.DataSet, dataseries);
            Console.WriteLine("end");
        }
    }
}