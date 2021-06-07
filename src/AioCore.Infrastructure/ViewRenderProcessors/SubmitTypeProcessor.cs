﻿using Package.ViewRender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AioCore.Infrastructure.ViewRenderProcessors
{
    public class SubmitTypeProcessor : IViewRenderProcessor
    {
        public string Type => "submit";

        public async Task<string> BuildOpeningTag(XElement element)
        {
            return await Task.FromResult("");
        }

        public async Task<string> BuildClosingTag(XElement element)
        {
            return await Task.FromResult("");
        }
    }
}