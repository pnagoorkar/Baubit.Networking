namespace Baubit.Networking.Test.TCPLoopback
{
    public class Test
    {
        [Fact]
        public async Task CreateNewAsync_ShouldCreateConnectedStreams()
        {
            using var loopback = await Baubit.Networking.TCPLoopback.CreateNewAsync();
            Assert.NotNull(loopback.ClientSideStream);
            Assert.NotNull(loopback.ServerSideStream);
            Assert.True(loopback.ClientSideStream.CanRead);
            Assert.True(loopback.ClientSideStream.CanWrite);
            Assert.True(loopback.ServerSideStream.CanRead);
            Assert.True(loopback.ServerSideStream.CanWrite);
        }

        [Fact]
        public async Task ClientToServer_Communication_Works()
        {
            using var loopback = await Baubit.Networking.TCPLoopback.CreateNewAsync();
            var message = "Hello, Server!";
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);

            await loopback.ClientSideStream.WriteAsync(buffer, 0, buffer.Length);
            loopback.ClientSideStream.Flush();

            var readBuffer = new byte[buffer.Length];
            int bytesRead = await loopback.ServerSideStream.ReadAsync(readBuffer, 0, readBuffer.Length);

            Assert.Equal(buffer.Length, bytesRead);
            Assert.Equal(message, System.Text.Encoding.UTF8.GetString(readBuffer));
        }

        [Fact]
        public async Task ServerToClient_Communication_Works()
        {
            using var loopback = await Baubit.Networking.TCPLoopback.CreateNewAsync();
            var message = "Hello, Client!";
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);

            await loopback.ServerSideStream.WriteAsync(buffer, 0, buffer.Length);
            loopback.ServerSideStream.Flush();

            var readBuffer = new byte[buffer.Length];
            int bytesRead = await loopback.ClientSideStream.ReadAsync(readBuffer, 0, readBuffer.Length);

            Assert.Equal(buffer.Length, bytesRead);
            Assert.Equal(message, System.Text.Encoding.UTF8.GetString(readBuffer));
        }

        [Fact]
        public async Task Dispose_ShouldCloseStreams()
        {
            var loopback = await Baubit.Networking.TCPLoopback.CreateNewAsync();
            loopback.Dispose();

            Assert.Throws<ObjectDisposedException>(() => { var _ = loopback.ClientSideStream.ReadByte(); });
            Assert.Throws<ObjectDisposedException>(() => { var _ = loopback.ServerSideStream.ReadByte(); });
        }
    }
}
