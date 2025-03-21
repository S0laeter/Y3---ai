﻿///********************************************************************************************************************************
///The code on this page is generated by the code generator, do not manually modify.
///CodeGenerator: Megumin.CSCodeGenerator.  Version: 1.0.2
///CodeGenericBy: Megumin.Serialization.AI.StringFormatterGenerator
///CodeGenericSourceFilePath: Packages/com.megumin.serialization/Editor/StringFormatterGenerator.asset
///********************************************************************************************************************************

using System;
using System.Collections.Generic;

namespace Megumin.Serialization
{
    public partial class StringFormatter
    {

        public sealed class BoolFormatter : IFormatter<string, bool>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (bool.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(bool value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out bool value)
            {
                if (bool.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class ByteFormatter : IFormatter<string, byte>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (byte.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(byte value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out byte value)
            {
                if (byte.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class SByteFormatter : IFormatter<string, sbyte>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (sbyte.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(sbyte value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out sbyte value)
            {
                if (sbyte.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class CharFormatter : IFormatter<string, char>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (char.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(char value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out char value)
            {
                if (char.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class ShortFormatter : IFormatter<string, short>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (short.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(short value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out short value)
            {
                if (short.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class UShortFormatter : IFormatter<string, ushort>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (ushort.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(ushort value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out ushort value)
            {
                if (ushort.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class IntFormatter : IFormatter<string, int>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (int.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(int value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out int value)
            {
                if (int.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class UIntFormatter : IFormatter<string, uint>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (uint.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(uint value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out uint value)
            {
                if (uint.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class LongFormatter : IFormatter<string, long>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (long.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(long value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out long value)
            {
                if (long.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class ULongFormatter : IFormatter<string, ulong>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (ulong.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(ulong value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out ulong value)
            {
                if (ulong.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class FloatFormatter : IFormatter<string, float>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (float.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(float value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out float value)
            {
                if (float.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }

        public sealed class DoubleFormatter : IFormatter<string, double>
        {
            public string Serialize(object value)
            {
                return value.ToString();
            }

            public bool TrySerialize(object value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out object value)
            {
                if (double.TryParse(source, out var result))
                {
                    value = result;
                    return true;
                }
                value = default;
                return false;
            }

            public bool TrySerialize(double value, out string destination)
            {
                destination = value.ToString();
                return true;
            }

            public bool TryDeserialize(string source, out double value)
            {
                if (double.TryParse(source, out value))
                {
                    return true;
                }

                value = default;
                return false;
            }
        }
    }
}
