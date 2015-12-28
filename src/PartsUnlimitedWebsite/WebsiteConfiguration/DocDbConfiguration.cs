using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;

namespace PartsUnlimited.WebsiteConfiguration
{
    public class DocDbConfiguration : IDocDbConfiguration
    {
        private readonly DocumentClient _client;

        public DocDbConfiguration(IConfiguration config)
        {
            URI = config["URI"];
            Key = config["Key"];
            DatabaseId = "PartsUnlimited";
            CollectionId = "ProductCollection";
            _client = null;
        }

        public string URI { get; }
        public string Key { get; }
        public string DatabaseId { get; }
        public string CollectionId { get; }
        public DocumentClient BuildClient()
        {
            if (_client == null)
            {
                var serviceEndpoint = new Uri(URI);
                var client = new DocumentClient(serviceEndpoint, Key, ConnectionPolicy.Default);
                return client;
            }

            return _client;
        }

        public Uri BuildProductCollectionLink()
        {
            return UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
        }

        public Uri BuildProductLink(string productId)
        {
            return UriFactory.CreateDocumentUri(DatabaseId, CollectionId, productId);
        }

        public Uri BuildDatabaseLink()
        {
            return UriFactory.CreateDatabaseUri(DatabaseId);
        }
    }
}