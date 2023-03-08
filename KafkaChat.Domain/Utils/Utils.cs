using KafkaChat.Domain.Entities.Config;
using System.Diagnostics;

namespace KafkaChat.Domain.Utils
{
    public static class Utils
    {
        public static void OpenIndexPage()
        {
            Task.Run(() =>
            {
                string caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Split("bin").FirstOrDefault(), "index.html");

                if (File.Exists(caminhoArquivo))
                    Process.Start(new ProcessStartInfo(caminhoArquivo) { UseShellExecute = true });
            });
        }

        public static void StartZookeeperAndKafka(CommandStartService command)
        {
            if (command.StartZookeeperServer)
            {
                Process.Start("cmd.exe", "/c " + command.Zookeeper);
                Thread.Sleep(3500);
            }
            if (command.StartKafkaServer)
            {
                Process.Start("cmd.exe", "/c " + command.Kafka);
                Thread.Sleep(8500);
            }
        }

        public static void DeleteAllDataLogKafkaZookeeper(CommandStartService command)
        {
            if (!command.ClearKafkaLogs) return;
            try
            {
                DeleteAllFromFolder(@"C:\Kafka\data\kafka-logs");
                DeleteAllFromFolder(@"C:\Kafka\data\zookeeper");
            }
            catch { }
        }

        public static void DeleteAllFromFolder(string folder)
        {
            if (!Directory.Exists(folder))
                return;

            string[] arquivos = Directory.GetFiles(folder);
            foreach (string arquivo in arquivos)
                File.Delete(arquivo);
            string[] subdiretorios = Directory.GetDirectories(folder);
            foreach (string subdiretorio in subdiretorios)
                Directory.Delete(subdiretorio, true);
        }
    }
}