﻿namespace PWManager.Domain.Services.Interfaces {
    public interface ICryptService {
        public string Encrypt(string input);
        public string Decrypt(string input);
        public string Hash(string input);
    }
}