﻿namespace Zeus.Demo.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException() : base()
        {

        }
    }
}
