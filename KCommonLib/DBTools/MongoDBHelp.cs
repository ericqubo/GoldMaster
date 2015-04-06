using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;

namespace KCommonLib.DBTools
{
    public class MongoDBHelp
    {
        //链接字符串(此处三个字段值根据需要可为读配置文件)  
        //public string connectionString = "mongodb://10.10.140.205";
        public string connectionString = DBCommon.GetConstr("mongoCon");
        //数据库名  
        //public string databaseName = "F10MongoDataBase";
        public string databaseName = DBCommon.GetConstr("mongoDBName");
        //集合名   
        private string collectionName = "TESTTABLE";
        private Mongo mongo;
        private MongoDatabase mongoDatabase;
        private MongoCollection<Document> mongoCollection;

        public MongoDBHelp(string colName)
        {
            collectionName = colName;
            mongo = new Mongo(connectionString);
            mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;
            mongo.Connect();
        }
        ~MongoDBHelp()
        {
            mongo.Disconnect();
        }

        /// <summary>  
        /// 增加一条用户记录  
        /// </summary>  
        /// <param name="doc"></param>  
        public void Add(Document doc)
        {
            mongoCollection.Insert(doc);
        }

        /// <summary>  
        /// 删除一条用户记录  
        /// </summary>  
        public void Delete(string name, string value)
        {
            mongoCollection.Remove(new Document { { name, value } });
        }

        /// <summary>  
        /// 更新一条用户记录  
        /// </summary>  
        /// <param name="doc"></param>  
        public void Update(Document oldDoc, Document newDoc)
        {
            mongoCollection.FindAndModify(newDoc, oldDoc);
        }

        /// <summary>  
        /// 查找所有用户记录  
        /// </summary>  
        /// <returns></returns>  
        public List<Document> FindAll()
        {
            var query = from result in mongoCollection.Linq()
                        select result;

            return query.ToList();

            //return mongoCollection.FindAll().Documents;
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="name">排序字段</param>
        /// <param name="type">1：倒序，-1：正序</param>
        /// <returns></returns>
        public Document FindOneOrderBy(string name, int type)
        {
            var query = from result in mongoCollection.Linq()
                        select result;

            switch (type)
            {
                default:
                case -1: return query.OrderByDescending(t => t[name]).ToList()[0];
                case 1: return query.OrderBy(t => t[name]).ToList()[0];
                //case 2:  return (Document)mongoCollection.FindAll().Sort("name").Limit(1).Documents;
            }
            //return mongoCollection.FindAll().Sort(new Document { { name, type } }).Documents.Take(1).ToList();
            //FindAll().Sort(new Document { { name, type } });    
        }

        public Document FindOne(string name, string value)
        {
            return mongoCollection.FindOne(new Document { { name, value } });
        }

        public Document FindOne(Document doc)
        {
            return mongoCollection.FindOne(doc);
        }
    }
}
