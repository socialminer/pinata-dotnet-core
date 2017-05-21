using PinataCore.Data;
using System;
using System.IO;
using Xunit;
using FluentAssertions;

namespace PinataCore.IntegrationTest
{
    public class PinataCoreMongoTest
    {
        private PinataCore _sutPinataCore = null;

        public class Execute : PinataCoreMongoTest
        {
            public class Given_A_Valid_Sample_Data : Execute
            {
                public Given_A_Valid_Sample_Data()
                {
                    string baseDataPath = @"JSON\MongoDB";

                    string[] dataPath =
                    {
                        //Path.Combine(baseDataPath, "CartAbandonment.json")
                        Path.Combine(baseDataPath, "Transition.json")
                    };

                    string connectionString = ConfigurationManager.GetConnectionString("SocialMiner.MongoDB");

                    _sutPinataCore = new PinataCore(connectionString, Provider.Type.MongoDB, dataPath);
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
