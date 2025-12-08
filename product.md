# Product

## Overview

This project provides a .NET implementation of the **Client ID Metadata Document (CIMD)** protocol, as defined in the [OAuth Client ID Metadata Document specification](https://datatracker.ietf.org/doc/draft-ietf-oauth-client-id-metadata-document/). The implementation is distributed as a set of NuGet packages designed to enable Duende Identity Server (and in general other OAuth authorization servers implemented in .NET) to dynamically discover OAuth clients via a standardized client metadata document endpoint.

## Purpose

The CIMD protocol allows OAuth clients to publish metadata about themselves through an endpoint, enabling authorization servers and other parties to discover client information dynamically. This implementation provides:

- Core CIMD protocol functionality:
    - And implementation of an HTTP client that fetches the OAuth client metadata document
    - OAuth client metadata document schema & content validation
- Duende Identity Server integration