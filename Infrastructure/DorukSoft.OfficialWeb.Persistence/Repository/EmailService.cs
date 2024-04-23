using DorukSoft.OfficialWeb.Application.Interfaces;
using System.Net.Mail;
using System.Net;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.Constants;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DorukSoft.OfficialWeb.Persistence.Repository
{
    public class EmailService : IEmailService
    {
        public async Task PublishQuee(EmailSendRequestDTO dto)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost", // RabbitMQ server address
                UserName = "guest",
                Password = "guest"
            };
            //connectionFactory.Uri = new Uri(RabbitMQConst.Url);

            //Connection active

            using var connection = connectionFactory.CreateConnection();
            using var channnel = connection.CreateModel();

            channnel.QueueDeclare(queue: RabbitMQConst.EmailQueue, exclusive: false, durable: true);

            IBasicProperties properties = channnel.CreateBasicProperties();
            properties.Persistent = true;

            var json = JsonConvert.SerializeObject(dto);

            byte[] message = Encoding.UTF8.GetBytes(json);

            channnel.BasicPublish(exchange: "", routingKey: RabbitMQConst.EmailQueue, body: message, basicProperties: properties);
        }
    }
}
