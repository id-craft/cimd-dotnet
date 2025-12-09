
### Dependencies

**IdCraft.Cimd project:**
- No new dependencies required (uses `System.Uri` from .NET Standard 2.0)
- May need: `Microsoft.Extensions.DependencyInjection.Abstractions` (verify - see Open Questions)

**Test project (new):**
- xUnit (industry standard for .NET)
- NSubstitute
- AutoFixture
- AutoFixture.xUnit
- AutoFixture.NSubstitute
- Microsoft.NET.Test.Sdk

## Tasks

### Phase 1: Core Implementation

- [ ] Create `Validation` namespace folder in `IdCraft.Cimd` project
- [ ] Define `IClientIdentifierValidator` interface
  - [ ] Add XML documentation comments
  - [ ] Define `bool IsValid(string clientIdentifier)` method
- [ ] Implement `ClientIdentifierValidator` class
  - [ ] Add null/empty validation
  - [ ] Add URI parsing with exception handling
  - [ ] Implement HTTPS scheme validation
  - [ ] Implement path component validation
  - [ ] Implement path segment validation (no `.` or `..`)
  - [ ] Implement fragment validation (must not contain)
  - [ ] Implement user info validation (must not contain username/password)
  - [ ] Implement query string validation (must not contain)
  - [ ] Add comprehensive XML documentation
  - [ ] Follow coding style guidelines (Allman braces, naming conventions)

### Phase 2: Dependency Injection Setup

- [ ] Create `Extensions` namespace folder in `IdCraft.Cimd` project
- [ ] Create `ServiceCollectionExtensions` class
  - [ ] Implement `AddClientIdentifierValidator()` extension method
  - [ ] Register `IClientIdentifierValidator` as singleton
  - [ ] Add XML documentation for extension method
- [ ] Update `.csproj` if needed for DI abstractions (verify .NET Standard 2.0 includes `Microsoft.Extensions.DependencyInjection.Abstractions`)

### Phase 3: Unit Tests

- [ ] Create `tests/IdCraft.Cimd.Tests` project
  - [ ] Create test project file targeting .NET Standard 2.0 compatible framework
  - [ ] Add NuGet packages mentioned above
  - [ ] Add project reference to `IdCraft.Cimd`
- [ ] Create `Validation` test folder
- [ ] Create `ClientIdentifierValidatorTests` class
  - [ ] **Valid scenarios:**
    - [ ] Test valid HTTPS URL with path
    - [ ] Test valid URL with port
    - [ ] Test valid URL with multiple path segments
  - [ ] **Invalid scenarios - MUST violations:**
    - [ ] Test null/empty/whitespace input
    - [ ] Test malformed URL
    - [ ] Test HTTP scheme (not HTTPS)
    - [ ] Test missing path component
    - [ ] Test single-dot path segment (`https://example.com/./path`)
    - [ ] Test double-dot path segment (`https://example.com/../path`)
    - [ ] Test fragment component (`https://example.com/path#fragment`)
    - [ ] Test username in URL (`https://user@example.com/path`)
    - [ ] Test password in URL (`https://user:pass@example.com/path`)
  - [ ] **Invalid scenarios - SHOULD NOT violations:**
    - [ ] Test query string component (`https://example.com/path?query=value`)
  - [ ] **Edge cases:**
    - [ ] Test URL with trailing slash
    - [ ] Test URL with encoded characters in path
    - [ ] Test URL with non-standard port
    - [ ] Test very long but valid URL

### Phase 4: Integration & Documentation

- [ ] Add solution reference for test project to `.sln` file
- [ ] Update architecture.md to document validator component
- [ ] Verify all code follows coding-style.md guidelines
- [ ] Run tests and ensure all pass
- [ ] Format code using dotnet-format tool

### Phase 5: Future Integration (Out of Scope - Noted for Reference)

- [ ] Integrate validator into `ClientIdMetadataDocumentClientStore` (separate task)
- [ ] Add logging for validation failures (separate task)
- [ ] Consider configuration options for strict/permissive mode (future enhancement)

## Open Questions

### 1. DI Package Availability
**Question:** Does .NET Standard 2.0 include `Microsoft.Extensions.DependencyInjection.Abstractions` by default, or do we need to add it as a NuGet package?

**Context:** The `ServiceCollectionExtensions` class needs `IServiceCollection` from this namespace.

**Resolution Options:**
- Check .NET Standard 2.0 API surface
- Add as NuGet package if not included (likely needed)
- Verify version compatibility with Duende IdentityServer requirements

### 2. Test Framework Target
**Question:** What .NET target framework should the test project use?

**Context:** The library targets .NET Standard 2.0, but test projects typically target concrete frameworks like .NET 6.0, .NET 8.0, etc.

**Resolution Options:**
- Use .NET 8.0 (latest LTS)
- Use .NET 6.0 (older LTS for broader compatibility)
- Multi-target both

### 3. Path Segment Validation Complexity
**Question:** Should path segment validation only check for literal `.` and `..` segments, or also handle encoded versions like `%2e` and `%2e%2e`?

**Context:** The spec says "MUST NOT contain single-dot or double-dot path segments". The `System.Uri` class may normalize these automatically.

**Resolution Options:**
- Rely on `Uri` class normalization behavior
- Add explicit checks for encoded versions
- Test `Uri` behavior and document findings