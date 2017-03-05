namespace Microphone
{
	public interface IMvxMicrophoneService
	{
		bool CanRecord { get; }

		bool IsRecording { get; }

		bool StartRecording(string destination);

		bool StopRecording();
	}
}