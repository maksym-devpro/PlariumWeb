using PlariumDomain.Entities;
using System.Collections.Generic;

namespace ParsingService.Abstraction
{
    public interface IParserLog
    {
        List<LogMessage> ParseLogFile(string fileContent);
        
    }
}
