using System.Buffers;
using System.IO.Pipelines;

namespace FivemServersTest
{
	public static class FrameReader
	{
		public static async Task ReadFramesAsync(Stream stream, Action<ReadOnlySequence<byte>> onFrame)
		{
			var reader = PipeReader.Create(stream);

			while (true)
			{
				var result = await reader.ReadAsync();
				var buffer = result.Buffer;

				while (TryReadFrame(ref buffer, out var frame))
					onFrame(frame);

				reader.AdvanceTo(buffer.Start, buffer.End);

				if (result.IsCompleted)
					break;
			}

			await reader.CompleteAsync();
		}

		private static bool TryReadFrame(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> frame)
		{
			frame = default;

			if (buffer.Length < 4)
				return false;

			var reader = new SequenceReader<byte>(buffer);

			if (!reader.TryReadLittleEndian(out int frameLength) || frameLength > 65535)
				throw new InvalidOperationException("A too large frame was passed.");

			if (reader.Remaining < frameLength)
				return false;

			frame = buffer.Slice(reader.Position, frameLength);
			buffer = buffer.Slice(frame.End);

			return true;
		}
	}
}
