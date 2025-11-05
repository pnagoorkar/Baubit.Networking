# Baubit.Networking

A .NET 9 networking library providing utilities for TCP communication and testing.

## Features

- **TCPLoopback**: A utility class that creates a loopback TCP connection with separate client and server streams for testing and inter-process communication.

## Installation

Install the NuGet package:

```bash
dotnet add package Baubit.Networking
```

## Quick Start

### Creating a TCP Loopback Connection

```csharp
using Baubit.Networking;

// Create a new loopback connection
using var loopback = await TCPLoopback.CreateNewAsync();

// Get the client and server streams
var clientStream = loopback.ClientSideStream;
var serverStream = loopback.ServerSideStream;
```

### Client to Server Communication

```csharp
using var loopback = await TCPLoopback.CreateNewAsync();

// Client sends a message
var message = "Hello, Server!";
var buffer = Encoding.UTF8.GetBytes(message);
await loopback.ClientSideStream.WriteAsync(buffer);
loopback.ClientSideStream.Flush();

// Server receives the message
var readBuffer = new byte[buffer.Length];
int bytesRead = await loopback.ServerSideStream.ReadAsync(readBuffer, 0, readBuffer.Length);
string receivedMessage = Encoding.UTF8.GetString(readBuffer, 0, bytesRead);
```

### Server to Client Communication

```csharp
using var loopback = await TCPLoopback.CreateNewAsync();

// Server sends a message
var message = "Hello, Client!";
var buffer = Encoding.UTF8.GetBytes(message);
await loopback.ServerSideStream.WriteAsync(buffer);
loopback.ServerSideStream.Flush();

// Client receives the message
var readBuffer = new byte[buffer.Length];
int bytesRead = await loopback.ClientSideStream.ReadAsync(readBuffer, 0, readBuffer.Length);
string receivedMessage = Encoding.UTF8.GetString(readBuffer, 0, bytesRead);
```

## API Reference

### TCPLoopback

A loopback TCP connection that simulates both client and server sides for testing purposes.

#### Properties

- **ClientSideStream** (`Stream`) - The stream representing the client side of the TCP loopback connection. Supports reading and writing.
- **ServerSideStream** (`Stream`) - The stream representing the server side of the TCP loopback connection. Supports reading and writing.

#### Methods

- **CreateNewAsync(CancellationToken cancellationToken = default)** - `static async Task<TCPLoopback>`
  
  Asynchronously creates a new TCP loopback instance with connected client and server streams.
  
  **Parameters:**
  - `cancellationToken` - A cancellation token to cancel the operation (optional).
  
  **Returns:** A task that represents the asynchronous operation. The task result contains the created `TCPLoopback` instance.

#### IDisposable

`TCPLoopback` implements `IDisposable`. Always dispose of the instance when done to properly close the underlying streams.

```csharp
using var loopback = await TCPLoopback.CreateNewAsync();
// Use loopback...
// Automatically disposed here
```

## Use Cases

- **Unit Testing**: Test network communication without actual network calls
- **Inter-process Communication**: Mock TCP communication between processes
- **Protocol Testing**: Test protocol implementations with guaranteed delivery
- **Network Simulation**: Simulate client-server interactions in controlled environments

## Testing

Run the test suite:

```bash
dotnet test
```

Tests include:
- Connection creation and stream validation
- Client-to-server communication
- Server-to-client communication
- Resource disposal and cleanup

## Requirements

- .NET 9 or later

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

For bug reports and feature requests, visit the [GitHub repository](https://github.com/pnagoorkar/Baubit.Networking).

---
## Copyright
Copyright (c) Prashant Nagoorkar. See [LICENSE](LICENSE) for details.
