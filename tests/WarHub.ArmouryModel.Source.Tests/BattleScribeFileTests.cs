﻿using Microsoft.XmlDiffPatch;
using System;
using System.IO;
using System.Xml;
using Xunit;

namespace WarHub.ArmouryModel.Source.Tests
{
    public class BattleScribeFileTests : SerializationTestBase
    {
        [Fact]
        [Trait("XmlSerialization", "ReadWriteTest")]
        public void ReadWriteGameSystem()
        {
            ReadWriteXml(XmlTestData.GameSystemFilename, SerializeGameSystem, DeserializeGameSystem);
        }

        [Theory]
        [Trait("XmlSerialization", "ReadWriteTest")]
        [InlineData(XmlTestData.Catalogue1Filename)]
        [InlineData(XmlTestData.Catalogue2Filename)]
        public void ReadWriteCatalogue(string filename)
        {
            ReadWriteXml(filename, SerializeCatalogue, DeserializeCatalogue);
        }

        [Fact]
        [Trait("XmlSerialization", "ReadWriteTest")]
        public void ReadWriteRoster()
        {
            ReadWriteXml(XmlTestData.RosterFilename, SerializeRoster, DeserializeRoster);
        }

        private static void ReadWriteXml(string filename, Action<SourceNode, Stream> serialize, Func<Stream, SourceNode> deserialize)
        {
            var input = Path.Combine(XmlTestData.InputDir, filename);
            var output = Path.Combine(XmlTestData.OutputDir, filename);
            var readNode = Deserialize();
            Serialize(readNode);
            var areXmlEqual = AreXmlEqual();
            Assert.True(areXmlEqual);




            SourceNode Deserialize()
            {
                using (var stream = File.OpenRead(input))
                {
                    return deserialize(stream);
                }
            }
            void Serialize(SourceNode node)
            {
                Assert.True(Directory.Exists(XmlTestData.OutputDir));
                using (var stream = File.Create(output))
                {
                    serialize(node, stream);
                }
            }
            bool AreXmlEqual()
            {
                var differ = new XmlDiff(XmlDiffOptions.None);
                //using (var diffStream = new MemoryStream())
                using (var diffStream = File.Create(output + ".diff"))
                {
                    using (var diffWriter = XmlWriter.Create(diffStream))
                    {
                        var areEqual = differ.Compare(input, output, false, diffWriter);
                        return areEqual;
                    }
                }
            }
        }
    }
}
