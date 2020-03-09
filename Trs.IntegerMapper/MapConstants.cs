namespace Trs.IntegerMapper
{
    public static class MapConstants
    {
        /// <summary>
        /// Represents Null for values and Empty for collections.
        /// </summary>
        public const uint NullOrEmpty = 0;

        /// <summary>
        /// All mapped values start from this integer. 
        /// It is here to simplifyu the management of special constants like <see cref="NullOrEmpty"/>
        /// </summary>
        public const uint FirstMappableInteger = 1;
    }
}
