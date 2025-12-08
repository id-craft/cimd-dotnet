# Architecture

## Project Structure

### IdCraft.Cimd (Core Library)

**Target Framework:** .NET Standard 2.0

**Responsibilities:**
- **Protocol Implementation**: Core CIMD protocol logic independent of any specific authorization server framework
- **Data Models**: Strongly-typed models representing CIMD metadata documents
- **Serialization**: JSON serialization and deserialization of metadata documents
- **Validation**: Validation of client metadata according to CIMD specification
- **Discovery Logic**: Implementation of the well-known endpoint discovery mechanism
- **Extensibility**: Interfaces and base classes for custom implementations

**Key Components:**
- Metadata document model (client metadata fields)
- HTTP client to fetch the OAuth client metadata document (with rate-limiting & timeout)
- OAuth client metadata document validator:
    - Schema validator
    - Content validator

### IdCraft.Cimd.DuendeIdentityServer (Integration Library)

**Target Framework:** .NET Standard 2.0

**Responsibilities:**
- **Duende Identity Server Integration**: Seamless integration with Duende Identity Server
- **Configuration**: Extension methods for configuring CIMD in Duende IS applications

**Key Components:**
- `ClientIdMetadataDocumentClientStore`: a `IClientStore` implementation that uses the HTTP client from `IdCraft.Cimd` to fetch the OAuth client metadata document, validate it and map it to the Duende Identity Server client model
- Extention method to configure and register the `ClientIdMetadataDocumentClientStore` and its dependencies

## Design Principles

### 1. Separation of Concerns
The architecture separates the core CIMD protocol implementation from OAuth server-specific integrations. This allows:
- `IdCraft.Cimd` library to be reused across different authorization server frameworks
- `IdCraft.DuendeIdentityServer` library to provide Duende Identity Server specific integration
- `IdCraft.{OAuthServerName}` future libraries to provide integration with more OAuth server implementations in .NET 

### 2. Standards Compliance
All implementations strictly adhere to the [OAuth Client ID Metadata Document specification](https://datatracker.ietf.org/doc/draft-ietf-oauth-client-id-metadata-document/).

### 3. Extensibility
The architecture supports extensibility through:
- Interface-based design for key components
- Configuration options for customization
- Extension points for custom metadata fields
- Pluggable validation and serialization

### 4. Integration Simplicity
The integration library (`IdCraft.Cimd.DuendeIdentityServer`) provides:
- Simple, fluent configuration API
- Minimal boilerplate code
- Convention-based setup with override capabilities
- Seamless integration with existing Duende IS applications

## Dependencies

### IdCraft.Cimd
- .NET Standard 2.0 base class libraries
- Minimal external dependencies (JSON serialization library)

### IdCraft.Cimd.DuendeIdentityServer
- IdCraft.Cimd (core library)
- Duende IdentityServer packages
- ASP.NET Core abstractions

## Deployment Model

### NuGet Package Distribution

**IdCraft.Cimd**
- Published as standalone NuGet package
- Can be referenced directly for custom integrations
- No runtime dependencies on web frameworks

**IdCraft.Cimd.DuendeIdentityServer**
- Published as separate NuGet package
- Requires IdCraft.Cimd package
- Requires Duende IdentityServer installation

### Installation Pattern

For Duende Identity Server integration:
```
Install-Package IdCraft.Cimd.DuendeIdentityServer
```
(This automatically includes IdCraft.Cimd as a dependency)

For custom integrations:
```
Install-Package IdCraft.Cimd
```

## Extensibility Points

### Custom Integrations
Developers can build custom integrations for other authorization servers by:
1. Referencing the core `IdCraft.Cimd` package
2. Implementing framework-specific middleware/handlers
3. Mapping framework concepts to CIMD abstractions
4. Following the pattern established by the Duende IS integration

### Custom Metadata Fields
The architecture supports extension fields beyond the standard CIMD specification:
- Custom metadata properties
- Custom validation rules
- Custom serialization behavior

### Storage Implementations
The design allows for various client metadata storage backends:
- Database-backed stores
- In-memory stores
- External API-based stores
- Custom implementations

## Security Considerations

- **HTTPS Enforcement**: CIMD endpoints should be served over HTTPS in production
- **Access Control**: Optional authentication/authorization for metadata endpoints
- **Validation**: Strict validation of client metadata to prevent injection attacks
- **Rate Limiting**: Recommended rate limiting on discovery endpoints
- **CORS**: Configurable CORS policies for cross-origin access

## Future Extensibility

The architecture is designed to accommodate:
- Additional authorization server integrations (e.g., IdentityServer4, custom servers)
- Protocol extensions and updates
- Additional metadata document formats
- Advanced caching strategies
- Monitoring and telemetry integration

## Version Compatibility

- **Target Framework**: .NET Standard 2.0 for maximum compatibility
- **Duende IdentityServer**: Compatible with Duende IdentityServer 6.x and later
- **ASP.NET Core**: Compatible with ASP.NET Core 3.1 and later (via integration package)
