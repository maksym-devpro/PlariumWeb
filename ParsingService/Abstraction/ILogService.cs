namespace ParsingService.Abstraction
{
    public interface ILogService
    {
        bool UploadLogsInDb(string fileContent);
    }
}
