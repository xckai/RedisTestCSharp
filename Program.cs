using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace redisTest
{
    class Todo
    {
        public long Id { set; get; }
        public string Content { set; get; }
        public int Order { set; get; }
        public bool Done { set; get; }
        public int MyId { set; get; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var redisManager = new PooledRedisClientManager("192.168.27.128"))
            using (var redis = redisManager.GetClient())
            {
               
                var redisTodos = redis.As<Todo>();
                for(int i =0 ;i<5;++i)
                {
                    var todo = new Todo
                    {
                        Id = redisTodos.GetNextSequence(),
                        Content = "Learn Redis"+i,
                        Order = i,
                    };

                    redisTodos.Store(todo);
                  
                }
                var _todo = new Todo
                {
                    Id = redisTodos.GetNextSequence(),
                    Content = "Learn Redis" + 100,
                    Order = 100,
                    MyId = 19,
                };

                redisTodos.Store(_todo);
                   
                "Updated Todo:".Print();
                redisTodos.GetAll().ToList().PrintDump();

              

                "No more Todos:".Print();
                redisTodos.GetAll().ToList().PrintDump();
            }
            Console.Read();
        }
    }
}
