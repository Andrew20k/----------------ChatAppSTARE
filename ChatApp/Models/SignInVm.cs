﻿using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models;

public class SignInVm
{
    [Required]
    public string Username { get; set; }
}