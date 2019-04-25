using System;

namespace Safeguard.Common.Ui.Fonts
{
    public interface IIconFont
    {
        string Value { get; }
    }

    public partial class IconFont : IIconFont, IEquatable<IconFont>
    {

        private IconFont(string value)
        {
            Value = value;
        }

        public string Value { get; }

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }

        #endregion

        #region Equality

        public bool Equals(IconFont other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != GetType()) { return false; }
            return Equals((IconFont) obj);
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public static bool operator ==(IconFont left, IconFont right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(IconFont left, IconFont right)
        {
            return !Equals(left, right);
        }

        #endregion


        #region Conversion Operators

        public static implicit operator IconFont(string unicodeString)
        {
            return string.IsNullOrEmpty(unicodeString) ? null : new IconFont(unicodeString);
        }

        public static implicit operator string(IconFont font)
        {
            return font == null ? string.Empty : font.Value;
        }

        #endregion

    }
}
