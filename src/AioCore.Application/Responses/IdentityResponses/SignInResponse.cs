﻿namespace AioCore.Application.Responses.IdentityResponses
{
    public class SignInResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}