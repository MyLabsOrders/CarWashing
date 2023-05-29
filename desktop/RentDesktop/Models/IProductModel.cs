﻿using Avalonia.Media.Imaging;
using System;

namespace RentDesktop.Models
{
    public interface IProductModel
    {
        string ID { get; }
        string Name { get; }
        string Company { get; }
        double Price { get; }
        DateTime CreationDate { get; }
        Bitmap? Icon { get; }
    }
}