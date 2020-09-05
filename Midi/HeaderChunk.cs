namespace score.Midi
{
    using System;
    using System.IO;
    using System.Text;

    public class HeaderChunk
    {
        public string Type { get; private set; }
        public UInt32 Length { get; private set; }
        public UInt16 Format { get; private set; }
        public UInt16 Tracks { get; set; }
        public UInt16 PPQN { get; private set; }

        public HeaderChunk()
        {
            Type = "MThd";
            Length = 6;
            Format = 0;
            Tracks = 1;
            PPQN = 24;
        }
        public byte[] ToBytes()
        {
            byte[] _bytes = Helper.Combine(new byte[][]{
                    Encoding.ASCII.GetBytes(Type),
                    BitConverter.GetBytes(Helper.ReverseBytes(Length)),
                    BitConverter.GetBytes(Helper.ReverseBytes(Format)),
                    BitConverter.GetBytes(Helper.ReverseBytes(Tracks)),
                    BitConverter.GetBytes(Helper.ReverseBytes(PPQN)),
                });

            return _bytes;
        }
        public void WriteBytes(BinaryWriter writer)
        {
            foreach (byte b in ToBytes())
            {
                writer.Write(b);
            }
        }
    }
}