namespace score
{
    using System;

    public class SkipFileException : Exception
    {
        public SkipFileException()
        {
        }

        public SkipFileException(string message)
            : base(message)
        {
        }

        public SkipFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}