using System.IO;
using FluentAssertions;
using PinataCore.Data;
using Xunit;

namespace PinataCore.IntegrationTest
{
    public class PinataCoreMySQLTest
    {
        private PinataCore _sutPinataCore = null;

        public class Execute : PinataCoreMySQLTest
        {
            public class Given_A_Valid_Sample_Data : Execute
            {
                public Given_A_Valid_Sample_Data()
                {
                    const string baseDataPath = @"JSON\MySQL";

                    string[] dataPath =
                    {
                        Path.Combine(baseDataPath, "SetupItem.json"),
                        Path.Combine(baseDataPath, "SetupDomain.json"),
                        Path.Combine(baseDataPath, "Setup.json"),
                        Path.Combine(baseDataPath, "SetupType.json"),
                        Path.Combine(baseDataPath, "SetupSource.json"),
                        Path.Combine(baseDataPath, "SetupRule.json")
                    };

                    string connectionString = ConfigurationManager.GetConnectionString("SocialMiner.MySQL");

                    _sutPinataCore = new PinataCore(connectionString, Provider.Type.MySQL, dataPath);
                    _sutPinataCore.Feed();
                }

                [Fact]
                public void When_Using_Delete_Command_Should_Return_True()
                {
                    bool success = _sutPinataCore.Execute(Command.CommandType.Delete);
                    success.Should().BeTrue();
                }

                [Fact]
                public void When_Using_Insert_Command_Should_Return_True()
                {
                    bool success = _sutPinataCore.Execute(Command.CommandType.Insert);
                    success.Should().BeTrue();
                }
            }
        }
    }
}
