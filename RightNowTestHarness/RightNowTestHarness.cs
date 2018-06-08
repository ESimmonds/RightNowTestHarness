using System;
using System.Linq;
using System.ServiceModel;
using System.Web.Services.Protocols;
using BasicSamples.RightNowService;

namespace RightNowTestHarness
{
    class RightNowTestHarness
    {
        readonly RightNowSyncPortClient client;

        public RightNowTestHarness()
        {
            this.client = new RightNowSyncPortClient();
            this.client.ClientCredentials.UserName.UserName = "<username>";
            this.client.ClientCredentials.UserName.Password = "<password>";
        }

        public void TestIncidentQuery()
        {
            try
            {
                QueryIncident("180326-000222");

            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Code);
                Console.WriteLine(ex.Message);
            }
            catch (SoapException ex)
            {
                Console.WriteLine(ex.Code);
                Console.WriteLine(ex.Message);
            }
        }

        private void QueryIncident(string incidentNumber)
        {
            Console.WriteLine("Querying Incident #" + incidentNumber);

            const int MaxRows = 1;

            var queryHeader = new ClientInfoHeader
            {
                AppID = "Query Incident by Reference Number"
            };

            var queryString = $"SELECT Incident FROM Incident WHERE Incident.ReferenceNumber='{incidentNumber}'";

            var incident = new Incident
            {
                StatusWithType = new StatusWithType { Status = new NamedID() },
                Threads = new Thread[] { }
            };

            var queryResultData = this.client.QueryObjects(queryHeader, queryString, new RNObject[] { incident }, MaxRows);

            var rnObjectsResult = queryResultData.First().RNObjectsResult;

            incident = rnObjectsResult.First() as Incident;

            Console.WriteLine("Result Data Length " + queryResultData.Length);

            if (incident != null)
            {
                Console.WriteLine("Incident Subject :" + incident.Subject);
            }
        }
    }
}
