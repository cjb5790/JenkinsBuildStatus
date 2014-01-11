using System;
using System.IO.Ports;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace JenkinsBuildStatus
{
    internal class Program
    {
        private const string Reset = "0";
        private const string Red = "1";
        private const string Yellow = "2";
        private const string Green = "3";
        private static SerialPort _serialPort;
        private static string JobName { get; set; }

        private static void Main(string[] args)
        {
            Console.Write("Enter a job name: ");
            JobName = Console.ReadLine();
            BuildStatus j;
            var client = new WebClient();

            _serialPort = new SerialPort {PortName = "COM3", BaudRate = 9600};
            _serialPort.Open();
            _serialPort.Write(Reset);

            do
            {
                _serialPort.Write(Yellow);
                Thread.Sleep(500);
                _serialPort.Write(Reset);
                string response =
                    client.DownloadString(
                        new Uri(String.Format("http://jenkins.aztekhq.local:8080/job/{0}/api/json?pretty=true", JobName)));
                j = JsonConvert.DeserializeObject<BuildStatus>(response);
            } while (j.LastBuild.Number != j.LastCompletedBuild.Number);

            _serialPort.Write(j.LastSuccessfulBuild.Number == j.LastBuild.Number ? Green : Red);
        }
    }

    public class BuildStatus
    {
        public string Name { get; set; }
        public Build LastBuild { get; set; }
        public Build LastCompletedBuild { get; set; }
        public Build LastFailedBuild { get; set; }
        public Build LastSuccessfulBuild { get; set; }
        public string NextBuildNumber { get; set; }
    }

    public class Build
    {
        public string Number { get; set; }
        public string Url { get; set; }
    }
}