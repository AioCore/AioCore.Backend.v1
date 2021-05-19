namespace AioCore.Application.Responses.SettingLayoutResponses
{
    public record UpdateLayoutResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}