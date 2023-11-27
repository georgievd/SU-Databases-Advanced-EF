using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Boardgames.Helpers;

public class XmlSerializationHelper
{
    /// <summary>
    /// Deserialize XML to DTOs (Data Transfer Objects).
    /// </summary>
    /// <typeparam name="T">Type of object to deserialize into.</typeparam>
    /// <param name="inputXml">XML string to deserialize.</param>
    /// <param name="rootName">Root element name of the XML.</param>
    /// <returns>Deserialized object of type T, or default value if deserialization fails.</returns>
    /// <exception cref="ArgumentNullException">Thrown if obj or rootName is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the serialization fails.</exception>
    public static T Deserialize<T>(string inputXml, string rootName)
    {
        if (string.IsNullOrEmpty(inputXml))
            throw new ArgumentException("Input XML cannot be null or empty.", nameof(inputXml));

        if (string.IsNullOrEmpty(rootName))
            throw new ArgumentException("Root name cannot be null or empty.", nameof(rootName));

        try
        {
            XmlRootAttribute xmlRoot = new(rootName);
            XmlSerializer xmlSerializer = new(typeof(T), xmlRoot);

            using var reader = new StringReader(inputXml);
            return (T)xmlSerializer.Deserialize(reader);
        }
        catch (XmlException ex)
        {
            Debug.WriteLine(ex);
            throw new InvalidOperationException("XML deserialization failed.", ex);
        }
        catch (InvalidOperationException ex)
        {
            Debug.WriteLine(ex);
            throw new InvalidOperationException($"{typeof(T)} deserialization failed.", ex);
        }
    }


    /// <summary>
    /// Serializes an object of type T to an XML string.
    /// </summary>
    /// <typeparam name="T">The type of object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="rootName">The root element name for the XML.</param>
    /// <param name="omitXmlDeclaration">If true, omits the XML declaration from the output.</param>
    /// <returns>A string representing the serialized XML.</returns>
    /// <exception cref="ArgumentNullException">Thrown if obj or rootName is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the serialization fails.</exception>
    public static string Serialize<T>(T obj, string rootName, bool omitXmlDeclaration = false)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj), "Object to serialize cannot be null.");

        if (string.IsNullOrEmpty(rootName))
            throw new ArgumentNullException(nameof(rootName), "Root name cannot be null or empty.");

        try
        {
            XmlRootAttribute xmlRoot = new(rootName);
            XmlSerializer xmlSerializer = new(typeof(T), xmlRoot);

            XmlSerializerNamespaces namespaces = new();
            namespaces.Add(string.Empty, string.Empty);

            XmlWriterSettings settings = new()
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = true
            };

            StringBuilder sb = new();
            using var stringWriter = new StringWriter(sb);
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            xmlSerializer.Serialize(xmlWriter, obj, namespaces);
            return sb.ToString().TrimEnd();
        }
        catch (InvalidOperationException ex)
        {
            Debug.WriteLine($"Serialization error: {ex.Message}");
            throw new InvalidOperationException($"Serializing {typeof(T)} failed.", ex);
        }
    }
}
