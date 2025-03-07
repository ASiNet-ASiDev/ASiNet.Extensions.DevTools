using System.Diagnostics;

namespace ASiNet.Extensions.DevTools.DebugTools;
public class SpeedTest : IDisposable
{
    public SpeedTest(Action<SpeedTestResult> action, bool autorun = false)
    {
        Autorun = autorun;
        _resultAction = action;
        _sw = new();
        if (autorun)
            _sw.Start();
    }

    public static SpeedTest New(Action<SpeedTestResult> action, bool autorun = false) =>
        new(action, autorun);

    public void Run()
    {
        if (Autorun)
            return;
        _sw!.Start();
    }

    public bool Autorun { get; }

    private Stopwatch? _sw;

    private Action<SpeedTestResult>? _resultAction;

    public void Dispose()
    {
        _sw!.Stop();
        _resultAction?.Invoke(new(_sw.ElapsedMilliseconds, _sw.ElapsedTicks, _sw.Elapsed));
        _resultAction = null;
        _sw = null;
        GC.SuppressFinalize(this);
    }
}

public struct SpeedTestResult
{
    public SpeedTestResult(long ms, long tk, TimeSpan time)
    {
        Milliseconds = ms;
        Ticks = tk;
        Time = time;
    }
    public long Milliseconds { get; }
    public long Ticks { get; }

    public TimeSpan Time { get; }

}