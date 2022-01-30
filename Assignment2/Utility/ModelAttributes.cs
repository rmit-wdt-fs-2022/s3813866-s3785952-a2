using System.ComponentModel.DataAnnotations;

namespace Assignment2.Utility;

public class ModelAttributes
{
        
    /// <summary>
    /// Utilities Check for model to see if string is a positive number
    /// </summary>
    public class IsAPositiveNumber : RegularExpressionAttribute
    {
        public IsAPositiveNumber(string pattern) : base(pattern) { }
    }

    /// <summary>
    /// Utilities Check for model to see if string is a number
    /// </summary>
    public class IsANumber : RegularExpressionAttribute
    {
        public IsANumber(string pattern) : base(pattern) { }
    }

    public class IsAName : RegularExpressionAttribute
    {
        public IsAName(string pattern) : base(pattern) { }
    }
    
    public class IsAAddress : RegularExpressionAttribute
    {
        public IsAAddress(string pattern) : base(pattern) { }
    }
    
    public class IsASuburb : RegularExpressionAttribute
    {
        public IsASuburb(string pattern) : base(pattern) { }
    }
    
    public class IsAState : RegularExpressionAttribute
    {
        public IsAState(string pattern) : base(pattern) { }
    }
    public class IsAPostCode : RegularExpressionAttribute
    {
        public IsAPostCode(string pattern) : base(pattern) { }
    }

    public class IsAPhoneNumber : RegularExpressionAttribute
    {
        public IsAPhoneNumber(string pattern) : base(pattern) { }
    }
    
    
    
}