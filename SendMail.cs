using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SystemInfo
{
    static class SendEmail
    {
        public static async Task SendEmailAsync(string text, string user, string fio) //Метод для отправки данных на емайл
        {
            MailAddress from = new MailAddress("sender@exmape.com", user); //Емайл отправителя
            MailAddress to = new MailAddress("recipient@exmape.com"); //Емайл получателя
            MailMessage m = new MailMessage(from, to)
            {
                Subject = fio, //Тема письма
                Body = text //Текст письма (собранные данные)
            };
            SmtpClient smtp = new SmtpClient("smtp.exmape.com", 587) //smtp сервер и порт
            {
                Credentials = new NetworkCredential("sender@exmape.com", "password"), //Емайл и пароль отправителя
                EnableSsl = true
            };
            await smtp.SendMailAsync(m);
        }
    }
}
