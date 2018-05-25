<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel dslVersion="1.0.0.0" Id="d0ed9acb-0435-4532-afdd-b5115bc4d562" namespace="_4screen.CSB.Common" xmlSchemaNamespace="http://www.sieme.net/config" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
    <externalType name="Guid" namespace="System" />
    <enumeratedType name="DeleteEvents" namespace="_4screen.CSB.Common">
      <literals>
        <enumerationLiteral name="Delete" value="1" />
        <enumerationLiteral name="StatusDeleted" value="2" />
      </literals>
    </enumeratedType>
    <externalType name="QuickSort" namespace="_4screen.CSB.Common" />
    <externalType name="MapLayout" namespace="_4screen.CSB.Common" />
    <externalType name="MapNavigation" namespace="_4screen.CSB.Common" />
    <externalType name="MapStyle" namespace="_4screen.CSB.Common" />
    <externalType name="AccessMode" namespace="_4screen.CSB.Common" />
    <externalType name="WidgetShowAfterInsert" namespace="_4screen.CSB.Common" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="CustomizationSection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="customizationSection">
      <elementProperties>
        <elementProperty name="CustomizationBar" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="customizationBar" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/CustomizationTabElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="Profile" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="profile" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ProfileSettingsElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="Modules" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="modules" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ModuleElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="Logins" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="logins" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoginElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="MyContent" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="myContent" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MyContentElement" />
          </type>
        </elementProperty>
        <elementProperty name="DefaultLayouts" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="defaultLayouts" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/DefaultLayouts" />
          </type>
        </elementProperty>
        <elementProperty name="Common" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="common" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/CommonCustomizationElement" />
          </type>
        </elementProperty>
        <elementProperty name="ContentReports" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="contentReports" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ContentReports" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="CustomizationTabElementCollection" namespace="_4screen.CSB.Common" collectionType="BasicMap" xmlItemName="customizationTab" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <attributeProperties>
        <attributeProperty name="Enabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="PageAddEnabled" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="pageAddEnabled" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="PageUpdateEnabled" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="pageUpdateEnabled" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/CustomizationTabElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="CustomizationTabElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Enabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ProfileSettingsElementCollection" namespace="_4screen.CSB.Common" collectionType="BasicMap" xmlItemName="profileSettings" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ProfileSettingsElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ProfileSettingsElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Enabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ModuleElementCollection" namespace="_4screen.CSB.Common" collectionType="BasicMap" xmlItemName="module" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ModuleElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ModuleElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Enabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="LoginElementCollection" namespace="_4screen.CSB.Common" collectionType="BasicMap" xmlItemName="login" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoginElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="LoginElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Enabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="WidgetSection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="widgetSection">
      <elementProperties>
        <elementProperty name="Widgets" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="widgets" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WidgetElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="WidgetElementCollection" namespace="_4screen.CSB.Common" xmlItemName="widget" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WidgetElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="WidgetElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Guid" />
          </type>
        </attributeProperty>
        <attributeProperty name="Control" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="control" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TitleKey" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="titleKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="DescriptionKey" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="descriptionKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="GroupId" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="groupId" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="OrderKey" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="orderKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Roles" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="roles" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Communities" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="communities" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="PageTypes" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="pageTypes" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Version" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="version" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="LocalizationBaseFileName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="localizationBaseFileName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="OutputTemplates" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="outputTemplates" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ShowAfterInsert" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="showAfterInsert" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WidgetShowAfterInsert" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Settings" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="settings" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WidgetSettingsElement" />
          </type>
        </elementProperty>
        <elementProperty name="Steps" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="steps" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WidgetStepElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="WidgetSettingsElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Value" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="WidgetStepElementCollection" namespace="_4screen.CSB.Common" collectionType="BasicMap" xmlItemName="step" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WidgetStepElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="WidgetStepElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="TitleKey" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="titleKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Control" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="control" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="MyContentElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="FeaturedEditEnabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="featuredEditEnabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="DeleteEvent" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="deleteEvent" isReadOnly="false">
          <type>
            <enumeratedTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/DeleteEvents" />
          </type>
        </attributeProperty>
        <attributeProperty name="DefaultSortOrder" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="defaultSortOrder" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/QuickSort" />
          </type>
        </attributeProperty>
        <attributeProperty name="AdminCommunitySelectionEnabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="adminCommunitySelectionEnabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="DefaultLayouts" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="ProfileCommunity" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="profileCommunity" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Community" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="community" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Page" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="page" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="SecuritySection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="securitySection">
      <elementProperties>
        <elementProperty name="SecurityAreas" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="securityAreas" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SecurityAreaElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="SecurityAreaElementCollection" namespace="_4screen.CSB.Common" xmlItemName="securityArea" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SecurityAreaElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="SecurityAreaElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Roles" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="roles" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="SiteObjectSection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="siteObjectSection">
      <elementProperties>
        <elementProperty name="SiteObjects" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="siteObjects" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SiteObjectElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="SiteObjectElementCollection" namespace="_4screen.CSB.Common" xmlItemName="siteObjectType" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SiteObjectType" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="SiteObjectType" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="id" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsActive" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="isActive" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="AllowedRoles" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="allowedRoles" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="HasOverview" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="hasOverview" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsFolderContent" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="isFolderContent" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsGeoTaggable" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="isGeoTaggable" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsMultiUpload" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="isMultiUpload" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="ImageActionSmall" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="imageActionSmall" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ImageActionExtraSmall" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="imageActionExtraSmall" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NameSingularKey" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="nameSingularKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NamePluralKey" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="namePluralKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NameCreateMenuKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="nameCreateMenuKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="AllowedFileExtensions" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="allowedFileExtensions" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Icon" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="icon" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="UploadWizard" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="uploadWizard" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="EditWizard" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="editWizard" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NumericId" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="numericId" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="NiceLinkToOverviewKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="niceLinkToOverviewKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MediaFolder" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mediaFolder" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NiceLinkToDetailKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="niceLinkToDetailKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="DefaultImageURL" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="defaultImageURL" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="LocalizationBaseFileName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="localizationBaseFileName" isReadOnly="false" documentation="Root of the Resource">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsSearchable" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="isSearchable" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="CropCheckAndArchive" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="cropCheckAndArchive" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ImageActionLarge" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="imageActionLarge" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ImageActionCrop" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="imageActionCrop" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="SearchResultCtrl" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="searchResultCtrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Assembly" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="assembly" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="SearchForSelectCtrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="searchForSelectCtrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="DefaultLoadAmount" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="defaultLoadAmount" isReadOnly="false" defaultValue="500">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Type" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="type" isReadOnly="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="UploadMaxFileSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="uploadMaxFileSize" isReadOnly="false" defaultValue="-1">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="UploadMaxFileCount" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="uploadMaxFileCount" isReadOnly="false" defaultValue="-1">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="UploadMaxFileSizeTotal" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="uploadMaxFileSizeTotal" isReadOnly="false" defaultValue="-1">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="TagHandlerCtrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="tagHandlerCtrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="DetailWidgetId" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="detailWidgetId" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Guid" />
          </type>
        </attributeProperty>
        <attributeProperty name="ViewHandlerCtrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="viewHandlerCtrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="LinkToDetailKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="linkToDetailKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="LinkToOverviewKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="linkToOverviewKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="LinkToWizardKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="linkToWizardKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MapIcon" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mapIcon" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ImageActionMedium" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="imageActionMedium" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MobileDetailsCtrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mobileDetailsCtrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NiceLinkToMobileDetailKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="niceLinkToMobileDetailKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NiceLinkToMobileOverviewKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="niceLinkToMobileOverviewKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MobileOverviewCtrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mobileOverviewCtrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MobileSmallOutputCtrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mobileSmallOutputCtrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="SiteLinkSection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="siteLinkSection">
      <elementProperties>
        <elementProperty name="SiteLinks" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="siteLinks" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SiteLinkElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="SiteLinkElementCollection" namespace="_4screen.CSB.Common" xmlItemName="siteLink" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SiteLink" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="SiteLink" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Url" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="url" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="PredefinedNaviLink" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="predefinedNaviLink" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="UrlTextKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="urlTextKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="LocalizationBaseFileName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="localizationBaseFileName" isReadOnly="false" documentation="Root of the Resource">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="WizardSection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="wizardSection">
      <elementProperties>
        <elementProperty name="Wizards" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="wizards" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WizardElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="WizardElementCollection" namespace="_4screen.CSB.Common" xmlItemName="wizard" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WizardElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="WizardElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="AccessMode" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="accessMode" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/AccessMode" />
          </type>
        </attributeProperty>
        <attributeProperty name="ObjectType" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="objectType" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ResourceKey" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="resourceKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="LocalizationBaseFileName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="localizationBaseFileName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NeedsAuthentication" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="needsAuthentication" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Steps" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="steps" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WizardStepElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="WizardStepElementCollection" namespace="_4screen.CSB.Common" xmlItemName="step" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WizardStepElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="WizardStepElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="ResourceKey" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="resourceKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Control" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="control" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="AllowBack" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="allowBack" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="AllowNext" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="allowNext" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="LastSaveStep" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="lastSaveStep" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Settings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="settings" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WizardStepSettingsElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="CommonCustomizationElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="BreadCrumbUserOrCommunityEnabled" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="breadCrumbUserOrCommunityEnabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="TitleFormat1" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="titleFormat1" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TitleFormat2" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="titleFormat2" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TitleFormat3" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="titleFormat3" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="ShopSection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="shopSection">
      <elementProperties>
        <elementProperty name="ShopSettings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="shopSettings" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ShopSettingElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="Tags" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="tags" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ShopTagElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="ShopSetting" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="value" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ShopSettingElementCollection" namespace="_4screen.CSB.Common" xmlItemName="shopSetting" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ShopSetting" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="ShopTagElementCollection" namespace="_4screen.CSB.Common" xmlItemName="tag" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ShopTagElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ShopTagElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Value" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="OutputTemplatesSection" namespace="_4screen.CSB.Common" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="outputTemplatesSection">
      <elementProperties>
        <elementProperty name="Templates" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="templates" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/OutputTemplateElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="OutputTemplateElementCollection" namespace="_4screen.CSB.Common" xmlItemName="template" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/OutputTemplateElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="OutputTemplateElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="RepeaterControl" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="repeaterControl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="OutputTemplateControl" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="outputTemplateControl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Image" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="image" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Guid" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ContentReports" xmlItemName="contentReport" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <attributeProperties>
        <attributeProperty name="LocalizationBaseFileName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="localizationBaseFileName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ContentReport" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ContentReport">
      <attributeProperties>
        <attributeProperty name="ReasonKey" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="reasonKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TextKey" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="textKey" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ReadOnly" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="readOnly" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="ShowSend" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="showSend" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="MapSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="mapSection">
      <elementProperties>
        <elementProperty name="Maps" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="maps" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="MapElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Width" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="width" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Height" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="height" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Zoom" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="zoom" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Latitude" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="latitude" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Double" />
          </type>
        </attributeProperty>
        <attributeProperty name="Longitude" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="longitude" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Double" />
          </type>
        </attributeProperty>
        <attributeProperty name="MapLayout" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mapLayout" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapLayout" />
          </type>
        </attributeProperty>
        <attributeProperty name="MapStyle" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mapStyle" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapStyle" />
          </type>
        </attributeProperty>
        <attributeProperty name="MapNavigation" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mapNavigation" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapNavigation" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="ObjectTypes" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="objectTypes" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapObjectTypeElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="MapObjectTypeElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IconUrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="iconUrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Selected" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="selected" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Tags" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="tags" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapObjectTypeTagElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="MapObjectTypeTagElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Id" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="id" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IconUrl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="iconUrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Selected" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="selected" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Title" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="title" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="MapElementCollection" namespace="_4screen.CSB.Common" xmlItemName="map" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="MapObjectTypeElementCollection" namespace="_4screen.CSB.Common" xmlItemName="objectType" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapObjectTypeElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="MapObjectTypeTagElementCollection" namespace="_4screen.CSB.Common" xmlItemName="tag" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MapObjectTypeTagElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="WizardStepSettingsElementCollection" namespace="_4screen.CSB.Common" xmlItemName="settingsEntry" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WizardStepSettingsElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="WizardStepSettingsElement" namespace="_4screen.CSB.Common">
      <attributeProperties>
        <attributeProperty name="Key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Value" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>