using System;

namespace FunBooksAndVideos.Models.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public ValidationErrorException(string message) : base(message)
        {}
    }
}