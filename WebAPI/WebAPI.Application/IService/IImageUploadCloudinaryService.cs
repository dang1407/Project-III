﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IImageUploadCloudinaryService
    {
        Task<string> UploadCloudinary(string url, string content, string contentType);
    }
}
