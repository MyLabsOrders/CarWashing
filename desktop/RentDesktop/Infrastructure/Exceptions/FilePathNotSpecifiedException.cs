﻿using System;

namespace RentDesktop.Infrastructure.Exceptions
{
    internal class FilePathNotSpecifiedException : ApplicationException
    {
        public FilePathNotSpecifiedException(string? message = null, Exception? innerException = null)
            : base(message ?? "The path to the file is not specified.", innerException)
        {
        }
    }
}
