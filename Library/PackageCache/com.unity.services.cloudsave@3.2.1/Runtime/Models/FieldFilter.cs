using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.CloudSave.Internal.Http;

namespace Unity.Services.CloudSave.Models
{
    /// <summary>
    /// A field filter for querying an index
    /// </summary>
    [Preserve]
    [DataContract(Name = "FieldFilter")]
    public class FieldFilter
    {
        /// <summary>
        /// A field filter for querying an index
        /// </summary>
        /// <param name="key">Item key</param>
        /// <param name="value">The indexed Cloud Save value</param>
        /// <param name="op">The comparison operator to use for the filter. The specified value is compared to the indexed value (lexicographically for string data, numerically for numerical data) using one of the following operators: * &#x60;EQ&#x60; - Equal * &#x60;NE&#x60; - Not Equal * &#x60;LT&#x60; - Less Than * &#x60;LE&#x60; - Less Than or Equal * &#x60;GT&#x60; - Greater Than * &#x60;GE&#x60; - Greater Than or Equal</param>
        /// <param name="asc">Whether the field is sorted in ascending order</param>
        [Preserve]
        public FieldFilter(string key, object value, OpOptions op, bool asc)
        {
            Key = key;
            Value = (IDeserializable)JsonObject.GetNewJsonObjectResponse(value);
            Op = op;
            Asc = asc;
        }

        /// <summary>
        /// Item key
        /// </summary>
        [Preserve]
        [DataMember(Name = "key", IsRequired = true, EmitDefaultValue = true)]
        public string Key { get; }

        /// <summary>
        /// The indexed Cloud Save value
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "value", IsRequired = true, EmitDefaultValue = true)]
        public IDeserializable Value { get; }

        /// <summary>
        /// The comparison operator to use for the filter. The specified value is compared to the indexed value (lexicographically for string data, numerically for numerical data) using one of the following operators: * &#x60;EQ&#x60; - Equal * &#x60;NE&#x60; - Not Equal * &#x60;LT&#x60; - Less Than * &#x60;LE&#x60; - Less Than or Equal * &#x60;GT&#x60; - Greater Than * &#x60;GE&#x60; - Greater Than or Equal
        /// </summary>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "op", IsRequired = true, EmitDefaultValue = true)]
        public OpOptions Op { get; }

        /// <summary>
        /// Whether the field is sorted in ascending order
        /// </summary>
        [Preserve]
        [DataMember(Name = "asc", IsRequired = true, EmitDefaultValue = true)]
        public bool Asc { get; }

        /// <summary>
        /// The comparison operator to use for the filter. The specified value is compared to the indexed value (lexicographically for string data, numerically for numerical data) using one of the following operators: * &#x60;EQ&#x60; - Equal * &#x60;NE&#x60; - Not Equal * &#x60;LT&#x60; - Less Than * &#x60;LE&#x60; - Less Than or Equal * &#x60;GT&#x60; - Greater Than * &#x60;GE&#x60; - Greater Than or Equal
        /// </summary>
        /// <value>The comparison operator to use for the filter. The specified value is compared to the indexed value (lexicographically for string data, numerically for numerical data) using one of the following operators: * &#x60;EQ&#x60; - Equal * &#x60;NE&#x60; - Not Equal * &#x60;LT&#x60; - Less Than * &#x60;LE&#x60; - Less Than or Equal * &#x60;GT&#x60; - Greater Than * &#x60;GE&#x60; - Greater Than or Equal</value>
        [Preserve]
        [JsonConverter(typeof(StringEnumConverter))]
        public enum OpOptions
        {
            /// <summary>
            /// Enum EQ for value: EQ
            /// </summary>
            [EnumMember(Value = "EQ")] EQ = 1,

            /// <summary>
            /// Enum NE for value: NE
            /// </summary>
            [EnumMember(Value = "NE")] NE = 2,

            /// <summary>
            /// Enum LT for value: LT
            /// </summary>
            [EnumMember(Value = "LT")] LT = 3,

            /// <summary>
            /// Enum LE for value: LE
            /// </summary>
            [EnumMember(Value = "LE")] LE = 4,

            /// <summary>
            /// Enum GT for value: GT
            /// </summary>
            [EnumMember(Value = "GT")] GT = 5,

            /// <summary>
            /// Enum GE for value: GE
            /// </summary>
            [EnumMember(Value = "GE")] GE = 6
        }
    }
}
