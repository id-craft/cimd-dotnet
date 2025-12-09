namespace IdCraft.Cimd.Validation
{
    /// <summary>
    /// Validates client identifiers according to the OAuth Client ID Metadata Document specification.
    /// </summary>
    /// <remarks>
    /// Client identifier URLs MUST:
    /// - Have an "https" scheme
    /// - Contain a path component
    /// - NOT contain single-dot or double-dot path segments
    /// - NOT contain a fragment component
    /// - NOT contain a username or password
    /// - NOT include a query string component (enforced as strict requirement)
    /// 
    /// Client identifier URLs MAY contain a port.
    /// </remarks>
    public interface IClientIdentifierValidator
    {
        /// <summary>
        /// Validates a client identifier URL against the CIMD specification requirements.
        /// </summary>
        /// <param name="clientIdentifier">The client identifier URL to validate.</param>
        /// <returns>
        /// <c>true</c> if the client identifier is valid according to the specification; 
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method uses a fail-fast approach and returns <c>false</c> on the first
        /// validation failure encountered.
        /// </remarks>
        bool IsValid(string clientIdentifier);
    }
}
