using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Enums;
using Domain.Repository.Announcements;
using Domain.Repository.Shared;
using Domain.Entity.Articles;
using Domain.Entity.Shared;
using Domain.Interface;

namespace Reklama.Models.Shared
{
    public class SearchModel<T> : ISearch<T> where T: SearchProviderEntity
    {
        /** Repository for entity */
        private IRepository<T> _repository;
        public IRepository<T> Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }

        public SearchModel(IRepository<T> repository)
        {
            Repository = repository;
        }

        public IQueryable<T> Search(string keywordPhrase, bool onlyByName)
        {
            IQueryable<T> resultSet = null;

            var keywords = keywordPhrase.ToLowerInvariant().Trim()
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (keywords.Any())
            {
                // For initialize the (IQueryable) resultSet (in other way it will be equals to null)
                string baseWord = string.Empty;
                foreach (string word in keywords)
                {
                    if (word.Length >= ProjectConfiguration.Get.MinSearchWordLength)
                    {
                        resultSet = SearchFilter(e => e.Name.ToLowerInvariant().Contains(word));
                        baseWord = word;
                        break;
                    }
                }


                // The resultSet is initialized. Filling it
                foreach (string keyword in keywords)
                {
                    if (keyword.Length < ProjectConfiguration.Get.MinSearchWordLength) continue;

                    if (!keyword.Equals(baseWord))
                    {
                        var resultSetName = SearchFilter(e => e.Name.ToLowerInvariant().Contains(keyword));

                        if (resultSetName != null)
                        {
                            resultSet = resultSet.Union(resultSetName);
                        }
                    }


                    // Searching by name, small description and description
                    if (!onlyByName)
                    {
                        var resultSetDescription = SearchFilter(e => e.Description.ToLowerInvariant().Contains(keyword));
                        var resultSetSmallDescription = SearchFilter(e => e.SmallDescription.ToLowerInvariant().Contains(keyword));

                        if (resultSetDescription != null)
                        {
                            resultSet = resultSet.Union(resultSetDescription);
                        }

                        if (resultSetSmallDescription != null)
                        {
                            resultSet = resultSet.Union(resultSetSmallDescription);
                        }
                    }
                }
            }

            return resultSet;
        }


        protected IQueryable<T> SearchFilter(Func<T, bool> predicate)
        {
            return Repository.FilterCollection(e => e.IsActive)
                .Where(predicate)
                .AsQueryable<T>();
        }
    }
}