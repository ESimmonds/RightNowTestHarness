
using System;

using System.Net;

namespace RightNowTestHarness
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.WriteLine("Starting RightNow Test Harness");

            RightNowTestHarness rightNowTestHarness = new RightNowTestHarness();
            rightNowTestHarness.TestIncidentQuery();

        }
    }
}
