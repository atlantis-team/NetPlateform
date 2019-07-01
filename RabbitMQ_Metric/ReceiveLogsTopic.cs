using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace RabbitMQ_Metric
{
    class ReceiveLogsTopic
    {

        private APICallHandler apiCall;

        public async void run()
        {
            apiCall = new APICallHandler("http://192.168.43.168:21061/jmsee-web/webresources/metric");

            var factory = new ConnectionFactory() { HostName = "192.168.43.217", UserName = "thomas", Password = "root" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");
                var queueName = channel.QueueDeclare().QueueName;

                //if (args.Length < 1)
                //{
                //    Console.Error.WriteLine("Usage: {0} [binding_key...]",
                //                            /*Environment.GetCommandLineArgs()[0]*/"altantis.metric");
                //    Console.WriteLine(" Press [enter] to exit.");
                //    Console.ReadLine();
                //    Environment.ExitCode = 1;
                //    return;
                //}

                string[] bindings = { "atlantis.metric" };

                foreach (var bindingKey in bindings)
                {
                    channel.QueueBind(queue: queueName,
                                      exchange: "topic_logs",
                                      routingKey: bindingKey);
                }

                Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" [x] Received '{0}':'{1}'",
                                      routingKey,
                                      message);

                    var m = apiCall.PostMetricAsync("/insertMetric", message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public static void Main(string[] args)
        {
            ReceiveLogsTopic rlt = new ReceiveLogsTopic();
            rlt.run();


        }
    }
}