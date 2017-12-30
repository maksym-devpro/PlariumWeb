using PlariumDomain.Entities;
using System.Collections.Generic;

namespace ParsingService.Abstract
{
    public interface IParserLog
    {
        List<LogMessage> ParseLogFile(string fileContent);
        
    }
}
