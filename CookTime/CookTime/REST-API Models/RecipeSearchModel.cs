﻿
namespace CookTime.REST_API_RecipeSearchModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    /// <summary>
    /// Class that packs recipe search objects into an array
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class RecipeModel
    {
        [JsonProperty("newrecipes")]
        public RecipeS[] Recipes { get; set; }
    }
    /// <summary>
    /// Class that creates a recipe search model from and to json files for search methods
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class RecipeS
    {
        [JsonProperty("search")]
        public string Search { get; set; }

        [JsonProperty("type")]
        public string TypeOfDish { get; set; }

        [JsonProperty("servings")]
        public int Servings { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }
    }

    public partial class RecipeModel
    {
        /// <summary>
        /// Methods that converts a json file to an object type
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static RecipeModel FromJson(string json) => JsonConvert.DeserializeObject<RecipeModel>(json, CookTime.REST_API_RecipeModel.Converter.Settings);
    }

    public static class Serialize
    {
        /// <summary>
        /// Methods that converts an object type to json file
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static string ToJson(this RecipeModel self) => JsonConvert.SerializeObject(self, CookTime.REST_API_RecipeModel.Converter.Settings);
    }
    /// <summary>
    /// Methods autogenerated by Newtonsoft.json
    /// author Jose Antonio Espinoza.
    /// </summary>
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }
        /// <summary>
        /// Creates singleton instance of the string parser.
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
