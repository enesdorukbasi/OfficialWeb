using DorukSoft.OfficialWeb.Domain.Constants;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Channels;
using Newtonsoft.Json;
using DorukSoft.OfficialWeb.Application.DTOs;

namespace DorukSoft.OfficialWeb.Application.Tools
{
    public class RabbitMQService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost", // RabbitMQ server address
                UserName = "guest",
                Password = "guest"
            };
            //_connectionFactory.Uri = new Uri(RabbitMQConst.Url);

            var connection = _connectionFactory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: RabbitMQConst.EmailQueue, durable: true, exclusive: false);
            ConsumeSendEmail();
        }

        async void SendMail(string message)
        {
            var messageDto = JsonConvert.DeserializeObject<EmailSendRequestDTO>(message);

            var mail = new MailMessage();

            mail.To.Add(messageDto!.ToMailAddress);
            if(!string.IsNullOrEmpty(messageDto.CcMailAddress))
            {
                mail.CC.Add(messageDto.CcMailAddress);
            }
            mail.Body = messageDto.Body;
            mail.Subject = messageDto.Subject;
            mail.From = new(RabbitMQConst.SenderEmailAddress, RabbitMQConst.SenderName, System.Text.Encoding.UTF8);
            var smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(RabbitMQConst.SenderEmailAddress, RabbitMQConst.SenderEmailPassword);
            smtp.EnableSsl = RabbitMQConst.EnableSSL;
            smtp.Host = RabbitMQConst.EmailHost;
            smtp.Port = RabbitMQConst.EmailPort;

            await smtp.SendMailAsync(mail);

        }

        public void ConsumeSendEmail()
        {
            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: RabbitMQConst.EmailQueue, autoAck: false, consumer: consumer);
            _channel.BasicQos(0, 1, false);
            consumer.Received += Consumer_Received;
            void Consumer_Received(object? sender, BasicDeliverEventArgs e)
            {
                //response message operation
                //e.Body:quue message data
                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

                SendMail(Encoding.UTF8.GetString(e.Body.Span));
                //e.tag uniqe multiple true denirse oncekileride başarılı der false olursa sadece o mesaj
                _channel.BasicAck(e.DeliveryTag, multiple: false);
            }
        }
    }
}
