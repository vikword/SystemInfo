using System;
using System.Windows.Forms;

namespace SystemInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e) //Кнопка отправить
        {
            string surname = textBox1.Text;
            string name = textBox2.Text;
            string patronymic = textBox3.Text;
            string fio = $"{surname} {name} {patronymic}"; //Фамилия Имя Отчество (в дальнейшем тема письма)
            Hide(); //Закрываем окно
            _ = SendEmail.SendEmailAsync(SystemInfo.CollectingInformation() + SystemInfo.SpeedTest(), "Системная информация", fio);
            InitializeComponent();
            Timer t = new Timer
            {
                Interval = 5000
            };
            t.Start(); //Запускаем таймер на 5 секунд
            t.Tick += new EventHandler(TimeTick);
        }

        void TimeTick(object sender, EventArgs e)
        {
            Close(); //Закрываем программу
        }
    }
}
