using ParsingService.Abstraction;
using System;
using System.Collections.Generic; 
using Plarium.Domain.Entities;

namespace ParsingService
{
    public class ParserLog : IParserLog
    {
        public List<LogMessage> ParseLogFile(string fileContent)
        {
            throw new NotImplementedException();
        }
    }
}
