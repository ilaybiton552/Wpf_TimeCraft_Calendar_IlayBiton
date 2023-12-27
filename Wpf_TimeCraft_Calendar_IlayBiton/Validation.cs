using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    public class ValidName : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = value.ToString();
                if (name.Length < Min) // name too short
                    return new ValidationResult(false, "Too short");
                if (name.Length > Max) // name too long
                    return new ValidationResult(false, "Too long");
                foreach (char c in name)
                    if (!Char.IsLetter(c) && c != ' ')
                        return new ValidationResult(false, "Only letters and spaces");
                if (!Char.IsUpper(name[0]))
                    return new ValidationResult(false, "Name must start with capital letter");
                if (!Char.IsLetter(name[name.Length - 1]))
                    return new ValidationResult(false, "Name can't end with space");
                if (name.IndexOf("  ") != -1)
                    return new ValidationResult(false, "Name must not include more than one space at a time");
            }
            catch (Exception ex) 
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidUsername : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string username = value.ToString();
                bool lower = false, upper = false;
                if (username.Length < Min) // username too short
                    return new ValidationResult(false, "Too short");
                if (username.Length > Max) // username too long
                    return new ValidationResult(false, "Too long");
                foreach (char c in username)
                    if (!Char.IsLetterOrDigit(c) && c != '_')
                        return new ValidationResult(false, "Only letters, numbers and underscore");
                    else
                        if (Char.IsLower(c)) lower = true;
                        else if (Char.IsUpper(c)) upper = true;
                if (!Char.IsLetter(username[0]))
                    return new ValidationResult(false, "Username must start with a letter");
                if (!lower) return new ValidationResult(false, "Username must contain lower letter");
                if (!upper) return new ValidationResult(false, "Username must contain upper letter");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string password = value.ToString();
                bool lower = false, upper = false, number = false;
                char[] specialChars = { '_', '-', '@', '#' };
                if (password.Length < 6) // password too short
                    return new ValidationResult(false, "Too short");
                if (password.Length > 15) // password too long
                    return new ValidationResult(false, "Too long");
                for (int i = 0; i < password.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(password[i]) && !specialChars.Contains(password[i]))
                        return new ValidationResult(false, "Only letters, numbers and: '_', '-', '@', '#'");
                    else
                    {
                        if (Char.IsLower(password[i]))
                            lower = true;
                        else if (Char.IsUpper(password[i]))
                            upper = true;
                        else
                            number = true;
                    }
                }
                if (!lower)
                    return new ValidationResult(false, "Password must include at least one lower letter");
                if (!upper)
                    return new ValidationResult(false, "Password must include at least one capital letter");
                if (!number)
                    return new ValidationResult(false, "Password must include at least one number");
                if (!Char.IsLetter(password[0]))
                    return new ValidationResult(false, "Password must start with a letter");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string email = value.ToString();
                Regex reg = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9._]+\.[a-zA-Z]{2,}$");
                if (!reg.IsMatch(email))
                    return new ValidationResult(false, "Email format: example@gmail.com");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidPhoneNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string phoneNumber = value.ToString();
                Regex reg = new Regex(@"^\d+$");
                if (!reg.IsMatch(phoneNumber))
                    return new ValidationResult(false, "Only numbers allowed");
                if (phoneNumber[0] != '0')
                    return new ValidationResult(false, "Phone number must start with 0");
                if (phoneNumber.Length != 10)
                    return new ValidationResult(false, "Phone number must be have 10 numbers");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidBirthday : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string birthday = value.ToString();
                MessageBox.Show(birthday);
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

}
