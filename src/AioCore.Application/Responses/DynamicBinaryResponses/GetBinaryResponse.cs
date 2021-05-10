using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using AutoMapper;
using Package.AutoMapper;

namespace AioCore.Application.Responses.DynamicBinaryResponses
{
    public class GetBinaryResponse : IMap
    {
        public byte[] Bytes { get; set; }

        public string SourceName { get; set; }

        public string ContentType { get; set; }

        public void MergeParams(byte[] bytes)
        {
            Bytes = bytes;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DynamicBinary, GetBinaryResponse>()
                .ForMember(d => d.Bytes, s => s.Ignore());
        }
    }
}