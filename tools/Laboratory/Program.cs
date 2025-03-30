// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;

Console.WriteLine("Hello, World!");




using var rsa = RSA.Create(2048);

// export private key
var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

// export public key
var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

Console.WriteLine($"PRIVATE KEY:\n{privateKey}");
Console.WriteLine($"PUBLIC KEY:\n{publicKey}");