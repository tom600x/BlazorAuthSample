﻿namespace BlazorAuthSample.Models
{
    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool? Active { get; set; }
    }
}
