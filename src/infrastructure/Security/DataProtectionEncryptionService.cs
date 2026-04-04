using Doyep.Analyzer.Application.Security;

using Microsoft.AspNetCore.DataProtection;

namespace Doyep.Analyzer.Infrastructure.Security;

/// <summary>
/// Implements the IEncryptionService interface using ASP.NET Core's data protection APIs to provide encryption and decryption functionality for sensitive data.
/// This service can be used to securely handle tokens, personal information, or any other data that requires confidentiality.
/// The specific encryption and decryption logic will be determined by the implementation of the Encrypt and Decrypt methods, utilizing the IDataProtectionProvider for key management and cryptographic operations.
/// </summary>
/// <param name="provider"></param>
public class DataProtectionEncryptionService(IDataProtectionProvider provider) : IEncryptionService
{
    private readonly IDataProtector _protector = provider.CreateProtector("strava-tokens");

    /// <inheritdoc/>
    public string Decrypt(string value) => _protector.Unprotect(value);

    /// <inheritdoc/>
    public string Encrypt(string value) => _protector.Protect(value);
}
