﻿using Doctrina.xAPI.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;

namespace Doctrina.Web.Areas.xAPI.Http
{
    public class ETagModelBinder : IModelBinder
    {
        ETagMatch _match;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var matches = new List<EntityTagHeader>();
            var request = bindingContext.ActionContext.HttpContext.Request;

            if (request.Headers.TryGetValue("If-Match", out StringValues ifMatchValue))
            {
                var parsed = EntityTagHeaderValue.Parse(ifMatchValue.ToString());
                matches.Add(new EntityTagHeader()
                {
                    Tag = parsed.Tag,
                    Match = ETagMatch.IfMatch
                });
            }

            if (request.Headers.TryGetValue("If-None-Match", out StringValues ifMatchNoneValue))
            {
                var parsed = EntityTagHeaderValue.Parse(ifMatchNoneValue.ToString());
                matches.Add(new EntityTagHeader()
                {
                    Tag = parsed.Tag,
                    Match = ETagMatch.IfNoneMatch
                });
            }

            if (request.Headers.TryGetValue("If-Modified-Since", out StringValues ifModifiedSinceValue))
            {

            }

            bindingContext.Result = ModelBindingResult.Success(matches);

            return Task.CompletedTask;
        }
    }
}
