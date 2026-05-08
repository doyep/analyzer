namespace Doyep.Analyzer.Application.Security;

/// <summary>
/// Defines an interface for encryption and decryption services, allowing for secure handling of sensitive data such as tokens or personal information.
/// Implementations of this interface can use various encryption algorithms and techniques to ensure data confidentiality and integrity.
/// </summary>
public interface IEncryptionService
{
    /// <summary>
    /// Encrypts the given string value and returns the encrypted result. The specific encryption method and key management are determined by the implementation of this interface.
    /// </summary>
    string Encrypt(string value);

    /// <summary>
    /// Decrypts the given encrypted string value and returns the original plaintext. The decryption process must correspond to the encryption method used, and proper key management is essential for successful decryption.
    /// </summary>
    string Decrypt(string value);
}
