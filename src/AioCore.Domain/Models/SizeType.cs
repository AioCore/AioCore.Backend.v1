using System.Collections.Generic;

namespace AioCore.Domain.Models
{
    public enum SizeType
    {
        _25 = 1,
        _50,
        _100,
        _200,
        _400,
        _800,
        _1600
    }

    public static class SiteTypeExtensions
    {
        public static SizeType ToSizeType(this int imageSize)
        {
            return imageSize switch
            {
                25 => SizeType._25,
                50 => SizeType._50,
                100 => SizeType._100,
                200 => SizeType._200,
                400 => SizeType._400,
                800 => SizeType._800,
                1600 => SizeType._1600,
                _ => throw new KeyNotFoundException("Could not found size type"),
            };
        }
    }
}