using System;
using System.Collections.Generic;
using System.Text;

namespace Promotions.Infrastucture.Data
{
    public class MongoDBSettings:IMongoDBSettings
    {
       public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

    public interface IMongoDBSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
