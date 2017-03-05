using System;
using Microphone;
using MvvmCross.Core.ViewModels;

namespace Client.Core.ViewModels
{
	public class FirstViewModel
		: MvxViewModel
	{
		private readonly IMvxMicrophoneService _microphoneService;

		public FirstViewModel(IMvxMicrophoneService microphoneService)
		{
			_microphoneService = microphoneService;

			ToggleRecordingCommand = new MvxCommand(ToggleRecordingCommandExecute);
		}

		public IMvxCommand ToggleRecordingCommand { get; }

		public string Path { get; set; }

		private void ToggleRecordingCommandExecute()
		{
			if (_microphoneService.IsRecording)
			{
				_microphoneService.StopRecording();
			}
			else
			{
				var beganRecording = _microphoneService.StartRecording("test");
				if (!string.IsNullOrEmpty(beganRecording.Path))
				{

				}
			}
		}

    }
}
