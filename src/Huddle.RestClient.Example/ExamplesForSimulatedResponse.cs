using System.Net;
using Huddle.Clients.Authentication;
using Huddle.Clients.Simulators;
using NUnit.Framework;

namespace Huddle.Clients.Example
{
    [TestFixture]
    public class Simulate_Xml_get_API_call
    {
        private IApiResponse _response;

        private const string _simulatedResponse = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <user> <link rel=\"self\" href=\"http://api.huddle.dev/users/13470075\" /> </user>";

        [SetUp]
        public void When_i_request_the_users_details()
        {
            var apiCall = new SimulatedXmlApiCall();
            var simulatedResponse = new SimulatedApiResponse {Response = _simulatedResponse, Status = HttpStatusCode.OK};
            apiCall.Response = simulatedResponse;
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
            Assert.That(_response.Response, Is.EqualTo(_simulatedResponse));
        }

    }

    [TestFixture]
    public class Simulate_Json_get_API_call
    {
        private IApiResponse _response;

        private const string _simulatedResponse = "{\"links\" : [ { \"rel\" : \"self\", \"href\" : \"http://api.huddle.dev/users/13470075\"}, ]";

        [SetUp]
        public void When_i_request_the_users_details()
        {
            var apiCall = new SimulatedJsonApiCall();
            var simulatedResponse = new SimulatedApiResponse {Response = _simulatedResponse, Status = HttpStatusCode.OK};
            apiCall.Response = simulatedResponse;
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
            Assert.That(_response.Response, Is.EqualTo(_simulatedResponse));
        }

    }
}
