using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyRegistrySearch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // GSIS SOAP endpoint
                string url = "https://www1.gsis.gr/wsaade/RgWsPublic2/RgWsPublic2";

                // SOAP request content
                // Replace UsernameExample, PasswordExample and AFMExample with your own credentials
                string soapRequest = @"
                <env:Envelope xmlns:env=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns1=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:ns2=""http://rgwspublic2/RgWsPublic2Service"" xmlns:ns3=""http://rgwspublic2/RgWsPublic2"">
                    <env:Header>
                        <ns1:Security>
                            <ns1:UsernameToken>
                                <ns1:Username>UsernameExample</ns1:Username>
                                <ns1:Password>PasswordExample</ns1:Password>
                            </ns1:UsernameToken>
                        </ns1:Security>
                    </env:Header>
                    <env:Body>
                        <ns2:rgWsPublic2AfmMethod>
                            <ns2:INPUT_REC>
                                <ns3:afm_called_by/>
                                <ns3:afm_called_for>AFMExample</ns3:afm_called_for>
                                <ns3:as_on_date>2024-07-01</ns3:as_on_date>
                            </ns2:INPUT_REC>
                        </ns2:rgWsPublic2AfmMethod>
                    </env:Body>
                </env:Envelope>";

                // Create HttpClient
                using HttpClient httpClient = new();

                // Set the content type
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/soap+xml;charset=UTF-8");

                // Send the SOAP request
                HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(soapRequest, Encoding.UTF8, "application/soap+xml"));

                // Read the response
                string soapResponse = await response.Content.ReadAsStringAsync();

                // Save SOAP response to a file
                await File.WriteAllTextAsync("response.xml", soapResponse);

                Console.WriteLine("SOAP request executed successfully. XML response saved to response.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
