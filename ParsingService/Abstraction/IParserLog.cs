using Plarium.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ParsingService.Abstraction
{
    public interface IParserLog
    {
        List<LogMessage> ParseLogFile(string[] fileContent, Progress<int> progress); 
    }
}
