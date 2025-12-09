using System;
using System.Linq;

namespace IdCraft.Cimd.Validation
{
    /// <summary>
    /// Default implementation of <see cref="IClientIdentifierValidator"/> that validates
    /// client identifiers according to the OAuth Client ID Metadata Document specification.
    /// </summary>
    public sealed class ClientIdentifierValidator : IClientIdentifierValidator
    {
        /// <inheritdoc />
        public bool IsValid(string clientIdentifier)
        {
            if (string.IsNullOrWhiteSpace(clientIdentifier))
            {
                return false;
            }

            if (!Uri.TryCreate(clientIdentifier, UriKind.Absolute, out Uri uri))
            {
                return false;
            }

            if (!string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (string.IsNullOrEmpty(uri.AbsolutePath) || uri.AbsolutePath == "/")
            {
                return false;
            }

            // Path Segment Check - MUST NOT contain single-dot or double-dot path segments
            // Check the original string since Uri normalizes these
            if (clientIdentifier.Contains("/./") 
                || clientIdentifier.Contains("/../")
                || clientIdentifier.EndsWith("/.")
                || clientIdentifier.EndsWith("/.."))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(uri.Fragment))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(uri.UserInfo))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(uri.Query))
            {
                return false;
            }

            return true;
        }
    }
}
