using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoleRollsPocketEdition.Templates.Entities.Json;

public class FormulaTokenTypeJsonConverter : JsonConverter<FormulaTokenType>
{
    public override FormulaTokenType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var raw = reader.GetString();
            return MapLegacyValue(raw);
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            var value = reader.GetInt32();
            return MapLegacyValue(value);
        }

        throw new JsonException($"Unable to convert value to {nameof(FormulaTokenType)}.");
    }

    public override void Write(Utf8JsonWriter writer, FormulaTokenType value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((int)value);
    }

    private static FormulaTokenType MapLegacyValue(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return FormulaTokenType.Manual;
        }

        return raw.ToLowerInvariant() switch
        {
            "number" => FormulaTokenType.Manual,
            "operator" => FormulaTokenType.Manual,
            "customvalue" => FormulaTokenType.Creature,
            _ when Enum.TryParse<FormulaTokenType>(raw, true, out var parsed) => parsed,
            _ => FormulaTokenType.Manual
        };
    }

    private static FormulaTokenType MapLegacyValue(int value)
    {
        return value switch
        {
            1 => FormulaTokenType.Manual,
            2 => FormulaTokenType.Manual,
            3 => FormulaTokenType.Creature,
            _ when Enum.IsDefined(typeof(FormulaTokenType), value) => (FormulaTokenType)value,
            _ => FormulaTokenType.Manual
        };
    }
}
