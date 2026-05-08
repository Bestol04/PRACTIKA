using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CarRentalApp.Models;

namespace CarRentalApp.Services
{
    public class ChatService
    {
        private const string PipeName = "CarRentalChat";
        private NamedPipeServerStream? _pipeServer;
        private CancellationTokenSource? _cancellationTokenSource;
        private readonly List<ChatMessage> _messages;

        public event EventHandler<ChatMessage>? MessageReceived;

        public ChatService()
        {
            _messages = new List<ChatMessage>();
        }

        public async Task StartServerAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            await Task.Run(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        _pipeServer = new NamedPipeServerStream(
                            PipeName,
                            PipeDirection.InOut,
                            NamedPipeServerStream.MaxAllowedServerInstances,
                            PipeTransmissionMode.Byte,
                            PipeOptions.Asynchronous);

                        await _pipeServer.WaitForConnectionAsync(_cancellationTokenSource.Token);

                        using var reader = new StreamReader(_pipeServer, Encoding.UTF8);
                        string? message = await reader.ReadLineAsync();

                        if (!string.IsNullOrEmpty(message))
                        {
                            var chatMessage = JsonSerializer.Deserialize<ChatMessage>(message);
                            if (chatMessage != null)
                            {
                                _messages.Add(chatMessage);
                                MessageReceived?.Invoke(this, chatMessage);
                            }
                        }

                        _pipeServer.Disconnect();
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка сервера чата: {ex.Message}");
                    }
                    finally
                    {
                        _pipeServer?.Dispose();
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        public async Task SendMessageAsync(ChatMessage message)
        {
            try
            {
                using var pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.Out);
                await pipeClient.ConnectAsync(1000);

                var json = JsonSerializer.Serialize(message);
                using var writer = new StreamWriter(pipeClient, Encoding.UTF8);
                await writer.WriteLineAsync(json);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки сообщения: {ex.Message}");
            }
        }

        public List<ChatMessage> GetMessages()
        {
            return new List<ChatMessage>(_messages);
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            _pipeServer?.Dispose();
        }
    }
}