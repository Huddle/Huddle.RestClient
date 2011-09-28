using System.Net;
using System.Xml.Serialization;
using Huddle.Clients.Authentication;
using NUnit.Framework;
using System.Collections.Generic;

namespace Huddle.Clients.Example
{
    [XmlRoot("document")]
    public class Document
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlElement("link")]
        public List<Link> Links { get; set; }

        public Document() { }

        public Document(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public Link GetLinkFor(string rel)
        {
            foreach (var link in Links)
            {
                if (link.Rel.Equals(rel))
                {
                    return link;
                }
            }

            return null;
        }
    }

    [XmlRoot("link")]
    public class Link
    {
        [XmlAttribute("rel")]
        public string Rel { get; set; }

        [XmlAttribute("href")]
        public string Href { get; set; }
    }

    [TestFixture]
    public class Given_a_user_who_wants_to_create_a_new_file_using_XML
    {
        IApiResponse _response;

        [SetUp]
        public void When_the_file_metadata_is_POSTed()
        {
            var newDocumentDetails = new Document("a new example document", "a description of the new example document");
            var apiCall = new XmlApiCall();
            apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
            _response = apiCall.Post(Configuration.ExampleCreateDocUri, newDocumentDetails);
        }

        [Test]
        public void It_returns_a_201_Created()
        {
            Assert.That(_response.Status, Is.EqualTo(HttpStatusCode.Created));
        }
    }

    [TestFixture]
    public class Given_a_user_who_wants_to_create_a_new_file_using_JSON
    {
        IApiResponse _response;

        [SetUp]
        public void When_the_file_metadata_is_POSTed()
        {
            var newDocumentDetails = new Document("a new example document", "a description of the new example document");
            var apiCall = new JsonApiCall();
            apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
            _response = apiCall.Post(Configuration.ExampleCreateDocUri, newDocumentDetails);
        }

        [Test]
        public void It_returns_a_201_Created()
        {
            Assert.That(_response.Status, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
