using System;
using System.Xml;
using System.ServiceModel;
using System.Collections.Generic;
using System.Net.Http;
using CodeHollow.FeedReader;
using RSSW.Domain.Models;
using RSSW.Data;

using static System.Console;

namespace RSSW.Business.Services
{
    public class ArticleService
    {
        private readonly RSSWDbContext _ctx;

        public ArticleService(RSSWDbContext ctx) {
            _ctx = ctx;
        }

        public List<Article> GetAll() {
            return _ctx.Articles.ToList();
        }

        public Article Get(int id) {
            return _ctx.Articles.Find(id);
        }

        public Article Get(string title) {
            return _db.Articles.FirstOrDefault(a => a.Title == title);
        }

        public void Add(Article article) {
            _ctx.Add(article);
            _ctx.SaveChanges();
        }

        public void Delete(int id) {
            var ToDelete = _ctx.Articles.Find(id);

            if (ToDelete != null) {
                _ctx.Remove(ToDelete);
            }

            throw new InvalidOperationException("no articles exists");
        }

    }
}
