using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

public class InvalidDateFormatException : Exception
{
    public InvalidDateFormatException(string text)
        : base($"Invalid date format: {text}. Expected format: dd/MM/yyyy")
    {
    }
}

public class CustomDateTimeConverter : ITypeConverter
{
    public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (DateTime.TryParseExact(text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        if (DateTime.TryParseExact(text, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result1))
        {
            return result1;
        }

        if (DateTime.TryParseExact(text, "dd/M/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result2))
        {
            return result2;
        }
        if (DateTime.TryParseExact(text, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result3))
        {
            return result3;
        }
        // If parsing fails, throw a custom exception
        throw new InvalidDateFormatException(text);
    }

    public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        // Implement conversion to string if needed
        throw new NotImplementedException();
    }
}
