using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace Microphone
{
	public abstract class BaseMvxMicrophoneService : IMvxMicrophoneService
	{
		public bool CanRecord => NativeCanRecord;

		public bool IsRecording { get; set; }

		public RecordingResult StartRecording(string fileName)
		{
			if (!CanRecord) return LogAndFail("Device does not support recording");
			if (IsRecording) return LogAndFail("Tried to start recording while microphone was already in use");

			var recordingResult = NativeStartRecording(fileName);
			IsRecording = recordingResult.Success;

			return recordingResult;                      
		}

		public bool StopRecording()
		{
			if (!CanRecord) return LogAndFail("Device does not support recording");
			if (!IsRecording) return LogAndFail("Tried to stop recording while microphone was not being used");

			IsRecording = !NativeStopRecording();

			return !IsRecording;
		}

		private bool LogAndFail(string message)
		{
			Mvx.Trace(MvxTraceLevel.Error, message);
			return false;
		}

		protected abstract bool NativeCanRecord { get; }

		protected abstract RecordingResult NativeStartRecording(string fileName);

		protected abstract bool NativeStopRecording();
	}
}