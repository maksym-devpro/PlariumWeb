namespace ParsingService.Abstraction
{
    public interface ILogService
    {
        int UploadLogsInDb(string[] fileContent);
    }
}
