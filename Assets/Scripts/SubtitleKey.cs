public class SubtitleKey {

    private readonly float _timeStamp;
    private readonly string _subtitle;

    public SubtitleKey(float timeStamp, string subtitle)
    {
        _timeStamp = timeStamp;
        _subtitle = subtitle;
    }

    public float GetTimeStamp()
    {
        return _timeStamp;
    }

    public string GetSubtitle()
    {
        return _subtitle;
    }
}
