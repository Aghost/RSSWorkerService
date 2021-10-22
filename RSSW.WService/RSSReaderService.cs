using System;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSSW.Business;

namespace RSSW.WService
{
    public class RSSReaderService : BackgroundService
    {
        private XmlDocument xml = new();
        private RSSFeedReader _FeedReader = new();

        private string LogFilePath;
        private int Interval;

        public RSSReaderService(int interval = 10000, string logfilepath = @"logs/log.txt") {
            Interval = interval;
            LogFilePath = logfilepath;
        }

        public override async Task StartAsync(CancellationToken cancellationToken) {
            File.AppendAllText(LogFilePath, "Service started" + Environment.NewLine);

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken) {
            File.AppendAllText(LogFilePath, "Service stopped" + Environment.NewLine);

            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                _FeedReader.ReadFeed();
                File.AppendAllText(LogFilePath, $"{DateTime.Now}" + Environment.NewLine);

                await Task.Delay(Interval, cancellationToken);
            }
        }

        public override void Dispose() {
        }
    }
}

/*
xml.Load("https://www.techvisor.nl/Rss/RssArtikelen/");
foreach (XmlNode rssnode in xml.SelectNodes("rss/channel/item")) {
    //Console.WriteLine(rssnode);
    Console.WriteLine(rssnode.SelectSingleNode("title").ToString());

}
*/
