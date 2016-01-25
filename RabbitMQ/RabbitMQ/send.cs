using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
//Aby rabbit poprawnie działał musimy najpierw ściągnąc bibliotekę do RabbitMQ.Client.dll do naszego folderu roboczego /bin/ w projekcie
//Następnie Tools->NuGet Package Manager w Visualu i w konsoli wpisujemy polecenie:
//Install-Package RabbitMQ.Client
//INFO: https://www.nuget.org/packages/RabbitMQ.Client
// konsola deweloperska: C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\Shortcuts
namespace RabbitMQ
{
    class send
    {//Trorzymy połaczenie z serwerem
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            { //deklarujemy kolejke, która będzie przechowywac naszą wysłana wiadomość
              //dodatkoow kolejka zostanie stworzona tylko wtedy jesli jej nie ma
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World"; //nasza wiadomośc jest tablicą, która mozna dowolnie modyfikować
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body:body);

                Console.WriteLine("[x] Sent {0}", message);
            }
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }//Zadziała tylko raz wysyłajac wiadomośc i przestanie działać
    }
}
