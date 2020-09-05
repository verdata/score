namespace score.Midi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Helper
    {
        public static UInt16 ReverseBytes(UInt16 value)
        {
            return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }
        public static UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }
        public static byte[] Combine(byte[][] arrays)
        {
            byte[] bytes = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;

            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, bytes, offset, array.Length);
                offset += array.Length;
            }

            return bytes;
        }
        public static byte[] VLQ(uint number)
        {
            var bts = VLQ2(number).ToArray<byte>();

            Array.Reverse(bts);

            return bts;
        }
        private static IEnumerable<byte> VLQ2(uint number)
        {
            var more = 0x00u;
            do
            {
                yield return (byte)((number & 0x7fu) | more);
                number >>= 7;
                more = 0x80u;
            } while (number > 0);
        }
    }
    public struct UInt24
    {
        private Byte _b0;
        private Byte _b1;
        private Byte _b2;

        public UInt24(UInt32 value)
        {
            _b0 = (byte)((value) & 0xFF);
            _b1 = (byte)((value >> 8) & 0xFF);
            _b2 = (byte)((value >> 16) & 0xFF);
        }

        public Byte Byte0 { get { return _b0; } }
        public Byte Byte1 { get { return _b1; } }
        public Byte Byte2 { get { return _b2; } }

        public UInt32 Value { get { return (UInt32)(_b0 | (_b1 << 8) | (_b2 << 16)); } }
    }
}