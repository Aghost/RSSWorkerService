using System;
using System.Xml;
using System.ServiceModel;
using System.Collections.Generic;
using System.Net.Http;
using CodeHollow.FeedReader;
using RSSW.Domain.Models;
using RSSW.Business.Services;
using RSSW.Data;

using static System.Console;

namespace RSSW.Business
{
    //https://yetanotherchris.dev/csharp/simplified-csharp-atom-and-rss-feed-parser/
    public class RSSFeedReader
    {
        private readonly ArticleService _ArticleService = new();

        public RSSFeedReader(ArticleService _as) { }

        public async void ReadFeed() {
            List<Uri> rssFeeds = new List<Uri> {
                new Uri(@"https://www.techvisor.nl/Rss/RssArtikelen"),
                new Uri(@"https://www.techrepublic.com/rssfeeds/articles/"),
                new Uri(@"https://www.nu.nl/rss/Tech")
            };

            var client = new HttpClient();

            foreach (var rssFeed in rssFeeds) {
                var result = await client.GetStreamAsync(rssFeed);//.Result;

                using (var xmlReader = XmlReader.Create(result)) {
                    var feed = await FeedReader.ReadAsync(rssFeed.ToString());
                    /* PROPERTIES:*//*
                    Items, Language, LastUpdatedDate, LastUpdatedDateString, Link, OriginalDocument, SpecificFeed, Title, Copyright, Description */
                    WriteLine($"\n--------------- Feed Title: {feed.Title}\nDescription: {feed.Description}\nImgUrl{feed.ImageUrl}\n--------------");

                    Article tmp = new();
                    tmp.Id = 0;
                    tmp.Title = "test";

                    // if LastUpdatedDateString != readerService.rssFeed.LastUpdatedDateString
                    //      Add Item to Database

                    foreach (var item in feed.Items) {
                        PrintItem(item);
                    }
                }
            }
        }

        /* PROPERTIES:*//*
        // Id, Link, PublishingDate, PublishingDateString, SpecificItem, Title, Author, Categories, Content, Description */
        private void PrintItem(FeedItem fi) {
            WriteLine($"{fi.Title}\t\t{fi.Link}\n{fi.Description}\n{fi.PublishingDateString}\n---");
            /* REVERSE *//*
            string tmpstr = $"{fi.Title}\t\t{fi.Link}\n{fi.Description}\n{fi.PublishingDateString}\n---";
            for(int i = tmpstr.Length - 1; i > 0; i--) { Write(tmpstr[i]); }
            WriteLine();
            */
        }

    }
}
