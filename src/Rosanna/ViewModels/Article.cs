﻿using System;
using System.IO;
using Nancy;

namespace Rosanna.ViewModels
{
    public class Article
    {
        private readonly IRosannaConfiguration _config;
        private readonly string _fileName;
        private readonly DateTime _date;
        private readonly DynamicDictionary _meta;
        private readonly string _body;

        public Article(string filePath, IRosannaConfiguration config)
        {
            _config = config;
            _fileName = System.IO.Path.GetFileName(filePath);
            _date = DateTime.Parse(_fileName.Substring(0, 10));

            string text = File.ReadAllText(filePath);

            string[] strings = text.Split(new []{"\n\n"}, 2, StringSplitOptions.None);
            _meta = strings[0].ToDynamicDictionary();
            _body = strings[1];
        }

        public string Path
        {
            get { return string.Format("/{0}/{1}/{2}", _config.Prefix, _date.ToString("yyyy/MM/dd"), Slug).Squeeze("/"); }
        }
        
        public string Permalink
        {
            get { return "http://" + string.Format("{0}{1}", _config.Url.Replace("http://", null), Path).Squeeze("/"); }
        }
        
        public string Slug
        {
            get { return Title.ToSlug(); }
        }

        public string Date
        {
            get { return _config.DateFormat(_date); }
        }
        
        public string Title
        {
            get { return Meta.title; }
        }

        public string Summary
        {
            get { return new ArticleSummary(_body, _config); }
        }

        public string Body
        {
            get { return _body.Replace(_config.SummaryDelimiter, null).TransformMarkdown(); }
        }

        public dynamic Meta
        {
            get { return _meta; }
        }

        public string Author
        {
            get { return Meta.author.HasValue ? Meta.author : _config.Author; }
        }

        public DateTime GetDate()
        {
            return _date;
        }
    }
}