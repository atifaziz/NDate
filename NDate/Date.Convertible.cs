#region Copyright (c) 2014 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace NDate
{
    using System;

    // ReSharper disable once PartialTypeWithSinglePart
    partial struct Date : IConvertible
    {
        TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => ToDateTime();

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            var result
                = conversionType == typeof(string) ? ToString(provider)
                : conversionType == typeof(DateTime) ? ToDateTime()
                : conversionType == typeof(DateTimeOffset) ? ToDateTimeOffset()
                : (object)null;

            if (result == null)
                throw InvalidCastError(conversionType);

            return result;
        }

        static InvalidCastException InvalidCastError(Type targetType) { throw new InvalidCastException($"Invalid case from '{nameof(Date)}' to '{targetType.Name}'."); }

        bool     IConvertible.ToBoolean(IFormatProvider provider)  { throw InvalidCastError(typeof(bool));    }
        char     IConvertible.ToChar(IFormatProvider provider)     { throw InvalidCastError(typeof(char));    }
        sbyte    IConvertible.ToSByte(IFormatProvider provider)    { throw InvalidCastError(typeof(sbyte));   }
        byte     IConvertible.ToByte(IFormatProvider provider)     { throw InvalidCastError(typeof(byte));    }
        short    IConvertible.ToInt16(IFormatProvider provider)    { throw InvalidCastError(typeof(short));   }
        ushort   IConvertible.ToUInt16(IFormatProvider provider)   { throw InvalidCastError(typeof(ushort));  }
        int      IConvertible.ToInt32(IFormatProvider provider)    { throw InvalidCastError(typeof(int));     }
        uint     IConvertible.ToUInt32(IFormatProvider provider)   { throw InvalidCastError(typeof(uint));    }
        long     IConvertible.ToInt64(IFormatProvider provider)    { throw InvalidCastError(typeof(long));    }
        ulong    IConvertible.ToUInt64(IFormatProvider provider)   { throw InvalidCastError(typeof(ulong));   }
        float    IConvertible.ToSingle(IFormatProvider provider)   { throw InvalidCastError(typeof(float));   }
        double   IConvertible.ToDouble(IFormatProvider provider)   { throw InvalidCastError(typeof(double));  }
        decimal  IConvertible.ToDecimal(IFormatProvider provider)  { throw InvalidCastError(typeof(decimal)); }
    }
}