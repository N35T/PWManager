﻿using PWManager.Domain.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using PWManager.Application.Context;
using PWManager.Application.Exceptions;

namespace PWManager.Application.Services {
    public class CryptService : ICryptService {

        private readonly ICryptEnvironment _env;
        
        public CryptService(ICryptEnvironment env) {
            _env = env;
        }

        public void Encrypt(ISecureProperties input, string key) {
            foreach ((var getter, var setter) in input.SecurableProperties()) {
                var val = Encrypt(getter());
                setter(val);
            }
        }

        public void Decrypt(ISecureProperties input, string key) {
            foreach ((var getter, var setter) in input.SecurableProperties()) {
                var val = Decrypt(getter());
                setter(val);
            }
        }

        public string Decrypt(string input) {
            if (_env.EncryptionKey is null) {
                throw new InvalidOperationException(MessageStrings.ENCRYPT_KEY_NULL);
            }
            
            var inputBytes = Convert.FromBase64String(input);

            using var aes = CreateAesWith(_env.EncryptionKey);
            var iv = inputBytes[0..16];
            var plainBytes = aes.DecryptCfb(inputBytes[16..], iv);

            return GetStringFrom(plainBytes);
        }

        public string Encrypt(string input) {
            if (_env.EncryptionKey is null) {
                throw new InvalidOperationException(MessageStrings.ENCRYPT_KEY_NULL);
            }
            
            var inputBytes = GetBytesFrom(input);

            using var aes = CreateAesWith(_env.EncryptionKey);
            aes.GenerateIV();
            var iv = aes.IV;

            var cipherBytes = aes.EncryptCfb(inputBytes, iv);
            var cipherWithIvHeading = iv.Concat(cipherBytes).ToArray();

            return Convert.ToBase64String(cipherWithIvHeading);
        }

        public string Hash(string input, string salt) {
            var inputBytes = GetBytesFrom(input);
            var saltBytes = GetBytesFrom(salt);

            var saltedInput = inputBytes.Concat(saltBytes).ToArray();
            var hashBytes = SHA512.HashData(saltedInput);

            return Convert.ToBase64String(hashBytes);
        }

        public string DeriveKeyFrom(string input, string salt) {
            var inputBytes = GetBytesFrom(input);
            var saltBytes = GetBytesFrom(salt);

            var hashBytes = Rfc2898DeriveBytes.Pbkdf2(inputBytes, saltBytes, 600_000, HashAlgorithmName.SHA256, 32);
            
            return GetStringFrom(hashBytes);
        }

        private static Aes CreateAesWith(string key) {
            var keyBytes = GetBytesFrom(key);

            var aes = Aes.Create();
            aes.Key = keyBytes;

            return aes;
        }

        public string GenerateSalt() {
            return Guid.NewGuid().ToString();
        }
        
        private static byte[] GetBytesFrom(string input) => Encoding.ASCII.GetBytes(input);
        private static string GetStringFrom(byte[] bytes) => Encoding.ASCII.GetString(bytes);
    }
}

