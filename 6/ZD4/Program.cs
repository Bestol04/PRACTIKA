using System;

public interface IVideoPlayer
{
    void Play(string fileName);
}

public interface IAudioPlayer
{
    void Play(string fileName);
}

public class MediaDevice : IVideoPlayer, IAudioPlayer
{
    void IVideoPlayer.Play(string fileName)
    {
        Console.WriteLine($"Воспроизведение видео: {fileName}");
    }

    void IAudioPlayer.Play(string fileName)
    {
        Console.WriteLine($"Воспроизведение аудио: {fileName}");
    }
}

public class Program
{
    public static void Main()
    {
        MediaDevice device = new MediaDevice();

        IVideoPlayer videoPlayer = device;
        IAudioPlayer audioPlayer = device;

        videoPlayer.Play("movie.mp4");
        audioPlayer.Play("song.mp3");
    }
}