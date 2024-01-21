namespace exp.NET6.Services.DBServices
{
    public interface IGenericService
    {
        string GetRankings();
        string? GetImagePath(string? newImgBase64, string? oldImgUrl, string folderName);
        string? GetImgBase64(string? filePath);
        bool ValidateEmail(string emailAddress);
        bool ValidatePhoneNumber(string phoneNumber);
        bool ValidatePostalCode(string postalCode);
        bool ValidateTime(string start, string end);
        bool ValidateDay(string day);
        int GetDayNumber(string day);
        bool ValidateDate(string start, string end);
        string GeneratePassword();

    }
}
