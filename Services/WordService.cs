﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordsApi.Models;

namespace WordsApi.Services
{
    public class WordService
    {
        private readonly IMongoCollection<Word> _words;

        public WordService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("WordstoreDb"));
            var database = client.GetDatabase("WordstoreDb");
            _words = database.GetCollection<Word>("Words");
        }

        public List<Word> Get()
        {
            return _words.Find(word => true).ToList();
        }

        public Word Get(string id)
        {
            return _words.Find<Word>(word => word.Id == id).FirstOrDefault();
        }

        public Word Create(Word word)
        {
            _words.InsertOne(word);
            return word;
        }

        public void Update(string id, Word wordIn)
        {
            _words.ReplaceOne(word => word.Id == id, wordIn);
        }

        public void Remove(Word wordIn)
        {
            _words.DeleteOne(word => word.Id == wordIn.Id);
        }

        public void Remove(string id)
        {
            _words.DeleteOne(word => word.Id == id);
        }
    }
}