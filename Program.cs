using PhoneNumbers;
using System.Globalization;
using System.Text.RegularExpressions;
public class MobileCountryCode
{
    public int code { get; set; }
    public string country { get; set; }
}
public class CountryCode
{
    public static void Main(string[] args)
    {
        int code = 0;
        List<PhoneNumberUtil> countryCodes= new List<PhoneNumberUtil>();
        PhoneNumberUtil phone=PhoneNumberUtil.GetInstance();
        List<MobileCountryCode> mobile = new List<MobileCountryCode>();

        foreach (var regionCode in phone.GetSupportedRegions())
        {
            var countryInfo = phone.GetCountryCodeForRegion(regionCode);
            mobile.Add(new MobileCountryCode { code=countryInfo,country=regionCode});
                code = countryInfo;
            var fullCountryName = GetFullCountryNameByCode(regionCode.ToString());
            Console.WriteLine(countryInfo + " " + regionCode+" "+ fullCountryName);
        }
        Console.WriteLine("Enter Your Mobile Number:");
        string mobileNumber=Console.ReadLine();
        string MobileNumberTrimmed1 = String.Concat(mobileNumber.Where(c => !Char.IsWhiteSpace(c)));
        Console.WriteLine("To be stored as:" + MobileNumberTrimmed1);
        Console.WriteLine("Enter Your Country Code:");
        string countryCode = Console.ReadLine();
        bool isValidNumber = IsPhoneNumberValid(mobileNumber,countryCode);
        Console.WriteLine(isValidNumber);
        if (isValidNumber)
        {
            mobileNumber ="+"+code+"-"+mobileNumber;
            string MobileNumberTrimmed=String.Concat(mobileNumber.Where(c=>!Char.IsWhiteSpace(c)));
            Console.WriteLine("To be stored as:"+MobileNumberTrimmed);
            if(ValidatePhoneNumberWithCountryCode(mobileNumber))
            {

            }
        }
    }
    public static bool IsPhoneNumberValid(string phoneNumber, string countryCode)
    {
        PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
        PhoneNumber number = null;

        try
        {
            number = phoneNumberUtil.Parse(phoneNumber, countryCode);
        }
        catch (Exception e)
        {
            // Invalid number format
            return false;
        }

        if (!phoneNumberUtil.IsValidNumberForRegion(number, countryCode))
        {
            // Number is not valid for the specified country
            return false;
        }

        return true;
    }
    public static string GetFullCountryNameByCode(string countryCode)
    {
        try
        {
            RegionInfo regionInfo = new RegionInfo(countryCode);
            return regionInfo.EnglishName;
        }
        catch (ArgumentException)
        {
            // Handle the case where no matching country was found for the given code
            return "Unknown";
        }
    }
    public static bool ValidatePhoneNumberWithCountryCode(string phoneNumber)
    {
        // Define the regex pattern
        string pattern = @"^\+\d+\d*$"; // Matches a country code with one or more digits

        // Create a Regex object
        Regex regex = new Regex(pattern);

        // Use the IsMatch method to check if the input matches the pattern
        return regex.IsMatch(phoneNumber);
    }

    //public static void Main()
    //{
    //    string phoneNumber1 = "+1234567890"; // Valid
    //    string phoneNumber2 = "1234567890";   // Invalid

    //    bool isValid1 = ValidatePhoneNumberWithCountryCode(phoneNumber1);
    //    bool isValid2 = ValidatePhoneNumberWithCountryCode(phoneNumber2);

    //    Console.WriteLine("Phone Number 1 is valid: " + isValid1);
    //    Console.WriteLine("Phone Number 2 is valid: " + isValid2);
    //}
}