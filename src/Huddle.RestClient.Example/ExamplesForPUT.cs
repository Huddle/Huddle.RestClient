using Huddle.Clients.Authentication;
using Huddle.Clients.Converters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace Huddle.Clients.Example
{
    [TestFixture]
    public class Given_a_user_who_wants_to_update_a_file_using_XML
    {
        ApiResponse _response;

        [SetUp]
        public void When_the_file_metadata_is_PUTed()
        {
            var newDocumentDetails = new Document("a new example document", "a description of the new example document");

            var apiCall = new XmlApiCall();
            apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
            _response = apiCall.Post(Configuration.ExampleCreateDocUri, newDocumentDetails);

            var doc = XmlConverter.FromXml<Document>(_response.Response);
            var editUrl = doc.GetLinkFor("edit").Href;
            var parentUrl = doc.GetLinkFor("parent").Href;
            var putData = new Document("newTitle", "newDescription")
                {
                    Links = new List<Link>
                        {
                            new Link
                                {
                                     Rel = "parent"
                                    ,Href = parentUrl
                                }
                        } 
                };

            _response = apiCall.Put(editUrl, putData);
        }

        [Test]
        public void It_returns_a_202_Accepted()
        {
            Assert.That(_response.Status, Is.EqualTo(HttpStatusCode.Accepted));
        }
    }
}
