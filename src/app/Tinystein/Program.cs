using System;
using System.Threading;
using Edelstein.Common.Network.Netty.Transport;
using Edelstein.Protocol.Network.Transport;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Tinystein.Logging;

namespace Tinystein
{
    class Program
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        /// <param name="type">The transport type</param>
        /// <param name="host">The host where Tinystein will start on</param>
        /// <param name="port">The port where Tinystein will start on</param>
        /// <param name="version">The game server protocol version</param>
        /// <param name="patch">The game server protocol patch</param>
        /// <param name="locale">The game server protocol locale</param>
        static void Main(
            TinysteinTransportType type = TinysteinTransportType.Server,
            string host = "127.0.0.1",
            int port = 8484,
            short version = 95,
            string patch = "1",
            byte locale = 8
        )
        {
            var token = new CancellationTokenSource();
            var factory = new TinysteinSocketHandlerFactory();
            var transport = (ITransport)(type switch
            {
                TinysteinTransportType.Client => new NettyTransportClient(factory, version, patch, locale),
                TinysteinTransportType.Server => new NettyTransportServer(factory, version, patch, locale),
                _ => throw new NotImplementedException(),
            });

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Console(theme: AnsiConsoleTheme.Code)
               .CreateLogger();

            transport.Start(host, port).Wait();
            Logger.Info($"Started Tinystein {type} v{version}.{patch} (locale: {locale})");

            Console.CancelKeyPress += delegate { token.Cancel(); };

            while (!token.IsCancellationRequested)
            {
            }
        }
    }
}
