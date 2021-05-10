using System.Collections.Generic;

namespace AioCore.Domain.AggregatesModel.DynamicBinaryAggregate
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
            switch (imageSize)
            {
                case 25:
                    return SizeType._25;

                case 50:
                    return SizeType._50;

                case 100:
                    return SizeType._100;

                case 200:
                    return SizeType._200;

                case 400:
                    return SizeType._400;

                case 800:
                    return SizeType._800;

                case 1600:
                    return SizeType._1600;
            }

            throw new KeyNotFoundException("Could not found size type");
        }
    }
}