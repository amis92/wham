﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using WarHub.ArmouryModel.ProjectModel;
using WarHub.ArmouryModel.Workspaces.BattleScribe;
using WarHub.ArmouryModel.Workspaces.JsonFolder;

namespace WarHub.ArmouryModel.CliTool.Utilities
{
    internal class AutoProjectConfigurationProvider : IProjectConfigurationProvider
    {
        public ProjectConfiguration Create(string path)
        {
            if (File.Exists(path))
            {
                return ReadFile(path);
            }
            return ReadDirectory(path);
        }

        private ProjectConfiguration ReadFile(string path)
        {
            if (!IsProjectConfituration(path))
            {
                throw new ArgumentException($"Path '{path}' is not a project file ({ProjectConfiguration.FileExtension}) or directory.");
            }
            // we have configuration to read
            return ReadConfigurationFile(path);
        }

        private ProjectConfiguration ReadDirectory(string path)
        {
            var directory = string.IsNullOrEmpty(path) ? "." : path;
            // search for single project configuration file in current directory
            var files = Directory.EnumerateFiles(directory, "*" + ProjectConfiguration.FileExtension)
                .ToImmutableArray();
            switch (files.Length)
            {
                case 0:
                    return AutoGeneratedConfiguration(path);
                case 1:
                    return ReadConfigurationFile(files[0]);
                default:
                    throw new InvalidOperationException(
                        $"Cannot open project file ({ProjectConfiguration.FileExtension})" +
                        $" when there are {files.Length} such in directory {path}.");
            }
        }

        private ProjectConfiguration AutoGeneratedConfiguration(string path)
        {
            var xmlWorkspace = XmlWorkspace.CreateFromDirectory(path);
            if (xmlWorkspace.Documents.Any(x => XmlFileExtensions.DataCatalogueKinds.Contains(x.Kind)))
            {
                // that's BattleScribe workspace
                return new BattleScribeProjectConfigurationProvider().Create(path);
            }
            return new JsonFolderProjectConfigurationProvider().Create(path);
        }

        private ProjectConfiguration ReadConfigurationFile(string path)
        {
            var raw = ProjectConfigurationProviderBase.ReadConfigurationFile(path);
            switch (raw.FormatProvider)
            {
                case ProjectFormatProviderType.JsonFolders:
                    return new JsonConfigurationSanitizingProvider().Sanitize(raw);
                case ProjectFormatProviderType.XmlCatalogues:
                    return new BattleScribeConfigurationSanitizingProvider().Sanitize(raw);
                default:
                    return raw;
            }
        }

        private static bool IsProjectConfituration(string path)
        {
            var extension = Path.GetExtension(path);
            return string.Equals(
                extension,
                ProjectConfiguration.FileExtension,
                StringComparison.OrdinalIgnoreCase);
        }

        private class JsonConfigurationSanitizingProvider : JsonFolderProjectConfigurationProvider
        {
            public ProjectConfiguration Sanitize(ProjectConfiguration raw)
            {
                return SanitizeConfiguration(raw);
            }
        }

        private class BattleScribeConfigurationSanitizingProvider : BattleScribeProjectConfigurationProvider
        {
            public ProjectConfiguration Sanitize(ProjectConfiguration raw)
            {
                return SanitizeConfiguration(raw);
            }
        }
    }
}
