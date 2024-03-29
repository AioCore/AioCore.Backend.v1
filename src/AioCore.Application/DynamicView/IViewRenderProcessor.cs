﻿using AioCore.Application.Plugin;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AioCore.Application.DynamicView
{
    public interface IViewRenderProcessor : IPlugin
    {
        string Type { get; }

        Task<string> BuildOpeningTag(XElement element);

        Task<string> BuildClosingTag(XElement element);
    }
}