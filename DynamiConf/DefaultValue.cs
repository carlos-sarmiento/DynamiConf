using System.Dynamic;

namespace DynamiConf
{
    /// <summary>
    /// Represents the lack of a value in a Configuration instance. As opossed to null, it will not throw a NullException when accessed.
    /// </summary>
    public class DefaultValue : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new DefaultValue();
            return true;
        }

        public override string ToString()
        {
            return string.Empty;
        }

        #region Implicit Convertions

        #region string
        public static implicit operator string (DefaultValue dv) { return null; }
        public static implicit operator string[] (DefaultValue dv) { return new string[0]; }
        #endregion

        #region bool
        public static implicit operator bool (DefaultValue dv) { return false; }
        public static implicit operator bool[] (DefaultValue dv) { return new bool[0]; }
        public static implicit operator bool? (DefaultValue dv) { return null; }
        #endregion

        #region decimal
        public static implicit operator decimal (DefaultValue dv) { return 0; }
        public static implicit operator decimal[] (DefaultValue dv) { return new decimal[0]; ; }
        public static implicit operator decimal? (DefaultValue dv) { return null; }
        #endregion

        #region double
        public static implicit operator double (DefaultValue dv) { return 0; }
        public static implicit operator double[] (DefaultValue dv) { return new double[0]; ; }
        public static implicit operator double? (DefaultValue dv) { return null; }
        #endregion

        #region float
        public static implicit operator float (DefaultValue dv) { return 0; }
        public static implicit operator float[] (DefaultValue dv) { return new float[0]; ; }
        public static implicit operator float? (DefaultValue dv) { return null; }
        #endregion

        #region int
        public static implicit operator int (DefaultValue dv) { return 0; }
        public static implicit operator int[] (DefaultValue dv) { return new int[0]; ; }
        public static implicit operator int? (DefaultValue dv) { return null; }
        #endregion

        #region long
        public static implicit operator long (DefaultValue dv) { return 0; }
        public static implicit operator long[] (DefaultValue dv) { return new long[0]; ; }
        public static implicit operator long? (DefaultValue dv) { return null; }
        #endregion

        #region short
        public static implicit operator short (DefaultValue dv) { return 0; }
        public static implicit operator short[] (DefaultValue dv) { return new short[0]; ; }
        public static implicit operator short? (DefaultValue dv) { return null; }
        #endregion

        #region uint
        public static implicit operator uint (DefaultValue dv) { return 0; }
        public static implicit operator uint[] (DefaultValue dv) { return new uint[0]; ; }
        public static implicit operator uint? (DefaultValue dv) { return null; }
        #endregion

        #region ulong
        public static implicit operator ulong (DefaultValue dv) { return 0; }
        public static implicit operator ulong[] (DefaultValue dv) { return new ulong[0]; ; }
        public static implicit operator ulong? (DefaultValue dv) { return null; }
        #endregion

        #region ushort
        public static implicit operator ushort (DefaultValue dv) { return 0; }
        public static implicit operator ushort[] (DefaultValue dv) { return new ushort[0]; ; }
        public static implicit operator ushort? (DefaultValue dv) { return null; }
        #endregion

        #endregion
    }
}