using System;
using System.Diagnostics;
using System.Management;
using System.Net;
using System.IO;

namespace SystemInfo
{
    static class SystemInfo
    {
        public static string Processor() //Получаем информацию о процессоре
        {
            ManagementObjectSearcher searcher_cpu =
    new ManagementObjectSearcher("root\\CIMV2",
    "SELECT * FROM Win32_Processor");

            string[] processor = new string[3];
            foreach (ManagementObject queryObj in searcher_cpu.Get())
            {
                processor[0] = "Информация о процессоре:" + "\n";
                processor[1] = "Производитель и модель: " + queryObj["Name"] + "\n";
                processor[2] = "Количество ядер: " + queryObj["NumberOfCores"] + "\n";
            }

            return processor[0] + processor[1] + processor[2] + "\n";
        }


        public static string Memory() //Получаем информацию о оперативной памяти
        { 
            ManagementObjectSearcher searcher_ram =
    new ManagementObjectSearcher("root\\CIMV2",
    "SELECT * FROM Win32_PhysicalMemory");

            string[] memory = new string[2];
            double sum = 0;
            memory[0] = "Оперативная память:" + "\n";
            foreach (ManagementObject queryObj in searcher_ram.Get())
            {
                sum += Math.Round(Convert.ToDouble(queryObj["Capacity"]) / 1024 / 1024 / 1024, 2);
            }
            memory[1] = "Объем " + Convert.ToString(sum) + "Gb" + "\n";

            return memory[0] + memory[1] + "\n";
        }


        public static string OsUsname() //Получаем информацию о операционной системе и имя пользователя
        {
            ManagementObjectSearcher searcher_platform =
                new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");

            string[] os = new string[3];
            string usname = null;
            foreach (ManagementObject queryObj in searcher_platform.Get())
            {
                os[0] = "Операционная система:" + "\n";

                os[1] = "Платформа: " + queryObj["Caption"] + "\n";
                usname = (string)queryObj["RegisteredUser"];
            }
            os[2] = "Имя пользователя: " + usname;

            return os[0] + os[1] + os[2] + "\n";
        }


        public static string IpAdres() //Узнаём IP Адрес
        {
            string pubIp = $"\nIP Адрес: \n{new WebClient().DownloadString("https://api.ipify.org")}\n";

            return pubIp + "\n";
        }

        public static string SpeedTest() //Проверяем скорость интернета
        {
            WebClient wc = new WebClient();
            string url = "direct link to the file"; //Прямая ссылка на файл для загрузки
            Stopwatch s = new Stopwatch();
            s.Start();
            wc.DownloadFile(url, "download path"); //Путь для загрузки файла
            s.Stop();
            File.Delete(@"file path"); //Удаляем файл
            double result = 144861 / (Convert.ToDouble(s.ElapsedMilliseconds) / 1000) * 8 / 1024; // 144861 - Это размер скачаного файла в Kb
            
            return $"Скорость интернета: \n {Math.Round(result, 1)} MBit/s\n";
        }

        public static string CollectingInformation() //Собираем вышеуказанную информацию в одно кроме проверки скрости интернета
        {
            string processor = Processor();
            string memory = Memory();
            string os = OsUsname();
            string ip = IpAdres();

            return processor + memory + os + ip;
        }
    }
}