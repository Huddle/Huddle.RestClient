using System.Net;
using Huddle.Clients.Authentication;
using NUnit.Framework;

namespace Huddle.Clients.Example
{
    [TestFixture]
    public class Given_an_existing_and_valid_oauth2_token_using_XMLApiCall
    {
        private IApiResponse _response;

        [SetUp]
        public void When_i_request_the_users_details()
        {
            var apiCall = new XmlApiCall();
            apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
            _response = apiCall.Get(Configuration.GetTestUserUri);
        }

        [Test]
        public void It_will_return_a_200_OK()
        {
            Assert.That(_response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void It_will_return_content()
        {
            Assert.That(_response.Response, Is.Not.Null);
        }

        [Test]
        public void It_will_return_the_user_ID_somewhere_in_the_response()
        {
            Assert.That(_response.Response, Is.StringMatching(Configuration.TestUserId));
        }
    }

    [TestFixture]
    public class Given_an_existing_and_valid_oauth2_token_using_XMLApiCall_over_SSL
    {
        private IApiResponse _response;

        [SetUp]
        public void When_i_request_the_users_details()
        {
            var apiCall = new XmlApiCall();
            apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
            _response = apiCall.Get(Configuration.GetSecureTestUserUri);
        }

        [Test]
        public void It_will_return_a_200_OK()
        {
            Assert.That(_response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void It_will_return_content()
        {
            Assert.That(_response.Response, Is.Not.Null);
        }

        [Test]
        public void It_will_return_the_user_ID_somewhere_in_the_response()
        {
            Assert.That(_response.Response, Is.StringMatching(Configuration.TestUserId));
        }
    }

    [TestFixture]
    public class Given_an_existing_and_valid_oauth2_token_using_JsonApiCall
    {
        private IApiResponse _response;

        [SetUp]
        public void When_i_request_the_users_details()
        {
            var apiCall = new JsonApiCall();
            apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
            _response = apiCall.Get(Configuration.GetTestUserUri);
        }

        [Test]
        public void It_will_return_a_200_OK()
        {
            Assert.That(_response.Status, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void It_will_return_content()
        {
            Assert.That(_response.Response, Is.Not.Null);
        }

        [Test]
        public void It_will_return_the_user_ID_somewhere_in_the_response()
        {
            Assert.That(_response.Response, Is.StringMatching(Configuration.TestUserId));
        }
    }
}
