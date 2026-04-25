using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library.Settings;

[Localizable(false)]
public class SettingsBuilder
{
    private const string ColumnSectionID = "id";
    private const string ColumnKey = "key";

    private const string DirSettings = ".settings";
    private const string GlobalSettingID = "AppSettings";

    private string _filePath;

    private bool _isNew;

    private XElement _sectionNode;
    private XElement _sectionsNodes;

    public SettingsBuilder(string sectionID, string settingID = null, string directory = null)
    {
        XContract.ArgIsNotNull(sectionID, nameof(sectionID));

        directory ??= DirSettings;
        settingID ??= GlobalSettingID;

        InitBuilder(directory, settingID, sectionID);
    }

    public bool IsNew
    {
        get
        {
            LoadSectionNode();

            return _isNew;
        }
    }

    public string this[string key] => GetSetting(key);

    public string SectionID { get; set; }

    public void AddSetting(string key, string xml)
    {
        var sectionNode = LoadSectionNode();

        var setting = sectionNode
            .Descendants("setting")
            .FirstOrDefault(element => element.Attribute(ColumnKey)?.Value == key);

        if (setting == null)
        {
            setting = new XElement("setting");
            setting.SetAttributeValue(ColumnKey, key);

            sectionNode.Add(setting);
        }

        setting.Value = xml ?? string.Empty;
    }

    public T DeserializeSetting<T>(string key, params Type[] knowTypes) where T : class
    {
        var xml = GetSetting(key);
        if (string.IsNullOrEmpty(xml))
        {
            return null;
        }

        var types = knowTypes.Concat(new[] {typeof(T)});

        return XHelper.Serializers.DataContract.Deserialize<T>(xml, types.ToArray());
    }

    public string GetSetting(string key)
    {
        var userNode = LoadSectionNode();
        var setting = userNode.Descendants("setting").FirstOrDefault(element => element.Attribute(ColumnKey)?.Value == key);

        return !string.IsNullOrEmpty(setting?.Value) ? setting.Value : null;
    }

    public void Save()
    {
        if (_sectionsNodes == null)
        {
            return;
        }

        using var writter = XmlWriter.Create(_filePath, new XmlWriterSettings {Encoding = Encoding.Unicode, Indent = true, IndentChars = "\t"});
        _sectionsNodes.Save(writter);

        writter.Flush();
        writter.Close();

        _isNew = false;
    }

    public void SerializeSetting(string key, object obj, params Type[] knowTypes)
    {
        IEnumerable<Type> types = knowTypes;
        if (obj != null)
        {
            types = types.Concat(new[] {obj.GetType()});
        }

        var xml = obj != null ? XHelper.Serializers.DataContract.Serialize(obj, types.ToArray()) : string.Empty;

        AddSetting(key, xml);
    }

    protected void InitBuilder(string directory, string settingID, string sectionID)
    {
        var defaultDirectory = Directory.CreateDirectory(directory);
        _filePath = Path.Combine(defaultDirectory.ToString(), $"{settingID}.xconfig");

        SectionID = sectionID;
    }

    private void LoadDocument()
    {
        if (_sectionsNodes != null)
        {
            return;
        }

        if (File.Exists(_filePath))
        {
            using var reader = XmlReader.Create(_filePath);
            _sectionsNodes = XElement.Load(reader);
        }
        else
        {
            _sectionsNodes = new XElement("sections");
        }
    }

    private XElement LoadSectionNode()
    {
        if (_sectionNode == null)
        {
            LoadDocument();

            _sectionNode = _sectionsNodes.Descendants("section").FirstOrDefault(section => section.Attribute(ColumnSectionID)?.Value == SectionID);
            if (_sectionNode == null)
            {
                _sectionNode = new XElement("section");
                _sectionNode.SetAttributeValue(ColumnSectionID, SectionID);

                _sectionsNodes.Add(_sectionNode);

                _isNew = true;
            }
        }

        return _sectionNode;
    }
}