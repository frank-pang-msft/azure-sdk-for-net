﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Azure.WebPubSub.Common
{
    internal class ConnectEventRequestJsonConverter : JsonConverter<ConnectEventRequest>
    {
        public override ConnectEventRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var element = JsonDocument.ParseValue(ref reader).RootElement;

            return new ConnectEventRequest(
                element.ReadToObject<Dictionary<string, string[]>>(ConnectEventRequest.ClaimsProperty),
                element.ReadToObject<Dictionary<string, string[]>>(ConnectEventRequest.QueryProperty),
                element.ReadToObject<string[]>(ConnectEventRequest.SubprotocolsProperty),
                element.ReadToObject<WebPubSubClientCertificate[]>(ConnectEventRequest.ClientCertificatesProperty));
        }

        public override void Write(Utf8JsonWriter writer, ConnectEventRequest value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(ConnectEventRequest.ClaimsProperty);
            JsonSerializer.Serialize(writer, value.Claims, options);
            writer.WritePropertyName(ConnectEventRequest.QueryProperty);
            JsonSerializer.Serialize(writer, value.Query, options);
            writer.WritePropertyName(ConnectEventRequest.SubprotocolsProperty);
            JsonSerializer.Serialize(writer, value.Subprotocols, options);
            writer.WritePropertyName(ConnectEventRequest.ClientCertificatesProperty);
            JsonSerializer.Serialize(writer, value.ClientCertificates, options);
            if (value.ConnectionContext != null)
            {
                writer.WritePropertyName(WebPubSubEventRequest.ConnectionContextProperty);
                JsonSerializer.Serialize(writer, value.ConnectionContext, options);
            }
            writer.WriteEndObject();
        }
    }
}
