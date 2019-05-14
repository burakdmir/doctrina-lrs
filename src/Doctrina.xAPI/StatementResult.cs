﻿using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Doctrina.xAPI
{
    [JsonObject]
    public class StatementsResult : JsonModel, IStatementsResult
    {
        //public StatementsResult()
        //{
        //}

        //public StatementsResult(string json)
        //   : this(json, ApiVersion.GetLatest(), ResultFormat.Exact)
        //{
        //}

        //public StatementsResult(string json, ApiVersion version)
        //    : this(json, version, ResultFormat.Exact)
        //{
        //}

        //public StatementsResult(string json, ApiVersion version, ResultFormat format)
        //{
        //    var apiSerializer = new xAPI.Json.ApiJsonSerializer(version, format);
        //    var reader = new JsonTextReader(new System.IO.StringReader(json));

        //    var result = apiSerializer.Deserialize<StatementsResult>(reader);
        //    Statements = result.Statements;
        //    More = result.More;
        //}

        /// <summary>
        /// List of Statements. If the list returned has been limited (due to pagination), and there are more results, they will be located at the "statements" property within the container located at the IRL provided by the "more" property of this Statement result Object. Where no matching Statements are found, this property will contain an empty array.
        /// </summary>
        public Statement[] Statements { get; set; }

        /// <summary>
        /// Relative IRL that can be used to fetch more results, including the full path and optionally a query string but excluding scheme, host, and port. Empty string if there are no more results to fetch.
        /// </summary>
        public Uri More { get; set; }

        public static StatementsResult Parse(string jsonString)
        {
            throw new NotImplementedException();
        }

        //public string ToJson()
        //{
        //    StringWriter sw = new StringWriter();
        //    JsonTextWriter writer = new JsonTextWriter(sw);

        //    // {
        //    writer.WriteStartObject();

        //    // "statements": [ ... ]
        //    writer.WritePropertyName("statements");
        //    writer.WriteStartArray();
        //    foreach (var statement in Statements)
        //    {
        //        writer.WriteRawValue(statement.ToJson());
        //    }
        //    writer.WriteEndArray();

        //    // "more": " ... "
        //    writer.WritePropertyName("more");
        //    writer.WriteValue(More);

        //    // }
        //    writer.WriteEndObject();

        //    return sw.ToString();
        //}

        public static async Task<StatementsResult> ReadAsMultipartAsync(string boundary, Stream stream)
        {
            var result = new StatementsResult();

            var multipartReader = new MultipartReader(boundary, stream);
            var section = await multipartReader.ReadNextSectionAsync();
            int sectionIxdex = 0;
            while (section != null)
            {
                if (sectionIxdex == 0)
                {
                    // StatementsResult
                    string jsonString = await section.ReadAsStringAsync();
                    result = StatementsResult.Parse(jsonString);
                }
                else
                {
                    // TODO: Read attachment
                }

                section = await multipartReader.ReadNextSectionAsync();
                sectionIxdex++;
            }

            return result;
        }

        public Attachment GetAttachmentByHash(string hash)
        {
            throw new NotImplementedException();
        }

        public Attachment GetStatementByHash(string hash)
        {
            throw new NotImplementedException();
        }

        public void SetAttachmentByHash(string hash, byte[] payload)
        {
            throw new NotImplementedException();
        }

        public override JObject ToJObject(ApiVersion version, ResultFormat format)
        {
            var obj = new JObject();
            obj["statements"] = new JArray(Statements.Select(x => x.ToJObject(x.Version, format)));
            obj["more"] = More.ToString();
            return obj;
        }
    }
}
