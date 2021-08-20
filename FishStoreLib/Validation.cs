using System;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Validation
{
    public static Boolean IsValidAddress(string emailid)
    {
        if (String.IsNullOrEmpty(emailid))
        {
            return false;
        }
        else
        {
            Regex regex = new Regex(@"^[\w0-9._%+-]+@[\w0-9.-]+\.[\w]{2,6}$");
            return regex.IsMatch(emailid);
        }
    }

    public static Boolean IsValidPassword(string pass)
    {
        int validConditions = 0;
        if (String.IsNullOrEmpty(pass))
        {
            return false;
        }
        else
        {
            foreach (char c in pass)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in pass)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in pass)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] specialSymbol = { '@', '#', '$', '^', '&', '+', '=' };
                if (pass.IndexOfAny(specialSymbol) == -1) return false;
            }
            return true;
        }
    }

    public static Boolean IsNameNull(string name)
    {
        return String.IsNullOrEmpty(name);
    }

}





