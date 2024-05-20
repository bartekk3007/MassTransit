using Zadanie;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OdbiorcaC
{
    public class Program
    {
        public static Task Handler(ConsumeContext<IMesg2> ctx) =>
            Console.Out.WriteLineAsync($"\n\tWiadomość to: {ctx.Message.Text2}");

        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint("queueConsumerC", ep =>
                {
                    ep.Handler<IMesg2>(Handler);
                });
            });
            bus.Start();
            Console.WriteLine("OdbiorcaC wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}