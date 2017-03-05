using System;
using AVFoundation;
using Foundation;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace Microphone.iOS
{
	public class MvxMicrophoneService : BaseMvxMicrophoneService
	{
		private readonly AudioSettings audioSettings = new AudioSettings(new NSDictionary(
			//AVAudioSettings.AVFormatIDKey, kAudioFormatAppleLossless,
	 		AVAudioSettings.AVEncoderAudioQualityKey, AVAudioQuality.Max,
			AVAudioSettings.AVEncoderBitRateKey, 320000,
			AVAudioSettings.AVNumberOfChannelsKey, 2,
			AVAudioSettings.AVSampleRateKey, 44100.0
		));
		
		public AVAudioRecorder AudioRecorder;

		protected override bool NativeCanRecord => true;

		protected override RecordingResult NativeStartRecording(string fileName)
		{
			var documentsPath = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory,
															NSSearchPathDomain.User, true);

			var path = $"{documentsPath[0]}/{fileName}.caf";
			var soundFileURL = new NSUrl(path);

			NSError error;
			AudioRecorder = AVAudioRecorder.Create(soundFileURL, audioSettings, out error);

			if (AudioRecorder == null)
			{
				Mvx.Trace(MvxTraceLevel.Error, "Error trying to create AVAudioRecorder. Got Exception: {0}", error.ToString()); 
				return false;
			}

			AudioRecorder.PrepareToRecord();
			AudioRecorder.Record();

			return new RecordingResult(true, path);
		}

		protected override bool NativeStopRecording()
		{
			AudioRecorder?.Stop();
			AudioRecorder = null;

			return true;
		}
	}
}