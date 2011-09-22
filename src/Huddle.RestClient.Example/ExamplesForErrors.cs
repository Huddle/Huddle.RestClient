using System;
using Huddle.Clients.ApiCallObservers;
using Huddle.Clients.Authentication;
using Huddle.Clients.Exceptions;
using NUnit.Framework;

namespace Huddle.Clients.Example
{
    [TestFixture]
    public class Given_I_have_not_got_a_valid_token
    {
        XmlApiCall _apiCall;

        [SetUp]
        public void When_i_make_a_request_for_a_users_details()
        {
            _apiCall = new XmlApiCall(
                new[] {new HttpStatusCodeExceptionHandler()});
            _apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism("i am not authorised"));
        }

        [Test]
        [ExpectedException(typeof(AuthenticationException))]
        public void It_returns_401_Unauthorized()
        {
            _apiCall.Get(Configuration.GetTestUserUri);
        }
    }

    [TestFixture]
    public class Given_I_have_made_a_request_against_an_invalid_URI
    {
        XmlApiCall _apiCall;
        string _invalidUri;

        [SetUp]
        public void When_i_make_a_request_for_a_users_details()
        {
            _apiCall = new XmlApiCall(
                new[] {new HttpStatusCodeExceptionHandler()});
            _apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
            _invalidUri = Configuration.BaseUrl + "users/haxxor";
        }

        [Test]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void It_returns_404_NotFound()
        {
            _apiCall.Get(_invalidUri);
        }
    }

    [TestFixture]
    public class Given_a_user_who_wants_to_post_something_that_cannot_be_serialised_to_XML
    {
        XmlApiCall _apiCall;

        [SetUp]
        public void When_the_file_metadata_is_POSTed()
        {
            _apiCall = new XmlApiCall(new[] {new HttpStatusCodeExceptionHandler()});
            _apiCall.SetRequestAuthenticationMechanism(new OAuth2AuthenticationMechanism(Configuration.OAuth2Token));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void It_returns_a_400_BadRequest()
        {
            var randomObject = new { thing = "hello", feet = "large"};
            _apiCall.Post(Configuration.ExampleCreateDocUri, randomObject);
        }
    }
}
