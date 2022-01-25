using System;
using System.Collections.Generic;
using Autofac;

namespace autofuncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<B>().As<IGet>();
            builder.RegisterType<C>().As<IGet>().SingleInstance();
            builder.RegisterType<A>().AsImplementedInterfaces();
            var container = builder.Build();

            var a = container.Resolve<IA>();
            for(int i = 0; i< 2; i++) {
                foreach (var g in a.Get()) {
                    Console.WriteLine(g);
                }
            }

            // for(int i = 0; i< 5; i++) {
                
            //     Console.WriteLine(a.Get());
            // }

            
            Console.WriteLine("Done!");
        }
    }

    public class A  : IA{

        readonly Func<IGet[]> getFunc;
        public A(Func<IGet[]> getFunc)
        {
            this.getFunc = getFunc;
        }

        public IEnumerable<string> Get()
        {
            var get = getFunc();
            List<string> values = new List<string>();
            foreach (var g in get) {
                values.Add(g.Get());
            }

            return values;
        }
    }

    public interface IA {
        IEnumerable<string> Get();
    }

    public class B : IGet
    {
        string value = Guid.NewGuid().ToString();
        
        public string Get()
        {
            return $"{this.GetType()}: {value}";
        }
    }

    public class C : IGet
    {
        string value = Guid.NewGuid().ToString();
        public string Get()
        {
            return $"{this.GetType()}: {value}";
        }
    }

    public interface IGet {
        string Get();
    }
}
