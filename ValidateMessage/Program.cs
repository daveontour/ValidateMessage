using System;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Schema;


namespace ValidateMessage
{
    internal class Program
    {
        static string xml = @"<aidx:IATA_AIDX_FlightLegRQ
            xmlns:aidx=""http://www.iata.org/IATA/2007/00"" 
            CorrelationID=""Oct202210101326testabc1""
            SequenceNmbr=""0""
            Target=""Production""
            TimeStamp=""2022-10-10T10:14:24Z""
            TransactionIdentifier=""QREIX"" Version=""16.1""
            xmlns:aip=""http://www.sita.aero/aip/XMLSchema"">  
            <aidx:TPA_Extension>
                        <aip:FlightDataCondition>
                                    <aip:FlightIDCondition>
                                                <aip:STOCondition>
                                                            <aip:Between>
                                                                        <aip:RangeFrom>2022-10-10T00:00:00+03:00</aip:RangeFrom>
                                                                        <aip:RangeTo>2022-10-10T23:59:00+03:00</aip:RangeTo>
                                                            </aip:Between>
                                                </aip:STOCondition>
                                    </aip:FlightIDCondition>
                        </aip:FlightDataCondition>
            </aidx:TPA_Extension>
</aidx:IATA_AIDX_FlightLegRQ>

";
        static void Main(string[] args)
        {
            try
            {
                XDocument xmlDocument = XDocument.Parse(xml);
  

                Console.WriteLine("Message XML: \n\n");
                Console.WriteLine(xmlDocument.ToString().PrintXML());

                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("http://www.iata.org/IATA/2007/00", @"XSD\IATA_AIDX_FlightLegRQ.xsd");
                schemas.Add("http://www.sita.aero/aip/XMLSchema", @"XSD\AIPComplexTypes.xsd");
                schemas.Add("http://www.sita.aero/aip/XMLSchema", @"XSD\AIPSimpleTypes.xsd");

                bool errors = false;
                xmlDocument.Validate(schemas, (o, e) =>
                {
                    Console.WriteLine("{0}", e.Message);
                    errors = true;
                });
                if (!errors) Console.WriteLine("\nXML Sample validated");
                if (errors) Console.WriteLine("\nXML Sample did not validated");

                Console.WriteLine("\nPress Any Key to Exit");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
