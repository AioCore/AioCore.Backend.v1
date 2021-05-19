namespace AioCore.Application.Responses.SettingLayoutResponses
{
    public record CreateLayoutResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}