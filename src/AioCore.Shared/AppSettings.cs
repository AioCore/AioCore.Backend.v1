using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AioCore.Shared
{
    public class AppSettings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Eventbus EventBus { get; set; }
        public Elasticsearch Elasticsearch { get; set; }
        public Minio Minio { get; set; }
        public Tokens Tokens { get; set; }
        public int ExpiredTime { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Eventbus
    {
        public bool AzureServiceBusEnabled { get; set; }
        public string Connection { get; set; }
        public string SubscriptionClientName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; }
    }

    public class Elasticsearch
    {
        public string Url { get; set; }
        public string Index { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Minio
    {
        public string Endpoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Bucket { get; set; }
    }

    public class Tokens
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }

}
