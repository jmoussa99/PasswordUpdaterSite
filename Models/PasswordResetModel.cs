﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Model: PasswordResetModel.cs
namespace PasswordUpdaterFramework.Models
{
    public class PasswordResetModel
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string repeatPassword { get; set; }
    }
}