using System.Net;
using System.Net.Sockets;

namespace Baubit.Networking
{
    /// <summary>
    /// Provides a loopback TCP connection with separate client and server streams for testing or inter-process communication.
    /// </summary>
    public class TCPLoopback : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Gets the stream representing the client side of the TCP loopback connection.
        /// </summary>
        public Stream ClientSideStream { get; init; }

        /// <summary>
        /// Gets the stream representing the server side of the TCP loopback connection.
        /// </summary>
        public Stream ServerSideStream { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TCPLoopback"/> class with the specified client and server streams.
        /// </summary>
        /// <param name="clientSideStream">The stream for the client side.</param>
        /// <param name="serverSideStream">The stream for the server side.</param>
        private TCPLoopback(Stream clientSideStream,
                            Stream serverSideStream)
        {
            ClientSideStream = clientSideStream;
            ServerSideStream = serverSideStream;
        }

        /// <summary>
        /// Asynchronously creates a new <see cref="TCPLoopback"/> instance with connected client and server streams.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="TCPLoopback"/> instance.</returns>
        public static async Task<TCPLoopback> CreateNewAsync(CancellationToken cancellationToken = default)
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;

            var client = new TcpClient();
            var connectTask = client.ConnectAsync(IPAddress.Loopback, port, cancellationToken);
            var clientProxy = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);
            await connectTask.ConfigureAwait(false);
            listener.Stop();

            return new TCPLoopback(client.GetStream(), clientProxy.GetStream());
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="TCPLoopback"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ClientSideStream.Dispose();
                    ServerSideStream.Dispose();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="TCPLoopback"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
