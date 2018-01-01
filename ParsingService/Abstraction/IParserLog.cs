using Plarium.Domain.Entities;
using System.Collections.Generic;

namespace ParsingService.Abstraction
{
    public interface IParserLog
    {
         LogMessage[] ParseLogFile(string[] fileContent);
    }
}
