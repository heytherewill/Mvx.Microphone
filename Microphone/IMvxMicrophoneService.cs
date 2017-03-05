namespace Microphone
{
	public interface IMvxMicrophoneService
	{
		bool CanRecord { get; }

		bool IsRecording { get; }

		RecordingResult StartRecording(string fileName);

		bool StopRecording();
	}

	public struct RecordingResult
	{
		public RecordingResult(bool success, string path = null)
		{
			Path = path;
			Success = success;
		}

		public bool Success { get; }

		public string Path { get; }

		public static implicit operator RecordingResult(bool value)
			=> new RecordingResult(value);

		public static implicit operator RecordingResult(string value)
			=> new RecordingResult(true, value);
	}
}